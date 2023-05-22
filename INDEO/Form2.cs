using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using WindowsFormsApplication1;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        string sciezka1;
        int SumaPLIK;
        string PlikExe1;
        string nPlik;
        private BackgroundWorker BackgroundWorker1 = null;

        public Form2()
        {
            InitializeComponent();
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label100.Text = progressBar1.Value * 100 / SumaPLIK + " %";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            sciezka1 = textBox1.Text;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(sciezka1) == false)
            {
                MessageBox.Show("Błędna Ścieżka !");
            }
            else
            {
                sciezka1 = sciezka1 + @"\";
                sciezka1 = Path.GetFullPath(sciezka1);
                string[] Pliki = Directory.GetFiles(sciezka1, "*.tif");
                SumaPLIK = 0;
                foreach (string iPlik in Pliki)
                {
                    ++SumaPLIK;
                }
                if (SumaPLIK == 0)
                {
                    MessageBox.Show("Brak Plików !");
                }
                else
                {
                    PlikExe1 = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                    if (File.Exists(PlikExe1) == false)
                    {
                        PlikExe1 = @"C:\Program Files (x86)\INDEOv2\bin\exiftool.exe";
                        if (File.Exists(PlikExe1) == false)
                        {
                            MessageBox.Show("Brak pliku exiftool !");
                        }
                        else
                        {
                            PlikExe1 = @"""C:\Program Files (x86)\INDEOv2\bin\exiftool.exe""";
                            GeoInfo();
                        }
                    }
                    else
                    {
                        PlikExe1 = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                        GeoInfo();
                    }
                }
            }
        }

        private void GeoInfo()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button2.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_GeoInfo_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_GeoInfo_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_GeoInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label100.Text = "OK";
            button2.Enabled = true;
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            listBox1.ScrollAlwaysVisible = false;
            listBox1.HorizontalScrollbar = true;
            listBox1.BorderStyle = BorderStyle.FixedSingle;
            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
            listBox1.MeasureItem += new MeasureItemEventHandler(ListBox1_MeasureItem);
            listBox1.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);
        }

        private void BackgroundWorker_GeoInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            listBox1.Items.Clear();
            listBox1.Sorted = true;
            listBox1.BeginUpdate();
            string[] Pliki = Directory.GetFiles(sciezka1, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                Process MyProcess = new Process();
                MyProcess.StartInfo.FileName = PlikExe1;
                MyProcess.StartInfo.Arguments = @" -a -u -g1 -w txt """ + iPlik + @"""";
                MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess.Start();
                MyProcess.WaitForExit();
                string txtPlik = iPlik.Remove(iPlik.Length - 3) + "txt";
                string zawartoscI = null;
                string zawartoscT = null;
                string zawartoscTFW = null;
                string zawartoscG = null;
                int nrW = 0;
                int nrH = 0;
                int nrB = 0;
                int nrC = 0;
                int nrTW = 0;
                int nrTL = 0;
                int nrIFD = -1;
                string[] zawartosc = File.ReadAllLines(txtPlik);
                foreach (string iLinia in zawartosc)
                {
                    int nr = 0;
                    nr = iLinia.IndexOf("File Size                       :");
                    if (nr > -1)
                    {
                        string xy = iLinia.Remove(0, 34);
                        zawartoscI = nPlik + "   " + xy;
                    }
                    nr = iLinia.IndexOf("Image Width                     :");
                    if (nr > -1)
                    {
                        if (nrW == 0)
                        {
                            string xy = iLinia.Remove(0, 34);
                            zawartoscI = zawartoscI + "   " + xy;
                            nrW = nrW + 1;
                        }
                    }
                    nr = iLinia.IndexOf("Image Height                    :");
                    if (nr > -1)
                    {
                        if (nrH == 0)
                        {
                            string xy = iLinia.Remove(0, 34);
                            zawartoscI = zawartoscI + "x" + xy;
                            nrH = nrH + 1;
                        }
                    }
                    nr = iLinia.IndexOf("Bits Per Sample                 :");
                    if (nr > -1)
                    {
                        if (nrB == 0)
                        {
                            string xy = iLinia.Remove(0, 34);
                            zawartoscI = zawartoscI + "   " + xy;
                            nrB = nrB + 1;
                        }
                    }
                    nr = iLinia.IndexOf("Compression                     :");
                    if (nr > -1)
                    {
                        if (nrC == 0)
                        {
                            string xy = iLinia.Remove(0, 34);
                            zawartoscI = zawartoscI + "   " + xy;
                            nrC = nrC + 1;
                        }
                    }
                    nr = iLinia.IndexOf("Tile Width                      :");
                    if (nr > -1)
                    {
                        if (nrTW == 0)
                        {
                            string xy = iLinia.Remove(0, 34);
                            zawartoscT = "   " + xy;
                            nrTW = nrTW + 1;
                        }
                    }
                    nr = iLinia.IndexOf("Tile Length                     :");
                    if (nr > -1)
                    {
                        if (nrTL == 0)
                        {
                            string xy = iLinia.Remove(0, 34);
                            zawartoscT = zawartoscT + "x" + xy + "   ";
                            nrTL = nrTL + 1;
                        }
                    }
                    nr = iLinia.IndexOf("---- IFD");
                    if (nr > -1)
                    {
                        nrIFD = nrIFD + 1;
                    }
                    nr = iLinia.IndexOf("Pixel Scale                     :");
                    if (nr > -1)
                    {
                        string xy = iLinia.Remove(0, 34);
                        nr = xy.LastIndexOf(" ");
                        xy = xy.Remove(nr);
                        nr = xy.LastIndexOf(" ");
                        string y = xy.Remove(0, nr);
                        zawartoscTFW = "  " + y;
                    }
                    nr = iLinia.IndexOf("Model Tie Point                 :");
                    if (nr > -1)
                    {
                        string xy = iLinia.Remove(0, 34);
                        nr = xy.LastIndexOf(" ");
                        xy = xy.Remove(nr);
                        nr = xy.LastIndexOf(" ");
                        string y = xy.Remove(0, nr);
                        xy = xy.Remove(nr);
                        nr = xy.LastIndexOf(" ");
                        string x = xy.Remove(0, nr);
                        zawartoscTFW = zawartoscTFW + "  " + x + y;
                    }
                    nr = iLinia.IndexOf("GT Citation                     :");
                    if (nr > -1)
                    {
                        string xy = iLinia.Remove(0, 34);
                        zawartoscG = "   " + xy;
                    }
                    nr = iLinia.IndexOf("Geographic Type                 :");
                    if (nr > -1)
                    {
                        string xy = iLinia.Remove(0, 34);
                        zawartoscG = zawartoscG + "   " + xy;
                    }
                    nr = iLinia.IndexOf("Geog Ellipsoid                  :");
                    if (nr > -1)
                    {
                        string xy = iLinia.Remove(0, 34);
                        zawartoscG = zawartoscG + "   " + xy;
                    }
                    nr = iLinia.IndexOf("Projected CS Type               :");
                    if (nr > -1)
                    {
                        string xy = iLinia.Remove(0, 34);
                        zawartoscG = zawartoscG + "   " + xy;
                    }
                    nr = iLinia.IndexOf("PCS Citation                    :");
                    if (nr > -1)
                    {
                        string xy = iLinia.Remove(0, 34);
                        zawartoscG = zawartoscG + "   " + xy;
                    }
                    nr = iLinia.IndexOf("Proj Linear Units               :");
                    if (nr > -1)
                    {
                        string xy = iLinia.Remove(0, 34);
                        zawartoscG = zawartoscG + "   " + xy;
                    }
                }
                if (zawartoscT == null)
                {
                    zawartoscT = "   UNTILED   ";
                }
                if (zawartoscTFW == null)
                {
                    zawartoscTFW = "    brak współrzędnych";
                }
                if (zawartoscG == null)
                {
                    zawartoscG = "          brak definicji układu";
                }
                zawartoscI = zawartoscI + zawartoscT + nrIFD + zawartoscTFW + zawartoscG;
                listBox1.Items.Add(zawartoscI);
                File.Delete(txtPlik);
            }
            listBox1.EndUpdate();
        }

       private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
       {
         if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
         {
              e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);
         }
         else
          {
              e.Graphics.FillRectangle(Brushes.Beige, e.Bounds);
          }
              e.Graphics.DrawRectangle(Pens.Blue, e.Bounds);
              e.Graphics.DrawString(listBox1.Items[e.Index].ToString(),
               new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238))), Brushes.Black, e.Bounds.X, e.Bounds.Y);
              e.DrawFocusRectangle();
        }

        private void ListBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox theListBox = (ListBox)sender;
            string itemString = (string)theListBox.Items[e.Index];
            string[] resultStrings = itemString.Split('.');
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string item = "";
            foreach (var i in listBox1.SelectedIndices)
            {
                item += listBox1.Items[(int)i] + Environment.NewLine;
            }
            MessageBox.Show(item);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nPlik = openFileDialog1.FileName;
                SPRAWDZ_TIFF();
            }
        }

        private void SPRAWDZ_TIFF()
        {
            string Tif = nPlik.Remove(0, nPlik.Length - 4);
            if (Tif == ".tif" || Tif == ".jpg" || Tif == ".TIF" || Tif == ".JPG")
            {
                PlikExe1 = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                if (File.Exists(PlikExe1) == false)
                {
                    PlikExe1 = @"C:\Program Files (x86)\INDEOv2\bin\exiftool.exe";
                    if (File.Exists(PlikExe1) == false)
                    {
                        MessageBox.Show("Brak pliku exiftool !");
                    }
                    else
                    {
                        PlikExe1 = @"""C:\Program Files (x86)\INDEOv2\bin\exiftool.exe""";
                        PLIK1();
                    }
                }
                else
                {
                    PlikExe1 = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                    PLIK1();
                }
            }
            else
            {
                MessageBox.Show("To nie jest Tif !");
            }

        }

        private void PLIK1()
        {
            Process MyProcess = new Process();
            MyProcess.StartInfo.FileName = PlikExe1;
            MyProcess.StartInfo.Arguments = @" -a -u -g1 -w txt """ + nPlik + @"""";
            MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess.Start();
            MyProcess.WaitForExit();
            listBox1.Items.Clear();
            listBox1.Sorted = false;
            listBox1.BeginUpdate();
            int nrL = 0;
            int nrG = 0;
            int nrIFD = 0;
            string txtPlik = nPlik.Remove(nPlik.Length - 3) + "txt";
            string[] zawartosc = File.ReadAllLines(txtPlik);
            foreach (string iLinia in zawartosc)
            {
                int nr = 0;
                nr = iLinia.IndexOf("---- IFD");
                if (nr > -1)
                {
                    nrIFD = nrIFD + 1;
                }
                nr = iLinia.IndexOf("---- IFD1 ----");
                if (nr > -1)
                {
                    nrL = nrL + 1;

                }
                else
                {
                    if (nrL == 0)
                    {
                        listBox1.Items.Add(iLinia);
                    }
                    else
                    {
                        int i = 0;
                        i = iLinia.IndexOf("---- GeoTiff ----");
                        if (i > -1)
                        {
                            nrG = nrG + 1;
                            listBox1.Items.Add(iLinia);
                        }
                        else
                        {
                            if (nrG > 0)
                            {
                                listBox1.Items.Add(iLinia);
                            }
                        }

                    }
                }
            }
            nrIFD = nrIFD - 1;
            listBox1.Items.Add("---- Overwiev ----");
            listBox1.Items.Add("Number of Overwiev              : " + nrIFD);
            File.Delete(txtPlik);
            listBox1.EndUpdate();
            listBox1.BorderStyle = BorderStyle.FixedSingle;
            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
            listBox1.MeasureItem += new MeasureItemEventHandler(ListBox1_MeasureItem);
            listBox1.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);
        }

        private void TextBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox1.Text = files[0];
                }
            }
        }

        private void TextBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;   
        }

        private void ListBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    nPlik = files[0];
                    SPRAWDZ_TIFF();
                }
            }
        }

        private void ListBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

    }
}
