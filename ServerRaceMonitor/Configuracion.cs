using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerRaceMonitor
{
    //[Serializable()]
    public class Configuracion : BibliotecaMaf.Clases.ConfiguracionBase.ConfiguracionBase
    {
        public string puertoRaceMonitor = "50000";
        public string puertoUDPCrono = "50001";
        public string listaDifusion = "";
        

        public Configuracion()
            : base("RMcfg.dat", "", "", false)
        {

        }
    }
}
