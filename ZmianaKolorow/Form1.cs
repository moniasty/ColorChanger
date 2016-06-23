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
        private List<Color> oldColor;
        private List<Color> newColor;

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

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bitmap = null;
                foreach (string s in sourcePath)
                {
                    bitmap = (Bitmap)Image.FromFile(s);
                    bitmap = change(bitmap);
                    string[] spliter = s.Split('\\');
                    bitmap.Save(destinationPath + spliter[1]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAddChange_Click(object sender, EventArgs e)
        {
            oldColor.Add(Color.FromArgb(
                Convert.ToInt32(OldColorR),
                Convert.ToInt32(OldColorG),
                Convert.ToInt32(OldColorB)));

            newColor.Add(Color.FromArgb(
                Convert.ToInt32(NewColorR),
                Convert.ToInt32(NewColorG),
                Convert.ToInt32(NewColorB)));
        }




        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private Bitmap change(Bitmap picture)
        {
            Color actualColor;

            Bitmap newPicture = new Bitmap(picture);
            for(int x=0; x<picture.Width;x++)
            {
                for(int y=0; y<picture.Height; y++)
                {
                    actualColor = picture.GetPixel(x, y);
                    for(int i=0; i< oldColor.Count;i++)
                    {
                        if(actualColor == oldColor[i])
                        {
                            picture.SetPixel(i, j, newColor[i]);
                        }
                        else
                        {
                            picture.SetPixel(i, j, actualColor);
                        }
                    }
                }
            }
            return picture;

        }


    }
}
