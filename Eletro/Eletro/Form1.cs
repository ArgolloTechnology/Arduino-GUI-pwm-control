using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Eletro
{
    public partial class Form1 : Form
    {
        string buffer;
        public Form1()
        {
            InitializeComponent();
            serialPort1.Open();
            CustomizeForm();
        }

        private void CustomizeForm()
        {
            // Configurar propriedades do formulário
            this.Text = "Eletro Control Panel";
            this.BackColor = Color.FromArgb(30, 30, 30); // Fundo mais escuro
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;


            // Personalizar ProgressBar
            trackBar1.ForeColor = Color.FromArgb(50, 200, 50); // Verde brilhante

            // Personalizar Labels
            label1.ForeColor = Color.White;  // Texto branco para contraste
            label2.ForeColor = Color.White;  // Texto branco para contraste
            label3.ForeColor = Color.White;  // Texto branco para contraste
            label4.ForeColor = Color.White;
            label5.ForeColor = Color.White;
            label6.ForeColor = Color.White;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            buffer = trackBar1.Value.ToString();
            label5.Text = buffer;
            buffer = "d" + trackBar1.Value;
            serialPort1.Write(buffer);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            buffer = trackBar2.Value.ToString();
            label6.Text = buffer;
            buffer = "f" + trackBar2.Value;
            serialPort1.Write(buffer);
        }

        private void onButton_Click(object sender, EventArgs e)
        {
            serialPort1.Write("A");
        }

        private void offButton_Click(object sender, EventArgs e)
        {
            serialPort1.Write("a");
        }
    }
}
