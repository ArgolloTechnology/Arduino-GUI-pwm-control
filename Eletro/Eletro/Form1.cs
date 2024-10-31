using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Eletro
{
    public partial class Form1 : Form
    {
        string buffer;  // Buffer para armazenar os dados a serem enviados para o Arduino

        public Form1()
        {
            InitializeComponent();
            // Encontrar a porta serial onde o Arduino está conectado
            string arduinoPort = FindArduinoPort();
            if (!string.IsNullOrEmpty(arduinoPort))
            {
                serialPort1.PortName = arduinoPort;  // Definir a porta serial para a comunicação
                serialPort1.Open();  // Abrir a porta serial
                CustomizeForm();  // Customizar o formulário
                // Adicionar eventos para quando o usuário soltar o mouse nos trackBars
                trackBar1.MouseUp += trackBar1_MouseUp;
                trackBar2.MouseUp += trackBar2_MouseUp;
            }
            else
            {
                // Mostrar mensagem se o Arduino não for encontrado
                MessageBox.Show("Arduino não encontrado.");
            }
        }

        private void CustomizeForm()
        {
            // Configurar propriedades do formulário
            this.Text = "Eletro Control Panel";  // Título do formulário
            this.BackColor = Color.FromArgb(30, 30, 30);  // Fundo mais escuro
            this.FormBorderStyle = FormBorderStyle.FixedSingle;  // Definir o estilo da borda do formulário
            this.MaximizeBox = false;  // Desabilitar o botão de maximizar
            // Personalizar a cor da trackBar
            trackBar1.ForeColor = Color.FromArgb(50, 200, 50);  // Verde brilhante
            // Personalizar a cor dos labels
            label1.ForeColor = Color.White;  // Texto branco para contraste
            label2.ForeColor = Color.White;
            label3.ForeColor = Color.White;
            label4.ForeColor = Color.White;
            label5.ForeColor = Color.White;
            label6.ForeColor = Color.White;
        }

        // Método para encontrar a porta serial do Arduino
        private string FindArduinoPort()
        {
            foreach (string port in SerialPort.GetPortNames())
            {
                try
                {
                    using (SerialPort tempPort = new SerialPort(port))
                    {
                        tempPort.Open();  // Abrir a porta serial temporariamente
                        tempPort.WriteLine("Hello");  // Enviar um comando para o Arduino
                        // Aguarde uma resposta do Arduino
                        System.Threading.Thread.Sleep(1500);  // Pausa para dar tempo de resposta
                        if (tempPort.ReadExisting().Contains("Hello"))
                        {
                            tempPort.Close();  // Fechar a porta temporária
                            return port;  // Retornar o nome da porta
                        }
                    }
                }
                catch { }
            }
            return null;  // Retornar null se a porta do Arduino não for encontrada
        }

        // Evento disparado quando o usuário move o trackBar1
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            buffer = trackBar1.Value.ToString();  // Converter o valor do trackBar para string
            label5.Text = buffer;  // Exibir o valor no label
            buffer = "d" + trackBar1.Value;  // Preparar o comando a ser enviado
            serialPort1.Write(buffer);  // Enviar o comando via serial
        }

        // Evento disparado quando o usuário move o trackBar2
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            buffer = trackBar2.Value.ToString();  // Converter o valor do trackBar para string
            label6.Text = buffer;  // Exibir o valor no label
            buffer = "f" + trackBar2.Value;  // Preparar o comando a ser enviado
            serialPort1.Write(buffer);  // Enviar o comando via serial
        }

        // Evento disparado quando o usuário solta o mouse no trackBar1
        private void trackBar1_MouseUp(object sender, EventArgs e)
        {
            buffer = trackBar1.Value.ToString();  // Converter o valor do trackBar para string
            label5.Text = buffer;  // Exibir o valor no label
            buffer = "d" + trackBar1.Value;  // Preparar o comando a ser enviado
            serialPort1.Write(buffer);  // Enviar o comando via serial
        }

        // Evento disparado quando o usuário solta o mouse no trackBar2
        private void trackBar2_MouseUp(object sender, EventArgs e)
        {
            buffer = trackBar2.Value.ToString();  // Converter o valor do trackBar para string
            label6.Text = buffer;  // Exibir o valor no label
            buffer = "f" + trackBar2.Value;  // Preparar o comando a ser enviado
            serialPort1.Write(buffer);  // Enviar o comando via serial
        }

        // Evento disparado quando o botão "on" é clicado
        private void onButton_Click(object sender, EventArgs e)
        {
            serialPort1.Write("A");  // Enviar o comando "A" via serial
        }

        // Evento disparado quando o botão "off" é clicado
        private void offButton_Click(object sender, EventArgs e)
        {
            serialPort1.Write("a");  // Enviar o comando "a" via serial
        }
    }
}
