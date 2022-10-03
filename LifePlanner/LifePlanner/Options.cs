﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifePlanner
{
    public partial class Options : Form
    {
        bool first = true;

        public Options()
        {
            InitializeComponent();
        }

        private void chatbot_panel_MouseClick(object sender, MouseEventArgs e)
        {
            if (first)
            {
                panel1.Visible = panel1.Enabled = true;
                foreach (Control c in panel1.Controls)
                {
                    c.Visible = true;
                }

                label1.Text = "Κάνε κλικ πανω στο ημερολόγιο\nγια τη δημιουργία του\n" +
                "ημερήσιου πλάνου σου ή στο\n" +
                "σπιτάκι για να διαχειριστείς τις\n" +
                "συσκευές του σπιτιού σου\n" +
                "από απόσταση!";

                first = false;

                return;
            }

            foreach (Control c in panel1.Controls)
            {
                c.Enabled = true;
            }
            
            chatbot_panel.Visible = chatbot_panel.Enabled = false;
            foreach (Control c in chatbot_panel.Controls)
            {
                c.Visible = false;
                c.Enabled = false;
            }
        }

        private void home_pictureBox_Click(object sender, EventArgs e)
        {
            Misc.openForm("Hall");
        }

        private void home_pictureBox_MouseHover(object sender, EventArgs e)
        {

            ((PictureBox)sender).BackColor = Color.FromArgb(128, 255, 128);
        }

        private void home_pictureBox_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Transparent;
        }

        private void calendar_pictureBox_Click(object sender, EventArgs e)
        {
            Misc.openForm("DailyPlan");
        }


        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ms = MessageBox.Show("Είσαι σίγουρος ότι θες να τερματίσεις την εφαρμογή; \n Όλες σου οι αλλαγές θα χαθούν.", "Ector", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (ms.Equals(DialogResult.OK))
            {
                Application.OpenForms[0].Close();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
