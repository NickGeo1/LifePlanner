﻿using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifePlanner
{
    public partial class Hall : Form
    {
        private bool menu_open = false;
        private bool lights_on = true;
        String door_status = "Κλειδωμένη";
        private int robot_clicks = 0;

        public Hall()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //run show/hide function for menu and return result in menu_open
            menu_open = Misc.ShowHide(this, button1, panel1, label3 ,menu_open);
        }

        private void Hall_Load(object sender, EventArgs e)
        {
            //disable the menu button that corresponds to the form
            Misc.manageButtons(this, panel1);

            //set initial menu button location
            button1.Location = new Point(panel1.Location.X, panel1.Location.Y);

            //Show or hide assistant based on file variable
            Misc.manageAssistantfromFile(this,chatbot_panel,"first_house");

        }

        //door lock
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = (door_status == "Ξεκλείδωτη") ? Resource1.locked : Resource1.unlocked;
            door_status = (door_status == "Ξεκλείδωτη") ? "Κλειδωμένη" : "Ξεκλείδωτη";
        }

        private void Exit_panel_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Κατάσταση εξώπορτας: " + door_status, "Εξώπορτα", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //lights
        private void panel7_MouseClick(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = (lights_on) ? Resource1.Hallway_Dark : Resource1.Hallway_Bright;
            lights_on = !lights_on;
        }

        private void chatbot_panel_MouseClick(object sender, MouseEventArgs e)
        {
            switch (robot_clicks)
            {
                case 0:
                    label2.Text = "Τα εικονίδια αριστερά από κάθε\n"+
                                    "πόρτα διευκρινίζουν τα δωμάτια.\n"+
                                    "Μπορείς επίσης να περιηγηθείς σε\n"+
                                    "αυτά κάνοντας κλίκ στο μενού πάνω\n"+
                                    "αριστερά!";
                    robot_clicks += 1;
                    break;

                case 1:
                    label2.Text = "Μπορείς να αλληλεπιδράσεις με\n"+
                                    "οτιδήποτε έχει άσπρο περίγραμμα\n"+
                                    "ή με κάποιο εικονίδιο που μετατρέπει\n"+
                                    "τον κέρσορά σου σε χεράκι όταν τον\n"+
                                    "περνάς από πάνω του! Δώσε προσοχή\n"+
                                    "στα σχετικά εικονίδια!";
                    robot_clicks += 1;
                    break;

                default:
                    //hide robot and enable the other controls
                    foreach (Control c in Controls)
                    {
                        if (c.Parent != chatbot_panel && c != chatbot_panel)
                            c.Enabled = true;
                        else if (c.Parent == chatbot_panel || c == chatbot_panel)
                            c.Enabled = c.Visible = false;
                    }

                    //change the file variable for this assistant
                    Misc.changeAssistantStateInFile("first_house");
                    label2.Text = "Καλωσήρθες στο έξυπνο σπίτι!\n" +
                                   "Μέσω της εφαρμογής αυτής μπορείς\n" +
                                   "πολύ εύκολα να διαχειριστείς τις\n" +
                                   "ηλεκτρικές συσκευές σου από όπου\n" +
                                   "και αν βρίσκεσαι!";

                    break;
            }
        }

        private void Kitchen_panel_MouseClick(object sender, MouseEventArgs e)
        {
            String panel = ((Control)sender).Name;
            String formname = panel.Replace("_panel","");

            Misc.openForm(formname);
            this.Hide();
        }  

        private void Hallbtn_Click(object sender, EventArgs e)
        {
            String form = ((Button)sender).Name.Replace("btn", "");
            Misc.openForm(form);
            this.Hide();
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Misc.openForm("Shoes");
            //Hide();
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.BackColor = Color.FromArgb(255, 128, 128);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.BackColor = Color.LightCyan;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Misc.openForm("Options");
            this.Hide();
        }

        private void Hall_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Misc.changeAssistantStateInFile("first_house", true);
            Misc.manageAssistantfromFile(this, chatbot_panel, "first_house");
            robot_clicks = 0;
            chatbot_panel.Show();
        }
    }
}