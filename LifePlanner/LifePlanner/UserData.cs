﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifePlanner
{
    public partial class UserData : Form
    {
        private bool submit = false;

        public UserData()
        {
            InitializeComponent();
        }

        private void UserData_Load(object sender, EventArgs e)
        {
            foreach (Control c in data_panel.Controls)
            {
                c.Hide();
            }
            foreach (Control c in chatbot_panel.Controls)
            {
                c.Show();
                c.BringToFront();
            }

            submit_button.Enabled = false;
            submit_button.Hide();

            label1.Show();
            label1.BringToFront();
        }

        private void submit_button_Click(object sender, EventArgs e)
        {
            // form validation

            if (textBox1.Text != "")
                Program.username = textBox1.Text;
            else
            {
                MessageBox.Show("Συμπλήρωσε το username σου.");
                Program.username = "null";
            }

            if (comboBox1.Text != "")
                Program.gender = comboBox1.Text;
            else
            {
                MessageBox.Show("Επίλεξε το φύλο σου.");
                Program.gender = "null";
            }

            if (new Regex(@"^(\d{1,2})+$").IsMatch(textBox2.Text))
                Program.age = textBox2.Text;
            else
            {
                MessageBox.Show("Συμπλήρωσε την ηλικία σου σε έτη (πχ. 25).");
                Program.age = "null";
            }

            if (new Regex(@"^([A-Za-zΑ-Ωα-ωίϊΐόάέύϋΰήώ]*[ ]\d{1,3})+$").IsMatch(textBox3.Text))
                Program.address = textBox3.Text;
            else
            {
                MessageBox.Show("Συμπλήρωσε μια διεύθυνση της ακόλουθης μορφής: \"Οδός Αριθμός\" (πχ. Ηροδότου 32).");
                Program.address = "null";
            }

            if (new Regex(@"^([A-Za-zΑ-Ωα-ωίϊΐόάέύϋΰήώ]*[ ]\d{1,3})+$").IsMatch(textBox4.Text))
                Program.work_address = textBox4.Text;
            else
            {
                MessageBox.Show("Συμπλήρωσε μια διεύθυνση της ακόλουθης μορφής: \"Οδός Αριθμός\" (πχ. Ηροδότου 32).");
                Program.work_address = "null";
            }

            if (comboBox2.Text != "")
                Program.transportation = comboBox2.Text;
            else
            {
                MessageBox.Show("Επίλεξε το σύνηθες μεταφορικό σου μέσο.");
                Program.transportation = "null";
            }

            if (new Regex(@"^(\b([3][5-9]|4[0-9])\b)+$").IsMatch(textBox5.Text))
                Program.shoe_size = textBox5.Text;
            else
            {
                MessageBox.Show("Συμπλήρωσε το νούμερο παπουτσιού σου (ακέραιος στο διάστημα 35-49.");
                Program.shoe_size = "null";
            }

            if (textBox6.Text != "")
                Program.beverage = textBox6.Text;
            else
            {
                MessageBox.Show("Συμπλήρωσε το αγαπημένο σου ρόφημα.");
                Program.beverage = "null";
            }

            if (comboBox3.Text != "")
                Program.pet = comboBox3.Text;
            else
            {
                MessageBox.Show("Επίλεξε το κατοικίδιό σου.");
                Program.pet = "null";
            }

            // add user data to the file
            if (Program.username != "null" & Program.gender != "null" & Program.age != "null" & Program.address != "null" & Program.work_address != "null" & Program.transportation != "null" & Program.shoe_size != "null" & Program.beverage != "null" & Program.pet != "null")
            {
                StreamWriter sw = new StreamWriter("UserData.txt", true);
                sw.WriteLine(textBox1.Text); //Username
                sw.WriteLine(comboBox1.Text); //Gender
                sw.WriteLine(textBox2.Text); //Age
                sw.WriteLine(textBox3.Text); //Address
                sw.WriteLine(textBox4.Text); //Work Address
                sw.WriteLine(comboBox2.Text); //Means of Transport
                sw.WriteLine(textBox5.Text); // Show Size
                sw.WriteLine(textBox6.Text); //Favorite Beverage
                sw.WriteLine(comboBox3.Text); //Pet
                sw.Write(Program.Date); //Date
                sw.Close();

                Misc.openForm("Options");
                submit = true;
                Close();
            }
            
        }

        private void chatbot_panel_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (Control c in data_panel.Controls)
            {
                c.Show();
            }
            foreach (Control c in chatbot_panel.Controls)
            {
                c.Hide();
                c.Enabled = false;
            }
            chatbot_panel.Enabled = false;
            chatbot_panel.Hide();

            submit_button.Enabled = true;
            submit_button.Show();

            label1.Hide();
        }

        private void UserData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!submit)
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
}
