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
    public partial class CustomMessageForm : Form
    {
        private string description;
        private string title;

        public int corona;

        public CustomMessageForm()
        {
            InitializeComponent();
        }

        public CustomMessageForm(string title, string description)
        {
            var dataSource = new List<string>();
            dataSource.Add("Alfil");
            dataSource.Add("Dama");
            dataSource.Add("Torre");
            dataSource.Add("Caballo");

            InitializeComponent();
            this.title = title;
            this.description = description;
            this.comboBox1.DataSource = dataSource;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

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
        }
    }
}
