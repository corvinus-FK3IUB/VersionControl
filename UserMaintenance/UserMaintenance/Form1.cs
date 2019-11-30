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
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();

        public Form1()
        {
            InitializeComponent();
            lblFullName.Text = Resource1.FullName; 
            btnAdd.Text = Resource1.Add;
            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = txtFullName.Text
            };
            users.Add(u);

            txtFullName.Clear();

        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            sfd.DefaultExt = "csv";

            if (sfd.ShowDialog() != DialogResult.OK) return;


            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {

                sw.WriteLine("ID; FullName");

                foreach (User u in listUsers.Items)
                {
                sw.Write(u.ID.ToString());
                sw.Write(";");
                sw.Write(u.FullName);
                sw.WriteLine();
                }

            }
            MessageBox.Show("A fájlba írás megtörtént");

            Application.Exit();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            users.Remove((User)listUsers.SelectedItem);
        }
    }
}
