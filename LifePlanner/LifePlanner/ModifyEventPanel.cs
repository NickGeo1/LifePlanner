﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace LifePlanner
{
    public partial class ModifyEventPanel : UserControl
    {
        public Dictionary<string, string> event_info = new Dictionary<string, string>();
        public static Panel provoker = new Panel();
        string StartTime;
        string EndTime;
        string Title;
        string selected_activity;
        string selected_address;
        string selected_transportation;
        string selected_beverage;

        public ModifyEventPanel()
        {

        }

        public ModifyEventPanel(Dictionary<string, string> info, Panel provoker)
        {
            InitializeComponent();
            BringToFront();
            event_info = info;
            ModifyEventPanel.provoker = provoker;
        }

        private void ModifyEventPanel_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(event_info["Address"] + " new " + event_info["Beverage"]);
            Parent.Visible = true;
            Title = event_info["Title"];
            textBox1.Text = event_info["Title"];
            StartTime = event_info["StartTime"];
            comboBox1.Text = event_info["StartTime"];
            EndTime = event_info["EndTime"];
            comboBox2.Text = event_info["EndTime"];
            selected_activity = event_info["Activity"];
            comboBox3.Text = event_info["Activity"];
            selected_address = event_info["Address"];
            comboBox4.Text = event_info["Address"];
            selected_transportation = event_info["Transportation"];
            comboBox5.Text = event_info["Transportation"];
            selected_beverage = event_info["Beverage"];
            comboBox7.Text = event_info["Beverage"];
            comboBox7.Items.AddRange(new object[] { "Δεν θέλω να αγοράσω ρόφημα", Program.beverage, "Άλλο" });
            textBox2.Hide();
            textBox3.Hide();
            comboBox6.Visible = false;
            comboBox6.Hide();

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DialogResult q = MessageBox.Show("Θέλεις σίγουρα να διαγράψεις τη δραστηριότητα;", "Διαγραφή Δραστηριότητας", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (q == DialogResult.OK)
            {
                DailyPlan.deleted = true;
                Parent.Hide();
                Hide();
                Parent.Visible = false;
                DailyPlan.modified = true;
    }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            TimeSpan t1 = TimeSpan.Parse(event_info["StartTime"]);
            TimeSpan t2 = TimeSpan.Parse(event_info["EndTime"]);
            TimeSpan t = t2.Subtract(t1);
            int d = (int)t.TotalHours;
            int start_hour = (int)t1.TotalHours;
            for (int i = 0; i < d; i++)
            {
                int k = start_hour + i;
                if (k / 10 >= 1)
                {
                    string k1 = k + ":00";
                    DailyPlan.start_time_restricted_hours.Add(k1);
                }
                else
                {
                    string k1 = "0" + k + ":00";
                    DailyPlan.start_time_restricted_hours.Add(k1);
                }
                int j = start_hour + i + 1;
                if (j / 10 >= 1)
                {
                    string j1 = j + ":00";
                    DailyPlan.end_time_restricted_hours.Add(j1);
                }
                else
                {
                    string j1 = "0" + j + ":00";
                    DailyPlan.end_time_restricted_hours.Add(j1);
                }
            }
            Parent.Visible = true;
            Parent.Hide();
            Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Title = textBox1.Text;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
           StartTime = comboBox1.Text;
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
           EndTime = comboBox2.Text;
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            selected_activity = comboBox3.Text;
            if (selected_activity == "Εντός Σπιτιού")
            {
                comboBox4.Enabled = false;
                textBox2.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
                comboBox7.Enabled = false;
                textBox3.Enabled = false;
            }
            else
            {
                comboBox4.Enabled = true;
                textBox2.Enabled = true;
                comboBox5.Enabled = true;
                comboBox6.Enabled = true;
                comboBox7.Enabled = true;
                textBox3.Enabled = true;
            }
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text == "Άλλο")
            {
                selected_address = comboBox4.Text;
                textBox2.Show();
            }
            else
            {
                if (comboBox4.Text == "Σπίτι")
                {
                    selected_address = Program.address;
                }
                if (comboBox4.Text == "Εργασία")
                {
                    selected_address = Program.work_address;
                }
                textBox2.Hide();
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            selected_address = textBox2.Text;
        }

        private void comboBox5_TextChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text == "Συνδυασμός")
            {
                comboBox6.Visible = true;
                comboBox6.Show();
            }
            else
            {
                comboBox6.Hide();
            }
            selected_transportation = comboBox5.Text;
        }

        private void comboBox6_TextChanged(object sender, EventArgs e)
        {
            selected_transportation = comboBox6.Text;
        }

        private void comboBox7_TextChanged(object sender, EventArgs e)
        {
            if (comboBox7.Text == "Άλλο")
            {
                textBox3.Show();
            }
            else
            {
                textBox3.Hide();
            }
            selected_beverage = comboBox7.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            selected_beverage = textBox3.Text;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            TimeSpan t1 = TimeSpan.Parse(event_info["StartTime"]);
            TimeSpan t2 = TimeSpan.Parse(event_info["EndTime"]);
            TimeSpan t = t2.Subtract(t1);
            int d = (int)t.TotalHours;
            int start_hour = (int)t1.TotalHours;
            for (int i = 0; i < d; i++)
            { 
                int k = start_hour + i;
                if (k / 10 >= 1)
                {
                    string k1 = k + ":00";
                    DailyPlan.start_time_restricted_hours.Remove(k1);
                }
                else
                {
                    string k1 = "0" + k + ":00";
                    DailyPlan.start_time_restricted_hours.Remove(k1);
                }
                int j = start_hour + i + 1;
                if (j / 10 >= 1)
                {
                    string j1 = j + ":00";
                    DailyPlan.end_time_restricted_hours.Remove(j1);
                }
                else
                {
                    string j1 = "0" + j + ":00";
                    DailyPlan.end_time_restricted_hours.Remove(j1);
                }
            }


            //form validation 
            if (textBox1.Text == "")
            {
                MessageBox.Show("Δώσε έναν τίτλο στη δραστηριότητά σου.");
                textBox1.Text = event_info["Title"];
                Title = "null";
            }
            if (DailyPlan.start_time_restricted_hours.Contains(comboBox1.Text))
            {
                MessageBox.Show("Επίλεξε μια ώρα έναρξης στην οποία δεν υπάρχει δραστηριότητα.");
                StartTime = "null";
            }
            if (DailyPlan.end_time_restricted_hours.Contains(comboBox2.Text))
            {
                MessageBox.Show("Επίλεξε μια ώρα λήξης στην οποία δεν υπάρχει δραστηριότητα.");
                EndTime = "null";
            }
            if (comboBox2.SelectedIndex <= comboBox1.SelectedIndex)
            {
                MessageBox.Show("Ή ώρα λήξης πρέπει να είναι μεταγενέστερη της ώρας έναρξης.");
                StartTime = "null";
                EndTime = "null";
            }
            if (comboBox3.Text == "")
            {
                MessageBox.Show("Επίλεξε το είδος της δραστηριότητάς σου.");
                selected_activity = "null";
            }
            if (comboBox4.Enabled == true & (comboBox4.Text == "" || (comboBox4.Text == "Άλλο" & textBox2.Text == "")))
            {
                MessageBox.Show("Διάλεξε ή συμπλήρωσε μια διεύθυνση της ακόλουθης μορφής: \"Οδός Αριθμός\".");
                selected_address = "null";
            }
            if (comboBox5.Enabled == true & (comboBox5.Text == "" || (comboBox5.Text == "Συνδυασμός" & comboBox6.Text == "")))
            {
                MessageBox.Show("Διάλεξε έναν τρόπο μεταφοράς.");
                selected_transportation = "null";
            }
            if (comboBox7.Enabled == true & (comboBox7.Text == "" || (comboBox7.Text == "Άλλο" & textBox3.Text == "")))
            {
                MessageBox.Show("Συμπλήρωσε το ρόφημα της αρεσκείας σου.");
                selected_beverage = "null";
            }
            //event added in planner
            if (Title != "null" & StartTime != "null" & EndTime != "null" & selected_activity != "null" & selected_address != "null" & selected_transportation != "null" & selected_beverage != "null")
            {
                event_info["Title"] = Title;
                event_info["StartTime"] = StartTime;
                event_info["EndTime"] = EndTime;
                event_info["Activity"] = selected_activity;
                event_info["Address"] = selected_address;
                event_info["Transportation"] = selected_transportation;
                event_info["Beverage"] = selected_beverage;

                //exit event form
                DailyPlan.saved = true;
                Parent.Visible = false;
                Parent.Hide();
                Hide();

                DailyPlan.modified = true;

            }
        }

    }
}
