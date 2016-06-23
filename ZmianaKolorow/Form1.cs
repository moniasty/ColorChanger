﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace ZmianaKolorow
{
    public partial class Form1 : Form
    {
        // 
        private string [] sourcePath;
        private string destinationPath;
        private List<Color> oldColor=new List<Color>();
        private List<Color> newColor = new List<Color>();

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
                    bitmap = (Bitmap)System.Drawing.Image.FromFile(s);
                    bitmap = change(bitmap);
                    string[] spliter = s.Split('\\');
                    
                    bitmap.Save(destinationPath+ '\\' + spliter[spliter.Count()-1]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAddChange_Click(object sender, EventArgs e)
        {
            /*
          Color  oc = Color.FromArgb(
                Convert.ToInt32(OldColorR.Text),
                Convert.ToInt32(OldColorG.Text),
                Convert.ToInt32(OldColorB.Text));

           Color nc = Color.FromArgb(
                Convert.ToInt32(NewColorR.Text),
                Convert.ToInt32(NewColorG.Text),
                Convert.ToInt32(NewColorB.Text));
            */

            oldColor.Add(Color.FromArgb(
                Convert.ToInt32(OldColorR.Text),
                Convert.ToInt32(OldColorG.Text),
                Convert.ToInt32(OldColorB.Text)));
            newColor.Add(Color.FromArgb(
                Convert.ToInt32(NewColorR.Text),
                Convert.ToInt32(NewColorG.Text),
                Convert.ToInt32(NewColorB.Text)));

            OldColorR.Clear();
            OldColorG.Clear();
            OldColorB.Clear();

            NewColorR.Clear();
            NewColorG.Clear();
            NewColorB.Clear();


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
                            picture.SetPixel(x, y, newColor[i]);
                            break;
                        }
                        //else
                        //{
                        //    picture.SetPixel(x, y, actualColor);
                        //}
                        
                    }
                }
            }
            return picture;

        }

        private void btnCreatePDF_Click(object sender, EventArgs e)
        {
            string[] bmpFiles;
            if (!string.IsNullOrWhiteSpace(destinationPath))
            {
                bmpFiles = Directory.GetFiles(destinationPath);
                
                System.Drawing.Image first = System.Drawing.Image.FromFile(bmpFiles[0]);
                Document doc = new Document(new iTextSharp.text.Rectangle(first.Width, first.Height));
                PdfWriter.GetInstance(doc, new FileStream("image.pdf", FileMode.Create) );
                doc.Open();
                iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(first, System.Drawing.Imaging.ImageFormat.Bmp);
                doc.Add(pdfImage);

                for (int i=1;i<bmpFiles.Count();i++)
                {
                    //System.Drawing.Image image = System.Drawing.Image.FromFile(bmpFiles[i]);
                    iTextSharp.text.Image pdfImage2 = iTextSharp.text.Image.GetInstance(System.Drawing.Image.FromFile(bmpFiles[i]), System.Drawing.Imaging.ImageFormat.Bmp);
                    doc.Add(pdfImage2);
                }


                doc.Close();

            }


        }
    }
}
