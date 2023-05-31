using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Express
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }
        public string Value { get { return textBox1.Text; } }
        public string Message { get { return label1.Text; } set { label1.Text = value; } }

        public InputBox(string message, string title, string def = "")
        {
            InitializeComponent();
            Text = title;
            Message = message;
            textBox1.Text = def;
        }

        public static string Show(string message, string title, string def = "")
        {
            var d = new InputBox(message, title, def);
            d.ShowDialog();
            return d.DialogResult == DialogResult.OK ? d.Value : null;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.OK;
        }

        private void RenameViewer_Shown(object sender, EventArgs e)
        {
            textBox1.Select();
        }
    }
}
