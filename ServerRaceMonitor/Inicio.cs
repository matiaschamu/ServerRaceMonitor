using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ServerRaceMonitor
{
    public partial class Inicio : Form
    {
        List<Socket> Clientes = new List<Socket>();
        object SemaforoAddClientesUDP = new object();
        object SemaforoChangeDataXML = new object();
        bool realizarLimpiezaClientesUDP = false;
        int ActualizacionesRecibidas = 0;
        DatosCarrera DataXML;
        TimeSpan TiempoCiclo;
        TimeSpan TiempoCicloMax;
        int mClientesMax = 0;
        int TimerChequeoInternet = 295;
        string PublicIP = "";
        TcpListener tcpListener = null;
        UdpClient udpClient = null;
        Configuracion mConfig = new Configuracion();
        DatosPrograma datosPrograma = new DatosPrograma();
        bool mMinimizar = true;
        delegate void FuncionSimple();
        public Inicio()
        {
            InitializeComponent();
            mConfig.Cargar();
        }
        private void Inicio_Load(object sender, EventArgs e)
        {
            this.Text = "Race Monitor Server " + Application.ProductVersion.ToString() ;
            textBoxPuertoCrono.Text = mConfig.puertoUDPCrono.ToString();
            textBoxPuertoRmon.Text = mConfig.puertoRaceMonitor.ToString();
            textBoxListaBroadCast.Text = mConfig.listaDifusion.Replace("\r\n","\n").Replace("\n", "\r\n");

            if (backgroundWorkerEscucha.IsBusy == false)
            {
                backgroundWorkerEscucha.RunWorkerAsync();
            }
            if (backgroundWorkerTrabajo.IsBusy == false)
            {
                backgroundWorkerTrabajo.RunWorkerAsync();
            }
            if (backgroundWorkerReceiverUDP.IsBusy == false)
            {
                backgroundWorkerReceiverUDP.RunWorkerAsync();
            }
        }

        private void BackgroundWorkerEscucha_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.CurrentThread.Name = "BackgroundWorkerEscucha";
            tcpListener = null;
            do
            {
                try
                {
                    if (tcpListener == null)
                    {
                        tcpListener = new TcpListener(System.Net.IPAddress.Any, datosPrograma.PuertoRaceMonitor);
                        tcpListener.Start();
                    }
                    Socket s = tcpListener.AcceptSocket();
                    lock (SemaforoAddClientesUDP)
                    {
                        Clientes.Add(s);
                    }
                }
                catch (Exception)
                {

                }
            } while (true);
        }
        private void TimerChequeoPorts_Tick(object sender, EventArgs e)
        {
            if (textBoxPuertoCrono.ForeColor == Color.Red || textBoxPuertoRmon.ForeColor == Color.Red || textBoxListaBroadCast.ForeColor == Color.Red)
            {
                labelRevisarConf.Visible = true;
            }
            else
            {
                labelRevisarConf.Visible = false;
            }
        }



        public static IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length != 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (!IPAddress.TryParse(ep[0], out ip))
            {
                throw new FormatException("Invalid ip-adress");
            }
            int port;
            if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }
            return new IPEndPoint(ip, port);
        }




        private void BackgroundWorkerBroadcastRaceMonitor_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.CurrentThread.Name = "BackgroundWorkerBroadcastRaceMonitor";
            do
            {
                List<Socket> cli = new List<Socket>();
                lock (SemaforoAddClientesUDP)
                {
                    cli.AddRange(Clientes.ToArray());
                }

                bool mCalcularTiempo = false;
                DateTime mHoraFin;
                DateTime mHoraInicio = DateTime.Now;
                foreach (Socket item in cli)
                {
                    try
                    {
                        using (Stream s = new NetworkStream(item))
                        {
                            using (StreamReader sr = new StreamReader(s))
                            {
                                using (StreamWriter sw = new StreamWriter(s))
                                {
                                    sw.AutoFlush = true;
                                    if (DataXML != null)
                                    {
                                        string bandera;
                                        switch (DataXML.EstadoCarrera)
                                        {
                                            case "0":
                                                bandera = "Finish";
                                                break;
                                            case "3":
                                                bandera = "Verde ";
                                                break;
                                            case "4":
                                                bandera = "Verde ";
                                                break;
                                            default:
                                                bandera = "Finish";
                                                break;
                                        }

                                        string ss = "";

                                        if (DataXML.DatosUsados == false)
                                        {
                                            mCalcularTiempo = true;
                                            sw.WriteLine("$I,\"16:36:08.000\",\"12 jan 01\"");
                                            ss = "$F, " + (DataXML.VueltasTotales - DataXML.VueltaActual).ToString() + ", \"\", \"" + DateTime.Now.ToString("HH:mm:ss") + "\", \"" + DataXML.TiempoActual.ToString() + "\", \"" + bandera + "\"";
                                            sw.WriteLine(ss);
                                            ss = "$B,5,\"" + DataXML.TituloA + "  (" + DataXML.TituloB + ")\"";
                                            sw.WriteLine(ss);
                                            ss = "$C,0,\"" + DataXML.TituloB + "\"";
                                            sw.WriteLine(ss);

                                            foreach (Lineas itemD in DataXML.DatosPilotos)
                                            {
                                                string[] nombre = itemD.NombreApellido.Split(',');
                                                string registration = GenerarGuid(itemD.NumPiloto + itemD.NombreApellido);
                                                TimeSpan h;
                                                string tiempoTotal = "";
                                                string MejorVuelta = "";
                                                string UltimaVuelta = "";

                                                if (itemD.TiempoTotal != "")
                                                {
                                                    h = TimeSpan.Parse("00:" + itemD.TiempoTotal.Substring(0, itemD.TiempoTotal.Length - 4) + "." + itemD.TiempoTotal.Substring(itemD.TiempoTotal.Length - 3));
                                                    tiempoTotal = h.ToString(@"hh\:mm\:ss\.fff");
                                                }
                                                if (itemD.MejorVuelta != "")
                                                {
                                                    h = TimeSpan.Parse("00:" + itemD.MejorVuelta.Substring(0, itemD.MejorVuelta.Length - 4) + "." + itemD.MejorVuelta.Substring(itemD.MejorVuelta.Length - 3));
                                                    MejorVuelta = h.ToString(@"hh\:mm\:ss\.fff");
                                                }
                                                if (itemD.UltimaVuelta != "")
                                                {
                                                    h = TimeSpan.Parse("00:" + itemD.UltimaVuelta.Substring(0, itemD.UltimaVuelta.Length - 4) + "." + itemD.UltimaVuelta.Substring(itemD.UltimaVuelta.Length - 3));
                                                    UltimaVuelta = h.ToString(@"hh\:mm\:ss\.fff");
                                                }

                                                ss = "$A,\"" + registration + "\",\"" + itemD.NumPiloto + "\",0,\"" + nombre[1].Trim() + "\",\"" + nombre[0] + "\",\"\",0";
                                                sw.WriteLine(ss);

                                                ss = "$H," + itemD.Puesto + ",\"" + registration + "\"," + itemD.VueltaMejorVuelta + ",\"" + MejorVuelta + "\"";
                                                sw.WriteLine(ss);

                                                ss = "$G," + itemD.Puesto + ",\"" + registration + "\"," + itemD.Vueltas + ",\"" + tiempoTotal + "\"";
                                                sw.WriteLine(ss);

                                                ss = "$J,\"" + registration + "\",\"" + UltimaVuelta + "\",\"" + tiempoTotal + "\"";
                                                sw.WriteLine(ss);
                                            }

                                            //ss = "$COMP,\"1234BE\",\"12X\",5,\"John\",\"Johnson\",\"USA\",\"CAMEL\"";
                                            //sw.WriteLine(ss);

                                            //ss = "$E,\"TRACKNAME\",\"Indianapolis Motor Speedway\"";
                                            //sw.WriteLine(ss);
                                            //ss = "$E,\"TRACKLENGTH\",\"2.500\"";
                                            //sw.WriteLine(ss);
                                        }
                                        else
                                        {
                                            ss = "$F, " + (DataXML.VueltasTotales - DataXML.VueltaActual).ToString() + ", \"\", \"" + DateTime.Now.ToString("HH:mm:ss") + "\", \"" + DataXML.TiempoActual.ToString() + "\", \"" + bandera + "\"";
                                            sw.WriteLine(ss);
                                            ss = "$B,5,\"" + DataXML.TituloA + "  (" + DataXML.TituloB + ")\"";
                                            sw.WriteLine(ss);
                                            ss = "$C,0,\"" + DataXML.TituloB + "\"";
                                            sw.WriteLine(ss);
                                        }
                                    }
                                    else
                                    {
                                        sw.WriteLine("$I,\"16:36:08.000\",\"12 jan 01\"");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        realizarLimpiezaClientesUDP = true;
                    }
                }
                if (DataXML != null)
                {
                    DataXML.DatosUsados = true;
                }
                mHoraFin = DateTime.Now;

                if (mCalcularTiempo)
                {
                    TiempoCiclo = mHoraFin - mHoraInicio;
                    if (TiempoCiclo.Ticks > TiempoCicloMax.Ticks)
                    {
                        TiempoCicloMax = TiempoCiclo;
                    }
                }

                if (realizarLimpiezaClientesUDP)
                {
                    LimpiarConectados();
                }
                System.Threading.Thread.Sleep(1000);
            } while (true);
        }
        void LimpiarConectados()
        {
            List<Socket> temp = new List<Socket>();
            lock (SemaforoAddClientesUDP)
            {
                foreach (var item in Clientes)
                {
                    if (item.Connected == true)
                    {
                        temp.Add(item);
                    }
                }
                Clientes = temp;
            }
        }
        string GenerarGuid(string Data)
        {
            string T = Data.Replace(",", "");
            return T.Substring(0, 6);
        }
        private void TimerUpdateEstadisticas_Tick(object sender, EventArgs e)
        {
            string T = "";
            string myHostName = Dns.GetHostName().ToString();
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

            T = "Direcciones locales para Soft Cronometraje: " + "\r\n";
            foreach (IPAddress N in localIPs)
            {
                if (N.AddressFamily == AddressFamily.InterNetwork)
                {
                    T = T + N.ToString() + " : " + datosPrograma.PuertoCronometraje.ToString() + "\r\n";
                }
            }

            T = T + "\r\n" + "Direcciones para RaceMonitor: " + "\r\n";
            foreach (IPAddress N in localIPs)
            {
                if (N.AddressFamily == AddressFamily.InterNetwork)
                {
                    T = T + N.ToString() + " : " + datosPrograma.PuertoRaceMonitor.ToString() + "\r\n";
                }
            }

            if (datosPrograma.mBroadCast.Count > 0)
            {
                T = T + "\r\n" + "Direcciones Broadcast UDP: " + "\r\n";
                foreach (IPEndPoint N in datosPrograma.mBroadCast)
                {
                    T = T + N.ToString() + "\r\n";
                }
            }

            if (TimerChequeoInternet > 300)
            {
                TimerChequeoInternet = 0;
                try
                {
                    HttpWebRequest W = (HttpWebRequest)(WebRequest.Create("http://icanhazip.com"));
                    W.Timeout = 5000;
                    W.ReadWriteTimeout = 5000;
                    HttpWebResponse R = (HttpWebResponse)(W.GetResponse());
                    if (R.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream dataStream = R.GetResponseStream())
                        {
                            using (StreamReader strReader = new StreamReader(dataStream, System.Text.Encoding.UTF8))
                            {
                                PublicIP = strReader.ReadToEnd().Trim();
                            }
                        }
                        R.Close();
                    }
                }
                catch (Exception)
                {
                    PublicIP = "Sin Internet";
                }
            }
            else
            {
                TimerChequeoInternet++;
            }


            T = T + "\r\n" + "Direccion IP Publica: " + PublicIP + "\r\n";


            T = T + "\r\n" + "Actualizaciones Recibidas: " + ActualizacionesRecibidas.ToString();
            T = T + "\r\n" + "Clientes conectados: " + Clientes.Count;

            if (Clientes.Count > mClientesMax)
            {
                mClientesMax = Clientes.Count;
            }

            T = T + "\r\n" + "Max cantidad de clientes: " + mClientesMax.ToString();
            T = T + "\r\n" + "Tiempo del ciclo de actualizacion: " + TiempoCiclo.ToString(@"ss\.fff") + " seg";
            T = T + "\r\n" + "Tiempo Max del ciclo de actualizacion: " + TiempoCicloMax.ToString(@"ss\.fff") + " seg";

            textBox1.Text = T;
        }
        private void MinimizarVentana()
        {
            if (this.InvokeRequired)
            {
                FuncionSimple F = new FuncionSimple(MinimizarVentana);
                this.Invoke(F);
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }
        private void BackgroundWorkerReceiverUDP_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.CurrentThread.Name = "BackgroundWorkerReceiverUDP";
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            do
            {
                if (udpClient == null)
                {
                    udpClient = new UdpClient(datosPrograma.PuertoCronometraje);
                }

                byte[] B = null;
                try
                {
                    B = udpClient.Receive(ref RemoteIpEndPoint);
                    if (mMinimizar == true)
                    {
                        MinimizarVentana();
                    }
                }
                catch (Exception)
                {
                    B = System.Text.Encoding.UTF8.GetBytes("Cancel!!!");
                }

                foreach (IPEndPoint ip in datosPrograma.mBroadCast)
                {
                    using (UdpClient c = new UdpClient())
                    {
                        try
                        {
                            c.Send(B, B.Length, ip.Address.ToString(), ip.Port);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }

                string temp = System.Text.Encoding.UTF8.GetString(B);
               

                byte[] bb = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("windows-1252"), B);
                //string p = System.Text.Encoding.ASCII.GetString(bb,1185,30);



                temp = temp.Trim();
                int l = temp.Length;
                DatosCarrera D = new DatosCarrera();
                D.DatosPilotos = new List<Lineas>();
                D.RecordVuelta = new Record();

                if (temp == "Cancel!!!")
                {
                    System.IO.File.Delete("Video.xml");
                    DataXML = null;
                    ActualizacionesRecibidas++;
                }
                if (temp.StartsWith("<Video><Prueba>") && temp.EndsWith("</Record></Video>"))
                {
                    System.IO.File.WriteAllBytes("Video.xml", B);

                    XmlDocument x = new System.Xml.XmlDocument();
                    //Stream xs = new MemoryStream(B);
                    //x.Load(xs);
                    x.LoadXml(temp);

                    XmlNodeList pru = x.SelectNodes("/Video/Prueba");
                    string TipoPrueba = Formatear(pru[0]);

                    if (TipoPrueba == "Clasificacion" || TipoPrueba == "Carrera")
                    {
                        pru = x.SelectNodes("/Video/Titulos/Titulo");

                        D.TituloA = Formatear(pru[0]);
                        D.TituloB = Formatear(pru[1]);

                        pru = x.SelectNodes("/Video/DatosCarrera");

                        D.TiempoActual = Formatear(pru[0].ChildNodes[0]);
                        D.EstadoCarrera = Formatear(pru[0].ChildNodes[1]);
                        D.VueltasTotales = int.Parse(Formatear(pru[0].ChildNodes[2]));
                        D.VueltaActual = int.Parse(Formatear(pru[0].ChildNodes[3]));

                        pru = x.SelectNodes("/Video/Datos/Linea");
                        int cant = pru.Count;

                        for (int i = 0; i < cant; i++)
                        {
                            Lineas L = new Lineas();
                            L.Puesto = Formatear(pru[i].ChildNodes[0]);
                            L.NumPiloto = Formatear(pru[i].ChildNodes[1]);
                            L.NombreApellido = Formatear(pru[i].ChildNodes[2]);

                            if (TipoPrueba == "Clasificacion")
                            {
                                L.MejorVuelta = Formatear(pru[i].ChildNodes[3]);
                                L.Diferencia = Formatear(pru[i].ChildNodes[4]);
                                L.UltimaVuelta = Formatear(pru[i].ChildNodes[5]);
                                L.Vueltas = Formatear(pru[i].ChildNodes[6]);
                                L.TiempoTotal = Formatear(pru[i].ChildNodes[7]);
                                L.Recargos = Formatear(pru[i].ChildNodes[8]);
                                L.Transponder = Formatear(pru[i].ChildNodes[9]);
                                L.VueltaMejorVuelta = Formatear(pru[i].ChildNodes[10]);
                            }
                            else if (TipoPrueba == "Carrera")
                            {
                                L.UltimaVuelta = Formatear(pru[i].ChildNodes[3]);
                                L.MejorVuelta = Formatear(pru[i].ChildNodes[4]);
                                L.TiempoTotal = Formatear(pru[i].ChildNodes[5]);
                                L.Diferencia = Formatear(pru[i].ChildNodes[6]);
                                L.Vueltas = Formatear(pru[i].ChildNodes[7]);
                                L.Recargos = Formatear(pru[i].ChildNodes[8]);
                                L.Transponder = Formatear(pru[i].ChildNodes[9]);
                                L.VueltaMejorVuelta = Formatear(pru[i].ChildNodes[10]);
                            }
                            D.DatosPilotos.Add(L);
                        }

                        pru = x.SelectNodes("/Video/Record");
                        D.RecordVuelta.NumPiloto = Formatear(pru[0].ChildNodes[0]);
                        D.RecordVuelta.NombreApellido = Formatear(pru[0].ChildNodes[1]);
                        D.RecordVuelta.Vuelta = Formatear(pru[0].ChildNodes[2]);
                        D.RecordVuelta.MejorVuelta = Formatear(pru[0].ChildNodes[3]);
                        D.RecordVuelta.Velocidad = Formatear(pru[0].ChildNodes[4]);

                        D.DatosUsados = false;
                        lock (SemaforoChangeDataXML)
                        {
                            DataXML = D;
                        }
                        ActualizacionesRecibidas++;
                    }
                }
            } while (true);
        }

        private string Formatear(XmlNode O)
        {
            if (O.ChildNodes[0] == null)
            {
                return "";
            }
            else
            {
                return O.ChildNodes[0].Value.ToString();
            }
        }

        private void textBoxListaBroadCast_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (mConfig.listaDifusion != textBoxListaBroadCast.Text)
                {
                    mConfig.listaDifusion = textBoxListaBroadCast.Text;
                    mConfig.Guardar();
                }
                datosPrograma.mBroadCast.Clear();

                if (textBoxListaBroadCast.Text.Length == 0)
                {
                    return;
                }

                string[] T = mConfig.listaDifusion.Replace("\n", "").Split(new char[] { ';', '\r' });
                bool mError = false;

                foreach (string ip in T)
                {
                    try
                    {
                        datosPrograma.mBroadCast.Add(CreateIPEndPoint(ip));
                    }
                    catch (Exception)
                    {
                        mError = true;
                    }
                }
                if (mError)
                {
                    textBoxListaBroadCast.ForeColor = Color.Red;
                }
                else
                {
                    textBoxListaBroadCast.ForeColor = Color.Black;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void textBoxPuertoCrono_TextChanged(object sender, EventArgs e)
        {
            if (mConfig.puertoUDPCrono != textBoxPuertoCrono.Text)
            {
                mConfig.puertoUDPCrono = textBoxPuertoCrono.Text;
                mConfig.Guardar();
            }

            bool mError = false;
            try
            {
                datosPrograma.PuertoCronometraje = int.Parse(mConfig.puertoUDPCrono);
            }
            catch (Exception)
            {
                mError = true;
            }
            if (udpClient != null)
            {
                udpClient.Close();
                udpClient = null;
            }

            if (mError)
            {
                textBoxPuertoCrono.ForeColor = Color.Red;
            }
            else
            {
                textBoxPuertoCrono.ForeColor = Color.Black;
            }
        }

        private void textBoxPuertoRmon_TextChanged(object sender, EventArgs e)
        {
            if (mConfig.puertoRaceMonitor != textBoxPuertoRmon.Text)
            {
                mConfig.puertoRaceMonitor = textBoxPuertoRmon.Text;
                mConfig.Guardar();
            }

            bool mError = false;
            try
            {
                datosPrograma.PuertoRaceMonitor = int.Parse(mConfig.puertoRaceMonitor);
            }
            catch (Exception)
            {
                mError = true;
            }
            if (tcpListener != null)
            {

                tcpListener.Stop();
                tcpListener = null;

            }
            if (mError)
            {
                textBoxPuertoRmon.ForeColor = Color.Red;
            }
            else
            {
                textBoxPuertoRmon.ForeColor = Color.Black;
            }
        }

        private void checkBoxMinimizar_CheckedChanged(object sender, EventArgs e)
        {
            mMinimizar = !checkBoxMinimizar.Checked;
        }
    }

    public class DatosPrograma
    {
        public int PuertoCronometraje = 50001;
        public int PuertoRaceMonitor = 50000;
        public List<IPEndPoint> mBroadCast = new List<IPEndPoint>();
    }

    public class Lineas
    {
        public string Puesto;
        public string NumPiloto;
        public string NombreApellido;
        public string UltimaVuelta;
        public string MejorVuelta;
        public string VueltaMejorVuelta;
        public string TiempoTotal;
        public string Diferencia;
        public string Vueltas;
        public string Recargos;
        public string Transponder;
    }
    public class Record
    {
        public string NumPiloto;
        public string NombreApellido;
        public string MejorVuelta;
        public string Vuelta;
        public string Velocidad;
        public string DistanciaCircuito;
    }
    public class DatosCarrera
    {
        public string TituloA;
        public string TituloB;
        public string TiempoActual;
        public string EstadoCarrera;
        public int VueltaActual;
        public int VueltasTotales;
        public List<Lineas> DatosPilotos;
        public Record RecordVuelta;
        public bool DatosUsados;
    }
}
