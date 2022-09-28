﻿using System;
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
    class Misc
    {
        //public list to access messagebox forms
        public static List<Messagebox> mblist = new List<Messagebox>();

        //Form variables
        private static Kitchen k = null;
        private static Bedroom br = null;
        private static LivingRoom lr = null;
        private static Bathroom b = null;
        private static Hall h = null;
        private static Feeder f = null;

        /**
         * Function that shows/hides the menu at House forms.
         * returns a boolean value which indicates if the menu has
         * been closed(false) or opened(true)
         */

        public static bool ShowHide(Form form, Button menubutton, Panel menupanel, bool menu_open)
        {
            if (!menu_open)
            {
                foreach (Control c in form.Controls)
                {
                    if (c.Parent != menupanel && c != menubutton)
                        c.Enabled = false;
                }

                menubutton.Location = new Point(menupanel.Location.X + menupanel.Width, menupanel.Location.Y);
                menupanel.Visible = menupanel.Enabled = true;
                menubutton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 128); //red
                return true;
            }
            else
            {
                foreach (Control c in form.Controls)
                {
                    if (c.Parent != menupanel && c != menubutton)
                        c.Enabled = true;
                }

                menubutton.Location = new Point(menupanel.Location.X, menupanel.Location.Y);
                menupanel.Visible = menupanel.Enabled = false;
                menubutton.FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 255, 128); //green
                return false;
            }
        }

        /**
         * Disable the menu button that corresponds to the current room
         */
        public static void manageButtons(Form form, Panel panel)
        {
            String formname = form.Name;
            
            foreach(Control c in panel.Controls)
            {
                if (c is Button && c.Name.Equals(formname + "btn"))
                {
                    c.Enabled = false;
                }

            }
        }

        /**
         * Check if current form's assistant has been viewed before. If yes, Disable interaction
         */
        public static bool manageAssistantfromFile(Form form, Panel chatbot_panel, String variable)
        {
            try
            {
                StreamReader sr = new StreamReader("OtherData.txt", true);
                String[] lines = sr.ReadToEnd().Split('|');
                sr.Close();

                //hide robot interaction if its not the first time
                if (lines.Contains(variable + ": false") )
                {
                    chatbot_panel.Hide();
                    return false;
                }                   
                else
                {
                    //disable form controls except robot's to interact with robot
                    foreach (Control c in form.Controls)
                    {
                        if (c.Parent != chatbot_panel && c != chatbot_panel)
                            c.Enabled = false;
                    }

                    return true;
                }              
            }
            catch (Exception)
            {
                Console.WriteLine("An Exception was occured while trying to read the file");
                return true;
            }
        }

        /**
         * Change the given file variable to false.
         * This is for disabling the assistant interaction next time
         */
        public static void changeAssistantStateInFile(String variable)
        {            
            try
            {
                //read all the lines and change only the desirable one.
                //Then rewrite all lines again
                StreamReader sr = new StreamReader("OtherData.txt", true);
                String[] lines = sr.ReadToEnd().Split('|');
                sr.Close();

                for (int i=0; i<lines.Length; i++)
                {
                    if (lines[i].StartsWith(variable + ": true"))
                        lines[i] = lines[i].Replace("true","false");
                }

                StreamWriter sw = new StreamWriter("OtherData.txt");
                foreach (String line in lines)
                {
                    if(!line.Equals(""))
                        sw.Write(line + "|");
                }
                    
                sw.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("An Exception was occured while trying to read/write the file");
            }
        }

        /**
         * Shows a form if already exists.
         * If not, the form object is being instantiated
         * and can be accessed later
         */
        public static void openForm(String formname)
        {
            /*foreach(Form f in Application.OpenForms)
            {
                if (f.Name.Equals(formname))
                {
                    f.Show();
                    return;
                }
                        
            }

            Console.WriteLine("Form not found!");

            switch (formname)
            {
                case "Kitchen":
                    Kitchen k = new Kitchen();
                    k.Show();
                    break;
                case "Bedroom":
                    Bedroom b = new Bedroom();
                    b.Show();
                    break;
                case "LivingRoom":
                    LivingRoom lr = new LivingRoom();
                    lr.Show();
                    break;
                case "Bathroom":
                    Bathroom br = new Bathroom();
                    br.Show();          
                    break;
                default:
                    Hall h = new Hall();
                    h.Show();
                    break;
            }*/


            switch (formname)
            {
                case "Kitchen":
                    if(k == null)
                    {
                        k = new Kitchen();
                        k.Show();
                    }
                    else
                    {
                        k.Show();
                    }
                    break;

                case "Bedroom":
                    if (br == null)
                    {
                        br = new Bedroom();
                        br.Show();
                    }
                    else
                    {
                        br.Show();
                    }
                    break;

                case "LivingRoom":
                    if (lr == null)
                    {
                        lr = new LivingRoom();
                        lr.Show();
                    }
                    else
                    {
                        lr.Show();
                    }
                    break;

                case "Bathroom":
                    if (b == null)
                    {
                        b = new Bathroom();
                        b.Show();
                    }
                    else
                    {
                        b.Show();
                    }
                    break;

                case "Feeder":
                    if(f == null)
                    {
                        Console.WriteLine("null");
                        f = new Feeder();
                        f.Show();
                    }
                    else
                    {
                        f.Show();
                    }
                    break;

                default:
                    if (h == null)
                    {
                        h = new Hall();
                        h.Show();
                    }
                    else
                    {
                        h.Show();
                    }
                    break;
            }
                
        }

    }
}