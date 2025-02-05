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
    public partial class Kitchen : Form
    {
        private bool menu_open = false;
        private bool lights_on = true;

        public Kitchen()
        {
            InitializeComponent();
        }

        private void Kitchen_Load(object sender, EventArgs e)
        {
            //disable the menu button that corresponds to the form
            Misc.manageButtons(this, panel1);

            //set initial menu button location
            button1.Location = new Point(panel1.Location.X, panel1.Location.Y);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //run show/hide function for menu and return result in menu_open
            menu_open = Misc.ShowHide(this, button1, panel1, label3, menu_open);
        }

        private void Hallbtn_Click(object sender, EventArgs e)
        {
            String form = ((Button)sender).Name.Replace("btn", "");
            Misc.openForm(form);
            this.Hide();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = (lights_on) ? Resource1.kitchen_Dark : Resource1.kitchen_Bright;
            lights_on = !lights_on;
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            Misc.openForm("Hall");
            this.Hide();
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

        private void Kitchen_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
