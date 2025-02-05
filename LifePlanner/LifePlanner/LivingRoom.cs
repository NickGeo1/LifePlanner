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
    public partial class LivingRoom : Form
    {
        private int robot_clicks = 0;

        private bool lights_on = true;
        private bool menu_open = false;
        private bool tv_on = false;
        public string channel;
        public Bitmap gif_channel;

        public LivingRoom()
        {
            InitializeComponent();
        }

        private void LivingRoom_Load(object sender, EventArgs e)
        {
            Misc.manageAssistantfromFile(this, chatbot_panel, "first_livingroom");

            //disable the menu button that corresponds to the form
            Misc.manageButtons(this, panel1);

            //set initial menu button location
            button1.Location = new Point(panel1.Location.X, panel1.Location.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //run show/hide function for menu and return result in menu_open
            menu_open = Misc.ShowHide(this, button1, panel1, label3 ,menu_open);
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = (lights_on) ? Resource1.living_room_Dark : Resource1.living_room_Bright;
            lights_on = !lights_on;
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            Misc.openForm("Hall");
            this.Hide();
        }

        private void tvpanel_MouseClick(object sender, MouseEventArgs e)
        {
            //if gif_channel is null set tvstatic gif to variable
            if (!tv_on)
                pictureBox1.Image = gif_channel ?? Resource1.tvstatic;
            else
                pictureBox1.Image = null;

            tv_on = !tv_on;
        }

        private void αλλαγήΚαναλιούToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!tv_on)
            {
                MessageBox.Show("Παρακαλώ ανοίξτε πρώτα την τηλεόραση!", "Προειδοποίηση", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TV control = new TV(channel, this, pictureBox1);
            control.Show();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //if gif_channel is null set tvstatic gif to variable
            if (!tv_on)
                pictureBox1.Image = gif_channel ?? Resource1.tvstatic;
            else
                pictureBox1.Image = null;

            tv_on = !tv_on;
        }

        private void Hallbtn_Click(object sender, EventArgs e)
        {
            String form = ((Button)sender).Name.Replace("btn", "");
            Misc.openForm(form);
            this.Hide();           
        }
       

        private void chatbot_panel_Click(object sender, EventArgs e)
        {
            switch (robot_clicks)
            {
                case 0:
                    label2.Text = "Στο παράθυρο που θα σου εμφανιστεί,\n" +
                                   "εισήγαγε έναν διψήφιο αριθμό πατώντας\n" +
                                   "τα κουμπιά και μετά πάτα 'ΟΚ'.Αν θες να\n" +
                                   "αλλάξεις την επιλογή σου, πάτα πρώτα\n" +
                                   "'Εκκαθάριση'";
                    robot_clicks += 1;
                    break;

                case 1:
                    label2.Text = "Η αυτόματη ταΐστρα βρίσκεται κάτω\nαριστερά στην οθόνη σου! Πάτα\nπάνω της για περισσότερες\nπληροφορίες!";
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
                    Misc.changeAssistantStateInFile("first_livingroom");
                    label2.Text = "Καλωσήρθες στο σαλόνι!Για να\n" +
                                  "ανάψεις ή να σβήσεις την τηλεόραση\n" +
                                  "κάνε αριστερό κλικ πάνω της. Για να\n" +
                                  "επιλέξεις κανάλι κάνε δεξί κλίκ στην\n" +
                                  "τηλεόραση όσο αυτή είναι αναμμένη και\n" +
                                  "πάτα 'Αλλαγή καναλιού'.";

                    break;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Misc.openForm("Feeder");
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
        private void LivingRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Misc.changeAssistantStateInFile("first_livingroom", true);
            Misc.manageAssistantfromFile(this, chatbot_panel, "first_livingroom");
            robot_clicks = 0;
            chatbot_panel.Show();
        }
    }
}
