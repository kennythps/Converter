using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text2 = textBox1.Text;
            Regex rg = new Regex(@"\$[0-9A-Fa-f]+\$",
                RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);

            foreach (Match str in rg.Matches(textBox1.Text))
            {
                byte[] b = new byte[(str.Value.Length - 2) / 2];
                for (int i = 0, j = 1; i < b.Length; i++, j += 2)
                    b[i] = byte.Parse(str.Value.Substring(j, 2),
                        NumberStyles.AllowHexSpecifier);

                string newstr = BitConverter.ToInt32(b, 0).ToString("x");
                text2 = text2.Replace(str.Value, "$" + newstr + "$");
            }
            textBox2.Text = text2;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                textBox1.SelectAll();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                textBox2.SelectAll();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Documents|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = System.IO.File.ReadAllText(ofd.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Documents|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(sfd.FileName, textBox2.Text);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Put incorrect values to left box and copy correct values from right box.", "How to use", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Original verison made by Konctantin\n\nModificated version made by Kenny", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
