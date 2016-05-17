using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Presentacion
{
    /// <summary>
    /// clase de ventana de coronacion
    /// </summary>
    public partial class CustomMessageForm : Form
    {

        public int corona;
        

        /// <summary>
        /// Constructor de clase
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public CustomMessageForm()
        {
            var dataSource = new List<string>();
            dataSource.Add("Alfil");
            dataSource.Add("Dama");
            dataSource.Add("Torre");
            dataSource.Add("Caballo");

            InitializeComponent();
            this.comboBox1.DataSource = dataSource;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Click que indica por cual pieza se cororna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Equals("Alfil")) {
                corona = 0;
            }
            if (comboBox1.SelectedItem.ToString().Equals("Dama"))
            {
                corona = 1;
            }
            if (comboBox1.SelectedItem.ToString().Equals("Torre"))
            {
                corona = 2;
            }
            if (comboBox1.SelectedItem.ToString().Equals("Caballo"))
            {
                corona = 3;
            }

            this.Close();
        }
    }
}
