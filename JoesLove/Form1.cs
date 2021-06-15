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

namespace JoesLove
{
    public partial class Form1 : Form
    {
        double DaysTogether = 0;

        string EntryDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\JoesLove";

        public Form1()
        {
            this.DaysTogether = GetDaysTogether();
            InitializeComponent();
            this.label3.Text = string.Format("Days we\'ve been together: {0}", this.DaysTogether);

            if (!Directory.Exists(this.EntryDirectory))
            {
                Directory.CreateDirectory(this.EntryDirectory);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            var entryFile = this.EntryDirectory + "\\" + this.textSubjectBox.Text + ".txt";

            if (string.IsNullOrEmpty(this.textSubjectBox.Text) && string.IsNullOrEmpty(this.textEntryBox.Text))
            {
                MessageBox.Show("You should create a subject and entry first sugs!");
            }
            else if(string.IsNullOrEmpty(this.textSubjectBox.Text))
            {
                MessageBox.Show("You should create a subject then save my love!");
            }
            else if (string.IsNullOrEmpty(this.textEntryBox.Text))
            {
                MessageBox.Show("You should add an entry first baby!");
            }
            else if (File.Exists(entryFile))
            {
                MessageBox.Show(string.Format("An entry with subject \"{0}\" already exists, try a new subject name!", this.textSubjectBox.Text));
            }
            else
            {
                using (FileStream fs = File.Create(entryFile))
                {
                    var bytes = new UTF8Encoding(true).GetBytes(this.textEntryBox.Text);
                    fs.Write(bytes, 0, bytes.Length);
                }

                MessageBox.Show(string.Format("I love you on our {0}th day, as much as I did on our first.. <3\n\nYour entry was saved at {1}", this.DaysTogether.ToString(), entryFile));

                this.ClearFields();
            }
        }

        private void ClearFields()
        {
            this.textSubjectBox.Text = string.Empty;
            this.textEntryBox.Text = string.Empty;
        }

        private double GetDaysTogether()
        {
            DateTime anniversary = new DateTime(2016, 1, 1);
            DateTime today = DateTime.Today;

            return (today - anniversary).TotalDays;
        }
    }
}
