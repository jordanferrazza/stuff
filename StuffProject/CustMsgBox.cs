using StuffProject.Toolbox.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StuffProject.Toolbox.Forms
{
    public partial class CustMsgBox : Form
    {
        public CustMsgBox()
        {
            InitializeComponent();
        }

        public CustMsgBox(string msg, string title, MessageBoxIcon messageBoxIcon, params string[] options) : this(msg, title, messageBoxIcon, options.ToDictionary(x => x, y => DialogResult.OK))
        {

        }
        public CustMsgBox(string msg, string title, MessageBoxIcon messageBoxIcon, Dictionary<string, DialogResult> options)
        {
            InitializeComponent();
            Text = title;
            switch (messageBoxIcon)
            {
                case MessageBoxIcon.None:
                    pictureBox1.Visible = false;
                    break;
                case MessageBoxIcon.Hand:
                    pictureBox1.Image = SystemIcons.Error.ToBitmap();
                    break;
                case MessageBoxIcon.Question:
                    pictureBox1.Image = SystemIcons.Question.ToBitmap();
                    break;
                case MessageBoxIcon.Exclamation:
                    pictureBox1.Image = SystemIcons.Exclamation.ToBitmap();
                    break;
                case MessageBoxIcon.Asterisk:
                    pictureBox1.Image = SystemIcons.Asterisk.ToBitmap();
                    break;
                default:
                    pictureBox1.Visible = false;
                    break;
            }

            label2.Text = msg;
            foreach (var item in options)
            {
                var d = new Button();
                d.Click += D_Click;
                d.Text = item.Key;
                d.Tag = item.Value;
                d.AutoSize = true;
                flowLayoutPanel3.Controls.Add(d);
            }
        }
        public int MsgBoxResultIndex { get; set; } = -1;

        private void D_Click(object sender, EventArgs e)
        {
            MsgBoxResultIndex = flowLayoutPanel3.Controls.IndexOf((Button)sender);
            DialogResult = sender.CastTo<Button>().Tag.CastTo<DialogResult>();
        }
    }
}
