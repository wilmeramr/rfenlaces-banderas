using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Davicom
{
    public partial class Form1 : MetroForm
    {

        List<string> circuitos = new List<string>() {
            "1,pacecar_inactiva,rojas-inactiva,verde-inactiva,amarillo-inactiva,azul-inactiva",
            "2,pacecar_inactiva,rojas-inactiva,verde-inactiva,amarillo-inactiva,azul-inactiva",
            "3,pacecar_inactiva,rojas-inactiva,verde-inactiva,amarillo-inactiva,azul-inactiva",
            "4,pacecar_inactiva,rojas-inactiva,verde-inactiva,amarillo-inactiva,azul-inactiva",
            "5,pacecar_inactiva,rojas-inactiva,verde-inactiva,amarillo-inactiva,azul-inactiva",
        };
        int circuitoActivo = 0;
        static SerialPort _port;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            MetroButton btns = (MetroButton)sender;

            GenerarTXT();

            switch (btns.Text)
            {
                case "1":
                    circuitoActivo = 1;
                    btnCircuito1.Style = MetroColorStyle.Red;
                    btnCircuito2.Style = MetroColorStyle.White;
                    btnCircuito3.Style = MetroColorStyle.White;
                    btnCircuito4.Style = MetroColorStyle.White;
                    btnAll.Style = MetroColorStyle.White;
                    circuitoActuales();
                    break;

                case "2":
                    circuitoActivo = 2;
                    btnCircuito1.Style = MetroColorStyle.White;
                    btnCircuito2.Style = MetroColorStyle.Red;
                    btnCircuito3.Style = MetroColorStyle.White;
                    btnCircuito4.Style = MetroColorStyle.White;
                    btnAll.Style = MetroColorStyle.White;
                    circuitoActuales();
                    break;

                case "3":
                    circuitoActivo = 3;
                    btnCircuito1.Style = MetroColorStyle.White;
                    btnCircuito2.Style = MetroColorStyle.White;
                    btnCircuito3.Style = MetroColorStyle.Red;
                    btnCircuito4.Style = MetroColorStyle.White;
                    btnAll.Style = MetroColorStyle.White;
                    circuitoActuales();

                    break;

                case "4":
                    circuitoActivo = 4;
                    btnCircuito1.Style = MetroColorStyle.White;
                    btnCircuito2.Style = MetroColorStyle.White;
                    btnCircuito3.Style = MetroColorStyle.White;
                    btnCircuito4.Style = MetroColorStyle.Red;
                    btnAll.Style = MetroColorStyle.White;
                    circuitoActuales();

                    break;

                case "TODAS":
                    circuitoActivo = 5;
                    btnCircuito1.Style = MetroColorStyle.White;
                    btnCircuito2.Style = MetroColorStyle.White;
                    btnCircuito3.Style = MetroColorStyle.White;
                    btnCircuito4.Style = MetroColorStyle.White;
                    btnAll.Style = MetroColorStyle.Red;
                    break;
                default:
                    break;
            }

            void circuitoActuales()
            {
                var circuitoActual = circuitos.ElementAt(circuitoActivo - 1).Split(',');

                if (circuitoActual[1] == "pacecar_inactiva")
                {
                    pacecar.Image = Properties.Resources.pacecar_inactiva;
                    pacecar.Tag = "pacecar_inactiva";

                }
                if (circuitoActual[1] == "pacecar_activa")
                {
                    pacecar.Image = Properties.Resources.pacecar_activa;
                    pacecar.Tag = "pacecar_activa";

                }


                if (circuitoActual[2] == "rojas-inactiva")
                {
                    rojas.Image = Properties.Resources.rojas_inactiva;
                    rojas.Tag = "rojas-inactiva";

                }
                if (circuitoActual[2] == "rojas_activa")
                {
                    rojas.Image = Properties.Resources.rojas_activa;
                    rojas.Tag = "rojas_activa";

                }
                if (circuitoActual[3] == "verde-inactiva")
                {
                    verdes.Image = Properties.Resources.verde_inactiva;
                    verdes.Tag = "verde-inactiva";

                }
                if (circuitoActual[3] == "verde_activa")
                {
                    verdes.Image = Properties.Resources.verde_activa;
                    verdes.Tag = "verde_activa";

                }
                if (circuitoActual[4] == "amarillo-inactiva")
                {
                    amarillas.Image = Properties.Resources.amarillo_inactiva;
                    amarillas.Tag = "amarillo-inactiva";

                }
                if (circuitoActual[4] == "amarillo_activa")
                {
                    amarillas.Image = Properties.Resources.amarillo_activa;
                    amarillas.Tag = "amarillo_activa";

                }

                if (circuitoActual[5] == "azul-inactiva")
                {
                    azules.Image = Properties.Resources.azul_inactiva;
                    azules.Tag = "azul-inactiva";

                }
                if (circuitoActual[5] == "azul_activa")
                {
                    azules.Image = Properties.Resources.azul_activa;
                    azules.Tag = "azul_activa";

                }
            }
        }

        private void PictureBoxAll_Click(object sender, EventArgs e)
        {
            if (_port != null)
            {
                if (circuitoActivo == 0)
                {
                    MessageBox.Show("Debes seleccionar un circuito.");
                    return;
                }

                PictureBox pictureBox = (PictureBox)sender;

                var circuitoActual2 = circuitos.ElementAt(circuitoActivo - 1).Split(',');

                var banderasActivas = circuitoActual2.Where(w => w.Contains("inactiva"));
                if (banderasActivas.Count() < (circuitoActual2.Length - 1) && pictureBox.Tag.ToString() != "negra-inactiva")
                {
                    MessageBox.Show("Hay una bandera activa, debes apagarla con la bandera negra.");
                    return;
                }

                byte[] data = new byte[2];

                //MessageBox.Show(pictureBox.Tag.ToString());
                switch (pictureBox.Tag.ToString())
                {
                    case "pacecar_inactiva":
                        data[0] = 1;
                        data[1] = 1;
                        _port.Write(data, 0, data.Length);
                        pacecar.Image = Properties.Resources.pacecar_activa;
                        pacecar.Tag = "pacecar_activa";
                        NewMethod("pacecar_activa", 1);
                        break;
                    case "pacecar_activa":
                        data[0] = 1;
                        data[1] = 6;
                        _port.Write(data, 0, data.Length);
                        pacecar.Image = Properties.Resources.pacecar_inactiva;
                        pacecar.Tag = "pacecar_inactiva";
                        NewMethod("pacecar_inactiva", 1);
                        break;

                    case "rojas-inactiva":
                        data[0] = 1;
                        data[1] = 4;
                        _port.Write(data, 0, data.Length);
                        rojas.Image = Properties.Resources.rojas_activa;
                        rojas.Tag = "rojas_activa";
                        NewMethod("rojas_activa", 2);

                        break;

                    case "rojas_activa":
                        data[0] = 1;
                        data[1] = 6;
                        _port.Write(data, 0, data.Length);
                        rojas.Image = Properties.Resources.rojas_inactiva;
                        rojas.Tag = "rojas-inactiva";
                        NewMethod("rojas-inactiva", 2);

                        break;
                    case "verde-inactiva":
                        data[0] = 1;
                        data[1] = 5;
                        _port.Write(data, 0, data.Length);
                        verdes.Image = Properties.Resources.verde_activa;
                        verdes.Tag = "verde_activa";
                        NewMethod("verde_activa", 3);

                        break;

                    case "verde_activa":
                        data[0] = 1;
                        data[1] = 6;
                        _port.Write(data, 0, data.Length);
                        verdes.Image = Properties.Resources.verde_inactiva;
                        verdes.Tag = "verde-inactiva";
                        NewMethod("verde-inactiva", 3);

                        break;
                    case "amarillo-inactiva":
                        data[0] = 1;
                        data[1] = 2;
                        _port.Write(data, 0, data.Length);
                        amarillas.Image = Properties.Resources.amarillo_activa;
                        amarillas.Tag = "amarillo_activa";
                        NewMethod("amarillo_activa", 4);

                        break;

                    case "amarillo_activa":
                        data[0] = 1;
                        data[1] = 6;
                        _port.Write(data, 0, data.Length);
                        amarillas.Image = Properties.Resources.amarillo_inactiva;
                        amarillas.Tag = "amarillo-inactiva";
                        NewMethod("amarillo-inactiva", 4);

                        break;

                    case "azul-inactiva":
                        data[0] = 1;
                        data[1] = 3;
                        _port.Write(data, 0, data.Length);
                        azules.Image = Properties.Resources.azul_activa;
                        azules.Tag = "azul_activa";
                        NewMethod("azul_activa", 5);

                        break;

                    case "azul_activa":
                        data[0] = 1;
                        data[1] = 6;
                        _port.Write(data, 0, data.Length);
                        azules.Image = Properties.Resources.azul_inactiva;
                        azules.Tag = "azul-inactiva";
                        NewMethod("azul-inactiva", 5);
                        break;
                    case "negra-inactiva":

                        BanderaNegra();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Problemas con el puerto");
            }


        }
        private void NewMethod(string tag, int index)
        {
            var circuitoActual = circuitos.ElementAt(circuitoActivo - 1).Split(',');
            circuitoActual[index] = tag;

            circuitos = circuitos.Select(s =>
            {
                if (s.Substring(0, 1) == circuitoActual[0])
                    return s = string.Join(",", circuitoActual.Select(x => x.ToString()).ToArray());

                return s;
            }).ToList();
            GuardarTXT();
        }
        private void BanderaNegra()
        {
            var circuitoActual3 = circuitos.ElementAt(circuitoActivo - 1).Split(',');
            foreach (var item in circuitoActual3)
            {
                switch (item)
                {
                    case "pacecar_activa":
                        pacecar.Image = Properties.Resources.pacecar_inactiva;
                        pacecar.Tag = "pacecar_inactiva";
                        NewMethod("pacecar_inactiva", 1);
                        break;
                    case "rojas_activa":
                        rojas.Image = Properties.Resources.rojas_inactiva;
                        rojas.Tag = "rojas-inactiva";
                        NewMethod("rojas-inactiva", 2);

                        break;

                    case "verde_activa":
                        verdes.Image = Properties.Resources.verde_inactiva;
                        verdes.Tag = "verde-inactiva";
                        NewMethod("verde-inactiva", 3);

                        break;
                    case "amarillo_activa":
                        amarillas.Image = Properties.Resources.amarillo_inactiva;
                        amarillas.Tag = "amarillo-inactiva";
                        NewMethod("amarillo-inactiva", 4);

                        break;

                    case "azul_activa":
                        azules.Image = Properties.Resources.azul_inactiva;
                        azules.Tag = "azul-inactiva";
                        NewMethod("azul-inactiva", 5);
                        break;
                    default:
                        break;
                }
            }
        }

        //private void GuardarPuntos(object sender, EventArgs e)
        //{
        //    string[] lineas = File.ReadAllLines(Application.StartupPath + (@"\Puntajes.txt");
        //    for (i = 0; i < lineas.Length; i++)
        //    {
        //        if (lineas[i].Substring(0, lineas[i].IndexOf(" - ")).Equals(stNombreJugador))
        //        {
        //            lineas[i] = stNombreJugador + " - " + inPuntos;
        //        }
        //    }

        //    File.WriteAllLines(Application.StartupPath + (@"\Puntajes.txt"), lineas);
        //}

        void GuardarTXT()
        {
            string rutaCompleta = Application.StartupPath + @"\historial.txt";
            if (File.Exists(rutaCompleta))
            {
                File.WriteAllLines(rutaCompleta, circuitos);

                circuitos = File.ReadAllLines(rutaCompleta).ToList();
            }
        

        }

        // para crear el archivo
        void GenerarTXT()
        {
            string rutaCompleta = Application.StartupPath + @"\historial.txt";


            if (!File.Exists(rutaCompleta))
                File.WriteAllLines(rutaCompleta, circuitos);
            else
                circuitos = File.ReadAllLines(rutaCompleta).ToList();

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var serial = SerialPort.GetPortNames();
            //if (serial.Length==0)
            //{
            //    MessageBox.Show("Sin puerto abiertos.");
            //}

            cmbPorts.DataSource = serial;
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {



            _port = new SerialPort(cmbPorts.SelectedItem.ToString());

            // configure serial port
            _port.BaudRate = 9600;
            _port.DataBits = 8;
            _port.Parity = Parity.None;
            _port.StopBits = StopBits.One;
            _port.ReadTimeout = 500;
            _port.WriteTimeout = 500;
            //_port.DataReceived += new
            // SerialDataReceivedEventHandler(port_DataReceived);
            _port.Open();

            btnConectar.Enabled = false;
            btnConectar.Style = MetroColorStyle.Green;
            btnDesconectar.Enabled = true;

        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            _port.Close();
            _port = null;
            btnConectar.Enabled = true;
            btnDesconectar.Enabled = false;
            btnConectar.Style = MetroColorStyle.White;

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_port != null)
                _port.Close();
        }
    }
}
