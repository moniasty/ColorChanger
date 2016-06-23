using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ZmianaKolorow
{
    public partial class Form1 : Form
    {
        // 
        private string [] sourcePath;
        private string destinationPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                sourcePath = Directory.GetFiles(fbd.SelectedPath);
                MessageBox.Show("File found "+ sourcePath.Length.ToString(), "Message");

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd2 = new FolderBrowserDialog();
            DialogResult result = fbd2.ShowDialog();
            if (!string.IsNullOrWhiteSpace(fbd2.SelectedPath))
            {
                destinationPath = fbd2.SelectedPath;
                MessageBox.Show("Destination Folder: " +destinationPath, "Message");

            }

        }
    }
}
