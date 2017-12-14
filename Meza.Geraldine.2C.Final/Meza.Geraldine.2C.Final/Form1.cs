using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using EntidadesSP;

namespace Meza.Geraldine._2C.Final
{
    public partial class Form1 : Form
    {
        List<Base> listaElementos;

        public Form1()
        {
            InitializeComponent();
            listaElementos = new List<Base>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Name = "Final - Geraldine Meza";
        }

        private void rtbTextoSalida_TextChanged(object sender, EventArgs e)
        {

        }
        delegate void CargarRichTextBoxCallBack(string datos);
        private void CargarRichTextBox(string datos)
        {
            if (rtbTextoSalida.InvokeRequired)
            {
                CargarRichTextBoxCallBack d = new CargarRichTextBoxCallBack(CargarRichTextBox);
                this.Invoke(d, new object[] { datos });
            }
            else
            {
                rtbTextoSalida.Text = datos;
            }
        }

        private void btnPunto1_Click(object sender, EventArgs e)
        {
            Base derUno1 = new DerivadaUno(10, 11, 12);
            DerivadaUno derUno2 = new DerivadaUno(1, 2, 3);
            Base derDos1 = new DerivadaDos();

            listaElementos.Add(derUno1);
            listaElementos.Add(derUno2);
            listaElementos.Add(derDos1);

            // Generar el código para obtener todos los datos de los elementos de la lista y luego utilizar CargarRichTextBox para mostrarlos
            StringBuilder sb = new StringBuilder();

            foreach (Base b in this.listaElementos)
            {
                sb.AppendLine(b.Version);
            }
            this.CargarRichTextBox(sb.ToString());
        }

        private void btnPunto2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Para primer parcial", "Para primer parcial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
