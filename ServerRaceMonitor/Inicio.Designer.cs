namespace ServerRaceMonitor
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.backgroundWorkerEscucha = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerTrabajo = new System.ComponentModel.BackgroundWorker();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorkerReceiverUDP = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxListaBroadCast = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxPuertoRmon = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxPuertoCrono = new System.Windows.Forms.TextBox();
            this.timerChequeoPorts = new System.Windows.Forms.Timer(this.components);
            this.labelRevisarConf = new System.Windows.Forms.Label();
            this.checkBoxMinimizar = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(6, 6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(309, 332);
            this.textBox1.TabIndex = 0;
            // 
            // backgroundWorkerEscucha
            // 
            this.backgroundWorkerEscucha.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerEscucha_DoWork);
            // 
            // backgroundWorkerTrabajo
            // 
            this.backgroundWorkerTrabajo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerBroadcastRaceMonitor_DoWork);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 1000;
            this.timerUpdate.Tick += new System.EventHandler(this.TimerUpdateEstadisticas_Tick);
            // 
            // backgroundWorkerReceiverUDP
            // 
            this.backgroundWorkerReceiverUDP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerReceiverUDP_DoWork);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(329, 370);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(321, 344);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Estadisticas";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxMinimizar);
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(321, 344);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configuracion";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(309, 314);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox3, 2);
            this.groupBox3.Controls.Add(this.textBoxListaBroadCast);
            this.groupBox3.Location = new System.Drawing.Point(3, 72);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 239);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Direcciones Broadcast UDP";
            // 
            // textBoxListaBroadCast
            // 
            this.textBoxListaBroadCast.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxListaBroadCast.Location = new System.Drawing.Point(6, 19);
            this.textBoxListaBroadCast.Multiline = true;
            this.textBoxListaBroadCast.Name = "textBoxListaBroadCast";
            this.textBoxListaBroadCast.Size = new System.Drawing.Size(291, 214);
            this.textBoxListaBroadCast.TabIndex = 0;
            this.textBoxListaBroadCast.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxListaBroadCast.TextChanged += new System.EventHandler(this.textBoxListaBroadCast_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.textBoxPuertoRmon);
            this.groupBox2.Location = new System.Drawing.Point(157, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(149, 47);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Puerto TCP RaceMonitor";
            // 
            // textBoxPuertoRmon
            // 
            this.textBoxPuertoRmon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPuertoRmon.Location = new System.Drawing.Point(6, 19);
            this.textBoxPuertoRmon.Name = "textBoxPuertoRmon";
            this.textBoxPuertoRmon.Size = new System.Drawing.Size(137, 20);
            this.textBoxPuertoRmon.TabIndex = 0;
            this.textBoxPuertoRmon.Text = "50000";
            this.textBoxPuertoRmon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxPuertoRmon.TextChanged += new System.EventHandler(this.textBoxPuertoRmon_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBoxPuertoCrono);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(148, 47);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Puerto UDP Crono";
            // 
            // textBoxPuertoCrono
            // 
            this.textBoxPuertoCrono.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPuertoCrono.Location = new System.Drawing.Point(6, 19);
            this.textBoxPuertoCrono.Name = "textBoxPuertoCrono";
            this.textBoxPuertoCrono.Size = new System.Drawing.Size(136, 20);
            this.textBoxPuertoCrono.TabIndex = 0;
            this.textBoxPuertoCrono.Text = "50001";
            this.textBoxPuertoCrono.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxPuertoCrono.TextChanged += new System.EventHandler(this.textBoxPuertoCrono_TextChanged);
            // 
            // timerChequeoPorts
            // 
            this.timerChequeoPorts.Enabled = true;
            this.timerChequeoPorts.Interval = 3000;
            this.timerChequeoPorts.Tick += new System.EventHandler(this.TimerChequeoPorts_Tick);
            // 
            // labelRevisarConf
            // 
            this.labelRevisarConf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRevisarConf.AutoSize = true;
            this.labelRevisarConf.ForeColor = System.Drawing.Color.Red;
            this.labelRevisarConf.Location = new System.Drawing.Point(227, 17);
            this.labelRevisarConf.Name = "labelRevisarConf";
            this.labelRevisarConf.Size = new System.Drawing.Size(110, 13);
            this.labelRevisarConf.TabIndex = 1;
            this.labelRevisarConf.Text = "Revisar configuracion";
            this.labelRevisarConf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelRevisarConf.Visible = false;
            // 
            // checkBoxMinimizar
            // 
            this.checkBoxMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMinimizar.AutoSize = true;
            this.checkBoxMinimizar.Location = new System.Drawing.Point(229, 4);
            this.checkBoxMinimizar.Name = "checkBoxMinimizar";
            this.checkBoxMinimizar.Size = new System.Drawing.Size(86, 17);
            this.checkBoxMinimizar.TabIndex = 3;
            this.checkBoxMinimizar.Text = "No Minimizar";
            this.checkBoxMinimizar.UseVisualStyleBackColor = true;
            this.checkBoxMinimizar.CheckedChanged += new System.EventHandler(this.checkBoxMinimizar_CheckedChanged);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 394);
            this.Controls.Add(this.labelRevisarConf);
            this.Controls.Add(this.tabControl1);
            this.Name = "Inicio";
            this.Text = "Server Race Monitor";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorkerEscucha;
        private System.ComponentModel.BackgroundWorker backgroundWorkerTrabajo;
        private System.Windows.Forms.Timer timerUpdate;
        private System.ComponentModel.BackgroundWorker backgroundWorkerReceiverUDP;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxListaBroadCast;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxPuertoRmon;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxPuertoCrono;
        private System.Windows.Forms.Timer timerChequeoPorts;
        private System.Windows.Forms.Label labelRevisarConf;
        private System.Windows.Forms.CheckBox checkBoxMinimizar;
    }
}

