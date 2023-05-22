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
using System.Data.OleDb;
using WindowsFormsApplication2;
using WindowsFormsApplication3;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        char[] spacja = { ' ' };
        bool isPanel1Open = false;
        bool isPanel2Open = false;
        string sciezka1;
        string sciezka2;
        string sciezkaO;
        string sciezkaS;
        string sciezkaW;
        string sciezkaT;
        string sciezkaP;
        string sciezkaM;
        string plik1;
        string plik2;
        string plik3;
        string plik4;
        string Image;
        string plikTif;
        string plikTfw;
        string plikShp;
        string plikDxf;
        string plikDgn;
        string plikPrj;
        string plikPrjO;
        string plikPrjW;
        string plikINPHOPrj;
        string plikDTM;
        string plikBreak;
        string plikObiekt;
        string plikPunkty;
        string plikDTMopts;
        string Wysokosc;
        string Deh;
        string TrueOrto = "0";
        string TrueOrtoA = "1";
        string Time;
        string plikCam;
        string plikS;
        string Kamera;
        string rozszerzenie;
        string katalogINPHO;
        string verINPHO;
        string katalogQGIS;
        string verQGIS;
        string PlikExePhoto;
        string verPhoto;
        string PlikExeEXIF;
        string PlikExeOrthoMaster;
        string PlikExeDtmTool;
        string PlikExeGDInfo;
        string PlikExeGDWarp;
        string PlikExeGDRaster;
        string PlikExeGDTrans;
        string PlikExeGDAddo;
        string PlikExeINPHOPYR;
        string PlikExePython;
        string PlikExeGM;
        string verGM;
        string PlikExe7z;
        string PlikExeRAR;
        string PlikINDEO = @"C:\Program Files\INDEOv2\INDEO.ind";
        string wiersz;
        int SumaPLIK;
        int SumaPLIKF;
        int wTablica;
        string WielkoscPix;
        string X;
        string Y;
        string XR;
        string YR;
        string Ogniskowa;
        string XPP;
        string YPP;
        string Ramka;
        bool Wybor = false;
        bool wybor1 = false;
        bool wybor2 = false;
        bool wybor3 = false;
        bool wybor4 = false;
        bool wybor5 = false;
        bool wybor6 = false;
        string FBS = "5";
        string q = "4";
        string metodaR = " -r average ";
        string metodaP = " -r cubicspline";
        string metodaT = " -r cubic ";
        string kompresja;
        string tile;
        string tifver;
        string georef;
        string geosrs;
        string kolorMode;
        string kolor;
        string metoda;
        string ImageDescription;
        string Software;
        string Copyright;
        string Resolution;
        string ResolutionUnit = "";
        string Info;
        string BOX1;
        string BOX2;
        string BOX3;
        string BOX4;
        string BOX5;
        string BOX6;
        string BOX7;
        string BOX8;
        string BOX9;
        string BOX10;
        string OverView = " 2 4 8 16 32 64 128 256 512 1024"; int i;
        string nPlik;
        string dPlik;
        string wPlik;
        string gmwPlik;
        int nrCONTROL_POINTS = 0;
        int nrEND_POINTS = 0;
        string[] DXFpoczatek =
                {
                    "  0",
                    "SECTION",
                    "  2",
                    "HEADER",
                    "  0",
                    "ENDSEC",
                    "  0",
                    "SECTION",
                    "  2",
                    "TABLES",
                    "  0",
                    "ENDSEC",
                    "  0",
                    "SECTION",
                    "  2",
                    "BLOCKS",
                    "  0",
                    "ENDSEC",
                    "  0",
                    "SECTION",
                    "  2",
                    "ENTITIES",
                };
        string[] DXFkoniec =
            {
                "  0",
                "ENDSEC",
                "  0",
                "EOF",
            };

        private BackgroundWorker BackgroundWorker1 = null;

        public Form1()
        {
            int Time = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (Time > 20240601)
            {
                MessageBox.Show("Coś poszło nie tak !");
                Application.Exit();
            }
            else
            {
                InitializeComponent();
                comboBox1.SelectedIndex = 2;
                comboBox2.SelectedIndex = 3;
                comboBox4.SelectedIndex = 1;
                comboBox5.SelectedIndex = 3;
                WczytajUstawienia();
            }
        }

        //---------------OGOLNE---------------------------------------------------------------------------

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label50.Text = progressBar1.Value + "  /  " + SumaPLIK + "  -  " + nPlik;
            label106.Text = progressBar1.Value * 100 / SumaPLIK + " %";
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            new INDEO.AboutBox1().ShowDialog();
        }

        private void TextBox62_TextChanged(object sender, EventArgs e)
        {
            katalogQGIS = textBox62.Text;
            int nr = katalogQGIS.IndexOf(" ");
            if (nr > -1)
            {
                verQGIS = katalogQGIS.Remove(0, katalogQGIS.LastIndexOf(" "));
            }
        }

        private void Button107_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox62.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox65_TextChanged(object sender, EventArgs e)
        {
            PlikExeGM = textBox65.Text;
            if (File.Exists(PlikExeGM) == true)
            {
                verGM = Path.GetDirectoryName(PlikExeGM);
                int nr = verGM.IndexOf(@"\");
                if (nr > -1)
                {
                    verGM = verGM.Remove(0, verGM.LastIndexOf(@"\") + 1);
                }
            }
            else
            {
                verGM = "";
            }
        }

        private void Button110_Click(object sender, EventArgs e)
        {
            if (openFileDialog10.ShowDialog() == DialogResult.OK)
            {
                textBox65.Text = openFileDialog10.FileName;
            }
        }

        private void TextBox64_TextChanged(object sender, EventArgs e)
        {
            katalogINPHO = textBox64.Text;
            int nr = katalogINPHO.IndexOf(" ");
            if (nr > -1)
            {
                verINPHO = katalogINPHO.Remove(0, katalogINPHO.LastIndexOf(" "));
            }
        }

        private void Button109_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox64.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox63_TextChanged(object sender, EventArgs e)
        {
            PlikExePhoto = textBox63.Text;
            if (File.Exists(PlikExePhoto) == true)
            {
                verPhoto = Path.GetDirectoryName(PlikExePhoto);
                int nr = verPhoto.IndexOf(@"\");
                if (nr > -1)
                {
                    verPhoto = verPhoto.Remove(0, verPhoto.LastIndexOf(@"\") + 1);
                }
            }
            else
            {
                verPhoto = "";
            }
        }

        private void Button108_Click(object sender, EventArgs e)
        {
            if (openFileDialog10.ShowDialog() == DialogResult.OK)
            {
                textBox63.Text = openFileDialog10.FileName;
            }
        }

        private void TextBox62_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox62.Text = files[0];
                }
            }
        }

        private void TextBox62_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox65_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".exe" || roz == ".EXE")
                    {
                        textBox65.Text = files[0];
                    }
                    else
                    {
                        textBox65.Text = "";
                    }
                }
            }
        }

        private void TextBox65_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox64_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox64.Text = files[0];
                }
            }
        }

        private void TextBox64_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox63_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".exe" || roz == ".EXE")
                    {
                        textBox63.Text = files[0];
                    }
                    else
                    {
                        textBox63.Text = "";
                    }
                }
            }
        }

        private void TextBox63_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void WczytajUstawienia ()
        {
            if (File.Exists(PlikINDEO) == true)
            {
                string[] zawartosc = File.ReadAllLines(PlikINDEO);
                foreach (string iLinia in zawartosc)
                {
                    int nr = 0;
                    nr = iLinia.IndexOf("QGIS               -> ");
                    if (nr > -1)
                    {
                        textBox62.Text = iLinia.Remove(0, 22);
                    }
                    nr = iLinia.IndexOf("Global Mapper      -> ");
                    if (nr > -1)
                    {
                        textBox65.Text = iLinia.Remove(0, 22);
                    }
                    nr = iLinia.IndexOf("INPHO              -> ");
                    if (nr > -1)
                    {
                        textBox64.Text = iLinia.Remove(0, 22);
                    }
                    nr = iLinia.IndexOf("Program Graficzny  -> ");
                    if (nr > -1)
                    {
                        textBox63.Text = iLinia.Remove(0, 22);
                    }
                    nr = iLinia.IndexOf("Katalog z Sekcjami -> ");
                    if (nr > -1)
                    {
                        textBox94.Text = iLinia.Remove(0, 22);
                    }
                    nr = iLinia.IndexOf("Katalog z Ortami   -> ");
                    if (nr > -1)
                    {
                        textBox12.Text = iLinia.Remove(0, 22);
                    }
                    nr = iLinia.IndexOf("Wektor             -> ");
                    if (nr > -1)
                    {
                        textBox92.Text = iLinia.Remove(0, 22);
                    }
                    nr = iLinia.IndexOf("Projekt            -> ");
                    if (nr > -1)
                    {
                        textBox11.Text = iLinia.Remove(0, 22);
                    }
                    nr = iLinia.IndexOf("Katalog Wynikowy   -> ");
                    if (nr > -1)
                    {
                        textBox13.Text = iLinia.Remove(0, 22);
                    }
                    nr = iLinia.IndexOf("Kolor              -> ");
                    if (nr > -1)
                    {
                        string Kol = iLinia.Remove(0, 22);
                        if (Kol == "255")
                        {
                            radioButton24.Checked = true;
                        }
                    }
                    if (textBox94.Text != "" && textBox12.Text != "" && textBox13.Text != "")
                    {
                        pictureBox15.BackColor = Color.LightSteelBlue;
                    }
                    else
                    {
                        pictureBox15.BackColor = SystemColors.Control;
                    }
                    if (textBox94.Text != "" && textBox11.Text != "" && textBox13.Text != "")
                    {
                        pictureBox14.BackColor = Color.PaleGreen;
                    }
                    else
                    {
                        pictureBox14.BackColor = SystemColors.Control;
                    }
                    if (textBox94.Text != "" && textBox92.Text != "" && textBox13.Text != "")
                    {
                        pictureBox13.BackColor = Color.Khaki;
                    }
                    else
                    {
                        pictureBox13.BackColor = SystemColors.Control;
                    }
                    if (textBox64.Text != "")
                    {
                        label94.Text = "INPHO ver." + verINPHO;
                    }
                    else
                    {
                        label94.Text = "INPHO";
                    }
                    if (textBox63.Text != "")
                    {
                        label93.Text = verPhoto;
                    }
                    else
                    {
                        label93.Text = "Program Graficzny";
                    }
                    if (textBox65.Text != "")
                    {
                        label165.Text = verGM;
                        label9.Text = verGM;
                        label80.Text = verGM;
                        label99.Text = verGM;
                    }
                    else
                    {
                        label165.Text = "Global Mapper";
                        label9.Text = "Global Mapper";
                        label80.Text = "Global Mapper";
                        label99.Text = "Global Mapper";
                    }
                    if (textBox62.Text != "")
                    {
                        label164.Text = "QGIS ver." + verQGIS;
                        label40.Text = "QGIS ver." + verQGIS;
                        label125.Text = "QGIS ver." + verQGIS;
                    }
                    else
                    {
                        label164.Text = "QGIS";
                        label40.Text = "QGIS";
                        label25.Text = "QGIS";
                    }
                }
            }
        }

        private void Button106_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (isPanel2Open == true)
            {
                
                panel2.Height -= 50;
                if (panel2.Height == 0)
                {
                    timer2.Stop();
                    isPanel2Open = false;
                    kolor = "";
                    if (radioButton24.Checked == true)
                    {
                        kolor = "255";
                    }
                    else
                    {
                        kolor = "0";
                    }
                    if (textBox94.Text != "" && textBox12.Text != "" && textBox13.Text != "")
                    {
                        pictureBox15.BackColor = Color.LightSteelBlue;
                    }
                    else
                    {
                        pictureBox15.BackColor = SystemColors.Control;
                    }
                    if (textBox94.Text != "" && textBox11.Text != "" && textBox13.Text != "")
                    {
                        pictureBox14.BackColor = Color.PaleGreen;
                    }
                    else
                    {
                        pictureBox14.BackColor = SystemColors.Control;
                    }
                    if (textBox94.Text != "" && textBox92.Text != "" && textBox13.Text != "")
                    {
                        pictureBox13.BackColor = Color.Khaki;
                    }
                    else
                    {
                        pictureBox13.BackColor = SystemColors.Control;
                    }
                    if (textBox64.Text != "")
                    {
                        label94.Text = "INPHO ver." + verINPHO;
                    }
                    else
                    {
                        label94.Text = "INPHO";
                    }
                    if (textBox63.Text != "")
                    {
                        label93.Text = verPhoto;
                    }
                    else
                    {
                        label93.Text = "Program Graficzny";
                    }
                    if (textBox65.Text != "")
                    {
                        label165.Text = verGM;
                        label9.Text = verGM;
                        label80.Text = verGM;
                        label99.Text = verGM;
                    }
                    else
                    {
                        label165.Text = "Global Mapper";
                        label9.Text = "Global Mapper";
                        label80.Text = "Global Mapper";
                        label99.Text = "Global Mapper";
                    }
                    if (textBox62.Text != "")
                    {
                        label164.Text = "QGIS ver." + verQGIS;
                        label40.Text = "QGIS ver." + verQGIS;
                        label125.Text = "QGIS ver." + verQGIS;
                    }
                    else
                    {
                        label164.Text = "QGIS";
                        label40.Text = "QGIS";
                        label25.Text = "QGIS";
                    }
                    string[] zawartosc =
                    {
                        "QGIS               -> " + textBox62.Text,
                        "Global Mapper      -> " + textBox65.Text,
                        "INPHO              -> " + textBox64.Text,
                        "Program Graficzny  -> " + textBox63.Text,
                        "Katalog z Sekcjami -> " + textBox94.Text,
                        "Katalog z Ortami   -> " + textBox12.Text,
                        "Wektor             -> " + textBox92.Text,
                        "Projekt            -> " + textBox11.Text,
                        "Katalog Wynikowy   -> " + textBox13.Text,
                        "Kolor              -> " + kolor,
                    };
                    File.WriteAllLines(PlikINDEO, zawartosc);
                }
            }
            else if (isPanel2Open == false)
            {
                panel2.Height += 50;
                if (panel2.Height == 700)
                {
                    timer2.Stop();
                    isPanel2Open = true;
                }
            }
        }


        //***************POPRAWA ORT START****************************************************************

        //---------------USTAWIENIA-----------------------------------------------------------------------

        private void Label27_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string indPlik = files[0];
                    string roz = indPlik.Remove(0, indPlik.Length - 4);
                    if (roz == ".ind" || roz == ".IND")
                    {
                        string[] zawartosc = File.ReadAllLines(indPlik);
                        foreach (string iLinia in zawartosc)
                        {
                            int nr = 0;
                            nr = iLinia.IndexOf("Katalog z Sekcjami -> ");
                            if (nr > -1)
                            {
                                textBox94.Text = iLinia.Remove(0, 22);
                            }
                            nr = iLinia.IndexOf("Katalog z Ortami   -> ");
                            if (nr > -1)
                            {
                                textBox12.Text = iLinia.Remove(0, 22);
                            }
                            nr = iLinia.IndexOf("Wektor             -> ");
                            if (nr > -1)
                            {
                                textBox92.Text = iLinia.Remove(0, 22);
                            }
                            nr = iLinia.IndexOf("Projekt            -> ");
                            if (nr > -1)
                            {
                                textBox11.Text = iLinia.Remove(0, 22);
                            }
                            nr = iLinia.IndexOf("Katalog Wynikowy   -> ");
                            if (nr > -1)
                            {
                                textBox13.Text = iLinia.Remove(0, 22);
                            }
                            nr = iLinia.IndexOf("Kolor              -> ");
                            if (nr > -1)
                            {
                                string Kol = iLinia.Remove(0, 22);
                                if (Kol == "255")
                                {
                                    radioButton24.Checked = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        textBox94.Text = "";
                        textBox12.Text = "";
                        textBox92.Text = "";
                        textBox11.Text = "";
                        textBox13.Text = "";
                        radioButton25.Checked = true;
                    }
                }
            }
            if (textBox94.Text != "" && textBox12.Text != "" && textBox13.Text != "")
            {
                pictureBox15.BackColor = Color.LightSteelBlue;
            }
            else
            {
                pictureBox15.BackColor = SystemColors.Control;
            }
            if (textBox94.Text != "" && textBox11.Text != "" && textBox13.Text != "")
            {
                pictureBox14.BackColor = Color.PaleGreen;
            }
            else
            {
                pictureBox14.BackColor = SystemColors.Control;
            }
            if (textBox94.Text != "" && textBox92.Text != "" && textBox13.Text != "")
            {
                pictureBox13.BackColor = Color.Khaki;
            }
            else
            {
                pictureBox13.BackColor = SystemColors.Control;
            }
            if (textBox64.Text != "")
            {
                label94.Text = "INPHO ver." + verINPHO;
            }
            else
            {
                label94.Text = "INPHO";
            }
            if (textBox63.Text != "")
            {
                label93.Text = verPhoto;
            }
            else
            {
                label93.Text = "Program Graficzny";
            }
            if (textBox65.Text != "")
            {
                label165.Text = verGM;
                label9.Text = verGM;
                label80.Text = verGM;
                label99.Text = verGM;
            }
            else
            {
                label165.Text = "Global Mapper";
                label9.Text = "Global Mapper";
                label80.Text = "Global Mapper";
                label99.Text = "Global Mapper";
            }
            if (textBox62.Text != "")
            {
                label164.Text = "QGIS ver." + verQGIS;
                label40.Text = "QGIS ver." + verQGIS;
                label125.Text = "QGIS ver." + verQGIS;
            }
            else
            {
                label164.Text = "QGIS";
                label40.Text = "QGIS";
                label25.Text = "QGIS";
            }
        }

        private void Label27_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Label27_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                kolor = "";
                if (radioButton24.Checked == true)
                {
                    kolor = "255";
                }
                else
                {
                    kolor = "0";
                }
                string indPlik = saveFileDialog1.FileName;
                string[] zawartosc =
                {
                    "Katalog z Sekcjami -> " + textBox94.Text,
                    "Katalog z Ortami   -> " + textBox12.Text,
                    "Wektor             -> " + textBox92.Text,
                    "Projekt            -> " + textBox11.Text,
                    "Katalog Wynikowy   -> " + textBox13.Text,
                    "Kolor              -> " + kolor,
                };
                File.WriteAllLines(indPlik, zawartosc);
            }
        }

        private void Button46_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (isPanel1Open == true)
            {
                panel1.Height -= 40;
                if (panel1.Height == 0)
                {
                    timer1.Stop();
                    isPanel1Open = false;
                    if (textBox94.Text != "" && textBox12.Text != "" && textBox13.Text != "")
                    {
                        pictureBox15.BackColor = Color.LightSteelBlue;
                    }
                    else
                    {
                        pictureBox15.BackColor = SystemColors.Control;
                    }
                    if (textBox94.Text != "" && textBox11.Text != "" && textBox13.Text != "")
                    {
                        pictureBox14.BackColor = Color.PaleGreen;
                    }
                    else
                    {
                        pictureBox14.BackColor = SystemColors.Control;
                    }
                    if (textBox94.Text != "" && textBox92.Text != "" && textBox13.Text != "")
                    {
                        pictureBox13.BackColor = Color.Khaki;
                    }
                    else
                    {
                        pictureBox13.BackColor = SystemColors.Control;
                    }
                    if (textBox64.Text != "")
                    {
                        label94.Text = "INPHO ver." + verINPHO;
                    }
                    else
                    {
                        label94.Text = "INPHO";
                    }
                    if (textBox63.Text != "")
                    {
                        label93.Text = verPhoto;
                    }
                    else
                    {
                        label93.Text = "Program Graficzny";
                    }
                    if (textBox65.Text != "")
                    {
                        label165.Text = verGM;
                        label9.Text = verGM;
                        label80.Text = verGM;
                        label99.Text = verGM;
                    }
                    else
                    {
                        label165.Text = "Global Mapper";
                        label9.Text = "Global Mapper";
                        label80.Text = "Global Mapper";
                        label99.Text = "Global Mapper";
                    }
                    if (textBox62.Text != "")
                    {
                        label164.Text = "QGIS ver." + verQGIS;
                        label40.Text = "QGIS ver." + verQGIS;
                        label125.Text = "QGIS ver." + verQGIS;
                    }
                    else
                    {
                        label164.Text = "QGIS";
                        label40.Text = "QGIS";
                        label25.Text = "QGIS";
                    }
                    kolor = "";
                    if (radioButton24.Checked == true)
                    {
                        kolor = "255";
                    }
                    else
                    {
                        kolor = "0";
                    }
                    string[] zawartosc =
                    {
                        "QGIS               -> " + textBox62.Text,
                        "Global Mapper      -> " + textBox65.Text,
                        "INPHO              -> " + textBox64.Text,
                        "Program Graficzny  -> " + textBox63.Text,
                        "Katalog z Sekcjami -> " + textBox94.Text,
                        "Katalog z Ortami   -> " + textBox12.Text,
                        "Wektor             -> " + textBox92.Text,
                        "Projekt            -> " + textBox11.Text,
                        "Katalog Wynikowy   -> " + textBox13.Text,
                        "Kolor              -> " + kolor,
                    };
                    File.WriteAllLines(PlikINDEO, zawartosc);
                }
            }
            else if (isPanel1Open == false)
            {
                panel1.Height += 40;
                if (panel1.Height == 480)
                {
                    timer1.Stop();
                    isPanel1Open = true;
                }
            }
        }

        private void Button73_Click(object sender, EventArgs e)
        {
            if (textBox13.Text != "")
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Usunąć Pliki z Katalogu Wynikowego ?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (Directory.Exists(textBox13.Text) == true)
                    {
                        string[] Pliki = Directory.GetFiles(textBox13.Text);
                        foreach (string iPlik in Pliki)
                        {
                            File.Delete(iPlik);
                        }
                    }
                }
            }
        }

        //---------------TNIJ-----------------------------------------------------------------------------

        private void RadioButton24_CheckedChanged(object sender, EventArgs e)
        {
            kolorMode = " -dstnodata 255 ";
            kolor = "255";
        }

        private void TextBox94_TextChanged(object sender, EventArgs e)
        {
            sciezkaS = textBox94.Text;
        }

        private void Button86_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox94.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {
            sciezkaO = textBox12.Text;
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox12.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox13.Text;
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox13.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox84_TextChanged(object sender, EventArgs e)
        {
            tile = textBox84.Text;
        }

        private void TextBox85_TextChanged(object sender, EventArgs e)
        {
            plikTif = textBox85.Text;
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            sciezkaS = textBox94.Text;
            if (Directory.Exists(sciezkaS) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Sekcjami !");
            }
            else
            {
                sciezkaS += @"\";
                sciezkaS = Path.GetFullPath(sciezkaS);
                sciezkaO = textBox12.Text;
                if (Directory.Exists(sciezkaO) == false)
                {
                    MessageBox.Show("Błędna Ścieżka do Katalogu z Ortami !");
                }
                else
                {
                    sciezkaO += @"\";
                    sciezkaO = Path.GetFullPath(sciezkaO);
                    sciezkaW = textBox13.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                    }
                    else
                    {
                        sciezkaW += @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        if (sciezkaS == sciezkaO || sciezkaS == sciezkaW || sciezkaO == sciezkaW)
                        {
                            MessageBox.Show("Te same katalogi !");
                        }
                        else
                        {
                            if (radioButton25.Checked == true)
                            {
                                kolorMode = " -dstnodata 0 ";
                            }
                            else
                            {
                                kolorMode = " -dstnodata 255 ";
                            }
                            if (textBox84.Text == "")
                            {
                                MessageBox.Show("Wpisz numer sekcji !");
                            }
                            else
                            {
                                tile = textBox84.Text;
                                string[] Pliki = Directory.GetFiles(sciezkaS, "*" + tile + "*.tif");
                                SumaPLIK = 0;
                                foreach (string iPlik in Pliki)
                                {
                                    ++SumaPLIK;
                                    tile = iPlik;
                                }
                                if (SumaPLIK == 0)
                                {
                                    MessageBox.Show("Brak sekcji !");
                                }
                                else
                                {
                                    if (SumaPLIK > 1)
                                    {
                                        MessageBox.Show("Kilka sekcji z tą samą nazwą !");
                                    }
                                    else
                                    {
                                        plikTfw = tile.Remove(tile.Length - 4) + ".tfw";
                                        if (File.Exists(plikTfw) == false)
                                        {
                                            MessageBox.Show("Brak Pliku TFW dla sekcji !");
                                        }
                                        else
                                        {
                                            if (textBox85.Text == "")
                                            {
                                                MessageBox.Show("Wpisz numer orta !");
                                            }
                                            else
                                            {
                                                plikTif = textBox85.Text;
                                                Pliki = Directory.GetFiles(sciezkaO, "*" + plikTif + "*.tif");
                                                SumaPLIK = 0;
                                                foreach (string iPlik in Pliki)
                                                {
                                                    ++SumaPLIK;
                                                    plikTif = iPlik;
                                                }
                                                if (SumaPLIK == 0)
                                                {
                                                    MessageBox.Show("Brak orta !");
                                                }
                                                else
                                                {
                                                    if (SumaPLIK > 1)
                                                    {
                                                        MessageBox.Show("Kilka ort z tą samą nazwą !");
                                                    }
                                                    else
                                                    {
                                                        plikTfw = plikTif.Remove(plikTif.Length - 4) + ".tfw";
                                                        if (File.Exists(plikTfw) == false)
                                                        {
                                                            MessageBox.Show("Brak Pliku TFW dla orta !");
                                                        }
                                                        else
                                                        {
                                                            PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                                                            PlikExeGDWarp = @"C:\Program Files\INDEOv2\bin\gdalwarp.exe";
                                                            PlikExePhoto = textBox63.Text;
                                                            if (File.Exists(PlikExeEXIF) == false || File.Exists(PlikExeGDWarp) == false || File.Exists(PlikExePhoto) == false)
                                                            {
                                                                MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin lub wskaż ścieżkę do Programu Graficznego !" + Environment.NewLine + @"Pomarańczowa Gwiazdka w Lewym Górnym Rogu !");
                                                            }
                                                            else
                                                            {
                                                                PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                                                                System.Environment.SetEnvironmentVariable("GDAL_DATA", @"C:\Program Files\INDEOv2\bin");
                                                                PlikExeGDWarp = @"""C:\Program Files\INDEOv2\bin\gdalwarp.exe""";
                                                                PlikExePhoto = @"""" + PlikExePhoto + @"""";
                                                                TnijTif();
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void TnijTif()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button46.Enabled = false;
            button11.Enabled = false;
            button19.Enabled = false;
            button73.Enabled = false;
            button87.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_TnijTif_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_TnijTif_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_TnijTif_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button46.Enabled = true;
            button11.Enabled = true;
            button19.Enabled = true;
            button73.Enabled = true;
            button87.Enabled = true;
        }

        private void BackgroundWorker_TnijTif_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            ++progressBarVal;
            BackgroundWorker1.ReportProgress(progressBarVal);
            Process MyProcess1 = new Process();
            MyProcess1.StartInfo.FileName = PlikExeEXIF;
            MyProcess1.StartInfo.Arguments = @" -a -u -g1 -w txt """ + tile + @"""";
            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess1.Start();
            MyProcess1.WaitForExit();
            string txtPlik = tile.Remove(tile.Length - 3) + "txt";
            decimal iSzerokosc = 0;
            decimal iWysokosc = 0;
            string[] zawartoscTxt = File.ReadAllLines(txtPlik);
            foreach (string iLinia in zawartoscTxt)
            {
                int nr = 0;
                nr = iLinia.IndexOf("Image Size                      :");
                if (nr > -1)
                {
                    string size = iLinia.Remove(0, 34);
                    nr = size.IndexOf("x");
                    string szerokosc = size.Remove(nr);
                    string wysokosc = size.Remove(0, nr + 1);
                    iSzerokosc = Convert.ToDecimal(szerokosc);
                    iWysokosc = Convert.ToDecimal(wysokosc);
                }
            }
            string tfwPlik = tile.Remove(tile.Length - 3) + "tfw";
            string[] zawartoscTfw = File.ReadAllLines(tfwPlik);
            foreach (string iLinia in zawartoscTfw)
            {
            }
            decimal pixel = Convert.ToDecimal(zawartoscTfw[0]);
            iSzerokosc *= pixel;
            iWysokosc *= pixel;
            pixel /= 2;
            decimal X = Convert.ToDecimal(zawartoscTfw[4]) - pixel;
            decimal Y = Convert.ToDecimal(zawartoscTfw[5]) + pixel;
            string TE = Convert.ToString(X) + " " + Convert.ToString(Y - iWysokosc) + " " + Convert.ToString(X + iSzerokosc) + " " + Convert.ToString(Y);
            File.Delete(txtPlik);
            nPlik = Path.GetFileName(plikTif);
            tile = Path.GetFileName(tile);
            tile = tile.Remove(tile.Length - 4);
            Process MyProcess2 = new Process();
            MyProcess2.StartInfo.FileName = PlikExeGDWarp;
            MyProcess2.StartInfo.Arguments = kolorMode + @"-multi -overwrite -te " + TE + @" """ + plikTif + @""" """ + sciezkaW + tile + "_" + nPlik + @"""";
            MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess2.Start();
            MyProcess2.WaitForExit();
            Process MyProcess3 = new Process();
            MyProcess3.StartInfo.FileName = PlikExePhoto;
            MyProcess3.StartInfo.Arguments = sciezkaW + tile + "_" + nPlik;
            MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess3.Start();
        }

        private void TextBox94_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox94.Text = files[0];
                }
            }
        }

        private void TextBox94_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox12_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox12.Text = files[0];
                }
            }
        }

        private void TextBox12_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox13_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox13.Text = files[0];
                }
            }
        }

        private void TextBox13_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------RASTERYZUJ-----------------------------------------------------------------------

        private void TextBox92_TextChanged(object sender, EventArgs e)
        {
            plikDxf = textBox92.Text;
        }

        private void Button84_Click(object sender, EventArgs e)
        {
            if (openFileDialog6.ShowDialog() == DialogResult.OK)
            {
                textBox92.Text = openFileDialog6.FileName;
            }
        }

        private void Button87_Click(object sender, EventArgs e)
        {
            plikDxf = textBox92.Text;
            if (File.Exists(plikDxf) == false)
            {
                MessageBox.Show("Brak Pliku DXF !");
            }
            else
            {
                sciezkaS = textBox94.Text;
                if (Directory.Exists(sciezkaS) == false)
                {
                    MessageBox.Show("Błędna Ścieżka do Katalogu z Sekcjami !");
                }
                else
                {
                    sciezkaS += @"\";
                    sciezkaS = Path.GetFullPath(sciezkaS);
                    sciezkaW = textBox13.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                    }
                    else
                    {
                        sciezkaW += @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        if (sciezkaS == sciezkaW)
                        {
                            MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Sekcjami !");
                        }
                        else
                        {
                            if (textBox84.Text == "")
                            {
                                MessageBox.Show("Wpisz numer sekcji !");
                            }
                            else
                            {
                                tile = textBox84.Text;
                                plikTif = sciezkaS + tile + ".tif";
                                if (File.Exists(plikTif) == false)
                                {
                                    MessageBox.Show("Brak Sekcji !");
                                }
                                else
                                {
                                    SumaPLIK = 1;
                                    plikTfw = plikTif.Remove(plikTif.Length - 4) + ".tfw";
                                    if (File.Exists(plikTfw) == false)
                                    {
                                        MessageBox.Show("Brak Pliku TFW !");
                                    }
                                    else
                                    {
                                        PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                                        PlikExeGDRaster = @"C:\Program Files\INDEOv2\bin\gdal_rasterize.exe";
                                        PlikExePhoto = textBox63.Text;
                                        if (File.Exists(PlikExeEXIF) == false || File.Exists(PlikExeGDRaster) == false || File.Exists(PlikExePhoto) == false)
                                        {
                                            MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin lub wskaż ścieżkę do Programu Graficznego !" + Environment.NewLine + @"Pomarańczowa Gwiazdka w Lewym Górnym Rogu !");
                                        }
                                        else
                                        {
                                            PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                                            System.Environment.SetEnvironmentVariable("GDAL_DATA", @"C:\Program Files\INDEOv2\bin");
                                            PlikExeGDRaster = @"""C:\Program Files\INDEOv2\bin\gdal_rasterize.exe""";
                                            PlikExePhoto = @"""" + PlikExePhoto + @"""";
                                            W2R();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void W2R()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button46.Enabled = false;
            button11.Enabled = false;
            button19.Enabled = false;
            button73.Enabled = false;
            button87.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_W2R_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_W2R_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_W2R_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button46.Enabled = true;
            button11.Enabled = true;
            button19.Enabled = true;
            button73.Enabled = true;
            button87.Enabled = true;
        }

        private void BackgroundWorker_W2R_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            ++progressBarVal;
            BackgroundWorker1.ReportProgress(progressBarVal);
            nPlik = Path.GetFileName(plikTif);
            Process MyProcess1 = new Process();
            MyProcess1.StartInfo.FileName = PlikExeEXIF;
            MyProcess1.StartInfo.Arguments = @" -a -u -g1 -w txt """ + plikTif + @"""";
            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess1.Start();
            MyProcess1.WaitForExit();
            string txtPlik = plikTif.Remove(plikTif.Length - 3) + "txt";
            decimal iSzerokosc = 0;
            decimal iWysokosc = 0;
            string[] zawartoscTxt = File.ReadAllLines(txtPlik);
            foreach (string iLinia in zawartoscTxt)
            {
                int nr = 0;
                nr = iLinia.IndexOf("Image Size                      :");
                if (nr > -1)
                {
                    string size = iLinia.Remove(0, 34);
                    nr = size.IndexOf("x");
                    string szerokosc = size.Remove(nr);
                    string wysokosc = size.Remove(0, nr + 1);
                    iSzerokosc = Convert.ToDecimal(szerokosc);
                    iWysokosc = Convert.ToDecimal(wysokosc);
                }
            }
            string tfwPlik = plikTif.Remove(plikTif.Length - 3) + "tfw";
            string[] zawartoscTfw = File.ReadAllLines(tfwPlik);
            foreach (string iLinia in zawartoscTfw)
            {
            }
            decimal pixel = Convert.ToDecimal(zawartoscTfw[0]);
            iSzerokosc *= pixel;
            iWysokosc *= pixel;
            string TR = Convert.ToString(pixel) + " " + Convert.ToString(pixel);
            pixel /= 2;
            decimal X = Convert.ToDecimal(zawartoscTfw[4]) - pixel;
            decimal Y = Convert.ToDecimal(zawartoscTfw[5]) + pixel;
            string TE = Convert.ToString(X) + " " + Convert.ToString(Y - iWysokosc) + " " + Convert.ToString(X + iSzerokosc) + " " + Convert.ToString(Y);
            File.Delete(txtPlik);
            string iPlik = sciezkaW + nPlik.Remove(nPlik.Length - 4) + "_Wektor.tif";
            Process MyProcess2 = new Process();
            MyProcess2.StartInfo.FileName = PlikExeGDRaster;
            MyProcess2.StartInfo.Arguments = @" -l entities -burn 0 -a_nodata 255 -tr " + TR + @" -te " + TE + @" -ot Byte -of GTiff """ + plikDxf + @""" """ + iPlik + @"""";
            MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess2.Start();
            MyProcess2.WaitForExit();
            Process MyProcess3 = new Process();
            MyProcess3.StartInfo.FileName = PlikExePhoto;
            MyProcess3.StartInfo.Arguments = iPlik;
            MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess3.Start();
        }

        private void TextBox92_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".dxf" || roz == ".DXF")
                    {
                        textBox92.Text = files[0];
                    }
                    else
                    {
                        textBox92.Text = "";
                    }
                }
            }
        }

        private void TextBox92_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------ORTO-----------------------------------------------------------------------------

        private void TextBox11_TextChanged(object sender, EventArgs e)
        {
            plikINPHOPrj = textBox11.Text;
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox11.Text = openFileDialog3.FileName;
            }
        }

        private void TextBox45_TextChanged(object sender, EventArgs e)
        {
            Wysokosc = textBox45.Text;
        }

        private void TextBox45_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && (((sender as TextBox).Text.IndexOf('.') > -1) || ((sender as TextBox).Text.Length == 0)))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '-') && ((sender as TextBox).Text.Length != 0))
            {
                e.Handled = true;
            }
        }

        private void RadioButton7_CheckedChanged(object sender, EventArgs e)
        {
            textBox45.Enabled = true;
            textBox41.Enabled = false;
         //   textBox41.Text = "";
            button47.Enabled = false;
            textBox44.Enabled = false;
            textBox44.Text = "";
            button72.Enabled = false;
            textBox43.Enabled = false;
            textBox43.Text = "";
            button57.Enabled = false;
            textBox42.Enabled = false;
            textBox42.Text = "";
            button48.Enabled = false;
        }

        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            textBox45.Enabled = false;
            textBox45.Text = "";
            textBox41.Enabled = true;
            button47.Enabled = true;
            textBox44.Enabled = false;
            textBox44.Text = "";
            button72.Enabled = false;
            textBox43.Enabled = false;
            textBox43.Text = "";
            button57.Enabled = false;
            textBox42.Enabled = false;
            textBox42.Text = "";
            button48.Enabled = false;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox45.Enabled = false;
            textBox45.Text = "";
            textBox41.Enabled = false;
         //   textBox41.Text = "";
            button47.Enabled = false;
            textBox44.Enabled = true;
            button72.Enabled = true;
            textBox43.Enabled = true;
            button57.Enabled = true;
            textBox42.Enabled = true;
            button48.Enabled = true;
        }

        private void TextBox41_TextChanged(object sender, EventArgs e)
        {
            plikDTM = textBox41.Text;
        }

        private void Button47_Click(object sender, EventArgs e)
        {
            if (openFileDialog8.ShowDialog() == DialogResult.OK)
            {
                textBox41.Text = openFileDialog8.FileName;
            }
        }

        private void TextBox44_TextChanged(object sender, EventArgs e)
        {
            plikBreak = textBox44.Text;
        }

        private void Button72_Click(object sender, EventArgs e)
        {
            if (openFileDialog6.ShowDialog() == DialogResult.OK)
            {
                textBox44.Text = openFileDialog6.FileName;
            }
        }

        private void TextBox43_TextChanged(object sender, EventArgs e)
        {
            plikObiekt = textBox43.Text;
        }

        private void Button57_Click(object sender, EventArgs e)
        {
            if (openFileDialog6.ShowDialog() == DialogResult.OK)
            {
                textBox43.Text = openFileDialog6.FileName;
            }
        }

        private void TextBox42_TextChanged(object sender, EventArgs e)
        {
            plikPunkty = textBox42.Text;
        }

        private void Button48_Click(object sender, EventArgs e)
        {
            if (openFileDialog9.ShowDialog() == DialogResult.OK)
            {
                textBox42.Text = openFileDialog9.FileName;
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            plikINPHOPrj = textBox11.Text;
            if (File.Exists(plikINPHOPrj) == false)
            {
                MessageBox.Show("Brak Pliku Projektowego INPHO !");
            }
            else
            {
                sciezkaS = textBox94.Text;
                if (Directory.Exists(sciezkaS) == false)
                {
                    MessageBox.Show("Błędna Ścieżka do Katalogu z Sekcjami !");
                }
                else
                {
                    sciezkaS += @"\";
                    sciezkaS = Path.GetFullPath(sciezkaS);
                    sciezkaW = textBox13.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                    }
                    else
                    {
                        sciezkaW += @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        if (sciezkaS == sciezkaW)
                        {
                            MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Sekcjami !");
                        }
                        else
                        {
                            if (textBox84.Text == "")
                            {
                                MessageBox.Show("Wpisz numer sekcji !");
                            }
                            else
                            {
                                tile = textBox84.Text;
                                plikS = sciezkaS + tile + ".tif";
                                if (File.Exists(plikS) == false)
                                {
                                    MessageBox.Show("Brak Sekcji !");
                                }
                                else
                                {
                                    SumaPLIK = 1;
                                    plikTfw = plikS.Remove(plikS.Length - 4) + ".tfw";
                                    if (File.Exists(plikTfw) == false)
                                    {
                                        MessageBox.Show("Brak Pliku TFW !");
                                    }
                                    else
                                    {
                                        plikTif = textBox85.Text;
                                        string sciezkaOrto = "";
                                        string[] zawartosc = File.ReadAllLines(plikINPHOPrj);
                                        foreach (string iLinia in zawartosc)
                                        {
                                            int nr = 0;
                                            nr = iLinia.IndexOf("  $PHOTO_FILE : ");
                                            if (nr > -1)
                                            {
                                                nr = iLinia.IndexOf(plikTif);
                                                if (nr > -1)
                                                {
                                                    sciezkaOrto = iLinia.Remove(0, 16);
                                                }
                                            }
                                        }
                                        if (sciezkaOrto == "")
                                        {
                                            MessageBox.Show("Brak zdefiniowanego zdjęcia: " + plikTif + " w pliku projektowym INPHO !");
                                        }
                                        else
                                        {
                                            if (File.Exists(sciezkaOrto) == false)
                                            {
                                                MessageBox.Show("Brak zdjęcia ! " + sciezkaOrto);
                                            }
                                            else
                                            {
                                                if (radioButton25.Checked == true)
                                                {
                                                    kolor = "0";
                                                }
                                                else
                                                {
                                                    kolor = "255";
                                                }
                                                PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                                                PlikExePhoto = textBox63.Text;
                                                katalogINPHO = textBox64.Text;
                                                PlikExeOrthoMaster = katalogINPHO + @"\bin\orthomaster.exe";
                                                PlikExeDtmTool = katalogINPHO + @"\bin\dtmtoolkit.exe";
                                                PlikExeGDWarp = @"C:\Program Files\INDEOv2\bin\gdalwarp.exe";
                                                if (File.Exists(PlikExeEXIF) == false || File.Exists(PlikExeOrthoMaster) == false || File.Exists(PlikExeDtmTool) == false || File.Exists(PlikExeGDWarp) == false || File.Exists(PlikExePhoto) == false)
                                                {
                                                    MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin lub wskaż ścieżkę do Programu Graficznego/INPHO !" + Environment.NewLine + @"Pomarańczowa Gwiazdka w Lewym Górnym Rogu !");
                                                }
                                                else
                                                {
                                                    PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                                                    PlikExeOrthoMaster = @"""" + PlikExeOrthoMaster + @"""";
                                                    PlikExeDtmTool = @"""" + PlikExeDtmTool + @"""";
                                                    System.Environment.SetEnvironmentVariable("GDAL_DATA", @"C:\Program Files\INDEOv2\bin");
                                                    PlikExeGDWarp = @"""C:\Program Files\INDEOv2\bin\gdalwarp.exe""";
                                                    PlikExePhoto = @"""" + PlikExePhoto + @"""";
                                                    GenDTM();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GenDTM()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            TrueOrto = "0";
            TrueOrtoA = "1";
            if (radioButton7.Checked == true)
            {
                if (textBox45.Text == "")
                {
                    MessageBox.Show("Wpisz wysokość terenu !");
                }
                else
                {
                    Wysokosc = textBox45.Text;
                    bool wynik = decimal.TryParse(Wysokosc, out _);
                    if (wynik == true)
                    {
                        Deh = "0";
                        GenOrto();
                    }
                    else
                    {
                        MessageBox.Show("Błędna wysokość terenu !");
                    }
                }
            }
            else if (radioButton6.Checked == true)
            {
                if (textBox41.Text == "")
                {
                    MessageBox.Show("Wskaż Plik DTM !");
                }
                else
                {
                    plikDTM = textBox41.Text;
                    if (File.Exists(plikDTM) == false)
                    {
                        MessageBox.Show("Brak Pliku DTM !");
                    }
                    else
                    {
                        if (radioButton5.Checked == true)
                        {
                            TrueOrto = "1";
                            TrueOrtoA = "2";
                        }
                        Deh = "1";
                        Wysokosc = "0";
                        GenOrto();
                    }
                }
            }
            else if (radioButton3.Checked == true)
            {
                if (radioButton5.Checked == true)
                {
                    TrueOrto = "1";
                    TrueOrtoA = "2";
                }
                Deh = "1";
                Wysokosc = "0";
                if (textBox44.Text == "" && textBox43.Text == "" && textBox42.Text == "")
                {
                    MessageBox.Show("Podaj pliki z terenem !");
                }
                else if (textBox44.Text == "" && textBox42.Text == "")
                {
                    MessageBox.Show("Podaj pliki z terenem na który będą rzutowane obiekty !");
                }
                else if (textBox44.Text != "" && textBox43.Text == "" && textBox42.Text == "")
                {
                    plikBreak = textBox44.Text;
                    if (File.Exists(plikBreak) == false)
                    {
                        MessageBox.Show("Brak Pliku Breaklines !");
                    }
                    else
                    {
                        string[] dtmTOOLopts =
                        {
                            "*** DTMToolkit Options File ***",
                            "",
                            "-mode vectordata",
                            "-steps surface",
                            @"-input """ + plikBreak + @"""",
                            "-file-wise false",
                            @"-outdir """ + sciezkaW + @"""",
                            @"-outprefix ""DTM""",
                            @"-layertype """ + plikBreak + @""" 50",
                        };
                        plikDTMopts = sciezkaW + @"\DTMopts.txt";
                        if (File.Exists(plikDTMopts) == true)
                        {
                            File.Delete(plikDTMopts);
                        }
                        File.WriteAllLines(plikDTMopts, dtmTOOLopts);
                        GenOrto();
                    }
                }
                else if (textBox44.Text == "" && textBox43.Text == "" && textBox42.Text != "")
                {
                    plikPunkty = textBox42.Text;
                    if (File.Exists(plikPunkty) == false)
                    {
                        MessageBox.Show("Brak Pliku z Punktami !");
                    }
                    else
                    {
                        string[] dtmTOOLopts =
                        {
                            "*** DTMToolkit Options File ***",
                            "",
                            "-mode vectordata",
                            "-steps surface",
                            @"-input """ + plikPunkty + @"""",
                            "-file-wise false",
                            @"-outdir """ + sciezkaW + @"""",
                            @"-outprefix ""DTM""",
                            @"-layertype """ + plikPunkty + @""" 30",
                        };
                        plikDTMopts = sciezkaW + @"\DTMopts.txt";
                        if (File.Exists(plikDTMopts) == true)
                        {
                            File.Delete(plikDTMopts);
                        }
                        File.WriteAllLines(plikDTMopts, dtmTOOLopts);
                        GenOrto();
                    }
                }
                else if (textBox44.Text != "" && textBox43.Text != "" && textBox42.Text == "")
                {
                    plikBreak = textBox44.Text;
                    plikObiekt = textBox43.Text;
                    if (File.Exists(plikBreak) == false || File.Exists(plikObiekt) == false)
                    {
                        MessageBox.Show("Brak Pliku Breaklines lub z Obiektami!");
                    }
                    else
                    {
                        string[] dtmTOOLopts =
                        {
                            "*** DTMToolkit Options File ***",
                            "",
                            "-mode vectordata",
                            "-steps surface",
                            @"-input """ + plikBreak + @"""",
                            @"       """ + plikObiekt + @"""",
                            "-file-wise false",
                            @"-outdir """ + sciezkaW + @"""",
                            @"-outprefix ""DTM""",
                            @"-layertype """ + plikBreak + @""" 50",
                            @"-layertype """ + plikObiekt + @""" 87",
                        };
                        plikDTMopts = sciezkaW + @"\DTMopts.txt";
                        if (File.Exists(plikDTMopts) == true)
                        {
                            File.Delete(plikDTMopts);
                        }
                        File.WriteAllLines(plikDTMopts, dtmTOOLopts);
                        GenOrto();
                    }
                }
                else if (textBox44.Text == "" && textBox43.Text != "" && textBox42.Text != "")
                {
                    plikObiekt = textBox43.Text;
                    plikPunkty = textBox42.Text;
                    if (File.Exists(plikObiekt) == false || File.Exists(plikPunkty) == false)
                    {
                        MessageBox.Show("Brak Pliku z Obiektami lub z Punktami !");
                    }
                    else
                    {
                        string[] dtmTOOLopts =
                        {
                            "*** DTMToolkit Options File ***",
                            "",
                            "-mode vectordata",
                            "-steps surface",
                            @"-input """ + plikPunkty + @"""",
                            @"       """ + plikObiekt + @"""",
                            "-file-wise false",
                            @"-outdir """ + sciezkaW + @"""",
                            @"-outprefix ""DTM""",
                            @"-layertype """ + plikPunkty + @""" 30",
                            @"-layertype """ + plikObiekt + @""" 87",
                        };
                        plikDTMopts = sciezkaW + @"\DTMopts.txt";
                        if (File.Exists(plikDTMopts) == true)
                        {
                            File.Delete(plikDTMopts);
                        }
                        File.WriteAllLines(plikDTMopts, dtmTOOLopts);
                        GenOrto();
                    }
                }
                else if (textBox44.Text != "" && textBox43.Text == "" && textBox42.Text != "")
                {
                    plikBreak = textBox44.Text;
                    plikPunkty = textBox42.Text;
                    if (File.Exists(plikBreak) == false || File.Exists(plikPunkty) == false)
                    {
                        MessageBox.Show("Brak Pliku Breaklines lub z Punktami !");
                    }
                    else
                    {
                        string[] dtmTOOLopts =
                        {
                            "*** DTMToolkit Options File ***",
                            "",
                            "-mode vectordata",
                            "-steps surface",
                            @"-input """ + plikPunkty + @"""",
                            @"       """ + plikBreak + @"""",
                            "-file-wise false",
                            @"-outdir """ + sciezkaW + @"""",
                            @"-outprefix ""DTM""",
                            @"-layertype """ + plikPunkty + @""" 30",
                            @"-layertype """ + plikBreak + @""" 50",
                        };
                        plikDTMopts = sciezkaW + @"\DTMopts.txt";
                        if (File.Exists(plikDTMopts) == true)
                        {
                            File.Delete(plikDTMopts);
                        }
                        File.WriteAllLines(plikDTMopts, dtmTOOLopts);
                        GenOrto();
                    }
                }
                else if (textBox44.Text != "" && textBox43.Text != "" && textBox42.Text != "")
                {
                    plikBreak = textBox44.Text;
                    plikObiekt = textBox43.Text;
                    plikPunkty = textBox42.Text;
                    if (File.Exists(plikBreak) == false || File.Exists(plikObiekt) == false || File.Exists(plikPunkty) == false)
                    {
                        MessageBox.Show("Brak Pliku Breaklines lub z Obiektami lub z Punktami !");
                    }
                    else
                    {
                        string[] dtmTOOLopts =
                        {
                            "*** DTMToolkit Options File ***",
                            "",
                            "-mode vectordata",
                            "-steps surface",
                            @"-input """ + plikPunkty + @"""",
                            @"       """ + plikBreak + @"""",
                            @"       """ + plikObiekt + @"""",
                            "-file-wise false",
                            @"-outdir """ + sciezkaW + @"""",
                            @"-outprefix ""DTM""",
                            @"-layertype """ + plikPunkty + @""" 30",
                            @"-layertype """ + plikBreak + @""" 50",
                            @"-layertype """ + plikObiekt + @""" 87",
                        };
                        plikDTMopts = sciezkaW + @"\DTMopts.txt";
                        if (File.Exists(plikDTMopts) == true)
                        {
                            File.Delete(plikDTMopts);
                        }
                        File.WriteAllLines(plikDTMopts, dtmTOOLopts);
                        GenOrto();
                    }
                }
            }
        }

        private void GenOrto()
        {
            progressBar1.Enabled = true;
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button46.Enabled = false;
            button11.Enabled = false;
            button19.Enabled = false;
            button73.Enabled = false;
            button87.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_GenOrto_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_GenOrto_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_GenOrto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button46.Enabled = true;
            button11.Enabled = true;
            button19.Enabled = true;
            button73.Enabled = true;
            button87.Enabled = true;
        }

        private void BackgroundWorker_GenOrto_DoWork(object sender, DoWorkEventArgs e)
        {
            Time = Convert.ToString(DateTime.Now);
            int no = Time.IndexOf(" ");
            Time = Time.Remove(0, no);
            Time = Time.Replace(":", "_");
            Time = Time.Replace(" ", "_");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            ++progressBarVal;
            BackgroundWorker1.ReportProgress(progressBarVal);
            nPlik = plikTif + ".tif";
            Process MyProcess1 = new Process();
            MyProcess1.StartInfo.FileName = PlikExeEXIF;
            MyProcess1.StartInfo.Arguments = @" -a -u -g1 -w txt """ + plikS + @"""";
            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess1.Start();
            MyProcess1.WaitForExit();
            string txtPlik = plikS.Remove(plikS.Length - 4) + ".txt";
            decimal iSzerokosc = 0;
            decimal iWysokosc = 0;
            string[] zawartoscTxt = File.ReadAllLines(txtPlik);
            foreach (string iLinia in zawartoscTxt)
            {
                int nr = 0;
                nr = iLinia.IndexOf("Image Size                      :");
                if (nr > -1)
                {
                    string size = iLinia.Remove(0, 34);
                    nr = size.IndexOf("x");
                    string szerokosc = size.Remove(nr);
                    string wysokosc = size.Remove(0, nr + 1);
                    iSzerokosc = Convert.ToDecimal(szerokosc);
                    iWysokosc = Convert.ToDecimal(wysokosc);
                }
            }
            string tfwPlik = plikS.Remove(plikS.Length - 4) + ".tfw";
            string[] zawartoscTfw = File.ReadAllLines(tfwPlik);
            decimal pixel = Convert.ToDecimal(zawartoscTfw[0]);
            WielkoscPix = Convert.ToString(pixel);
            iSzerokosc *= pixel;
            iWysokosc *= pixel;
            pixel /= 2;
            decimal X = Convert.ToDecimal(zawartoscTfw[4]) - pixel;
            decimal Y = Convert.ToDecimal(zawartoscTfw[5]) + pixel;
            string Xmin = Convert.ToString(X);
            string Ymin = Convert.ToString(Y - iWysokosc);
            string Xmax = Convert.ToString(X + iSzerokosc);
            string Ymax = Convert.ToString(Y);
            File.Delete(txtPlik);
            if (radioButton3.Checked == true)
            {
                plikDTM = sciezkaW + @"DTM\DTM_surfacemodel.dtm";
                if (File.Exists(plikDTM) == true)
                {
                    File.Delete(plikDTM);
                }
                X = X - (40 * pixel);
                Y = (Y - iWysokosc) - (40 * pixel);
                string Xdtm = Convert.ToString(X);
                string Ydtm = Convert.ToString(Y);
                iSzerokosc = iSzerokosc + (80 * pixel);
                iWysokosc = iWysokosc + (80 * pixel);
                string Xex = Convert.ToString(iSzerokosc);
                string Yex = Convert.ToString(iWysokosc);
                pixel = 20 * pixel;
                string Grid = Convert.ToString(pixel);
                string[] dtmTOOLoptsK =
                {
                    @"-unit ""m""",
                    "-rasterorigin " + Xdtm + " " + Ydtm,
                    "-rasterextent " + Xex + " " + Yex,
                    "-surfgridwidth " + Grid,
                };
                File.AppendAllLines(plikDTMopts, dtmTOOLoptsK);
                Process MyProcess2 = new Process();
                MyProcess2.StartInfo.FileName = PlikExeDtmTool;
                MyProcess2.StartInfo.Arguments = @" -optsfile """ + plikDTMopts + @"""";
                MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess2.Start();
                MyProcess2.WaitForExit();
                plikDTM = sciezkaW + @"DTM\DTM_surfacemodel.dtm";
            }
            if (radioButton3.Checked == true && File.Exists(plikDTM) == false)
            {
                MessageBox.Show("Nie wygenerował się DTM spróbuj użyć DTMToolkit'a");
            }
            else
            {
                string plikPrjW = Path.GetFileName(plikINPHOPrj);
                plikPrjW = sciezkaW + plikPrjW;
                if (File.Exists(plikPrjW) == true)
                {
                    File.Delete(plikPrjW);
                }
                if (Deh == "0")
                {
                    StreamReader sr = new StreamReader(plikINPHOPrj);
                    StreamWriter sw = new StreamWriter(plikPrjW);
                    string iLinia;
                    string PhotoName = null;
                    int ni = 0;
                    int np = 0;
                    while ((iLinia = sr.ReadLine()) != null)
                    {
                        ni = iLinia.IndexOf("$PHOTO_FILE :");
                        if (ni > -1)
                        {
                            ni = iLinia.IndexOf(plikTif);
                            if (ni > -1)
                            {
                                sw.WriteLine(iLinia);
                                Image = PhotoName.Remove(0, 15);
                            }
                            else
                            {
                                sw.WriteLine("$PHOTO_FILE :");
                            }
                        }
                        else
                        {
                            ni = iLinia.IndexOf("$DTM");
                            if (ni > -1)
                            {
                                np = 1;
                            }
                            else
                            {
                                if (np == 1)
                                {
                                    ni = iLinia.IndexOf("$END");
                                    if (ni > -1)
                                    {
                                        np = 0;
                                    }
                                }
                                else
                                {
                                    sw.WriteLine(iLinia);
                                }
                            }
                        }
                        PhotoName = iLinia;
                    }
                    sw.Close();
                    sr.Close();
                }
                else
                {
                    StreamReader sr = new StreamReader(plikINPHOPrj);
                    StreamWriter sw = new StreamWriter(plikPrjW);
                    string iLinia;
                    string PhotoName = null;
                    int ni = 0;
                    int np = 0;
                    while ((iLinia = sr.ReadLine()) != null)
                    {
                        ni = iLinia.IndexOf("$PHOTO_FILE :");
                        if (ni > -1)
                        {
                            ni = iLinia.IndexOf(plikTif);
                            if (ni > -1)
                            {
                                sw.WriteLine(iLinia);
                                Image = PhotoName.Remove(0, 15);
                            }
                            else
                            {
                                sw.WriteLine("$PHOTO_FILE :");
                            }
                        }
                        else
                        {
                            ni = iLinia.IndexOf("$DTM");
                            if (ni > -1)
                            {
                                np = 1;
                            }
                            else
                            {
                                if (np == 1)
                                {
                                    ni = iLinia.IndexOf("$END");
                                    if (ni > -1)
                                    {
                                        np = 0;
                                    }
                                }
                                else
                                {
                                    sw.WriteLine(iLinia);
                                }
                            }
                        }
                        PhotoName = iLinia;
                    }
                    sw.Close();
                    sr.Close();
                    string DTM = Path.GetFileName(plikDTM);
                    DTM = DTM.Remove(DTM.Length - 4);
                    string[] prjDTM =
                    {
                        "$DTM",
                        "  $DTM_ID : " + DTM,
                        "  $DTM_FILE: " + plikDTM,
                        "  $FORMAT: scop:",
                        "  $XY_SCALE: 1.0000000000",
                        "  $Z_SCALE: 1.0000000000",
                        "  $STATUS: original",
                        "$END"
                    };
                    File.AppendAllLines(plikPrjW, prjDTM);
                }
                string[] xmlZawartosc =
                {
                    "<?xml version='1.0'  encoding='UTF-8' ?>",
                    "<!DOCTYPE inpho-project-xml>",
                    "<inpho-product>",
                    " <projectproperties>",
                    @"  <arialproperty id=""" + plikTif + @""">",
                    "   <visible>true</visible>",
                    "  </arialproperty>",
                    " </projectproperties>",
                    " <xpcl-apps>",
                    "  <OrthoImageRelations>",
                    "  </OrthoImageRelations>",
                    "  <OrthoAreas>",
                    @"   <geomIndex value=""1""/>",
                    @"   <Element name=""" + Image + @""" type=""48"" imptype=""3"">",
                    @"    <Geometry type=""11"">",
                    @"     <GeometryList n=""4"">",
                    "      <item_0>",
                    @"       <Geometry type=""2001"">",
                    "        <Parameters>",
                    @"         <n value=""3""/>",
                    @"         <par0 value=""" + Xmin + @"""/>",
                    @"         <par1 value=""" + Ymin + @"""/>",
                    @"         <par2 value=""""/>",
                    "        </Parameters>",
                    "       </Geometry>",
                    "      </item_0>",
                    "      <item_1>",
                    @"       <Geometry type=""2001"">",
                    "        <Parameters>",
                    @"         <n value=""3""/>",
                    @"         <par0 value=""" + Xmin + @"""/>",
                    @"         <par1 value=""" + Ymax + @"""/>",
                    @"         <par2 value=""""/>",
                    "        </Parameters>",
                    "       </Geometry>",
                    "      </item_1>",
                    "      <item_2>",
                    @"       <Geometry type=""2001"">",
                    "        <Parameters>",
                    @"         <n value=""3""/>",
                    @"         <par0 value=""" + Xmax + @"""/>",
                    @"         <par1 value=""" + Ymax + @"""/>",
                    @"         <par2 value=""""/>",
                    "        </Parameters>",
                    "       </Geometry>",
                    "      </item_2>",
                    "      <item_3>",
                    @"       <Geometry type=""2001"">",
                    "        <Parameters>",
                    @"         <n value=""3""/>",
                    @"         <par0 value=""" + Xmax + @"""/>",
                    @"         <par1 value=""" + Ymin + @"""/>",
                    @"         <par2 value=""""/>",
                    "        </Parameters>",
                    "       </Geometry>",
                    "      </item_3>",
                    "     </GeometryList>",
                    "    </Geometry>",
                    "   </Element>",
                    "  </OrthoAreas>",
                    "  <ProjectParameters>",
                    "   <OrthoBlockProjectParameters>",
                    "    <OrthoGenerationParameters>",
                    @"     <m_useDeh value=""" + Deh + @"""/>",
                    @"     <m_dehPlane0 value=""" + Wysokosc + @"""/>",
                    @"     <m_dehPlane1 value=""0""/>",
                    @"     <m_dehPlane2 value=""0""/>",
                    @"     <m_dehPlane3 value=""0""/>",
                    @"     <m_dehPlane4 value=""0""/>",
                    @"     <m_outputFormat value=""1""/>",
                    @"     <m_useAOI value=""0""/>",
                    @"     <m_nullColor0 value=""" + kolor + @"""/>",
                    @"     <m_nullColor1 value=""" + kolor + @"""/>",
                    @"     <m_nullColor2 value=""" + kolor + @"""/>",
                    @"     <m_nodehColor0 value=""" + kolor + @"""/>",
                    @"     <m_nodehColor1 value=""" + kolor + @"""/>",
                    @"     <m_nodehColor2 value=""" + kolor + @"""/>",
                    @"     <m_resamplingMethod value=""2""/>",
                    @"     <m_resolution value=""" + WielkoscPix + @"""/>",
                    @"     <m_useAnchors value=""0""/>",
                    @"     <m_anchorSize value=""50""/>",
                    @"     <m_azimuth value=""0""/>",
                    @"     <m_overlap value=""0.5""/>",
                    @"     <m_overlapAbsolute value=""0""/>",
                    @"     <m_selMethod value=""2""/>",
                    @"     <m_resoMethod value=""2""/>",
                    @"     <m_useWatermark value=""0""/>",
                    @"     <m_watermarkFile value=""""/>",
                    @"     <m_outputDatatype value=""2""/>",
                    @"     <m_overviewType value=""0""/>",
                    @"     <m_overviewWidth value=""1024""/>",
                    @"     <m_isTiledOutput value=""1""/>",
                    @"     <m_tileWidth value=""256""/>",
                    @"     <m_tileHeight value=""256""/>",
                    @"     <m_areaReplType value=""2""/>",
                    @"     <m_isForceRectangular value=""1""/>",
                    @"     <m_clipPrecentage value=""0""/>",
                    @"     <m_isOnlyConnected value=""0""/>",
                    @"     <m_outdirPath value=""&quot;" + sciezkaW + @"&quot;""/>",
                    @"     <m_nameMask value=""ortho_&lt;IMAGE>_" + Time + @"""/>",
                    @"     <m_transparencyWM value=""75""/>",
                    @"     <m_isVisibilityMask value=""" + TrueOrto + @"""/>",
                    @"     <m_isStoreVisibilityMask value=""0""/>",
                    @"     <m_isJpeg value=""0""/>",
                    @"     <m_isBigTiff value=""0""/>",
                    @"     <m_jpegQ value=""50""/>",
                    @"     <m_ecwCompressRatio value=""0.2""/>",
                    @"     <m_isForceNoNullValues value=""0""/>",
                    @"     <m_VisiMapAccuracy value=""" + TrueOrtoA + @"""/>",
                    @"     <m_VisiMapBitmatthick value=""0""/>",
                    @"     <m_clipFlight value=""600""/>",
                    @"     <m_clipAcrossFlight value=""600""/>",
                    @"     <m_useOnlyCornerPoints value=""1""/>",
                    @"     <m_isFrameCameraProject value=""1""/>",
                    @"     <m_maxImagefileSize value=""3000000000""/>",
                    @"     <m_overlapOfParts value=""200""/>",
                    @"     <m_GeotiffTagFile value=""""/>",
                    @"     <m_UseRadiometrix value=""0""/>",
                    @"     <m_useImageCenter value=""0""/>",
                    @"     <m_IsSkipExistingImages value=""0""/>",
                    @"     <m_IsCreateRegionFiles value=""0""/>",
                    @"     <m_CheckForHoles value=""0""/>",
                    @"     <m_IsParallelProcessingGUI value=""1""/>",
                    @"     <m_ThreadPriority value=""0""/>",
                    @"     <m_MaxNumParallelProcesses value=""4""/>",
                    @"     <m_IsOAForActiveOnly value=""0""/>",
                    @"     <m_IsOAForDTMAreaOnly value=""0""/>",
                    @"     <m_ADSL0BandIndex value=""-1""/>",
                    @"     <m_ADSL0ViewIndex value=""-1""/>",
                    @"     <m_NofSubProcesses value=""2""/>",
                    @"     <m_isADS_L0_Project value=""0""/>",
                    @"     <m_combineMultiHead value=""0""/>",
                    @"     <m_useIntermediatePoints value=""0""/>",
                    @"     <m_nofIntermediatePoints value=""10""/>",
                    @"     <m_IsBatchProcessing value=""0""/>",
                    @"     <m_IsPostProcessing value=""0""/>",
                    @"     <m_IsSynchronousProcesses value=""0""/>",
                    @"     <m_IWD value=""""/>",
                    @"     <m_IWDNofFiles value=""8""/>",
                    @"     <m_OWD value=""""/>",
                    @"     <m_LastPostProcessType value=""0""/>",
                    @"     <m_MaxOVParallelProcesses value=""4""/>",
                    @"     <m_IsMetaFileInUse value=""0""/>",
                    @"     <m_MetaFileTemplateFile value=""""/>",
                    @"     <NofChannelDefinitions value=""0""/>",
                    "    </OrthoGenerationParameters>",
                    "   </OrthoBlockProjectParameters>",
                    "  </ProjectParameters>",
                    "  <DeactivatedElements/>",
                    " </xpcl-apps>",
                    "</inpho-product>"
                };
                string xmlPlik = plikPrjW.Remove(plikPrjW.Length - 4) + ".xml";
                if (File.Exists(xmlPlik) == true)
                {
                    File.Delete(xmlPlik);
                }
                File.WriteAllLines(xmlPlik, xmlZawartosc);
                Process MyProcess3 = new Process();
                MyProcess3.StartInfo.FileName = PlikExeOrthoMaster;
                MyProcess3.StartInfo.Arguments = @" -batch -prj """ + plikPrjW + @"""";
                MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess3.Start();
                MyProcess3.WaitForExit();
                string[] zawartoscXml = File.ReadAllLines(xmlPlik);
                foreach (string iLinia in zawartoscXml)
                {
                    int nr = iLinia.IndexOf(" orthoID=");
                    if (nr > -1)
                    {
                        plikTif = iLinia.Remove(0, nr + 10);
                        nr = plikTif.IndexOf(@"""");
                        plikTif = plikTif.Remove(nr);
                    }
                }
                Process MyProcess4 = new Process();
                MyProcess4.StartInfo.FileName = PlikExePhoto;
                MyProcess4.StartInfo.Arguments = sciezkaW + plikTif + ".tif";
                MyProcess4.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess4.Start();
            }
        }

        private void TextBox11_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".prj" || roz == ".PRJ")
                    {
                        textBox11.Text = files[0];
                    }
                    else
                    {
                        textBox11.Text = "";
                    }
                }
            }
        }

        private void TextBox11_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox41_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".dtm" || roz == ".DTM")
                    {
                        textBox41.Text = files[0];
                    }
                    else
                    {
                        textBox41.Text = "";
                    }
                }
            }
        }

        private void TextBox41_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox44_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".dxf" || roz == ".DXF")
                    {
                        textBox44.Text = files[0];
                    }
                    else
                    {
                        textBox44.Text = "";
                    }
                }
            }
        }

        private void TextBox44_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox43_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".dxf" || roz == ".DXF")
                    {
                        textBox43.Text = files[0];
                    }
                    else
                    {
                        textBox43.Text = "";
                    }
                }
            }
        }

        private void TextBox43_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox42_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".las" || roz == ".LAS")
                    {
                        textBox42.Text = files[0];
                    }
                    else
                    {
                        textBox42.Text = "";
                    }
                }
            }
        }

        private void TextBox42_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //***************POPRAWA ORT END******************************************************************


        //***************TFW START************************************************************************

        //---------------GENERUJ TFW----------------------------------------------------------------------

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            sciezkaT = textBox1.Text;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            sciezkaT = textBox1.Text;
            if (Directory.Exists(sciezkaT) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaT += @"\";
                sciezkaT = Path.GetFullPath(sciezkaT);
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
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
                    PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                    if (File.Exists(PlikExeEXIF) == false)
                    {
                        MessageBox.Show(@"Brak pliku exiftool w katalogu INDEOv2\bin !");
                    }
                    else
                    {
                        PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                        TFW();
                    }
                }
            }
        }

        private void TFW()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button2.Enabled = false;
            button3.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_TFW_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_TFW_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_TFW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Wygenerowano z " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void BackgroundWorker_TFW_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                Process MyProcess = new Process();
                MyProcess.StartInfo.FileName = PlikExeEXIF;
                MyProcess.StartInfo.Arguments = @" -a -u -g1 -w txt """ + iPlik + @"""";
                MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess.Start();
                MyProcess.WaitForExit();
                string txtPlik = iPlik.Remove(iPlik.Length - 3) + "txt";
                decimal ipx = 0;
                decimal ipy = 0;
                decimal ix = 0;
                decimal iy = 0;
                string[] zawartosc = File.ReadAllLines(txtPlik);
                foreach (string iLinia in zawartosc)
                {
                    int nr = 0;
                    nr = iLinia.IndexOf("Pixel Scale                     :");
                    if (nr > -1)
                    {
                        string xy = iLinia.Remove(0, 34);
                        nr = xy.LastIndexOf(" ");
                        xy = xy.Remove(nr);
                        nr = xy.LastIndexOf(" ");
                        string y = xy.Remove(0, nr);
                        string x = xy.Remove(nr);
                        nr = y.IndexOf("e");
                        if (nr > -1)
                        {
                            string stopien = y.Remove(0, nr + 2);
                            double st = Convert.ToDouble(stopien);
                            st = Math.Pow(10, st);
                            decimal sto = Convert.ToDecimal(st);
                            y = y.Remove(nr);
                            ipy = Convert.ToDecimal(y);
                            ipy = ipy / sto;
                        }
                        else
                        {
                            ipy = Convert.ToDecimal(y);
                        }
                        nr = x.IndexOf("e");
                        if (nr > -1)
                        {
                            string stopien = x.Remove(0, nr + 2);
                            double st = Convert.ToDouble(stopien);
                            st = Math.Pow(10, st);
                            decimal sto = Convert.ToDecimal(st);
                            x = x.Remove(nr);
                            ipx = Convert.ToDecimal(x);
                            ipx = ipx / sto;
                        }
                        else
                        {
                            ipx = Convert.ToDecimal(x);
                        }
                    }
                    nr = iLinia.IndexOf("Model Tie Point                 :");
                    if (nr > -1)
                    {
                        string xy = iLinia.Remove(0, 34);
                        nr = xy.LastIndexOf(" ");
                        xy = xy.Remove(nr);
                        nr = xy.LastIndexOf(" ");
                        string y = xy.Remove(0, nr);
                        iy = Convert.ToDecimal(y);
                        iy = iy - (ipy / 2);
                        xy = xy.Remove(nr);
                        nr = xy.LastIndexOf(" ");
                        string x = xy.Remove(0, nr);
                        ix = Convert.ToDecimal(x);
                        ix = ix + (ipx / 2);
                    }
                }
                string[] zawartosct =
                {
                    " " + ipx.ToString(),
                    " 0",
                    " 0",
                    "-" + ipy.ToString(),
                    " " + ix.ToString(),
                    " " + iy.ToString(),
                };
                string tPlik = iPlik.Remove(iPlik.Length - 3) + "tfw";
                File.WriteAllLines(tPlik, zawartosct);
                File.Delete(txtPlik);
            }
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

        //---------------EDYTUJ TFW-----------------------------------------------------------------------

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox2.Text;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            WielkoscPix = textBox4.Text;
        }

        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && (((sender as TextBox).Text.IndexOf('.') > -1) || ((sender as TextBox).Text.Length == 0)))
            {
                e.Handled = true;
            }
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            X = textBox5.Text;
        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && (((sender as TextBox).Text.IndexOf('.') > -1) || ((sender as TextBox).Text.Length == 0)))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '-') && ((sender as TextBox).Text.Length != 0))
            {
                e.Handled = true;
            }
        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            Y = textBox6.Text;
        }

        private void TextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && (((sender as TextBox).Text.IndexOf('.') > -1) || ((sender as TextBox).Text.Length == 0)))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '-') && ((sender as TextBox).Text.Length != 0))
            {
                e.Handled = true;
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                textBox5.Enabled = true;
                textBox6.Enabled = true;
            }
            else
            {
                textBox5.Enabled = false;
                textBox6.Enabled = false;
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                textBox5.Enabled = true;
                textBox6.Enabled = true;
            }
            else
            {
                textBox5.Enabled = false;
                textBox6.Enabled = false;
            }
        }

        private void RadioButton26_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton26.Checked == true)
            {
                textBox5.Enabled = false;
                textBox5.Text = "";
                textBox6.Enabled = false;
                textBox6.Text = "";
            }
            else
            {
                textBox5.Enabled = true;
                textBox6.Enabled = true;
            }
        }

        private void Przelaczniki()
        {
            if (radioButton1.Checked == true)
            {
                metoda = "pomnoz";
            }
            else if (radioButton2.Checked == true)
            {
                metoda = "dodaj";
            }
            else if (radioButton26.Checked == true)
            {
                metoda = "wyrownaj";
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            sciezkaT = textBox1.Text;
            if (Directory.Exists(sciezkaT) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaT += @"\";
                sciezkaT = Path.GetFullPath(sciezkaT);
                sciezkaW = textBox2.Text;
                if (Directory.Exists(sciezkaW) == false)
                {
                    MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                }
                else
                {
                    sciezkaW += @"\";
                    sciezkaW = Path.GetFullPath(sciezkaW);
                    if (sciezkaT == sciezkaW)
                    {
                        MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Danymi !");
                    }
                    else
                    {
                        string[] Pliki = Directory.GetFiles(sciezkaT, "*.tfw");
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
                            WielkoscPix = textBox4.Text;
                            if (WielkoscPix == "")
                            {
                                MessageBox.Show("Wpisz Wielkość Piksela !");
                            }
                            else
                            {
                                bool wynik = decimal.TryParse(WielkoscPix, out _);
                                if (wynik == true)
                                {
                                    Przelaczniki();
                                    if (metoda != "wyrownaj")
                                    {
                                        X = textBox5.Text;
                                        if (X == "")
                                        {
                                            MessageBox.Show("Wpisz Wartość X !");
                                        }
                                        else
                                        {
                                            wynik = decimal.TryParse(X, out _);
                                            if (wynik == true)
                                            {
                                                Y = textBox6.Text;
                                                if (Y == "")
                                                {
                                                    MessageBox.Show("Wpisz Wartość Y !");
                                                }
                                                else
                                                {
                                                    wynik = decimal.TryParse(Y, out _);
                                                    if (wynik == true)
                                                    {
                                                        EdytujTFW();
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Błędna Wartość Y !");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Błędna Wartość X !");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        EdytujTFW();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Błędna Wielkość Piksela !");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void EdytujTFW()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button2.Enabled = false;
            button3.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_EdytujTFW_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_EdytujTFW_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_EdytujTFW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void BackgroundWorker_EdytujTFW_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaT, "*.tfw");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                decimal ix = 0;
                decimal iy = 0;
                string[] zawartosc = File.ReadAllLines(iPlik);
                foreach (string iLinia in zawartosc)
                {
                }
                if (metoda == "pomnoz")
                {
                    ix = Convert.ToDecimal(zawartosc[4]) * Convert.ToDecimal(X);
                    iy = Convert.ToDecimal(zawartosc[5]) * Convert.ToDecimal(Y);
                }
                else if (metoda == "dodaj")
                {
                    ix = Convert.ToDecimal(zawartosc[4]) + Convert.ToDecimal(X);
                    iy = Convert.ToDecimal(zawartosc[5]) + Convert.ToDecimal(Y);
                }
                else if (metoda == "wyrownaj")
                {
                    decimal WPix = Convert.ToDecimal(WielkoscPix);
                    ix = Convert.ToDecimal(zawartosc[4]) - (WPix / 2);
                    iy = Convert.ToDecimal(zawartosc[5]) + (WPix / 2);
                    ix = ix / WPix;
                    iy = iy / WPix;
                    string s = Convert.ToString(ix);
                    int nr = 0;
                    nr = s.IndexOf(".");
                    if (nr > -1)
                    {
                        string reszta = s.Remove(0, nr + 1);
                        if (reszta.Length == 1)
                        {
                            reszta = "0";
                        }
                        else
                        {
                            reszta = reszta.Remove(1);
                        }
                        s = s.Remove(nr);
                        if (reszta == "0" || reszta == "1" || reszta == "2" || reszta == "3" || reszta == "4")
                        {
                            ix = Convert.ToDecimal(s) * WPix;
                            ix = ix + (WPix / 2);
                        }
                        else
                        {
                            ix = (Convert.ToDecimal(s) + 1) * WPix;
                            ix = ix + (WPix / 2);
                        }
                    }
                    else
                    {
                        ix = Convert.ToDecimal(zawartosc[4]);
                    }
                    s = Convert.ToString(iy);
                    nr = s.IndexOf(".");
                    if (nr > -1)
                    {
                        string reszta = s.Remove(0, nr + 1);
                        if (reszta.Length == 1)
                        {
                            reszta = "0";
                        }
                        else
                        {
                            reszta = reszta.Remove(1);
                        }
                        s = s.Remove(nr);
                        if (reszta == "0" || reszta == "1" || reszta == "2" || reszta == "3" || reszta == "4")
                        {
                            iy = Convert.ToDecimal(s) * WPix;
                            iy = iy - (WPix / 2);
                        }
                        else
                        {
                            iy = (Convert.ToDecimal(s) + 1) * WPix;
                            iy = iy - (WPix / 2);
                        }
                    }
                    else
                    {
                        iy = Convert.ToDecimal(zawartosc[5]);
                    }
                }
                string[] zawartosct =
                {
                    //Nie zmienia wielkości piksela ale zachowuje kąty. Druga wersja zmienia wielość piksela i zeruje kąty.
                    zawartosc[0], //" " + WielkoscPix,
                    zawartosc[1], //" 0",
                    zawartosc[2], //" 0",
                    zawartosc[3], //"-" + WielkoscPix,
                    ix.ToString(), //" " + ix.ToString(),
                    iy.ToString(), //" " + iy.ToString(),
                };
                string tPlik = sciezkaW + Path.GetFileName(iPlik);
                File.WriteAllLines(tPlik, zawartosct);
            }
        }

        private void TextBox2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox2.Text = files[0];
                }
            }
        }

        private void TextBox2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //***************TFW_END***************************************************************************


        //---------------RAMKI TIF-------------------------------------------------------------------------

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            sciezkaT = textBox8.Text;
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox8.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            sciezkaT = textBox8.Text;
            if (Directory.Exists(sciezkaT) == false)
            {
                MessageBox.Show("Błędna Ścieżka !");
            }
            else
            {
                sciezkaT = sciezkaT + @"\";
                sciezkaT = Path.GetFullPath(sciezkaT);
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
                SumaPLIK = 0;
                foreach (string iPlik in Pliki)
                {
                    ++SumaPLIK;
                }
                if (SumaPLIK == 0)
                {
                    MessageBox.Show("Brak Plików TIF !");
                }
                else
                {
                    string[] Pliki2 = Directory.GetFiles(sciezkaT, "*.tfw");
                    int SumaPLIK2 = 0;
                    foreach (string iPlik in Pliki2)
                    {
                        ++SumaPLIK2;
                    }
                    if (SumaPLIK2 == 0)
                    {
                        MessageBox.Show("Brak Plików TFW !");
                    }
                    else
                    {
                        if (SumaPLIK == SumaPLIK2)
                        {
                            PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                            if (File.Exists(PlikExeEXIF) == false)
                            {
                                MessageBox.Show(@"Brak pliku exiftool w katalogu INDEOv2\bin !");
                            }
                            else
                            {
                                PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                                RamkiTif();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ilość plików TIF nie zgadza się z ilością plików TFW !");
                        }
                    }
                }
            }
        }

        private void RamkiTif()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button12.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_RamkiTif_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RamkiTif_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_RamkiTif_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Wygenerowano z " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button12.Enabled = true;
        }

        private void BackgroundWorker_RamkiTif_DoWork(object sender, DoWorkEventArgs e)
        {
            plikDxf = sciezkaT + "ZAKRESY.dxf";
            File.WriteAllLines(plikDxf, DXFpoczatek);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                Process MyProcess = new Process();
                MyProcess.StartInfo.FileName = PlikExeEXIF;
                MyProcess.StartInfo.Arguments = @" -a -u -g1 -w txt """ + iPlik + @"""";
                MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess.Start();
                MyProcess.WaitForExit();
                string txtPlik = iPlik.Remove(iPlik.Length - 3) + "txt";
                decimal iSzerokosc = 0;
                decimal iWysokosc = 0;
                string[] zawartoscTxt = File.ReadAllLines(txtPlik);
                foreach (string iLinia in zawartoscTxt)
                {
                    int nr = 0;
                    nr = iLinia.IndexOf("Image Size                      :");
                    if (nr > -1)
                    {
                        string size = iLinia.Remove(0, 34);
                        nr = size.IndexOf("x");
                        string szerokosc = size.Remove(nr);
                        string wysokosc = size.Remove(0, nr + 1);
                        iSzerokosc = Convert.ToDecimal(szerokosc);
                        iWysokosc = Convert.ToDecimal(wysokosc);
                    }
                }
                string tPlik = iPlik.Remove(iPlik.Length - 3) + "tfw";
                string[] zawartoscTfw = File.ReadAllLines(tPlik);
                foreach (string iLinia in zawartoscTfw)
                {
                }
                decimal pixel = Convert.ToDecimal(zawartoscTfw[0]);
                iSzerokosc = iSzerokosc * pixel;
                iWysokosc = iWysokosc * pixel;
                pixel = pixel / 2;
                decimal X = Convert.ToDecimal(zawartoscTfw[4]) - pixel;
                decimal Y = Convert.ToDecimal(zawartoscTfw[5]) + pixel;
                string[] zawartosc =
                    {
                        "  0",
                        "POLYLINE",
                        "  8",
                        Convert.ToString(numericUpDown2.Value),
                        " 62",
                        Convert.ToString(numericUpDown3.Value),
                        " 66",
                        "1",
                        " 10",
                        "0.0",
                        " 20",
                        "0.0",
                        " 30",
                        "0.0",
                        " 70",
                        "1",
                        "  0",
                        "VERTEX",
                        "  8",
                        Convert.ToString(numericUpDown2.Value),
                        " 62",
                        Convert.ToString(numericUpDown3.Value),
                        " 10",
                        Convert.ToString(X),
                        " 20",
                        Convert.ToString(Y),
                        " 30",
                        "0.0",
                        "  0",
                        "VERTEX",
                        "  8",
                        Convert.ToString(numericUpDown2.Value),
                        " 62",
                        Convert.ToString(numericUpDown3.Value),
                        " 10",
                        Convert.ToString(X + iSzerokosc),
                        " 20",
                        Convert.ToString(Y),
                        " 30",
                        "0.0",
                        "  0",
                        "VERTEX",
                        "  8",
                        Convert.ToString(numericUpDown2.Value),
                        " 62",
                        Convert.ToString(numericUpDown3.Value),
                        " 10",
                        Convert.ToString(X + iSzerokosc),
                        " 20",
                        Convert.ToString(Y - iWysokosc),
                        " 30",
                        "0.0",
                        "  0",
                        "VERTEX",
                        "  8",
                        Convert.ToString(numericUpDown2.Value),
                        " 62",
                        Convert.ToString(numericUpDown3.Value),
                        " 10",
                        Convert.ToString(X),
                        " 20",
                        Convert.ToString(Y - iWysokosc),
                        " 30",
                        "0.0",
                        "  0",
                        "SEQEND",
                        "  8",
                        Convert.ToString(numericUpDown2.Value),
                        " 62",
                        Convert.ToString(numericUpDown3.Value),
                        "  0",
                        "TEXT",
                        "  8",
                        Convert.ToString(numericUpDown5.Value),
                        " 62",
                        Convert.ToString(numericUpDown4.Value),
                        " 10",
                        Convert.ToString(X + (iSzerokosc / 2)),
                        " 20",
                        Convert.ToString(Y - (iWysokosc / 2)),
                        " 30",
                        "0.0",
                        " 40",
                        Convert.ToString(iWysokosc / 8),
                        "  1",
                        nPlik.Remove(nPlik.Length - 4),
                        " 41",
                        "0.75",
                        " 51",
                        "15",
                    };
                File.AppendAllLines(plikDxf, zawartosc);
                File.Delete(txtPlik);
            }
            File.AppendAllLines(plikDxf, DXFkoniec);
        }

        private void TextBox8_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox8.Text = files[0];
                }
            }
        }

        private void TextBox8_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------7z-------------------------------------------------------------------------------

        private void TextBox16_TextChanged(object sender, EventArgs e)
        {
            sciezkaO = textBox16.Text;
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox16.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox14_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox14.Text;
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox14.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox15_TextChanged(object sender, EventArgs e)
        {
            rozszerzenie = textBox15.Text;
        }

        private void ZipPrzelaczniki()
        {
            if (radioButton29.Checked == true)
            {
                metoda = " a -t7z ";
                tile = ".7z";
            }
            else if (radioButton19.Checked == true)
            {
                metoda = " a -tzip ";
                tile = ".zip";
            }
            else if (radioButton18.Checked == true)
            {
                metoda = " a -sfx ";
                tile = ".exe";
            }
            else if (radioButton17.Checked == true)
            {
                metoda = " a -ttar ";
                tile = ".tar";
            }
        }

        private void CheckBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked == true)
            {
                textBox14.Enabled = true;
                button21.Enabled = true;
            }
            else
            {
                textBox14.Enabled = false;
                button21.Enabled = false;
                textBox14.Text = "";
            }
        }

        private void CheckBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked == true)
            {
                textBox15.Enabled = true;
            }
            else
            {
                textBox15.Enabled = false;
                textBox15.Text = "";
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            sciezkaO = textBox16.Text;
            if (Directory.Exists(sciezkaO) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaO = sciezkaO + @"\";
                sciezkaO = Path.GetFullPath(sciezkaO);
                if (checkBox13.Checked == true)
                {
                    sciezkaW = textBox14.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                    }
                    else
                    {
                        sciezkaW = sciezkaW + @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        if (sciezkaO == sciezkaW)
                        {
                            MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Danymi !");
                        }
                        else
                        {
                            SprawdzRozsz();
                        }
                    }
                }
                else
                {
                    sciezkaW = sciezkaO;
                    SprawdzRozsz();
                }
            }
        }

        private void SprawdzRozsz()
        {
            if (checkBox14.Checked == true)
            {
                rozszerzenie = textBox15.Text;
                if (rozszerzenie == "")
                {
                    MessageBox.Show("Wpisz rozszerzenie pliku wiodącego !");
                }
                else
                {
                    PoliczPliki();
                }
            }
            else
            {
                rozszerzenie = "*";
                PoliczPliki();
            }

        }

        private void PoliczPliki()
        {
            string[] Pliki = Directory.GetFiles(sciezkaO, "*." + rozszerzenie);
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
                PlikExe7z = @"C:\Program Files\7-Zip\7z.exe";
                if (File.Exists(PlikExe7z) == false)
                {
                    PlikExe7z = @"C:\Program Files (x86)\7-Zip\7z.exe";
                    if (File.Exists(PlikExe7z) == false)
                    {
                        MessageBox.Show("Brak zainstalowanego 7-zip !");
                    }
                    else
                    {
                        PlikExe7z = @"""C:\Program Files (x86)\7-Zip\7z.exe""";
                        SevenZ();
                    }
                }
                else
                {
                    PlikExe7z = @"""C:\Program Files\7-Zip\7z.exe""";
                    SevenZ();
                }
            }
        }

        private void SevenZ()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button20.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_SevenZ_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_SevenZ_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_SevenZ_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Spakowano " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button20.Enabled = true;
        }

        private void BackgroundWorker_SevenZ_DoWork(object sender, DoWorkEventArgs e)
        {
            ZipPrzelaczniki();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaO, "*." + rozszerzenie);
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                string sPlik = iPlik;
                if (checkBox14.Checked == true)
                {
                    nPlik = nPlik.Remove(nPlik.Length - 4);
                    sPlik = iPlik.Remove(iPlik.Length - 3) + "*";
                }
                Process MyProcess = new Process();
                MyProcess.StartInfo.FileName = PlikExe7z;
                MyProcess.StartInfo.Arguments = metoda + @"""" + sciezkaW + nPlik + tile + @""" """ + sPlik + @"""";
                MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess.Start();
                MyProcess.WaitForExit();
            }
        }

        private void TextBox16_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox16.Text = files[0];
                }
            }
        }

        private void TextBox16_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox14_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox14.Text = files[0];
                }
            }
        }

        private void TextBox14_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------RESAMPLING------------------------------------------------------------------------

        private void TextBox27_TextChanged(object sender, EventArgs e)
        {
            sciezkaO = textBox27.Text;
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox27.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox26_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox26.Text;
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox26.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox23_TextChanged(object sender, EventArgs e)
        {
            WielkoscPix = textBox23.Text;
        }

        private void TextBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && (((sender as TextBox).Text.IndexOf('.') > -1) || ((sender as TextBox).Text.Length == 0)))
            {
                e.Handled = true;
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                metoda = " -r near";
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                metoda = " -r bilinear";
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                metoda = " -r cubic";
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                metoda = " -r cubicspline";
            }
            else if (comboBox2.SelectedIndex == 4)
            {
                metoda = " -r lanczos";
            }
            else if (comboBox2.SelectedIndex == 5)
            {
                metoda = " -r average";
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            sciezkaO = textBox27.Text;
            if (Directory.Exists(sciezkaO) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaO = sciezkaO + @"\";
                sciezkaO = Path.GetFullPath(sciezkaO);
                sciezkaW = textBox26.Text;
                if (Directory.Exists(sciezkaW) == false)
                {
                    MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                }
                else
                {
                    sciezkaW = sciezkaW + @"\";
                    sciezkaW = Path.GetFullPath(sciezkaW);
                    if (sciezkaO == sciezkaW)
                    {
                        MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Danymi !");
                    }
                    else
                    {
                        string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
                        SumaPLIK = 0;
                        foreach (string iPlik in Pliki)
                        {
                            ++SumaPLIK;
                        }
                        if (SumaPLIK == 0)
                        {
                            MessageBox.Show("Brak Plików .tif !");
                        }
                        else
                        {
                            WielkoscPix = textBox23.Text;
                            if (WielkoscPix == "")
                            {
                                MessageBox.Show("Wpisz Wielkość Piksela !");
                            }
                            else
                            {
                                bool wynik = decimal.TryParse(WielkoscPix, out _);
                                if (wynik == true)
                                {
                                    if (metoda == "")
                                    {
                                        MessageBox.Show("Wybierz Metodę Resamplingu !");
                                    }
                                    else
                                    {
                                        PlikExeGDWarp = @"C:\Program Files\INDEOv2\bin\gdalwarp.exe";
                                        if (File.Exists(PlikExeGDWarp) == false)
                                        {
                                            MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin !");
                                        }
                                        else
                                        {
                                            System.Environment.SetEnvironmentVariable("GDAL_DATA", @"C:\Program Files\INDEOv2\bin");
                                            PlikExeGDWarp = @"""C:\Program Files\INDEOv2\bin\gdalwarp.exe""";
                                            Resampling();
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Błędna Wielkość Piksela !");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Resampling()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button9.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker1_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button9.Enabled = true;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                Process MyProcess1 = new Process();
                MyProcess1.StartInfo.FileName = PlikExeGDWarp;
                MyProcess1.StartInfo.Arguments = metoda + " -tr " + WielkoscPix + " " + WielkoscPix + @" """ + iPlik + @""" -co TFW=YES """ + sciezkaW + nPlik + @"""";
                MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess1.Start();
                MyProcess1.WaitForExit();
            }
        }

        private void TextBox27_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox27.Text = files[0];
                }
            }
        }

        private void TextBox27_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox26_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox26.Text = files[0];
                }
            }
        }

        private void TextBox26_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------NAGLOWEK_TIFF--------------------------------------------------------------------

        private void TextBox36_TextChanged(object sender, EventArgs e)
        {
            sciezkaT = textBox36.Text;
        }

        private void TextBox35_TextChanged(object sender, EventArgs e)
        {
            ImageDescription = textBox35.Text;
        }

        private void TextBox37_TextChanged(object sender, EventArgs e)
        {
            Software = textBox37.Text;
        }

        private void TextBox38_TextChanged(object sender, EventArgs e)
        {
            Copyright = textBox38.Text;
        }

        private void TextBox39_TextChanged(object sender, EventArgs e)
        {
            Resolution = textBox39.Text;
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                ResolutionUnit = "";
            }
            else if (comboBox3.SelectedIndex == 1)
            {
                ResolutionUnit = "None";
            }
            else if (comboBox3.SelectedIndex == 2)
            {
                ResolutionUnit = "inches";
            }
            else if (comboBox3.SelectedIndex == 3)
            {
                ResolutionUnit = "cm";
            }
        }

        private void Button44_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox36.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void CheckBox20_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox20.Checked == true)
            {
                wybor1 = true;
                textBox35.Enabled = true;
            }
            else
            {
                wybor1 = false;
                textBox35.Enabled = false;
            }
        }

        private void CheckBox19_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox19.Checked == true)
            {
                wybor2 = true;
                textBox37.Enabled = true;
            }
            else
            {
                wybor2 = false;
                textBox37.Enabled = false;
            }
        }

        private void CheckBox18_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox18.Checked == true)
            {
                wybor3 = true;
                textBox38.Enabled = true;
            }
            else
            {
                wybor3 = false;
                textBox38.Enabled = false;
            }
        }

        private void CheckBox17_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox17.Checked == true)
            {
                wybor4 = true;
                textBox39.Enabled = true;
            }
            else
            {
                wybor4 = false;
                textBox39.Enabled = false;
            }
        }

        private void CheckBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked == true)
            {
                wybor5 = true;
                comboBox3.Enabled = true;
            }
            else
            {
                wybor5 = false;
                comboBox3.Enabled = false;
            }
        }

        private void Button42_Click(object sender, EventArgs e)
        {
            sciezkaT = textBox36.Text;
            if (Directory.Exists(sciezkaT) == false)
            {
                MessageBox.Show("Błędna Ścieżka !");
            }
            else
            {
                sciezkaT = sciezkaT + @"\";
                sciezkaT = Path.GetFullPath(sciezkaT);
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
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
                    PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                    if (File.Exists(PlikExeEXIF) == false)
                    {
                        MessageBox.Show(@"Brak pliku exiftool w katalogu INDEOv2\bin !");
                    }
                    else
                    {
                        PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                        PARAMETRY1();
                    }
                }
            }
        }

        private void PARAMETRY1()
        {
            if (wybor1 == true)
            {
                ImageDescription = @"""" + textBox35.Text + @"""";
                ImageDescription = " -imagedescription=" + ImageDescription;
            }
            else
            {
                ImageDescription = "";
            }
            if (wybor2 == true)
            {
                Software = @"""" + textBox37.Text + @"""";
                Software = " -software=" + Software;
            }
            else
            {
                Software = "";
            }
            if (wybor3 == true)
            {
                Copyright = @"""" + textBox38.Text + @"""";
                Copyright = " -copyright=" + Copyright;
            }
            else
            {
                Copyright = "";
            }
            if (wybor4 == true)
            {
                Resolution = textBox39.Text;
                if (Resolution == "")
                {
                    Resolution = " -xresolution= -yresolution=";
                    PARAMETRY2();
                }
                else
                {
                    bool wynik = decimal.TryParse(Resolution, out decimal d);
                    if (wynik == true)
                    {
                        Resolution = textBox39.Text;
                        Resolution = " -xresolution=" + Resolution + " -yresolution=" + Resolution;
                        PARAMETRY2();
                    }
                    else
                    {
                        MessageBox.Show("Błędna Wielkość Resolution !");
                    }
                }
            }
            else
            {
                Resolution = "";
                PARAMETRY2();
            }
        }

        private void PARAMETRY2()
        {
            if (wybor5 == true)
            {
                ResolutionUnit = " -resolutionunit=" + ResolutionUnit;
            }
            else
            {
                ResolutionUnit = "";
            }
            metoda = ImageDescription + Software + Copyright + Resolution + ResolutionUnit + " -modifydate= -overwrite_original_in_place";
            NAGLOWEK_TIFF();
        }

        private void NAGLOWEK_TIFF()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button42.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_NAGLOWEK_TIFF_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_NAGLOWEK_TIFF_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_NAGLOWEK_TIFF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Wygenerowano z " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button42.Enabled = true;
        }

        private void BackgroundWorker_NAGLOWEK_TIFF_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                Process MyProcess = new Process();
                MyProcess.StartInfo.FileName = PlikExeEXIF;
                MyProcess.StartInfo.Arguments = metoda + @" """ + iPlik + @"""";
                MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess.Start();
                MyProcess.WaitForExit();
            }
        }

        private void TextBox36_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox36.Text = files[0];
                }
            }
        }

        private void TextBox36_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------RAR-------------------------------------------------------------------------------

        private void TextBox69_TextChanged(object sender, EventArgs e)
        {
            sciezkaT = textBox69.Text;
        }

        private void Button51_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox69.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox67_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox67.Text;
        }

        private void Button50_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox67.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox68_TextChanged(object sender, EventArgs e)
        {
            rozszerzenie = textBox68.Text;
        }

        private void CheckBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked == true)
            {
                textBox67.Enabled = true;
                button50.Enabled = true;
            }
            else
            {
                textBox67.Enabled = false;
                button50.Enabled = false;
                textBox67.Text = "";
            }
        }

        private void CheckBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked == true)
            {
                textBox68.Enabled = true;
            }
            else
            {
                textBox68.Enabled = false;
                textBox68.Text = "";
            }
        }

        private void Button49_Click(object sender, EventArgs e)
        {
            sciezkaT = textBox69.Text;
            if (Directory.Exists(sciezkaT) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaT = sciezkaT + @"\";
                sciezkaT = Path.GetFullPath(sciezkaT);
                if (checkBox22.Checked == true)
                {
                    sciezkaW = textBox67.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                    }
                    else
                    {
                        sciezkaW = sciezkaW + @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        if (sciezkaT == sciezkaW)
                        {
                            MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Danymi !");
                        }
                        else
                        {
                            SprawdzRozszRAR();
                        }
                    }
                }
                else
                {
                    sciezkaW = sciezkaT;
                    SprawdzRozszRAR();
                }
            }
        }

        private void SprawdzRozszRAR()
        {
            if (checkBox21.Checked == true)
            {
                rozszerzenie = textBox68.Text;
                if (rozszerzenie == "")
                {
                    MessageBox.Show("Wpisz rozszerzenie pliku wiodącego !");
                }
                else
                {
                    PoliczPlikiRAR();
                }
            }
            else
            {
                rozszerzenie = "*";
                PoliczPlikiRAR();
            }

        }

        private void PoliczPlikiRAR()
        {
            string[] Pliki = Directory.GetFiles(sciezkaT, "*." + rozszerzenie);
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
                PlikExeRAR = @"C:\Program Files\WinRAR\Rar.exe";
                if (File.Exists(PlikExeRAR) == false)
                {
                    PlikExeRAR = @"C:\Program Files (x86)\WinRAR\Rar.exe";
                    if (File.Exists(PlikExeRAR) == false)
                    {
                        MessageBox.Show("Brak zainstalowanego RAR'a !");
                    }
                    else
                    {
                        PlikExeRAR = @"""C:\Program Files (x86)\WinRAR\Rar.exe""";
                        RAR();
                    }
                }
                else
                {
                    PlikExeRAR = @"""C:\Program Files\WinRAR\Rar.exe""";
                    RAR();
                }
            }
        }

        private void RAR()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button49.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_RAR_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RAR_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_RAR_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Spakowano " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button49.Enabled = true;
        }

        private void BackgroundWorker_RAR_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaT, "*." + rozszerzenie);
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                string sPlik = iPlik;
                if (checkBox21.Checked == true)
                {
                    nPlik = nPlik.Remove(nPlik.Length - 4);
                    sPlik = iPlik.Remove(iPlik.Length - 3) + "*";
                }
                Process MyProcess = new Process();
                MyProcess.StartInfo.FileName = PlikExeRAR;
                MyProcess.StartInfo.Arguments = @" a -ep """ + sciezkaW + nPlik + @".rar"" """ + sPlik + @"""";
                MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess.Start();
                MyProcess.WaitForExit();
            }
        }

        private void TextBox67_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox67.Text = files[0];
                }
            }
        }

        private void TextBox67_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox69_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox69.Text = files[0];
                }
            }
        }

        private void TextBox69_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //***************INPHO****************************************************************************

        //---------------Prj2XYZ--------------------------------------------------------------------------

        private void TextBox70_TextChanged(object sender, EventArgs e)
        {
            plikPrj = textBox70.Text;
        }

        private void Button53_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox70.Text = openFileDialog3.FileName;
            }
        }

        private void CheckBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox23.Checked == true)
            {
                wybor1 = true;
            }
            else
            {
                wybor1 = false;
            }
        }

        private void CheckBox24_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox24.Checked == true)
            {
                wybor2 = true;
            }
            else
            {
                wybor2 = false;
            }
        }

        private void CheckBox26_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox26.Checked == true)
            {
                wybor4 = true;
            }
            else
            {
                wybor4 = false;
            }
        }

        private void CheckBox27_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox27.Checked == true)
            {
                wybor5 = true;
            }
            else
            {
                wybor5 = false;
            }
        }

        private void CheckBox28_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox28.Checked == true)
            {
                wybor6 = true;
            }
            else
            {
                wybor6 = false;
            }
        }

        private void Button52_Click(object sender, EventArgs e)
        {
            plikPrj = textBox70.Text;
            if (File.Exists(plikPrj) == false)
            {
                MessageBox.Show("Brak Pliku Prj !");
            }
            else
            {
                if (wybor1 == false && wybor2 == false && wybor4 == false && wybor5 == false && wybor6 == false)
                {
                    MessageBox.Show("Może wybierzesz jakąś opcję ?");
                }
                else
                {
                    PrzelacznikiPrj();
                }
            }
        }

        private void PrzelacznikiPrj()
        {
            if (wybor1 == true || wybor2 == true || wybor6 == true)
            {
                SumaPLIK = 0;
                string[] zawartosc = File.ReadAllLines(plikPrj);
                foreach (string iLinia in zawartosc)
                {
                    int nr = 0;
                    nr = iLinia.IndexOf("$PHOTO_FILE");
                    if (nr > -1)
                    {
                        ++SumaPLIK;
                    }
                }
                if (SumaPLIK == 0)
                {
                    MessageBox.Show("Brak zdjęć zdefiniowanych w pliku projektowym !");
                }
                else
                {
                    if (wybor4 == true || wybor5 == true)
                    {
                        SumaPLIKF = 0;
                        int no = 0;
                        string[] zawartoscF = File.ReadAllLines(plikPrj);
                        foreach (string iLinia in zawartoscF)
                        {
                            ++no;
                            int nr = 0;
                            nr = iLinia.IndexOf("$CONTROL_POINTS");
                            if (nr > -1)
                            {
                                nrCONTROL_POINTS = no;
                            }
                        }
                        if (nrCONTROL_POINTS == 0)
                        {
                            MessageBox.Show("Brak fotopunktów zdefiniowanych w pliku projektowym !");
                        }
                        else
                        {
                            no = 0;
                            foreach (string iLinia in zawartoscF)
                            {
                                ++no;
                                int nr = 0;
                                nr = iLinia.IndexOf("$END_POINTS");
                                if (nr > -1)
                                {
                                    nrEND_POINTS = no;
                                    if (nrEND_POINTS - nrCONTROL_POINTS > 0)
                                    {
                                        if (SumaPLIKF == 0)
                                        {
                                            SumaPLIKF = nrEND_POINTS - nrCONTROL_POINTS - 1;
                                        }
                                    }
                                }
                            }
                            Prj2XYZ();
                        }
                    }
                    else
                    {
                        Prj2XYZ();
                    }
                }
            }
            else
            {
                if (wybor4 == true || wybor5 == true)
                {
                    SumaPLIKF = 0;
                    int no = 0;
                    string[] zawartoscF = File.ReadAllLines(plikPrj);
                    foreach (string iLinia in zawartoscF)
                    {
                        ++no;
                        int nr = 0;
                        nr = iLinia.IndexOf("$CONTROL_POINTS");
                        if (nr > -1)
                        {
                            nrCONTROL_POINTS = no;
                        }
                    }
                    if (nrCONTROL_POINTS == 0)
                    {
                        MessageBox.Show("Brak fotopunktów zdefiniowanych w pliku projektowym !");
                    }
                    else
                    {
                        no = 0;
                        foreach (string iLinia in zawartoscF)
                        {
                            ++no;
                            int nr = 0;
                            nr = iLinia.IndexOf("$END_POINTS");
                            if (nr > -1)
                            {
                                nrEND_POINTS = no;
                                if (nrEND_POINTS - nrCONTROL_POINTS > 0)
                                {
                                    if (SumaPLIKF == 0)
                                    {
                                        SumaPLIKF = nrEND_POINTS - nrCONTROL_POINTS - 1;
                                    }
                                }
                            }
                        }
                        Prj2XYZ();
                    }
                }
                else
                {
                    Prj2XYZ();

                }
            }
        }

        private void Prj2XYZ()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            if (SumaPLIK < SumaPLIKF)
            {
                SumaPLIK = SumaPLIKF;
            }
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button52.Enabled = false;
            button53.Enabled = false;
            button55.Enabled = false;
            button56.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_Prj2XYZ_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_Prj2XYZ_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_Prj2XYZ_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Wygenerowano dla " + SumaPLIK.ToString() + " zdjęć.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button52.Enabled = true;
            button53.Enabled = true;
            button55.Enabled = true;
            button56.Enabled = true;
        }

        private void BackgroundWorker_Prj2XYZ_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string[] zawartosc = File.ReadAllLines(plikPrj);
            if (wybor1 == true)
            {
                string[] zawartosct = new string[SumaPLIK];
                int progressBarVal = 0;
                int no = 0;
                int i = 0;
                foreach (string iLinia in zawartosc)
                {
                    ++no;
                    int nr = 0;
                    nr = iLinia.IndexOf("$PHOTO_FILE");
                    if (nr > -1)
                    {
                        ++progressBarVal;
                        BackgroundWorker1.ReportProgress(progressBarVal);
                        wiersz = iLinia;
                        nr = wiersz.LastIndexOf(@"\");
                        wiersz = wiersz.Remove(0, nr + 1);
                        nr = wiersz.LastIndexOf(".");
                        wiersz = wiersz.Remove(nr);
                    }
                    nr = iLinia.IndexOf("$EXT_ORI");
                    if (nr > -1)
                    {
                        string xyz = zawartosc[no];
                        xyz = xyz.Remove(0, 19);
                        zawartosct[i] = wiersz + "  " + xyz;
                        ++i;
                    }
                }
                string tPlik = plikPrj.Remove(plikPrj.Length - 4) + "_XYZ.txt";
                if (File.Exists(tPlik) == true)
                {
                    File.Delete(tPlik);
                }
                File.WriteAllLines(tPlik, zawartosct);
            }
            if (wybor2 == true)
            {
                string tPlik = plikPrj.Remove(plikPrj.Length - 4) + "_XYZ.dxf";
                if (File.Exists(tPlik) == true)
                {
                    File.Delete(tPlik);
                }
                File.WriteAllLines(tPlik, DXFpoczatek);
                int progressBarVal = 0;
                int no = 0;
                int i = 0;
                foreach (string iLinia in zawartosc)
                {
                    ++no;
                    int nr = 0;
                    nr = iLinia.IndexOf("$PHOTO_FILE");
                    if (nr > -1)
                    {
                        ++progressBarVal;
                        BackgroundWorker1.ReportProgress(progressBarVal);
                        wiersz = iLinia;
                        nr = wiersz.LastIndexOf(@"\");
                        wiersz = wiersz.Remove(0, nr + 1);
                        nr = wiersz.LastIndexOf(".");
                        wiersz = wiersz.Remove(nr);
                    }
                    nr = iLinia.IndexOf("$EXT_ORI");
                    if (nr > -1)
                    {
                        string xyz = zawartosc[no];
                        xyz = xyz.Remove(0, 19);
                        nr = xyz.LastIndexOf(" ");
                        string Z = xyz.Remove(0, nr + 1);
                        xyz = xyz.Remove(nr);
                        char[] spacja = { ' ' };
                        xyz = xyz.TrimEnd(spacja);
                        nr = xyz.LastIndexOf(" ");
                        string Y = xyz.Remove(0, nr + 1);
                        xyz = xyz.Remove(nr);
                        string X = xyz.Trim();
                        string[] DXFzawartosc =
                        {
                            "  0",
                            "TEXT",
                            "  8",
                            "1",
                            " 62",
                            "0",
                            " 10",
                            X,
                            " 20",
                            Y,
                            " 30",
                            Z,
                            " 40",
                            "50",
                            "  1",
                            wiersz,
                            " 41",
                            "0.75",
                            " 51",
                            "15",
                        };
                        File.AppendAllLines(tPlik, DXFzawartosc);
                        ++i;
                    }
                }
                File.AppendAllLines(tPlik, DXFkoniec);
            }
            if (wybor4 == true)
            {
                string[] zawartosct = new string[SumaPLIKF];
                int progressBarVal = 0;
                int no = nrCONTROL_POINTS;
                for (int i = 0; i <= SumaPLIKF - 1; ++i)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    string xyz = zawartosc[no];
                    char[] spacja = { ' ' };
                    int nr = xyz.LastIndexOf("*");
                    if (nr > -1)
                    {
                        xyz = xyz.Remove(nr);
                        xyz = xyz.TrimEnd(spacja);
                    }
                    nr = xyz.LastIndexOf(" ");
                    xyz = xyz.Remove(nr);
                    xyz = xyz.TrimEnd(spacja);
                    nr = xyz.LastIndexOf(" ");
                    xyz = xyz.Remove(nr);
                    xyz = xyz.TrimEnd(spacja);
                    nr = xyz.LastIndexOf(" ");
                    xyz = xyz.Remove(nr);
                    xyz = xyz.TrimEnd(spacja);
                    nr = xyz.LastIndexOf(" ");
                    xyz = xyz.Remove(nr);
                    xyz = xyz.TrimEnd(spacja);
                    zawartosct[i] = xyz;
                    ++no;
                }
                string tPlik = plikPrj.Remove(plikPrj.Length - 4) + "_GCP.txt";
                if (File.Exists(tPlik) == true)
                {
                    File.Delete(tPlik);
                }
                File.WriteAllLines(tPlik, zawartosct);
            }
            if (wybor5 == true)
            {
                string tPlik = plikPrj.Remove(plikPrj.Length - 4) + "_GCP.dxf";
                if (File.Exists(tPlik) == true)
                {
                    File.Delete(tPlik);
                }
                File.WriteAllLines(tPlik, DXFpoczatek);
                int progressBarVal = 0;
                int no = nrCONTROL_POINTS;
                for (int i = 0; i <= SumaPLIKF - 1; ++i)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    string W = "FOTOPUNKTY";
                    string A = null;
                    string xyz = zawartosc[no];
                    char[] spacja = { ' ' };
                    int nr = xyz.LastIndexOf("*");
                    if (nr > -1)
                    {
                        xyz = xyz.Remove(nr);
                        xyz = xyz.TrimEnd(spacja);
                        A = "NIEAKTYWNY_";
                        W = "NIEAKTYWNE";
                    }
                    nr = xyz.LastIndexOf(" ");
                    xyz = xyz.Remove(nr);
                    xyz = xyz.TrimEnd(spacja);
                    nr = xyz.LastIndexOf(" ");
                    xyz = xyz.Remove(nr);
                    xyz = xyz.TrimEnd(spacja);
                    nr = xyz.LastIndexOf(" ");
                    xyz = xyz.Remove(nr);
                    xyz = xyz.TrimEnd(spacja);
                    nr = xyz.LastIndexOf(" ");
                    string R = xyz.Remove(0, nr + 1);
                    if (R == "3")
                    {
                        R = "HV_";
                    }
                    if (R == "2")
                    {
                        R = "HO_";
                    }
                    if (R == "1")
                    {
                        R = "VE_";
                    }
                    if (R == "-3")
                    {
                        R = "CHV_";
                    }
                    if (R == "-2")
                    {
                        R = "CHO_";
                    }
                    if (R == "-1")
                    {
                        R = "CVE_";
                    }
                    xyz = xyz.Remove(nr);
                    xyz = xyz.TrimEnd(spacja);
                    nr = xyz.LastIndexOf(" ");
                    string Z = xyz.Remove(0, nr + 1);
                    xyz = xyz.Remove(nr);
                    xyz = xyz.TrimEnd(spacja);
                    nr = xyz.LastIndexOf(" ");
                    string Y = xyz.Remove(0, nr + 1);
                    xyz = xyz.Remove(nr);
                    xyz = xyz.TrimEnd(spacja);
                    nr = xyz.LastIndexOf(" ");
                    string X = xyz.Remove(0, nr + 1);
                    xyz = xyz.Remove(nr);
                    string Nazwa = xyz.Trim();
                    string[] DXFzawartosc =
                    {
                        "  0",
                        "TEXT",
                        "  8",
                        W,
                        " 62",
                        "0",
                        " 10",
                        X,
                        " 20",
                        Y,
                        " 30",
                        Z,
                        " 40",
                        "50",
                        "  1",
                        A + R + Nazwa,
                        " 41",
                        "0.75",
                        " 51",
                        "15",
                    };
                    File.AppendAllLines(tPlik, DXFzawartosc);
                    ++no;
                }
                File.AppendAllLines(tPlik, DXFkoniec);
            }
            if (wybor6 == true)
            {
                string[] zawartosct = new string[SumaPLIK];
                int progressBarVal = 0;
                int no = 0;
                int i = 0;
                foreach (string iLinia in zawartosc)
                {
                    ++no;
                    int nr = 0;
                    nr = iLinia.IndexOf("$PHOTO_FILE");
                    if (nr > -1)
                    {
                        ++progressBarVal;
                        BackgroundWorker1.ReportProgress(progressBarVal);
                        wiersz = iLinia;
                        nr = wiersz.LastIndexOf(@"\");
                        wiersz = wiersz.Remove(0, nr + 1);
                        nr = wiersz.LastIndexOf(".");
                        wiersz = wiersz.Remove(nr);
                    }
                    nr = iLinia.IndexOf("$EXT_ORI");
                    if (nr > -1)
                    {
                        string xyz = zawartosc[no];
                        xyz = xyz.Remove(0, 19);
                        string A = zawartosc[no + 1];
                        A = A.Remove(24);
                        double a11 = Convert.ToDouble(A);
                        A = zawartosc[no + 2];
                        A = A.Remove(24);
                        double a21 = Convert.ToDouble(A);
                        A = zawartosc[no + 3];
                        string A1 = A.Remove(24);
                        double a31 = Convert.ToDouble(A1);
                        A = A.Remove(0, 24);
                        A1 = A.Remove(20);
                        double a32 = Convert.ToDouble(A1);
                        A1 = A.Remove(0, 20);
                        double a33 = Convert.ToDouble(A1);
                        double omega = -1 * Math.Atan(a32 / a33);
                        double phi = Math.Asin(a31);
                        double kappa = -1 * Math.Atan(a21 / a11);
                        omega = omega * 180 / Math.PI;
                        phi = phi * 180 / Math.PI;
                        kappa = kappa * 180 / Math.PI;
                        zawartosct[i] = wiersz + "  " + xyz + "   " + omega.ToString() + "   " + phi.ToString() + "   " + kappa.ToString();
                        ++i;
                    }
                }
                string tPlik = plikPrj.Remove(plikPrj.Length - 4) + "_XYZOPK.txt";
                if (File.Exists(tPlik) == true)
                {
                    File.Delete(tPlik);
                }
                File.WriteAllLines(tPlik, zawartosct);
            }
        }

        private void TextBox70_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".prj" || roz == ".PRJ")
                    {
                        textBox70.Text = files[0];
                    }
                    else
                    {
                        textBox70.Text = "";
                    }
                }
            }
        }

        private void TextBox70_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //-----Prj_v7.1.0._2_Prj_v5.0.0-------------------------------------------------------------------

        private void Button55_Click(object sender, EventArgs e)
        {
            plikPrj = textBox70.Text;
            if (File.Exists(plikPrj) == false)
            {
                MessageBox.Show("Brak Pliku Prj !");
            }
            else
            {
                SumaPLIK = 0;
                SumaPLIKF = 0;
                wTablica = 0;
                int no = 0;
                string[] zawartosc = File.ReadAllLines(plikPrj);
                foreach (string iLinia in zawartosc)
                {
                    ++SumaPLIK;
                    int nr = 0;
                    nr = iLinia.IndexOf("$PROJECT 7");
                    if (nr > -1)
                    {
                        SumaPLIKF = 7;
                    }
                    nr = iLinia.IndexOf("$PROJECT 9");
                    if (nr > -1)
                    {
                        SumaPLIKF = 9;
                    }
                    nr = iLinia.IndexOf("$COORDINATE_SYSTEM :");
                    if (nr > -1)
                    {
                        no = 1;
                    }
                    if (no == 1)
                    {
                        ++wTablica;
                    }
                    nr = iLinia.IndexOf("$END_COORDINATE_SYSTEM");
                    if (nr > -1)
                    {
                        no = 0;
                    }
                }
                if (SumaPLIKF == 0)
                {
                    MessageBox.Show("To nie jest plik projektowy INPHO w wersji 7 albo 9 !");
                }
                else
                {
                    if (SumaPLIKF == 7)
                    {
                        MessageBox.Show("Poprawnie przetwarza projekty z kamerami bez PPS (PPA only) ! \r\n Jak jest PPS to go usuń i wprowadź w pliku po przetworzeniu.");
                        Prjv7_2_Prjv5();
                    }
                    else
                    {
                        MessageBox.Show("Poprawnie przetwarza projekty z kamerami bez PPS (PPA only) ! \r\n Jak jest PPS to go usuń i wprowadź w pliku po przetworzeniu.");
                        SumaPLIK = SumaPLIK * 2;
                        Prjv9_2_Prjv5();
                    }
                }
            }
        }

        private void Prjv7_2_Prjv5()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button52.Enabled = false;
            button53.Enabled = false;
            button55.Enabled = false;
            button56.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_Prjv7_2_Prjv5_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_Prj_2_Prjv5_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void Prjv9_2_Prjv5()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button52.Enabled = false;
            button53.Enabled = false;
            button55.Enabled = false;
            button56.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_Prjv9_2_Prjv5_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_Prj_2_Prjv5_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_Prj_2_Prjv5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono !!! PAMIĘTAJ PRZEEDYTUJ KAMERĘ !!!");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button52.Enabled = true;
            button53.Enabled = true;
            button55.Enabled = true;
            button56.Enabled = true;
        }

        private void BackgroundWorker_Prjv7_2_Prjv5_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string plikNew = plikPrj.Remove(plikPrj.Length - 4) + "_5.0.prj";
            if (File.Exists(plikNew) == true)
            {
                File.Delete(plikNew);
            }
            StreamReader sr = new StreamReader(plikPrj);
            StreamWriter sw = new StreamWriter(plikNew);
            string iLinia;
            int no = 0;
            int nu = 0;
            int ni = 0;
            int nc = 0;
            int nd = 0;
            while ((iLinia = sr.ReadLine()) != null)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                no = iLinia.IndexOf("  $PHOTO_FOOTPRINT :");
                if (no > -1)
                {
                    nu = 1;
                }
                else
                {
                    if (nu == 1)
                    {
                        int nr = 0;
                        nr = iLinia.IndexOf("  $END_POINTS");
                        if (nr > -1)
                        {
                            nu = 0;
                        }
                    }
                    else
                    {
                        ni = iLinia.IndexOf("  $CALIBRATION_SET :");
                        if (ni > -1)
                        {
                            nc = 1;
                        }
                        else
                        {
                            if (nc == 1)
                            {
                                int nr = 0;
                                nr = iLinia.IndexOf("    $DATE :");
                                if (nr > -1)
                                {
                                    nd = 1;
                                }
                                if (nd == 1)
                                {
                                    nr = iLinia.IndexOf("  $END");
                                    if (nr > -1)
                                    {
                                        nc = 0;
                                        nd = 0;
                                    }
                                    else
                                    {
                                        sw.WriteLine(iLinia);
                                    }
                                }
                            }
                            else
                            {
                                int nr = 0;
                                nr = iLinia.IndexOf("$PROJECT 7");
                                if (nr > -1)
                                {
                                    iLinia = "$PROJECT 5.0.0";
                                }
                                nr = iLinia.IndexOf("ElementPhoto");
                                if (nr > -1)
                                {
                                    iLinia = iLinia.Remove(nr, 13);
                                }
                                nr = iLinia.IndexOf("$CAMERA_DEFINITION");
                                if (nr > -1)
                                {
                                    iLinia = "$CAMERA";
                                }
                                nr = iLinia.IndexOf("  $ID :");
                                if (nr > -1)
                                {
                                    iLinia = iLinia.Replace("$ID", "$TYPE");
                                }
                                sw.WriteLine(iLinia);
                            }
                        }
                    }
                }
            }
            sw.Close();
            sr.Close();
        }

        private void BackgroundWorker_Prjv9_2_Prjv5_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string plikTmp = plikPrj.Remove(plikPrj.Length - 4) + "_Tmp.prj";
            if (File.Exists(plikTmp) == true)
            {
                File.Delete(plikTmp);
            }
            StreamReader sr = new StreamReader(plikPrj);
            StreamWriter sw = new StreamWriter(plikTmp);
            string[] uklad = new string[wTablica];
            string iLinia;
            string conPT = "";
            int nu = 0;
            int nc = 0;
            int nd = 0;
            int np = 0;
            int ns = 0;
            int n = 0;
            while ((iLinia = sr.ReadLine()) != null)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                if (np == 1)
                {
                    int nr = iLinia.IndexOf("$ID");
                    if (nr > -1)
                    {
                        conPT = iLinia.Remove(0, 8);
                    }
                    nr = iLinia.IndexOf("$POSITION");
                    if (nr > -1)
                    {
                        conPT = conPT + iLinia.Remove(0, 15);
                    }
                    nr = iLinia.IndexOf("$TYPE");
                    if (nr > -1)
                    {
                        conPT = conPT + iLinia.Remove(0, 11) + " 0  0  0";
                    }
                    nr = iLinia.IndexOf("$ACTIVE");
                    if (nr > -1)
                    {
                        nr = iLinia.IndexOf("on");
                        if (nr > -1)
                        {
                            sw.WriteLine(conPT);
                        }
                        else
                        {
                            conPT = conPT + " *";
                            sw.WriteLine(conPT);
                        }
                    }
                    nr = iLinia.IndexOf("$END_POINTS");
                    if (nr > -1)
                    {
                        np = 0;
                        sw.WriteLine(iLinia);
                    }
                    nr = iLinia.IndexOf("$PHOTO_NUM");
                    if (nr > -1)
                    {
                        sw.WriteLine(iLinia);
                    }
                    nr = iLinia.IndexOf("$GPS");
                    if (nr > -1)
                    {
                        sw.WriteLine(iLinia);
                    }
                    nr = iLinia.IndexOf("$INS");
                    if (nr > -1)
                    {
                        sw.WriteLine(iLinia);
                    }
                    nr = iLinia.IndexOf("$ENDNAV");
                    if (nr > -1)
                    {
                        np = 0;
                        sw.WriteLine(iLinia);
                    }
                }
                else
                {
                    int nr = iLinia.IndexOf("  $PHOTO_FOOTPRINT :");
                    int na = iLinia.IndexOf("$NAVIGATION_PAR");
                    int nb = iLinia.IndexOf("$COORDINATE_SYSTEM_SET");
                    if ((nr > -1) || (na > -1) || (nb > -1))
                    {
                        nu = 1;
                    }
                    else
                    {
                        if (nu == 1)
                        {
                            nr = iLinia.IndexOf("  $END_POINTS");
                            na = iLinia.IndexOf("$END");
                            nb = iLinia.IndexOf("$END_COORDINATE_SYSTEM");
                            if ((nr > -1) || (na > -1) || (nb > -1))
                            {
                                nu = 0;
                                ns = 0;
                            }
                            else
                            {
                                if (ns == 0)
                                {
                                    nr = iLinia.IndexOf("$COORDINATE_SYSTEM :");
                                    if (nr > 1)
                                    {
                                        ns = 1;
                                    }
                                }
                                else
                                {
                                    ++n;
                                    uklad[n] = iLinia;
                                }
                            }
                        }
                        else
                        {
                            nr = iLinia.IndexOf("  $CALIBRATION_SET :");
                            if (nr > -1)
                            {
                                nc = 1;
                            }
                            else
                            {
                                if (nc == 1)
                                {
                                    nr = iLinia.IndexOf("    $DATE :");
                                    if (nr > -1)
                                    {
                                        nd = 1;
                                    }
                                    if (nd == 1)
                                    {
                                        nr = iLinia.IndexOf("  $END");
                                        if (nr > -1)
                                        {
                                            nc = 0;
                                            nd = 0;
                                        }
                                        else
                                        {
                                            sw.WriteLine(iLinia);
                                        }
                                    }
                                }
                                else
                                {
                                    nr = iLinia.IndexOf("$PROJECT 9");
                                    if (nr > -1)
                                    {
                                        iLinia = "$PROJECT 5.0.0";
                                    }
                                    nr = iLinia.IndexOf("ElementPhoto");
                                    if (nr > -1)
                                    {
                                        int nr2 = 0;
                                        nr2 = iLinia.IndexOf("{");
                                        iLinia = iLinia.Remove(nr, nr2 - nr);
                                        iLinia = iLinia.Insert(nr, "0.00   0.00   0.00   0.00   0.00 ");
                                    }
                                    nr = iLinia.IndexOf("$CAMERA_DEFINITION");
                                    if (nr > -1)
                                    {
                                        iLinia = "$CAMERA";
                                    }
                                    nr = iLinia.IndexOf("  $ID :");
                                    if (nr > -1)
                                    {
                                        iLinia = iLinia.Replace("$ID", "$TYPE");
                                    }
                                    nr = iLinia.IndexOf("$CONTROL_POINTS");
                                    na = iLinia.IndexOf("$NAVIGATION");
                                    if ((nr > -1) || (na > -1))
                                    {
                                        np = 1;
                                    }
                                    sw.WriteLine(iLinia);
                                }
                            }
                        }
                    }
                }
            }
            sw.Close();
            sr.Close();
            string plikNew = plikPrj.Remove(plikPrj.Length - 4) + "_5.0.prj";
            if (File.Exists(plikNew) == true)
            {
                File.Delete(plikNew);
            }
            sr = new StreamReader(plikTmp);
            sw = new StreamWriter(plikNew);
            while ((iLinia = sr.ReadLine()) != null)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                int nr = iLinia.IndexOf("$INTERMEDIATE_COORD_SYSTEM");
                if (nr > -1)
                {
                    iLinia = "  $COORDINATE_SYSTEM :";
                    sw.WriteLine(iLinia);
                    foreach (string i in uklad)
                    {
                        sw.WriteLine(i);
                    }
                }
                else
                {
                    nr = iLinia.IndexOf("$DELIVERABLES_COORD_SYSTEM");
                    if (nr > -1)
                    {
                    }
                    else
                    {
                        sw.WriteLine(iLinia);
                    }
                }
            }
            sw.Close();
            sr.Close();
            File.Delete(plikTmp);
        }

        //-------------INPHO_2_DEPHOS---------------------------------------------------------------------

        private void Button56_Click(object sender, EventArgs e)
        {
            plikPrj = textBox70.Text;
            if (File.Exists(plikPrj) == false)
            {
                MessageBox.Show("Brak Pliku Prj !");
            }
            else
            {
                SumaPLIK = 0;
                SumaPLIKF = 0;
                string[] zawartosc = File.ReadAllLines(plikPrj);
                foreach (string iLinia in zawartosc)
                {
                    ++SumaPLIK;
                    int nr = 0;
                    nr = iLinia.IndexOf("$PROJECT");
                    if (nr > -1)
                    {
                        SumaPLIKF = 1;
                    }
                }
                if (SumaPLIKF == 0)
                {
                    MessageBox.Show("To nie jest plik projektowy INPHO !");
                }
                else
                {
                    INPHO_2_DEPHOS();
                }
            }
        }

        private void INPHO_2_DEPHOS()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            //            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button52.Enabled = false;
            button53.Enabled = false;
            button55.Enabled = false;
            button56.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_INPHO_2_DEPHOS_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_INPHO_2_DEPHOS_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }
        private void BackgroundWorker_INPHO_2_DEPHOS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button52.Enabled = true;
            button53.Enabled = true;
            button55.Enabled = true;
            button56.Enabled = true;
        }

        private void BackgroundWorker_INPHO_2_DEPHOS_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string plikfcm = "";
            string plikfct = "";
            string plikfmc = "";
            string plikfmd = "";
            string plikfmt = "";
            string plikfpr = "";
            string plikfpt = "";
            int sumaKamer = 0;
            int sumaSzeregow = 0;
            int sumaZdjec = 0;
            int nr = 0;
            nr = plikPrj.LastIndexOf(@"\");
            if (nr > -1)
            {
                sciezka1 = plikPrj.Remove(nr + 1);
                sciezka2 = sciezka1 + @"DEPHOS\";
                if (Directory.Exists(sciezka2))
                {
                    string[] Pliki = Directory.GetFiles(sciezka2);
                    foreach (string iPlik in Pliki)
                    {
                        File.Delete(iPlik);
                    }
                    Directory.Delete(sciezka2);
                }
                Directory.CreateDirectory(sciezka2);
                string Prj = plikPrj.Remove(0, nr + 1);
                plikfcm = sciezka2 + Prj.Remove(Prj.Length - 4) + ".fcm";
                plikfct = sciezka2 + Prj.Remove(Prj.Length - 4) + ".fct";
                plikfmc = sciezka2 + Prj.Remove(Prj.Length - 4) + ".fmc";
                File.Create(plikfmc);
                plikfmd = sciezka2 + Prj.Remove(Prj.Length - 4) + ".fmd";
                plikfmt = sciezka2 + Prj.Remove(Prj.Length - 4) + ".fmt";
                File.Create(plikfmt);
                plikfpr = sciezka2 + Prj.Remove(Prj.Length - 4) + ".fpr";
                plikfpt = sciezka2 + Prj.Remove(Prj.Length - 4) + ".fpt";
            }
            StreamReader srfpr = new StreamReader(plikPrj);
            StreamWriter swfpr = new StreamWriter(plikfpr);
            string FH = "";
            string ATE = "";
            string ER = "";
            string AC = "";
            string CC = "";
            string LU = "";
            string AU = "";
            string iLinia;
            int no = 0;
            int nu = 0;
            int ns = 0;
            while (((iLinia = srfpr.ReadLine()) != null) & (nu != 2))
            {
                nr = iLinia.IndexOf("  $REFRACT_CORR_DEFAULT : ");
                if (nr > -1)
                {
                    AC = iLinia.Remove(0, 26);
                }
                nr = iLinia.IndexOf("  $CURV_CORR_DEFAULT : ");
                if (nr > -1)
                {
                    CC = iLinia.Remove(0, 23);
                }
                nr = iLinia.IndexOf("         UNIT[");
                if (nr > -1)
                {
                    LU = iLinia.Remove(0, 15);
                    LU = LU.Remove(1);
                }
                nr = iLinia.IndexOf("  $OBJECT_TO_METER : ");
                if (nr > -1)
                {
                    ER = iLinia.Remove(0, 21);
                    decimal ERd = Convert.ToDecimal(ER);
                    ERd = 6378000 / ERd;
                    ER = Convert.ToString(ERd);
                    nr = ER.IndexOf(".");
                    if (nr > -1)
                    {
                        ER = ER.Remove(nr);
                    }
                }
                nr = iLinia.IndexOf("  $ANGULAR_UNITS : ");
                if (nr > -1)
                {
                    AU = iLinia.Remove(0, 19);
                    AU = AU.Remove(1);
                }
                if (no == 0)
                {
                    nr = iLinia.IndexOf("  $TERRAIN_HEIGHT : ");
                    if (nr > -1)
                    {
                        ATE = iLinia.Remove(0, 20);
                        no = 1;
                        nr = ATE.IndexOf(".");
                        if (nr > -1)
                        {
                            ATE = ATE.Remove(nr);
                        }
                    }
                }
                if (nu == 1)
                {
                    nr = iLinia.LastIndexOf(" ");
                    if (nr > -1)
                    {
                        FH = iLinia.Remove(0, nr + 1);
                        nu = 2;
                        nr = FH.IndexOf(".");
                        if (nr > -1)
                        {
                            FH = FH.Remove(nr);
                        }
                    }
                }
                if (nu == 0)
                {
                    nr = iLinia.IndexOf("  $EXT_ORI :");
                    if (nr > -1)
                    {
                        nu = 1;
                    }
                }
            }
            if (ER == "")
            {
                ER = "6378000";
            }
            if (LU == "m")
            {
                LU = "meters";
            }
            if (LU == "f")
            {
                LU = "feet";
            }
            if (AU == "d")
            {
                AU = "degrees";
            }
            if (AU == "g")
            {
                AU = "grads";
            }
            if (AU == "r")
            {
                AU = "radians";
            }
            swfpr.WriteLine("[Project]");
            swfpr.WriteLine("Flight height=" + Convert.ToString(Convert.ToDecimal(FH) - Convert.ToDecimal(ATE)));
            swfpr.WriteLine("Average terrain elevation=" + ATE);
            swfpr.WriteLine("Earth radius=" + ER);
            swfpr.WriteLine("Atmospheric correction=" + AC);
            swfpr.WriteLine("Curvature correction=" + CC);
            swfpr.WriteLine("Linear units=" + LU);
            swfpr.WriteLine("Angular units=" + AU);
            swfpr.WriteLine("Area units=square " + LU);
            swfpr.WriteLine("Width Coverage %=");
            swfpr.WriteLine("Height Coverage %=");
            swfpr.WriteLine("Image Size=");
            swfpr.WriteLine("Photos Scale=");
            swfpr.WriteLine("Pixel Size=");
            swfpr.WriteLine("Photo Area=");
            swfpr.WriteLine("Oryginal=");
            swfpr.Close();
            StreamReader srfct = new StreamReader(plikPrj);
            StreamWriter swfct = new StreamWriter(plikfct);
            string sdxy = "";
            string sdz = "";
            string kl = "";
            string NrPkt = "";
            string Z = "";
            string X = "";
            string Y = "";
            no = 0;
            nu = 0;
            ns = 0;
            swfct.WriteLine("[Controls]");
            while ((iLinia = srfct.ReadLine()) != null)
            {
                nr = iLinia.IndexOf("    $FOCAL_LENGTH :");
                if (nr > -1)
                {
                    ++sumaKamer;
                }
                nr = iLinia.IndexOf("  $PHOTO_FILE :");
                if (nr > -1)
                {
                    ++sumaZdjec;
                }
                nr = iLinia.IndexOf("  $STD_DEV_OBJECT_POINTS : ");
                if (nr > -1)
                {
                    sdxy = iLinia.Remove(0, 27);
                }
                nr = iLinia.IndexOf("  $STD_DEV_OBJECT_Z_POINTS : ");
                if (nr > -1)
                {
                    sdz = iLinia.Remove(0, 29);
                }
                if (ns == 1)
                {
                    nr = iLinia.IndexOf("  $END_STRIPS");
                    if (nr > -1)
                    {
                        ns = 0;
                    }
                    else
                    {
                        nr = iLinia.IndexOf(" { ");
                        if (nr > -1)
                        {
                            ++sumaSzeregow;
                        }
                    }
                }
                if (ns == 0)
                {
                    nr = iLinia.IndexOf("  $STRIPS :");
                    if (nr > -1)
                    {
                        ns = 1;
                    }
                }
                if (nu == 1)
                {
                    nr = iLinia.IndexOf("$END_POINTS");
                    if (nr > -1)
                    {
                        nu = 0;
                    }
                    else
                    {
                        nr = iLinia.LastIndexOf(" ");
                        if (nr > -1)
                        {
                            iLinia = iLinia.Remove(nr);
                            iLinia = iLinia.Trim();
                            nr = iLinia.LastIndexOf(" ");
                            if (nr > -1)
                            {
                                iLinia = iLinia.Remove(nr);
                                iLinia = iLinia.Trim();
                                nr = iLinia.LastIndexOf(" ");
                                if (nr > -1)
                                {
                                    iLinia = iLinia.Remove(nr);
                                    iLinia = iLinia.Trim();
                                    nr = iLinia.LastIndexOf(" ");
                                    if (nr > -1)
                                    {
                                        kl = iLinia.Remove(0, nr + 1);
                                        iLinia = iLinia.Remove(nr);
                                        iLinia = iLinia.Trim();
                                        if (kl == "1")
                                        {
                                            kl = "4";
                                        }
                                        if (kl == "2")
                                        {
                                            kl = "2";
                                        }
                                        if (kl == "3")
                                        {
                                            kl = "1";
                                        }
                                        if (kl == "-3")
                                        {
                                            kl = "9";
                                        }
                                        if (kl == "-2")
                                        {
                                            kl = "10";
                                        }
                                        if (kl == "-1")
                                        {
                                            kl = "12";
                                        }
                                        nr = iLinia.LastIndexOf(" ");
                                        if (nr > -1)
                                        {
                                            Z = iLinia.Remove(0, nr + 1);
                                            iLinia = iLinia.Remove(nr);
                                            iLinia = iLinia.Trim();
                                            nr = iLinia.LastIndexOf(" ");
                                            if (nr > -1)
                                            {
                                                Y = iLinia.Remove(0, nr + 1);
                                                iLinia = iLinia.Remove(nr);
                                                iLinia = iLinia.Trim();
                                                nr = iLinia.LastIndexOf(" ");
                                                if (nr > -1)
                                                {
                                                    X = iLinia.Remove(0, nr + 1);
                                                    iLinia = iLinia.Remove(nr);
                                                    NrPkt = iLinia.Trim();
                                                    swfct.WriteLine(NrPkt + "=" + X + ";" + Y + ";" + Z + ";" + sdxy + ";" + sdxy + ";" + sdz + ";" + kl);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (nu == 0)
                {
                    nr = iLinia.IndexOf("$CONTROL_POINTS");
                    if (nr > -1)
                    {
                        nu = 1;
                    }
                }
            }
            swfct.Close();
            StreamReader srfcm = new StreamReader(plikPrj);
            StreamWriter swfcm = new StreamWriter(plikfcm);
            string CN = "";
            string FL = "";
            string CCDC = "";
            string CCDR = "";
            X = "";
            Y = "";
            string XY = "";
            string NrSzeregu = "";
            string[,] Kamery = new string[sumaKamer, 6];
            string[,] Zdjecia = new string[sumaZdjec, 19];
            string[] Szeregi = new string[sumaSzeregow];
            no = 0;
            nu = 0;
            ns = 0;
            int iKamera = 0;
            int iSzereg = 0;
            int iZdjecia = 0;
            while ((iLinia = srfcm.ReadLine()) != null)
            {
                if (ns == 1)
                {
                    nr = iLinia.IndexOf("  $END_STRIPS");
                    if (nr > -1)
                    {
                        ns = 0;
                    }
                    else
                    {
                        nr = iLinia.IndexOf("}");
                        if (nr > -1)
                        {
                            iLinia = iLinia.Remove(nr);
                            Szeregi[iSzereg] = Szeregi[iSzereg] + iLinia;
                            ++iSzereg;
                        }
                        else
                        {
                            nr = iLinia.IndexOf("{");
                            if (nr > -1)
                            {
                                string nLinia = iLinia.Remove(0, nr + 1);
                                iLinia = iLinia.Trim();
                                nr = iLinia.IndexOf(" ");
                                if (nr > -1)
                                {
                                    NrSzeregu = iLinia.Remove(nr);
                                    iLinia = iLinia.Remove(0, nr + 1);
                                    iLinia = iLinia.Trim();
                                    nr = iLinia.IndexOf("ElementPhoto");
                                    if (nr > -1)
                                    {
                                        iLinia = iLinia.Remove(0, 13);
                                        iLinia = iLinia.Trim();
                                    }
                                    nr = iLinia.IndexOf(" ");
                                    if (nr > -1)
                                    {
                                        string KatSzeregu = iLinia.Remove(nr);
                                        Szeregi[iSzereg] = NrSzeregu + " " + KatSzeregu + " " + nLinia;
                                    }
                                }
                            }
                            else
                            {
                                Szeregi[iSzereg] = Szeregi[iSzereg] + iLinia;
                            }
                        }
                    }
                }
                if (ns == 0)
                {
                    nr = iLinia.IndexOf("  $STRIPS :");
                    if (nr > -1)
                    {
                        ns = 1;
                    }
                }

                if (nu == 1)
                {
                    nr = iLinia.LastIndexOf(" ");
                    if (nr > -1)
                    {
                        nu = 2;
                        CN = iLinia.Remove(0, nr + 1);
                        swfcm.WriteLine("[" + CN + "]");
                    }
                }
                if (nu == 2)
                {
                    nr = iLinia.IndexOf("  $CCD_COLUMNS : ");
                    if (nr > -1)
                    {
                        nr = iLinia.LastIndexOf(" ");
                        if (nr > -1)
                        {
                            CCDC = iLinia.Remove(0, 17);
                        }
                    }
                    nr = iLinia.IndexOf("  $CCD_ROWS : ");
                    if (nr > -1)
                    {
                        nu = 0;
                        nr = iLinia.LastIndexOf(" ");
                        if (nr > -1)
                        {
                            CCDR = iLinia.Remove(0, 14);
                        }
                    }
                }
                if (nu == 0)
                {
                    nr = iLinia.IndexOf("$CAMERA_DEFINITION");
                    if (nr > -1)
                    {
                        nu = 1;
                    }
                    nr = iLinia.IndexOf("$CAMERA");
                    if (nr > -1)
                    {
                        nr = iLinia.IndexOf(" ");
                        if (nr > -1)
                        {
                        }
                        else
                        {
                            nu = 1;
                        }
                    }
                }
                if (no == 2)
                {
                    no = 0;
                    nr = iLinia.LastIndexOf(" ");
                    if (nr > -1)
                    {
                        Y = iLinia.Remove(0, nr + 1);
                    }
                }
                if (no == 1)
                {
                    no = 2;
                    nr = iLinia.LastIndexOf(" ");
                    if (nr > -1)
                    {
                        X = iLinia.Remove(0, nr + 1);
                        XY = iLinia.Remove(nr);
                        XY = XY.Trim();
                        nr = XY.LastIndexOf(" ");
                        if (nr > -1)
                        {
                            XY = XY.Remove(nr);
                            XY = XY.Trim();
                        }
                    }
                }
                if (no == 0)
                {
                    nr = iLinia.IndexOf("    $CCD_INTERIOR_ORIENTATION :");
                    if (nr > -1)
                    {
                        no = 1;
                    }
                }
                nr = iLinia.IndexOf("    $FOCAL_LENGTH :");
                if (nr > -1)
                {
                    nr = iLinia.LastIndexOf(" ");
                    if (nr > -1)
                    {
                        FL = iLinia.Remove(0, nr + 1);
                        swfcm.WriteLine("Focal length=" + FL);
                        swfcm.WriteLine("Camera type=frame");
                        swfcm.WriteLine("Media type=digital");
                        decimal dX = Convert.ToDecimal(X);
                        decimal dY = Convert.ToDecimal(Y);
                        decimal dXY = Convert.ToDecimal(XY);
                        decimal dCCDC = Convert.ToDecimal(CCDC);
                        decimal dCCDR = Convert.ToDecimal(CCDR);
                        decimal dPPAX = (dX / dXY) - ((dCCDC - 1) / dXY / 2);
                        decimal dPPAY = ((dCCDR - 1) / dXY / 2) - (dY / dXY);
                        swfcm.WriteLine("PPAC X=" + Convert.ToString(dPPAX));
                        swfcm.WriteLine("PPAC Y=" + Convert.ToString(dPPAY));
                        swfcm.WriteLine("PPBS X=0");
                        swfcm.WriteLine("PPBS Y=0");
                        swfcm.WriteLine("Film format X=" + Convert.ToString(dCCDC / dXY));
                        swfcm.WriteLine("Film format Y=" + Convert.ToString(dCCDR / dXY));
                        swfcm.WriteLine("Pixel size=" + Convert.ToString(1 / dXY));
                        string X0 = Convert.ToString(dX / dXY);
                        string Y0 = Convert.ToString(dY / dXY);
                        string X1 = Convert.ToString(((dCCDC - 1) / dXY) - (dX / dXY));
                        string Y1 = Convert.ToString(((dCCDR - 1) / dXY) - (dY / dXY));
                        swfcm.WriteLine("Fiducials=3x4,TopLeft,-" + X0 + "," + Y0 + ",TopRight," + X1 + "," + Y0 + ",BottomRight," + X1 + ",-" + Y1 + ",BottomLeft,-" + X0 + ",-" + Y1);
                        swfcm.WriteLine("Distortion correction=off");
                        swfcm.WriteLine("Normal Radius=");
                        swfcm.WriteLine(@"Distortion data=""2x6""");
                        swfcm.WriteLine("Distortion values=linear");
                        swfcm.WriteLine("Dist. coefficient K0=");
                        swfcm.WriteLine("Dist. coefficient K1=");
                        swfcm.WriteLine("Dist. coefficient K2=");
                        swfcm.WriteLine("Dist. coefficient K3=");
                        swfcm.WriteLine("Dist. coefficient P1=");
                        swfcm.WriteLine("Dist. coefficient P2=");
                        swfcm.WriteLine("Dist. coefficient P3=");
                        swfcm.WriteLine("Dist. coefficient Af1=");
                        swfcm.WriteLine("Dist. coefficient Af2=");
                        swfcm.WriteLine("Affine deformation axis=Y");
                        swfcm.WriteLine("Oryginal=");
                        swfcm.WriteLine("");
                        Kamery[iKamera, 0] = CN;
                        Kamery[iKamera, 1] = Convert.ToString(1 / dXY);
                        Kamery[iKamera, 2] = X0;
                        Kamery[iKamera, 3] = Y0;
                        Kamery[iKamera, 4] = CCDC;
                        Kamery[iKamera, 5] = CCDR;
                        ++iKamera;
                    }
                }
            }
            swfcm.Close();
            StreamReader srfpt = new StreamReader(plikPrj);
            StreamWriter swfpt = new StreamWriter(plikfpt);
            NrSzeregu = "";
            foreach (string sLinia in Szeregi)
            {
                nr = sLinia.IndexOf(" ");
                if (nr > -1)
                {
                    NrSzeregu = sLinia.Remove(nr);
                    string zdjecia = sLinia.Remove(0, nr + 1);
                    zdjecia = zdjecia.TrimStart(spacja);
                    nr = zdjecia.IndexOf(" ");
                    if (nr > -1)
                    {
                        string KatSzeregu = zdjecia.Remove(nr);
                        zdjecia = zdjecia.Remove(0, nr + 1);
                        zdjecia = zdjecia.TrimStart(spacja);
                        while (zdjecia != null)
                        {
                            i = zdjecia.IndexOf(" ");
                            if (i > -1)
                            {
                                string Nzdj = zdjecia.Remove(i);
                                zdjecia = zdjecia.Remove(0, i + 1);
                                zdjecia = zdjecia.TrimStart(spacja);
                                Zdjecia[iZdjecia, 0] = Nzdj;
                                Zdjecia[iZdjecia, 1] = NrSzeregu;
                                Zdjecia[iZdjecia, 18] = KatSzeregu;
                                ++iZdjecia;
                            }
                            else
                            {
                                zdjecia = null;
                            }
                        }
                    }
                }
            }
            iZdjecia = 0;
            SumaPLIK = sumaZdjec;
            progressBar1.Maximum = SumaPLIK;
            progressBarVal = 0;
            for (iZdjecia = 0; iZdjecia < sumaZdjec; iZdjecia++)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                no = 0;
                ns = 0;
                string[] zawartosc = File.ReadAllLines(plikPrj); ;
                foreach (string sLinia in zawartosc)
                {
                    ++no;
                    nr = 0;
                    if (ns == 0 & Zdjecia[iZdjecia, 0] != null)
                    {
                        nr = sLinia.IndexOf("$PHOTO_NUM : " + Zdjecia[iZdjecia, 0]);
                        if (nr > -1)
                        {
                            ns = 1;
                        }
                    }
                    if (ns == 1)
                    {
                        nr = sLinia.IndexOf("$PHOTO_FILE : ");
                        if (nr > -1)
                        {
                            Zdjecia[iZdjecia, 2] = sLinia.Remove(0, 16);
                        }
                        nr = sLinia.IndexOf("$CAMERA_ID : ");
                        if (nr > -1)
                        {
                            Zdjecia[iZdjecia, 3] = sLinia.Remove(0, 15);
                        }
                        nr = sLinia.IndexOf("  $EXT_ORI : ");
                        if (nr > -1)
                        {
                            string xyz = zawartosc[no];
                            xyz = xyz.TrimStart(spacja);
                            i = xyz.IndexOf(" ");
                            xyz = xyz.Remove(0, i);
                            xyz = xyz.TrimStart(spacja);
                            i = xyz.IndexOf(" ");
                            Zdjecia[iZdjecia, 4] = xyz.Remove(i);
                            xyz = xyz.Remove(0, i);
                            xyz = xyz.TrimStart(spacja);
                            i = xyz.IndexOf(" ");
                            Zdjecia[iZdjecia, 5] = xyz.Remove(i);
                            xyz = xyz.Remove(0, i);
                            xyz = xyz.TrimStart(spacja);
                            Zdjecia[iZdjecia, 6] = xyz;
                            string A = zawartosc[no + 1];
                            A = A.Remove(24);
                            double a11 = Convert.ToDouble(A);
                            A = zawartosc[no + 2];
                            A = A.Remove(24);
                            double a21 = Convert.ToDouble(A);
                            A = zawartosc[no + 3];
                            string A1 = A.Remove(24);
                            double a31 = Convert.ToDouble(A1);
                            A = A.Remove(0, 24);
                            A1 = A.Remove(20);
                            double a32 = Convert.ToDouble(A1);
                            A1 = A.Remove(0, 20);
                            double a33 = Convert.ToDouble(A1);
                            double omega = -1 * Math.Atan(a32 / a33);
                            double phi = Math.Asin(a31);
                            double kappa = -1 * Math.Atan(a21 / a11);
                            omega = omega * 180 / Math.PI;
                            phi = phi * 180 / Math.PI;
                            kappa = kappa * 180 / Math.PI;
                            A = zawartosc[no + 5];
                            A = A.TrimStart(spacja);
                            i = A.IndexOf(" ");
                            string X1 = A.Remove(i);
                            A = A.Remove(0, i);
                            A = A.TrimStart(spacja);
                            i = A.IndexOf(" ");
                            string Y1 = A.Remove(i);
                            A = zawartosc[no + 6];
                            A = A.TrimStart(spacja);
                            i = A.IndexOf(" ");
                            string X2 = A.Remove(i);
                            A = A.Remove(0, i);
                            A = A.TrimStart(spacja);
                            i = A.IndexOf(" ");
                            string Y2 = A.Remove(i);
                            A = zawartosc[no + 7];
                            A = A.TrimStart(spacja);
                            i = A.IndexOf(" ");
                            string X3 = A.Remove(i);
                            A = A.Remove(0, i);
                            A = A.TrimStart(spacja);
                            i = A.IndexOf(" ");
                            string Y3 = A.Remove(i);
                            A = zawartosc[no + 8];
                            A = A.TrimStart(spacja);
                            i = A.IndexOf(" ");
                            string X4 = A.Remove(i);
                            A = A.Remove(0, i);
                            A = A.TrimStart(spacja);
                            i = A.IndexOf(" ");
                            string Y4 = A.Remove(i);
                            if ((Convert.ToDouble(Zdjecia[iZdjecia, 4]) - Convert.ToDouble(X1) > 0) && (Convert.ToDouble(Zdjecia[iZdjecia, 5]) - Convert.ToDouble(Y1) < 0))
                            {
                                Zdjecia[iZdjecia, 10] = X1;
                                Zdjecia[iZdjecia, 11] = Y1;
                            }
                            else
                            {
                                if ((Convert.ToDouble(Zdjecia[iZdjecia, 4]) - Convert.ToDouble(X2) > 0) && (Convert.ToDouble(Zdjecia[iZdjecia, 5]) - Convert.ToDouble(Y2) < 0))
                                {
                                    Zdjecia[iZdjecia, 10] = X2;
                                    Zdjecia[iZdjecia, 11] = Y2;
                                }
                                else
                                {
                                    if ((Convert.ToDouble(Zdjecia[iZdjecia, 4]) - Convert.ToDouble(X3) > 0) && (Convert.ToDouble(Zdjecia[iZdjecia, 5]) - Convert.ToDouble(Y3) < 0))
                                    {
                                        Zdjecia[iZdjecia, 10] = X3;
                                        Zdjecia[iZdjecia, 11] = Y3;
                                    }
                                    else
                                    {
                                        Zdjecia[iZdjecia, 10] = X4;
                                        Zdjecia[iZdjecia, 11] = Y4;
                                    }
                                }
                            }
                            if ((Convert.ToDouble(Zdjecia[iZdjecia, 4]) - Convert.ToDouble(X1) < 0) && (Convert.ToDouble(Zdjecia[iZdjecia, 5]) - Convert.ToDouble(Y1) < 0))
                            {
                                Zdjecia[iZdjecia, 12] = X1;
                                Zdjecia[iZdjecia, 13] = Y1;
                            }
                            else
                            {
                                if ((Convert.ToDouble(Zdjecia[iZdjecia, 4]) - Convert.ToDouble(X2) < 0) && (Convert.ToDouble(Zdjecia[iZdjecia, 5]) - Convert.ToDouble(Y2) < 0))
                                {
                                    Zdjecia[iZdjecia, 12] = X2;
                                    Zdjecia[iZdjecia, 13] = Y2;
                                }
                                else
                                {
                                    if ((Convert.ToDouble(Zdjecia[iZdjecia, 4]) - Convert.ToDouble(X3) < 0) && (Convert.ToDouble(Zdjecia[iZdjecia, 5]) - Convert.ToDouble(Y3) < 0))
                                    {
                                        Zdjecia[iZdjecia, 12] = X3;
                                        Zdjecia[iZdjecia, 13] = Y3;
                                    }
                                    else
                                    {
                                        Zdjecia[iZdjecia, 12] = X4;
                                        Zdjecia[iZdjecia, 13] = Y4;
                                    }
                                }
                            }
                            if ((Convert.ToDouble(Zdjecia[iZdjecia, 4]) - Convert.ToDouble(X1) < 0) && (Convert.ToDouble(Zdjecia[iZdjecia, 5]) - Convert.ToDouble(Y1) > 0))
                            {
                                Zdjecia[iZdjecia, 14] = X1;
                                Zdjecia[iZdjecia, 15] = Y1;
                            }
                            else
                            {
                                if ((Convert.ToDouble(Zdjecia[iZdjecia, 4]) - Convert.ToDouble(X2) < 0) && (Convert.ToDouble(Zdjecia[iZdjecia, 5]) - Convert.ToDouble(Y2) > 0))
                                {
                                    Zdjecia[iZdjecia, 14] = X2;
                                    Zdjecia[iZdjecia, 15] = Y2;
                                }
                                else
                                {
                                    if ((Convert.ToDouble(Zdjecia[iZdjecia, 4]) - Convert.ToDouble(X3) < 0) && (Convert.ToDouble(Zdjecia[iZdjecia, 5]) - Convert.ToDouble(Y3) > 0))
                                    {
                                        Zdjecia[iZdjecia, 14] = X3;
                                        Zdjecia[iZdjecia, 15] = Y3;
                                    }
                                    else
                                    {
                                        Zdjecia[iZdjecia, 14] = X4;
                                        Zdjecia[iZdjecia, 15] = Y4;
                                    }
                                }
                            }
                            if ((Convert.ToDouble(Zdjecia[iZdjecia, 4]) - Convert.ToDouble(X1) > 0) && (Convert.ToDouble(Zdjecia[iZdjecia, 5]) - Convert.ToDouble(Y1) > 0))
                            {
                                Zdjecia[iZdjecia, 16] = X1;
                                Zdjecia[iZdjecia, 17] = Y1;
                            }
                            else
                            {
                                if ((Convert.ToDouble(Zdjecia[iZdjecia, 4]) - Convert.ToDouble(X2) > 0) && (Convert.ToDouble(Zdjecia[iZdjecia, 5]) - Convert.ToDouble(Y2) > 0))
                                {
                                    Zdjecia[iZdjecia, 16] = X2;
                                    Zdjecia[iZdjecia, 17] = Y2;
                                }
                                else
                                {
                                    if ((Convert.ToDouble(Zdjecia[iZdjecia, 4]) - Convert.ToDouble(X3) > 0) && (Convert.ToDouble(Zdjecia[iZdjecia, 5]) - Convert.ToDouble(Y3) > 0))
                                    {
                                        Zdjecia[iZdjecia, 16] = X3;
                                        Zdjecia[iZdjecia, 17] = Y3;
                                    }
                                    else
                                    {
                                        Zdjecia[iZdjecia, 16] = X4;
                                        Zdjecia[iZdjecia, 17] = Y4;
                                    }
                                }
                            }
                            Zdjecia[iZdjecia, 7] = omega.ToString();
                            Zdjecia[iZdjecia, 8] = phi.ToString();
                            //                            Zdjecia[iZdjecia, 9] = kappa.ToString();
                            iKamera = 0;
                            for (iKamera = 0; iKamera < sumaKamer; iKamera++)
                            {
                                if (Zdjecia[iZdjecia, 3] == Kamery[iKamera, 0])
                                {
                                    double Azymut = Convert.ToDouble(Zdjecia[iZdjecia, 18]);
                                    double KappaAz = Azymut + kappa;
                                    if (Convert.ToDecimal(Kamery[iKamera, 4]) > Convert.ToDecimal(Kamery[iKamera, 5]))
                                    {
                                        if (KappaAz > 90)
                                        {
                                            kappa = kappa - 180;
                                        }
                                        if (KappaAz < -90)
                                        {
                                            kappa = kappa + 180;
                                        }
                                    }
                                    else
                                    {
                                        if (KappaAz < 0 & KappaAz > -180)
                                        {
                                            kappa = kappa + 180;
                                        }
                                    }
                                    Zdjecia[iZdjecia, 9] = kappa.ToString();
                                    swfpt.WriteLine("[" + Zdjecia[iZdjecia, 1] + "~" + Zdjecia[iZdjecia, 0] + "]");
                                    swfpt.WriteLine("Strip=" + Zdjecia[iZdjecia, 1]);
                                    swfpt.WriteLine("Camera Name=" + Zdjecia[iZdjecia, 3]);
                                    swfpt.WriteLine("Camera orientation=0");
                                    swfpt.WriteLine("Image File=" + Zdjecia[iZdjecia, 2]);
                                    swfpt.WriteLine("View geometry=nadir");
                                    swfpt.WriteLine("Internal Orientation type=AFFINE");
                                    swfpt.WriteLine("EO X=" + Zdjecia[iZdjecia, 4]);
                                    swfpt.WriteLine("EO Y=" + Zdjecia[iZdjecia, 5]);
                                    swfpt.WriteLine("EO Z=" + Zdjecia[iZdjecia, 6]);
                                    swfpt.WriteLine("EO Omega=" + Zdjecia[iZdjecia, 7]);
                                    swfpt.WriteLine("EO Fi=" + Zdjecia[iZdjecia, 8]);
                                    swfpt.WriteLine("EO Kappa=" + Zdjecia[iZdjecia, 9]);
                                    swfpt.WriteLine("Footprint=2x4," + Zdjecia[iZdjecia, 10] + "," + Zdjecia[iZdjecia, 11] + "," + Zdjecia[iZdjecia, 12] + "," + Zdjecia[iZdjecia, 13] + "," + Zdjecia[iZdjecia, 14] + "," + Zdjecia[iZdjecia, 15] + "," + Zdjecia[iZdjecia, 16] + "," + Zdjecia[iZdjecia, 17]);
                                    swfpt.WriteLine("Fiducials=");
                                    swfpt.WriteLine("IO Ax=-" + Kamery[iKamera, 2]);
                                    swfpt.WriteLine("IO Bx=" + Kamery[iKamera, 1]);
                                    swfpt.WriteLine("IO Cx=0");
                                    swfpt.WriteLine("IO Dx=0");
                                    swfpt.WriteLine("IO Ay=" + Kamery[iKamera, 3]);
                                    swfpt.WriteLine("IO By=0");
                                    swfpt.WriteLine("IO Cy=" + Kamery[iKamera, 1]);
                                    swfpt.WriteLine("IO Dy=0");
                                    swfpt.WriteLine("Invert=off");
                                    swfpt.WriteLine("Oryginal=");
                                    swfpt.WriteLine("");
                                }
                            }
                            ns = 0;
                        }
                    }
                }
            }
            swfpt.Close();
            StreamWriter swfmd = new StreamWriter(plikfmd);
            iZdjecia = 0;
            for (iZdjecia = 0; iZdjecia < (sumaZdjec - 1); iZdjecia++)
            {
                if (Zdjecia[iZdjecia, 1] == Zdjecia[iZdjecia + 1, 1] & Zdjecia[iZdjecia, 0] != null)
                {
                    swfmd.WriteLine("[" + Zdjecia[iZdjecia, 1] + "~" + Zdjecia[iZdjecia, 0] + "+" + Zdjecia[iZdjecia + 1, 1] + "~" + Zdjecia[iZdjecia + 1, 0] + "]");
                    swfmd.WriteLine("EO to use=EO");
                    swfmd.WriteLine("Left photo=" + Zdjecia[iZdjecia, 1] + "~" + Zdjecia[iZdjecia, 0]);
                    swfmd.WriteLine("Right photo=" + Zdjecia[iZdjecia + 1, 1] + "~" + Zdjecia[iZdjecia + 1, 0]);
                    double XC = (((Convert.ToDouble(Zdjecia[iZdjecia, 4])) + (Convert.ToDouble(Zdjecia[iZdjecia + 1, 4]))) / 2);
                    swfmd.WriteLine("Center X=" + XC);
                    double YC = (((Convert.ToDouble(Zdjecia[iZdjecia, 5])) + (Convert.ToDouble(Zdjecia[iZdjecia + 1, 5]))) / 2);
                    swfmd.WriteLine("Center Y=" + YC);
                    swfmd.WriteLine("Left AO X=" + Zdjecia[iZdjecia, 4]);
                    swfmd.WriteLine("Left AO Y=" + Zdjecia[iZdjecia, 5]);
                    swfmd.WriteLine("Left AO Z=" + Zdjecia[iZdjecia, 6]);
                    swfmd.WriteLine("Left AO Omega=" + Zdjecia[iZdjecia, 7]);
                    swfmd.WriteLine("Left AO Fi=" + Zdjecia[iZdjecia, 8]);
                    swfmd.WriteLine("Left AO Kappa=" + Zdjecia[iZdjecia, 9]);
                    swfmd.WriteLine("Right AO X=" + Zdjecia[iZdjecia + 1, 4]);
                    swfmd.WriteLine("Right AO Y=" + Zdjecia[iZdjecia + 1, 5]);
                    swfmd.WriteLine("Right AO Z=" + Zdjecia[iZdjecia + 1, 6]);
                    swfmd.WriteLine("Right AO Omega=" + Zdjecia[iZdjecia + 1, 7]);
                    swfmd.WriteLine("Right AO Fi=" + Zdjecia[iZdjecia + 1, 8]);
                    swfmd.WriteLine("Right AO Kappa=" + Zdjecia[iZdjecia + 1, 9]);
                    swfmd.WriteLine("");
                }
            }
            swfmd.Close();
        }

        //***************INPHO_END************************************************************************

        //---------------PIX4D----------------------------------------------------------------------------

        private void TextBox74_TextChanged(object sender, EventArgs e)
        {
            plikCam = textBox74.Text;
        }

        private void Button58_Click(object sender, EventArgs e)
        {
            if (openFileDialog4.ShowDialog() == DialogResult.OK)
            {
                textBox74.Text = openFileDialog4.FileName;
            }
        }

        private void TextBox75_TextChanged(object sender, EventArgs e)
        {
            Kamera = textBox75.Text;
        }

        private void Button59_Click(object sender, EventArgs e)
        {
            plikCam = textBox74.Text;
            if (File.Exists(plikCam) == false)
            {
                MessageBox.Show("Brak Pliku Cam !");
            }
            else
            {
                SumaPLIK = 0;
                string[] zawartosc = File.ReadAllLines(plikCam);
                foreach (string iLinia in zawartosc)
                {
                    int nr = 0;
                    nr = iLinia.IndexOf("camera_calibration_file");
                    if (nr > -1)
                    {
                        ++SumaPLIK;
                    }
                }
                if (SumaPLIK < 1)
                {
                    MessageBox.Show("Brak w pliku *.cam definicji kamery !");
                }
                else
                {
                    if (Kamera == null)
                    {
                        MessageBox.Show("Wpisz Nazwę Kamery !");
                    }
                    else
                    {
                        PIX4D_2_INPHO();
                    }
                }
            }
        }

        private void PIX4D_2_INPHO()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button59.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_PIX4D_2_INPHO_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_PIX4D_2_INPHO_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_PIX4D_2_INPHO_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Wygenerowano " + SumaPLIK.ToString() + " kamery.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button59.Enabled = true;
        }

        private void BackgroundWorker_PIX4D_2_INPHO_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            int nr = plikCam.IndexOf("calibrated_internal_camera_parameters");
            if (nr > -1)
            {
                plikPrj = plikCam.Remove(plikCam.Length - nr) + ".prj";
            }
            else
            {
                plikPrj = plikCam.Remove(plikCam.Length - 4) + ".prj";
            }
            if (File.Exists(plikPrj) == true)
            {
                File.Delete(plikPrj);
            }
            nPlik = Path.GetFileName(plikPrj);
            nPlik = nPlik.Remove(nPlik.Length - 4);
            StreamReader sr = new StreamReader(plikCam);
            StreamWriter sw = new StreamWriter(plikPrj);
            sw.WriteLine(@"$PROJECT 7.1.0");
            sw.WriteLine(@"  $PROJECT_NAME : " + nPlik);
            sw.WriteLine(@"  $IMAGE_TYPE : Aerial");
            sw.WriteLine(@"  $REFRACT_CORR_DEFAULT : on");
            sw.WriteLine(@"  $CURV_CORR_DEFAULT : on");
            sw.WriteLine(@"  $COORDINATE_SYSTEM :");
            sw.WriteLine(@"     LOCAL_CS[""Local Space Rectangular (LSR)"",");
            sw.WriteLine(@"         UNIT[""m"",1.0000000000]]");
            sw.WriteLine(@"  $LINEAR_UNITS_OF_OBJECT : m");
            sw.WriteLine(@"  $LINEAR_UNITS_OF_IMAGE : pixel");
            sw.WriteLine(@"  $ANGULAR_UNITS : deg");
            sw.WriteLine(@"$END");
            string iLinia;
            nr = 0;
            int no = 0;
            int nu = 0;
            int nrKamery = 0;
            while ((iLinia = sr.ReadLine()) != null)
            {
                no = iLinia.IndexOf("camera_calibration_file");
                if (no > -1)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    ++nrKamery;
                    nu = 1;
                }
                else
                {
                    if (nu == 1)
                    {
                        nr = iLinia.IndexOf("#Focal Length");
                        if (nr > -1)
                        {
                            Ramka = iLinia.Remove(0, 46);
                            nr = Ramka.IndexOf("x");
                            Ramka = Ramka.Remove(nr);
                        }
                        nr = iLinia.IndexOf("#Image size");
                        if (nr > -1)
                        {
                            string RR = iLinia.Remove(0, 12);
                            nr = RR.IndexOf("x");
                            XR = RR.Remove(nr);
                            YR = RR.Remove(0, nr + 1);
                            nr = YR.IndexOf(" pixel");
                            YR = YR.Remove(nr);
                        }
                        nr = iLinia.IndexOf("FOCAL");
                        if (nr > -1)
                        {
                            Ogniskowa = iLinia.Remove(0, 6);
                        }
                        nr = iLinia.IndexOf("XPOFF");
                        if (nr > -1)
                        {
                            XPP = iLinia.Remove(0, 6);
                        }
                        nr = iLinia.IndexOf("YPOFF");
                        if (nr > -1)
                        {
                            YPP = iLinia.Remove(0, 6);
                            nu = 0;
                            decimal M11 = Convert.ToDecimal(Ramka) / Convert.ToDecimal(XR);
                            M11 = Math.Round(M11, 7, MidpointRounding.ToEven);
                            decimal XPR = Convert.ToDecimal(XPP) / M11;
                            decimal YPR = Convert.ToDecimal(YPP) / M11;
                            M11 = 1 / M11;
                            XPR = ((Convert.ToDecimal(XR) / 2) - Convert.ToDecimal(0.5)) + XPR;
                            YPR = ((Convert.ToDecimal(YR) / 2) - Convert.ToDecimal(0.5)) - YPR;
                            sw.WriteLine(@"$CAMERA_DEFINITION");
                            sw.WriteLine(@"  $ID : " + Kamera + "_" + nrKamery);
                            sw.WriteLine(@"  $BRAND : Custom");
                            sw.WriteLine(@"  $KIND : CCDFrame");
                            sw.WriteLine(@"  $CCD_COLUMNS : " + XR);
                            sw.WriteLine(@"  $CCD_ROWS : " + YR);
                            sw.WriteLine(@"  $PIXEL_REFERENCE : CenterTopLeft");
                            sw.WriteLine(@"  $CAMERA_MOUNT_ROTATION :     0.000000");
                            sw.WriteLine(@"  $ACTIVE_CALIBRATION : 1");
                            sw.WriteLine(@"  $CALIBRATION_SET :");
                            sw.WriteLine(@"    $ID : 1");
                            sw.WriteLine(@"    $MODE : manual");
                            sw.WriteLine(@"    $DATE : " + DateTime.Now);
                            sw.WriteLine(@"    $CCD_INTERIOR_ORIENTATION :");
                            sw.WriteLine(@"     " + Convert.ToString(M11) + "      -0.0000000000    " + Convert.ToString(XPR));
                            sw.WriteLine(@"       0.0000000000    -" + Convert.ToString(M11) + "    " + Convert.ToString(YPR));
                            sw.WriteLine(@"    $FOCAL_LENGTH :    " + Ogniskowa);
                            sw.WriteLine(@"    $PRINCIPAL_POINT_PPA :     0.000000     0.000000");
                            sw.WriteLine(@"    $GPS_ANTENNA_OFFSET :     0.000000     0.000000     0.000000");
                            sw.WriteLine(@"  $END");
                            sw.WriteLine(@"$END");
                        }
                    }
                }
            }
            sw.Close();
        }

        private void TextBox74_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".cam" || roz == ".CAM")
                    {
                        textBox74.Text = files[0];
                    }
                    else
                    {
                        textBox74.Text = "";
                    }
                }
            }
        }

        private void TextBox74_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------4Kdo3K---------------------------------------------------------------------------

        private void TextBox78_TextChanged(object sender, EventArgs e)
        {
            sciezkaT = textBox78.Text;
        }

        private void Button62_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox78.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox76_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox76.Text;
        }

        private void Button60_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox76.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button63_Click(object sender, EventArgs e)
        {
            sciezkaT = textBox78.Text;
            if (Directory.Exists(sciezkaT) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaT += @"\";
                sciezkaT = Path.GetFullPath(sciezkaT);
                sciezkaW = textBox76.Text;
                if (Directory.Exists(sciezkaW) == false)
                {
                    MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                }
                else
                {
                    sciezkaW += @"\";
                    sciezkaW = Path.GetFullPath(sciezkaW);
                    if (sciezkaT == sciezkaW)
                    {
                        MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Danymi !");
                    }
                    else
                    {
                        string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
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
                            PlikExeGDTrans = @"C:\Program Files\INDEOv2\bin\gdal_translate.exe";
                            if (File.Exists(PlikExeGDTrans) == false)
                            {
                                MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin !");
                            }
                            else
                            {
                                System.Environment.SetEnvironmentVariable("GDAL_DATA", @"C:\Program Files\INDEOv2\bin");
                                PlikExeGDTrans = @"""C:\Program Files\INDEOv2\bin\gdal_translate.exe""";
                                SprawdzRGB_CIR();
                            }
                        }
                    }
                }
            }
        }

        private void SprawdzRGB_CIR()
        {
            if (radioButton48.Checked == true)
            {
                string Red = Convert.ToString(numericUpDown7.Value);
                string Green = Convert.ToString(numericUpDown8.Value);
                string Blue = Convert.ToString(numericUpDown9.Value);
                kolorMode = " -b " + Red + " -b " + Green + " -b " + Blue + " ";
            }
            else
            {
                if (radioButton46.Checked == true)
                {
                    kolorMode = " -b 4 -b 1 -b 2 ";
                }
                else
                {
                    kolorMode = " -b 1 -b 2 -b 3 ";
                }
            }
            CzterydoTrzech();
        }

        private void CzterydoTrzech()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button63.Enabled = false;
            textBox76.Enabled = false;
            textBox78.Enabled = false;
            button60.Enabled = false;
            button62.Enabled = false;
            radioButton46.Enabled = false;
            radioButton47.Enabled = false;
            radioButton48.Enabled = false;
            numericUpDown7.Enabled = false;
            numericUpDown8.Enabled = false;
            numericUpDown9.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_CzterydoTrzech_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_CzterydoTrzech_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_CzterydoTrzech_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button63.Enabled = true;
            textBox76.Enabled = true;
            textBox78.Enabled = true;
            button60.Enabled = true;
            button62.Enabled = true;
            radioButton46.Enabled = true;
            radioButton47.Enabled = true;
            radioButton48.Enabled = true;
            numericUpDown7.Enabled = true;
            numericUpDown8.Enabled = true;
            numericUpDown9.Enabled = true;
        }

        private void BackgroundWorker_CzterydoTrzech_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                Process MyProcess1 = new Process();
                MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                MyProcess1.StartInfo.Arguments = @"""" + iPlik + @""" -ot byte -strict -scale" + kolorMode + @"""" + sciezkaW + nPlik + @"""";
                MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess1.Start();
                MyProcess1.WaitForExit();
            }
        }

        private void TextBox76_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox76.Text = files[0];
                }
            }
        }

        private void TextBox76_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox78_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox78.Text = files[0];
                }
            }
        }

        private void TextBox78_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------HISTOGRAMY-----------------------------------------------------------------------

        private void TextBox79_TextChanged(object sender, EventArgs e)
        {
            sciezkaT = textBox79.Text;
        }

        private void Button65_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox79.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox77_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox77.Text;
        }

        private void Button64_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox77.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button61_Click(object sender, EventArgs e)
        {
            sciezkaT = textBox79.Text;
            if (Directory.Exists(sciezkaT) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaT += @"\";
                sciezkaT = Path.GetFullPath(sciezkaT);
                sciezkaW = textBox77.Text;
                if (Directory.Exists(sciezkaW) == false)
                {
                    MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                }
                else
                {
                    sciezkaW += @"\";
                    sciezkaW = Path.GetFullPath(sciezkaW);
                    if (sciezkaT == sciezkaW)
                    {
                        MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Danymi !");
                    }
                    else
                    {
                        string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
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
                            PlikExeGDInfo = @"C:\Program Files\INDEOv2\bin\gdalinfo.exe";
                            PlikExeGDTrans = @"C:\Program Files\INDEOv2\bin\gdal_translate.exe";
                            if (File.Exists(PlikExeGDInfo) == false || File.Exists(PlikExeGDTrans) == false)
                            {
                                MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin !");
                            }
                            else
                            {
                                PlikExeGDInfo = @"""C:\Program Files\INDEOv2\bin\gdalinfo.exe""";
                                System.Environment.SetEnvironmentVariable("GDAL_DATA", @"C:\Program Files\INDEOv2\bin");
                                PlikExeGDTrans = @"""C:\Program Files\INDEOv2\bin\gdal_translate.exe""";
                                HISTOGRAMY();
                            }
                        }
                    }
                }
            }
        }

        private void HISTOGRAMY()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button61.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_HISTOGRAMY_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_HISTOGRAMY_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_HISTOGRAMY_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Wygenerowano z " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button61.Enabled = true;
        }

        private void BackgroundWorker_HISTOGRAMY_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            int MinP = Convert.ToInt32(numericUpDown11.Value);
            int MaxP = Convert.ToInt32(numericUpDown10.Value);
            string MinO = Convert.ToString(numericUpDown6.Value);
            string MaxO = Convert.ToString(numericUpDown1.Value);
            if ((radioButton10.Checked == true) || ((MinO == "0") && (MaxO == "255")))
            {
                decimal[] HistoR = new decimal[256];
                decimal[] HistoG = new decimal[256];
                decimal[] HistoB = new decimal[256];
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
                foreach (string iPlik in Pliki)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    nPlik = Path.GetFileName(iPlik);
                    Process MyProcess1 = new Process();
                    MyProcess1.StartInfo.FileName = PlikExeGDInfo;
                    MyProcess1.StartInfo.Arguments = @" -hist """ + iPlik + @"""";
                    MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    MyProcess1.Start();
                    MyProcess1.WaitForExit();
                    string txtPlik = iPlik + ".aux.xml";
                    string his = "";
                    decimal par = 0;
                    string hi;
                    int no = 0;
                    string[] zawartosc = File.ReadAllLines(txtPlik);
                    foreach (string iLinia in zawartosc)
                    {
                        int nr = 0;
                        nr = iLinia.IndexOf("        <HistCounts>");
                        if (nr > -1)
                        {
                            if (no == 2)
                            {
                                his = iLinia.Remove(0, 20);
                                nr = his.Length;
                                his = his.Remove(nr - 13);
                                his = his + "|";
                                for (int i = 0; i <= 255; i++)
                                {
                                    nr = his.IndexOf("|");
                                    hi = his.Remove(nr);
                                    par = Convert.ToDecimal(hi);
                                    HistoB[i] = HistoB[i] + par;
                                    his = his.Remove(0, nr + 1);
                                }
                            }
                            else
                            {
                                if (no == 1)
                                {
                                    no = 2;
                                    his = iLinia.Remove(0, 20);
                                    nr = his.Length;
                                    his = his.Remove(nr - 13);
                                    his = his + "|";
                                    for (int i = 0; i <= 255; i++)
                                    {
                                        nr = his.IndexOf("|");
                                        hi = his.Remove(nr);
                                        par = Convert.ToDecimal(hi);
                                        HistoG[i] = HistoG[i] + par;
                                        his = his.Remove(0, nr + 1);
                                    }
                                }
                                else
                                {
                                    no = 1;
                                    his = iLinia.Remove(0, 20);
                                    nr = his.Length;
                                    his = his.Remove(nr - 13);
                                    his = his + "|";
                                    for (int i = 0; i <= 255; i++)
                                    {
                                        nr = his.IndexOf("|");
                                        hi = his.Remove(nr);
                                        par = Convert.ToDecimal(hi);
                                        HistoR[i] = HistoR[i] + par;
                                        his = his.Remove(0, nr + 1);
                                    }
                                }
                            }
                        }
                    }
                    File.Delete(txtPlik);
                    int MinR = 0;
                    int MaxR = 255;
                    int MinG = 0;
                    int MaxG = 255;
                    int MinB = 0;
                    int MaxB = 255;
                    for (int i = 1; i <= 254; i++)
                    {
                        if (MinR == 0)
                        {
                            if (HistoR[i] > MinP)
                            {
                                MinR = i;
                            }
                        }
                        if (MinG == 0)
                        {
                            if (HistoG[i] > MinP)
                            {
                                MinG = i;
                            }
                        }
                        if (MinB == 0)
                        {
                            if (HistoB[i] > MinP)
                            {
                                MinB = i;
                            }
                        }
                    }
                    for (int i = 254; i >= 1; i--)
                    {
                        if (MaxR == 255)
                        {
                            if (HistoR[i] > MaxP)
                            {
                                MaxR = i;
                            }
                        }
                        if (MaxG == 255)
                        {
                            if (HistoG[i] > MaxP)
                            {
                                MaxG = i;
                            }
                        }
                        if (MaxB == 255)
                        {
                            if (HistoB[i] > MaxP)
                            {
                                MaxB = i;
                            }
                        }
                    }
                    Process MyProcess2 = new Process();
                    MyProcess2.StartInfo.FileName = PlikExeGDTrans;
                    if (radioButton13.Checked == true)
                    {
                        MyProcess2.StartInfo.Arguments = @"""" + iPlik + @""" -strict -scale_1 " + Convert.ToString(MinR) + " " + Convert.ToString(MaxR) + " -scale_2 " + Convert.ToString(MinG) + " " + Convert.ToString(MaxG) + " -scale_3 " + Convert.ToString(MinB) + " " + Convert.ToString(MaxB) + @" """ + sciezkaW + nPlik + @"""";
                    }
                    else
                    {
                        MyProcess2.StartInfo.Arguments = @"""" + iPlik + @""" -strict -scale " + Convert.ToString(MinR) + " " + Convert.ToString(MaxR) + @" """ + sciezkaW + nPlik + @"""";
                    }
                    MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    MyProcess2.Start();
                    MyProcess2.WaitForExit();
                }
            }
            else
            {
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
                foreach (string iPlik in Pliki)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    nPlik = Path.GetFileName(iPlik);
                    Process MyProcess2 = new Process();
                    MyProcess2.StartInfo.FileName = PlikExeGDTrans;
                    if (radioButton13.Checked == true)
                    {
                        MyProcess2.StartInfo.Arguments = @"""" + iPlik + @""" -strict -scale_1 " + MinO + " " + MaxO + " -scale_2 " + MinO + " " + MaxO + " -scale_3 " + MinO + " " + MaxO + @" """ + sciezkaW + nPlik + @"""";
                    }
                    else
                    {
                        MyProcess2.StartInfo.Arguments = @"""" + iPlik + @""" -strict -scale " + MinO + " " + MaxO + @" """ + sciezkaW + nPlik + @"""";
                    }
                    MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    MyProcess2.Start();
                    MyProcess2.WaitForExit();
                }
            }
        }

        private void TextBox79_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox79.Text = files[0];
                }
            }
        }

        private void TextBox79_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox77_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox77.Text = files[0];
                }
            }
        }

        private void TextBox77_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------OVERVIEW-------------------------------------------------------------------------

        private void TextBox80_TextChanged(object sender, EventArgs e)
        {
            sciezkaT = textBox80.Text;
        }

        private void Button67_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox80.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox81_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox81.Text;
        }

        private void Button68_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox81.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void CheckBox30_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox30.Checked == true)
            {
                textBox81.Enabled = true;
                button68.Enabled = true;
            }
            else
            {
                textBox81.Enabled = false;
                button68.Enabled = false;
                textBox81.Text = "";
            }
        }

        private void RadioButton52_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton52.Checked == true)
            {
                groupBox18.Visible = true;
            }
            else
            {
                groupBox18.Visible = false;
            }
        }

        private void RadioButton57_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton57.Checked == true)
            {
                numericUpDown12.Enabled = true;
            }
            else
            {
                numericUpDown12.Enabled = false;
            }
        }

        private void NumericUpDown12_ValueChanged(object sender, EventArgs e)
        {
            q = Convert.ToString(numericUpDown12.Value);
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == 0)
            {
                metodaR = " -r nearest ";
            }
            else if (comboBox4.SelectedIndex == 1)
            {
                metodaR = " -r average ";
            }
            else if (comboBox4.SelectedIndex == 2)
            {
                metodaR = " -r gauss ";
            }
            else if (comboBox4.SelectedIndex == 3)
            {
                metodaR = " -r cubic ";
            }
            else if (comboBox4.SelectedIndex == 4)
            {
                metodaR = " -r cubicspline ";
            }
            else if (comboBox4.SelectedIndex == 5)
            {
                metodaR = " -r lanczos ";
            }
            else if (comboBox4.SelectedIndex == 6)
            {
                metodaR = " -r average_mp ";
            }
            else if (comboBox4.SelectedIndex == 7)
            {
                metodaR = " -r average_magphase ";
            }
            else if (comboBox4.SelectedIndex == 8)
            {
                metodaR = " -r mode ";
            }
        }

        private void Button66_Click(object sender, EventArgs e)
        {
            sciezkaT = textBox80.Text;
            if (Directory.Exists(sciezkaT) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaT += @"\";
                sciezkaT = Path.GetFullPath(sciezkaT);
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
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
                    if (checkBox30.Checked == true)
                    {
                        sciezkaW = textBox81.Text;
                        if (Directory.Exists(sciezkaW) == false)
                        {
                            MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                        }
                        else
                        {
                            sciezkaW += @"\";
                            sciezkaW = Path.GetFullPath(sciezkaW);
                            if (sciezkaT == sciezkaW)
                            {
                                MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Danymi !");
                            }
                            else
                            {
                                OvExeki();
                            }
                        }
                    }
                    else
                    {
                        sciezkaW = sciezkaT;
                        OvExeki();
                    }

                }
            }
        }

        private void OvExeki()
        {
            PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
            PlikExeGDTrans = @"C:\Program Files\INDEOv2\bin\gdal_translate.exe";
            PlikExeGDAddo = @"C:\Program Files\INDEOv2\bin\gdaladdo.exe";
            if (File.Exists(PlikExeEXIF) == false || File.Exists(PlikExeGDTrans) == false || File.Exists(PlikExeGDAddo) == false)
            {
                MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin !");
            }
            else
            {
                PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                System.Environment.SetEnvironmentVariable("GDAL_DATA", @"C:\Program Files\INDEOv2\bin");
                PlikExeGDTrans = @"""C:\Program Files\INDEOv2\bin\gdal_translate.exe""";
                PlikExeGDAddo = @"""C:\Program Files\INDEOv2\bin\gdaladdo.exe""";
                OvPrzelaczniki();
            }
        }

        private void OvPrzelaczniki()
        {
            if (radioButton58.Checked == true)
            {
                kompresja = "";
            }
            else if (radioButton57.Checked == true)
            {
                q = Convert.ToString(numericUpDown12.Value);
                kompresja = @" -co ""COMPRESS=JPEG"" -co ""JPEG_QUALITY=" + q + @""" -co ""PHOTOMETRIC=YCBCR"" """;
            }
            if (radioButton59.Checked == true)
            {
                tile = "";
            }
            else if (radioButton60.Checked == true)
            {
                tile = @" -co ""TILED=YES"" -co ""BLOCKXSIZE=128"" -co ""BLOCKYSIZE=128"" """;
            }
            else if (radioButton61.Checked == true)
            {
                tile = @" -co ""TILED=YES"" -co ""BLOCKXSIZE=256"" -co ""BLOCKYSIZE=256"" """;
            }
            else if (radioButton63.Checked == true)
            {
                tile = @" -co ""TILED=YES"" -co ""BLOCKXSIZE=512"" -co ""BLOCKYSIZE=512"" """;
            }
            else if (radioButton62.Checked == true)
            {
                tile = @" -co ""TILED=YES"" -co ""BLOCKXSIZE=1024"" -co ""BLOCKYSIZE=1024"" """;
            }
            if (radioButton54.Checked == true)
            {
                OverView = "";
                GDAL_OV();
            }
            else if (radioButton53.Checked == true)
            {
                OverView = " 2 4 8 16 32 64 128 256 512 1024";
                GDAL_OV();
            }
            else if (radioButton52.Checked == true)
            {
                if (checkBox40.Checked == true)
                {
                    BOX1 = " 2";
                }
                if (checkBox39.Checked == true)
                {
                    BOX2 = " 4";
                }
                if (checkBox38.Checked == true)
                {
                    BOX3 = " 8";
                }
                if (checkBox37.Checked == true)
                {
                    BOX4 = " 16";
                }
                if (checkBox36.Checked == true)
                {
                    BOX5 = " 32";
                }
                if (checkBox35.Checked == true)
                {
                    BOX6 = " 64";
                }
                if (checkBox34.Checked == true)
                {
                    BOX7 = " 128";
                }
                if (checkBox33.Checked == true)
                {
                    BOX8 = " 256";
                }
                if (checkBox32.Checked == true)
                {
                    BOX9 = " 512";
                }
                if (checkBox31.Checked == true)
                {
                    BOX10 = " 1024";
                }
                OverView = BOX1 + BOX2 + BOX3 + BOX4 + BOX5 + BOX6 + BOX7 + BOX8 + BOX9 + BOX10;
                if (OverView == "")
                {
                    MessageBox.Show("Wskaż Overview do wygenerowania !");
                }
                else
                {
                    BOX1 = ""; BOX2 = ""; BOX3 = ""; BOX4 = ""; BOX5 = ""; BOX6 = ""; BOX7 = ""; BOX8 = ""; BOX9 = ""; BOX10 = "";
                    GDAL_OV();
                }
            }
        }

        private void GDAL_OV()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button66.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_GDAL_OV_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_GDAL_OV_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_GDAL_OV_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button66.Enabled = true;
        }

        private void BackgroundWorker_GDAL_OV_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            if (kompresja == "" && tile == "" && OverView == "")
            {
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
                foreach (string iPlik in Pliki)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    nPlik = Path.GetFileName(iPlik);
                    if (checkBox30.Checked == true)
                    {
                        string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                        if (File.Exists(sciezkaT + tPlik))
                        {
                            if (File.Exists(sciezkaW + tPlik))
                            {
                                File.Delete(sciezkaW + tPlik);
                            }
                            File.Copy(sciezkaT + tPlik, sciezkaW + tPlik);
                        }
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = @"""" + iPlik + @""" """ + sciezkaW + nPlik + @"""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaW + nPlik + @"""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                    }
                    else
                    {
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = @"""" + iPlik + @""" """ + sciezkaT + @"Temp.tif""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaT + @"Temp.tif""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(iPlik);
                        File.Move(sciezkaT + "Temp.tif", iPlik);
                    }
                }
            }
            else if (kompresja == "" && tile == "" && OverView != "")
            {
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
                foreach (string iPlik in Pliki)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    nPlik = Path.GetFileName(iPlik);
                    if (checkBox30.Checked == true)
                    {
                        string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                        if (File.Exists(sciezkaT + tPlik))
                        {
                            if (File.Exists(sciezkaW + tPlik))
                            {
                                File.Delete(sciezkaW + tPlik);
                            }
                            File.Copy(sciezkaT + tPlik, sciezkaW + tPlik);
                        }
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = @"""" + iPlik + @""" """ + sciezkaW + nPlik + @"""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaW + nPlik + @"""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeGDAddo;
                        MyProcess3.StartInfo.Arguments = metodaR + @"""" + sciezkaW + nPlik + @"""" + OverView;
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                    }
                    else
                    {
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = @"""" + iPlik + @""" """ + sciezkaT + @"Temp.tif""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaT + @"Temp.tif""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(iPlik);
                        File.Move(sciezkaT + "Temp.tif", iPlik);
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeGDAddo;
                        MyProcess3.StartInfo.Arguments = metodaR + iPlik + OverView;
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                    }
                }
            }
            else if (kompresja == "" && tile != "" && OverView == "")
            {
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
                foreach (string iPlik in Pliki)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    nPlik = Path.GetFileName(iPlik);
                    if (checkBox30.Checked == true)
                    {
                        string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                        if (File.Exists(sciezkaT + tPlik))
                        {
                            if (File.Exists(sciezkaW + tPlik))
                            {
                                File.Delete(sciezkaW + tPlik);
                            }
                            File.Copy(sciezkaT + tPlik, sciezkaW + tPlik);
                        }
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = tile + iPlik + @""" """ + sciezkaW + nPlik + @"""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaW + nPlik + @"""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                    }
                    else
                    {
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = tile + iPlik + @""" """ + sciezkaT + @"Temp.tif""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaT + @"Temp.tif""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(iPlik);
                        File.Move(sciezkaT + "Temp.tif", iPlik);
                    }
                }
            }
            else if (kompresja != "" && tile == "" && OverView == "")
            {
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
                foreach (string iPlik in Pliki)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    nPlik = Path.GetFileName(iPlik);
                    if (checkBox30.Checked == true)
                    {
                        string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                        if (File.Exists(sciezkaT + tPlik))
                        {
                            if (File.Exists(sciezkaW + tPlik))
                            {
                                File.Delete(sciezkaW + tPlik);
                            }
                            File.Copy(sciezkaT + tPlik, sciezkaW + tPlik);
                        }
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = kompresja + iPlik + @""" """ + sciezkaW + nPlik + @"""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaW + nPlik + @"""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                    }
                    else
                    {
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = kompresja + iPlik + @""" """ + sciezkaT + @"Temp.tif""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaT + @"Temp.tif""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(iPlik);
                        File.Move(sciezkaT + "Temp.tif", iPlik);
                    }
                }
            }
            else if (kompresja != "" && tile != "" && OverView == "")
            {
                tile = tile.Remove(tile.Length - 2);
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
                foreach (string iPlik in Pliki)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    nPlik = Path.GetFileName(iPlik);
                    if (checkBox30.Checked == true)
                    {
                        string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                        if (File.Exists(sciezkaT + tPlik))
                        {
                            if (File.Exists(sciezkaW + tPlik))
                            {
                                File.Delete(sciezkaW + tPlik);
                            }
                            File.Copy(sciezkaT + tPlik, sciezkaW + tPlik);
                        }
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = tile + kompresja + iPlik + @""" """ + sciezkaW + nPlik + @"""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaW + nPlik + @"""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                    }
                    else
                    {
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = tile + kompresja + iPlik + @""" """ + sciezkaT + @"Temp.tif""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaT + @"Temp.tif""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(iPlik);
                        File.Move(sciezkaT + "Temp.tif", iPlik);
                    }
                }
            }
            else if (kompresja == "" && tile != "" && OverView != "")
            {
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
                foreach (string iPlik in Pliki)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    nPlik = Path.GetFileName(iPlik);
                    if (checkBox30.Checked == true)
                    {
                        string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                        if (File.Exists(sciezkaT + tPlik))
                        {
                            if (File.Exists(sciezkaW + tPlik))
                            {
                                File.Delete(sciezkaW + tPlik);
                            }
                            File.Copy(sciezkaT + tPlik, sciezkaW + tPlik);
                        }
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = tile + iPlik + @""" """ + sciezkaW + nPlik + @"""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaW + nPlik + @"""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeGDAddo;
                        MyProcess3.StartInfo.Arguments = metodaR + @"""" + sciezkaW + nPlik + @"""" + OverView;
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                    }
                    else
                    {
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = tile + iPlik + @""" """ + sciezkaT + @"Temp.tif""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaT + @"Temp.tif""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(iPlik);
                        File.Move(sciezkaT + "Temp.tif", iPlik);
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeGDAddo;
                        MyProcess3.StartInfo.Arguments = metodaR + iPlik + OverView;
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                    }
                }
            }
            else if (kompresja != "" && tile == "" && OverView != "")
            {
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
                foreach (string iPlik in Pliki)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    nPlik = Path.GetFileName(iPlik);
                    if (checkBox30.Checked == true)
                    {
                        string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                        if (File.Exists(sciezkaT + tPlik))
                        {
                            if (File.Exists(sciezkaW + tPlik))
                            {
                                File.Delete(sciezkaW + tPlik);
                            }
                            File.Copy(sciezkaT + tPlik, sciezkaW + tPlik);
                        }
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = kompresja + iPlik + @""" """ + sciezkaW + nPlik + @"""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaW + nPlik + @"""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeGDAddo;
                        MyProcess3.StartInfo.Arguments = metodaR + @"""" + sciezkaW + nPlik + @"""" + OverView;
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                    }
                    else
                    {
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = kompresja + iPlik + @""" """ + sciezkaT + @"Temp.tif""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaT + @"Temp.tif""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(iPlik);
                        File.Move(sciezkaT + "Temp.tif", iPlik);
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeGDAddo;
                        MyProcess3.StartInfo.Arguments = metodaR + iPlik + OverView;
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                    }
                }
            }
            else if (kompresja != "" && tile != "" && OverView != "")
            {
                tile = tile.Remove(tile.Length - 2);
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
                foreach (string iPlik in Pliki)
                {
                    ++progressBarVal;
                    BackgroundWorker1.ReportProgress(progressBarVal);
                    nPlik = Path.GetFileName(iPlik);
                    if (checkBox30.Checked == true)
                    {
                        string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                        if (File.Exists(sciezkaT + tPlik))
                        {
                            if (File.Exists(sciezkaW + tPlik))
                            {
                                File.Delete(sciezkaW + tPlik);
                            }
                            File.Copy(sciezkaT + tPlik, sciezkaW + tPlik);
                        }
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = tile + kompresja + iPlik + @""" """ + sciezkaW + nPlik + @"""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaW + nPlik + @"""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeGDAddo;
                        MyProcess3.StartInfo.Arguments = metodaR + @"""" + sciezkaW + nPlik + @"""" + OverView;
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                    }
                    else
                    {
                        Process MyProcess1 = new Process();
                        MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                        MyProcess1.StartInfo.Arguments = tile + kompresja + iPlik + @""" """ + sciezkaT + @"Temp.tif""";
                        MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess1.Start();
                        MyProcess1.WaitForExit();
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @"-TagsFromFile """ + iPlik + @""" -XMP:All= -modifydate= -imagedescription= -overwrite_original_in_place """ + sciezkaT + @"Temp.tif""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(iPlik);
                        File.Move(sciezkaT + "Temp.tif", iPlik);
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeGDAddo;
                        MyProcess3.StartInfo.Arguments = metodaR + iPlik + OverView;
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                    }
                }
            }
        }

        private void TextBox80_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox80.Text = files[0];
                }
            }
        }

        private void TextBox80_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox81_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox81.Text = files[0];
                }
            }
        }

        private void TextBox81_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------SOS-------------------------------------------------------------------------------

        private void TextBox83_TextChanged(object sender, EventArgs e)
        {
            sciezkaS = textBox83.Text;
        }

        private void Button71_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox83.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox82_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox82.Text;
        }

        private void Button70_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox82.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button69_Click(object sender, EventArgs e)
        {
            sciezkaS = textBox83.Text;
            if (Directory.Exists(sciezkaS) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaS += @"\";
                sciezkaS = Path.GetFullPath(sciezkaS);
                sciezkaW = textBox82.Text;
                if (Directory.Exists(sciezkaW) == false)
                {
                    MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                }
                else
                {
                    sciezkaW += @"\";
                    sciezkaW = Path.GetFullPath(sciezkaW);
                    if (sciezkaS == sciezkaW)
                    {
                        MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Danymi !");
                    }
                    else
                    {
                        string[] Pliki = Directory.GetFiles(sciezkaS, "*.sos");
                        SumaPLIK = 0;
                        foreach (string iPlik in Pliki)
                        {
                            ++SumaPLIK;
                        }
                        if (SumaPLIK == 0)
                        {
                            MessageBox.Show("Brak Plików *.sos !");
                        }
                        else
                        {
                            SOS();
                        }
                    }
                }
            }
        }

        private void SOS()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button69.Enabled = false;
            textBox83.Enabled = false;
            textBox82.Enabled = false;
            button71.Enabled = false;
            button70.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_SOS_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_SOS_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_SOS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button69.Enabled = true;
            textBox83.Enabled = true;
            textBox82.Enabled = true;
            button71.Enabled = true;
            button70.Enabled = true;
        }

        private void BackgroundWorker_SOS_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaS, "*.sos");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                string tPlik = sciezkaW + Path.GetFileName(iPlik);
                if (File.Exists(tPlik) == true)
                {
                    File.Delete(tPlik);
                }
                int UTF = 0;
                StreamReader isr = new StreamReader(iPlik);
                string iLinia;
                for (int i = 1; i <= 10; i++)
                {
                    iLinia = isr.ReadLine();
                    int nr = 0;
                    nr = iLinia.IndexOf("..TEGNSETT UTF-8");
                    if (nr > -1)
                    {
                        UTF = 1;
                    }
                    nr = iLinia.IndexOf("..TEGNSETT ISO8859");
                    if (nr > -1)
                    {
                        UTF = 2;
                    }
                }
                if (UTF == 1)
                {
                    StreamReader sr = new StreamReader(iPlik);
                    StreamWriter sw = new StreamWriter(tPlik);
                    while ((iLinia = sr.ReadLine()) != null)
                    {
                        iLinia = iLinia.Replace("Ř", "R");
                        iLinia = iLinia.Replace("ř", "r");
                        iLinia = iLinia.Replace("Ĺ", "L");
                        iLinia = iLinia.Replace("ĺ", "l");
                        iLinia = iLinia.Replace("Ø", "O");
                        iLinia = iLinia.Replace("ø", "o");
                        iLinia = iLinia.Replace("Å", "A");
                        iLinia = iLinia.Replace("å", "a");
                        sw.WriteLine(iLinia);
                    }
                    sw.Close();
                }
                if (UTF == 2)
                {
                    StreamReader sr = new StreamReader(iPlik, Encoding.Default);
                    StreamWriter sw = new StreamWriter(tPlik);
                    while ((iLinia = sr.ReadLine()) != null)
                    {
                        iLinia = iLinia.Replace("Ř", "R");
                        iLinia = iLinia.Replace("ř", "r");
                        iLinia = iLinia.Replace("Ĺ", "L");
                        iLinia = iLinia.Replace("ĺ", "l");
                        iLinia = iLinia.Replace("Ø", "O");
                        iLinia = iLinia.Replace("ø", "o");
                        iLinia = iLinia.Replace("Å", "A");
                        iLinia = iLinia.Replace("å", "a");
                        sw.WriteLine(iLinia);
                    }
                    sw.Close();
                }
                if (UTF == 0)
                {
                    MessageBox.Show("Niepoprawne kodowanie pliku   " + nPlik + "   !!!");
                }

            }
        }

        private void TextBox83_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox83.Text = files[0];
                }
            }
        }

        private void TextBox83_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox82_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox82.Text = files[0];
                }
            }
        }

        private void TextBox82_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------MOZAIKUJ GLOBAL MAPPER-----------------------------------------------------------

        private void TextBox88_TextChanged(object sender, EventArgs e)
        {
            sciezkaO = textBox88.Text;
        }

        private void Button77_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox88.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox87_TextChanged(object sender, EventArgs e)
        {
            plikShp = textBox87.Text;
        }

        private void Button76_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox87.Text = openFileDialog1.FileName;
            }
        }

        private void TextBox86_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox86.Text;
        }

        private void Button75_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox86.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void RadioButton55_CheckedChanged(object sender, EventArgs e)
        {
            kolorMode = "RGB(255,255,255)";
        }

        private void NumericUpDown13_ValueChanged(object sender, EventArgs e)
        {
            FBS = Convert.ToString(numericUpDown13.Value);
        }

        private void Button78_Click(object sender, EventArgs e)
        {
            sciezkaO = textBox88.Text;
            if (Directory.Exists(sciezkaO) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaO += @"\";
                sciezkaO = Path.GetFullPath(sciezkaO);
                sciezkaW = textBox86.Text;
                if (Directory.Exists(sciezkaW) == false)
                {
                    MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                }
                else
                {
                    sciezkaW += @"\";
                    sciezkaW = Path.GetFullPath(sciezkaW);
                    if (sciezkaO == sciezkaW)
                    {
                        MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Danymi !");
                    }
                    else
                    {
                        string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
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
                            string[] Pliki2 = Directory.GetFiles(sciezkaO, "*.tfw");
                            int SumaPLIK2 = 0;
                            foreach (string iPlik in Pliki2)
                            {
                                ++SumaPLIK2;
                            }
                            if (SumaPLIK2 == 0)
                            {
                                MessageBox.Show("Brak Plików TFW !");
                            }
                            else
                            {
                                if (SumaPLIK == SumaPLIK2)
                                {
                                    string[] Pliki3 = Directory.GetFiles(sciezkaO, "*.dxf");
                                    int SumaPLIK3 = 0;
                                    foreach (string iPlik in Pliki2)
                                    {
                                        ++SumaPLIK3;
                                    }
                                    if (SumaPLIK3 == 0)
                                    {
                                        MessageBox.Show("Brak Plików DXF !");
                                    }
                                    else
                                    {
                                        if (SumaPLIK == SumaPLIK3)
                                        {
                                            PlikExeGM = textBox65.Text;
                                            if (File.Exists(PlikExeGM) == false)
                                            {
                                                MessageBox.Show(@"Wskaż ścieżkę do Global Mappera !" + Environment.NewLine + @"Pomarańczowa Gwiazdka w Lewym Górnym Rogu !");
                                            }
                                            else
                                            {
                                                PlikExeGM = @"""" + PlikExeGM + @"""";
                                                plikShp = textBox87.Text;
                                                if (File.Exists(plikShp) == false)
                                                {
                                                    MessageBox.Show("Brak Pliku SHP - Mapsheet !");
                                                }
                                                else
                                                {
                                                    plikPrj = plikShp.Remove(plikShp.Length - 4) + ".prj";
                                                    if (File.Exists(plikPrj) == false)
                                                    {
                                                        MessageBox.Show("Brak Pliku PRJ !");
                                                    }
                                                    else
                                                    {
                                                        if (radioButton56.Checked == true)
                                                        {
                                                            kolorMode = "RGB(0,0,0)";
                                                        }
                                                        else
                                                        {
                                                            kolorMode = "RGB(255,255,255)";
                                                        }
                                                        if (Wybor == true)
                                                        {
                                                            Podglad();
                                                        }
                                                        else
                                                        {
                                                            Mozaikuj();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ilość plików TIF nie zgadza się z ilością plików DXF !");
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Ilość plików TIF nie zgadza się z ilością plików TFW !");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Button79_Click(object sender, EventArgs e)
        {
            Wybor = true;
            Button78_Click(sender, e);
        }

        private void Podglad()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button78.Enabled = false;
            button79.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_Podglad_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_PodgladMozaikuj_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void Mozaikuj()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button78.Enabled = false;
            button79.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_Mozaikuj_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_PodgladMozaikuj_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_PodgladMozaikuj_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button78.Enabled = true;
            button79.Enabled = true;
            Wybor = false;
        }

        private void BackgroundWorker_Podglad_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] gmwStart =
            {
                "GLOBAL_MAPPER_SCRIPT VERSION=1.00",
                "UNLOAD_ALL",
                "SET_BG_COLOR COLOR=" + kolorMode,
                @"LOAD_PROJECTION PROJ=""" + plikPrj + @"""",
            };
            gmwPlik = sciezkaW + "Podglad_GM.gmw";
            File.WriteAllLines(gmwPlik, gmwStart);
            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
            foreach (string iPlik in Pliki)
            {
                nPlik = Path.GetFileName(iPlik);
                dPlik = iPlik.Remove(iPlik.Length - 4) + ".dxf";
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                string[] gmwSrodek =
                {
                    @"IMPORT FILENAME=""" + iPlik + @""" TYPE=GEOTIFF PROJ=""" + plikPrj + @""" CONTRAST_MODE=NONE CLIP_COLLAR=NONE TRANSPARENT_COLOR=" + kolorMode + @" FEATHER_BLEND_EDGES=64 FEATHER_BLEND_POLY_FILE=""" + dPlik + @""" FEATHER_BLEND_SIZE=" + FBS + @" POLYGON_CROP_USE_ALL=YES",
                };
                File.AppendAllLines(gmwPlik, gmwSrodek);
            }
            string[] gmwEnd =
            {
                "SET_BG_COLOR COLOR=RGB(85,170,255)",
            };
            File.AppendAllLines(gmwPlik, gmwEnd);
            Process MyProcess1 = new Process();
            MyProcess1.StartInfo.FileName = PlikExeGM;
            MyProcess1.StartInfo.Arguments = gmwPlik;
            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            MyProcess1.Start();
        }

        private void BackgroundWorker_Mozaikuj_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] gmsStart =
            {
                "GLOBAL_MAPPER_SCRIPT VERSION=1.00",
                "UNLOAD_ALL",
                "SET_BG_COLOR COLOR=" + kolorMode,
                @"LOAD_PROJECTION PROJ=""" + plikPrj + @"""",
            };
            string gmsPlik = sciezkaW + "Mozaikuj_GM.gms";
            File.WriteAllLines(gmsPlik, gmsStart);
            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
            foreach (string iPlik in Pliki)
            {
                nPlik = Path.GetFileName(iPlik);
                dPlik = iPlik.Remove(iPlik.Length - 4) + ".dxf";
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                string[] gmsSrodek =
                {
                    @"IMPORT FILENAME=""" + iPlik + @""" TYPE=GEOTIFF PROJ=""" + plikPrj + @""" CONTRAST_MODE=NONE CLIP_COLLAR=NONE TRANSPARENT_COLOR=" + kolorMode + @" FEATHER_BLEND_EDGES=64 FEATHER_BLEND_POLY_FILE=""" + dPlik + @""" FEATHER_BLEND_SIZE=" + FBS + @" POLYGON_CROP_USE_ALL=YES",
                };
                File.AppendAllLines(gmsPlik, gmsSrodek);
            }
            string[] gmsEnd =
            {
                @"EXPORT_RASTER FILENAME=""" + sciezkaW + @""" TYPE=GEOTIFF GEN_WORLD_FILE=YES POLYGON_CROP_FILE=""" + plikShp + @""" POLYGON_CROP_USE_EACH=YES POLYGON_CROP_NAME_ATTR=""NAME"" POLYGON_CROP_FILENAME_SUFFIX="".tif""",
                "SET_BG_COLOR COLOR=RGB(85,170,255)",
            };
            File.AppendAllLines(gmsPlik, gmsEnd);
            Process MyProcess1 = new Process();
            MyProcess1.StartInfo.FileName = PlikExeGM;
            MyProcess1.StartInfo.Arguments = gmsPlik + " /showprogress";
            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess1.Start();
            MyProcess1.WaitForExit();
            File.Delete(gmsPlik);
        }

        private void TextBox88_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox88.Text = files[0];
                }
            }
        }

        private void TextBox88_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox86_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox86.Text = files[0];
                }
            }
        }

        private void TextBox86_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox87_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".shp" || roz == ".SHP")
                    {
                        textBox87.Text = files[0];
                    }
                    else
                    {
                        textBox87.Text = "";
                    }
                }
            }
        }

        private void TextBox87_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------PANSHARPENING QGIS---------------------------------------------------------------

        private void TextBox91_TextChanged(object sender, EventArgs e)
        {
            sciezkaP = textBox91.Text;
        }

        private void Button82_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox91.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox90_TextChanged(object sender, EventArgs e)
        {
            sciezkaM = textBox90.Text;
        }

        private void Button81_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox90.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox89_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox89.Text;
        }

        private void Button80_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox89.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex == 0)
            {
                metodaP = " -r nearest";
            }
            else if (comboBox5.SelectedIndex == 1)
            {
                metodaP = " -r bilinear";
            }
            else if (comboBox5.SelectedIndex == 2)
            {
                metodaP = " -r cubic";
            }
            else if (comboBox5.SelectedIndex == 3)
            {
                metodaP = " -r cubicspline";
            }
            else if (comboBox5.SelectedIndex == 4)
            {
                metodaP = " -r lanczos";
            }
            else if (comboBox5.SelectedIndex == 5)
            {
                metodaP = " -r average";
            }
        }

        private void Button83_Click(object sender, EventArgs e)
        {
            sciezkaP = textBox91.Text;
            if (Directory.Exists(sciezkaP) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi - obrazy panchromatyczne !");
            }
            else
            {
                sciezkaP += @"\";
                sciezkaP = Path.GetFullPath(sciezkaP);
                sciezkaM = textBox90.Text;
                if (Directory.Exists(sciezkaM) == false)
                {
                    MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi - obrazy multispektralne !");
                }
                else
                {
                    sciezkaM += @"\";
                    sciezkaM = Path.GetFullPath(sciezkaM);
                    sciezkaW = textBox89.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu wynikowego !");
                    }
                    else
                    {
                        sciezkaW += @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        if ((sciezkaP == sciezkaM) || (sciezkaP == sciezkaW) || (sciezkaM == sciezkaW))
                        {
                            MessageBox.Show("Te Same Ścieżki !");
                        }
                        else
                        {
                            string[] Pliki = Directory.GetFiles(sciezkaP, "*.tif");
                            SumaPLIK = 0;
                            foreach (string iPlik in Pliki)
                            {
                                ++SumaPLIK;
                            }
                            if (SumaPLIK == 0)
                            {
                                MessageBox.Show("Brak Plików - obrazy panchromatyczne !");
                            }
                            else
                            {
                                string[] Pliki2 = Directory.GetFiles(sciezkaM, "*.tif");
                                int SumaPLIK2 = 0;
                                foreach (string iPlik in Pliki2)
                                {
                                    ++SumaPLIK2;
                                }
                                if (SumaPLIK2 == 0)
                                {
                                    MessageBox.Show("Brak Plików - obrazy multispektralne !");
                                }
                                else
                                {
                                    if (SumaPLIK == SumaPLIK2)
                                    {
                                        katalogQGIS = textBox62.Text;
                                        PlikExePython = katalogQGIS + @"\bin\python-qgis.bat";
                                        if (File.Exists(PlikExePython) == false)
                                        {
                                            MessageBox.Show(@"Wskaż ścieżkę do katalogu QGIS !" + Environment.NewLine + @"Pomarańczowa Gwiazdka w Lewym Górnym Rogu !");
                                        }
                                        else
                                        {
                                            PlikExePython = @"""" + PlikExePython + @"""";
                                            Pansharpening();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ilość obrazów panchromatycznych nie zgadza się z ilością obrazów multispektralnych !");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Pansharpening()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button83.Enabled = false;
            textBox91.Enabled = false;
            textBox90.Enabled = false;
            textBox89.Enabled = false;
            button82.Enabled = false;
            button81.Enabled = false;
            button80.Enabled = false;
            comboBox5.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_Pansharpening_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_Pansharpening_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_Pansharpening_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button83.Enabled = true;
            textBox91.Enabled = true;
            textBox90.Enabled = true;
            textBox89.Enabled = true;
            button82.Enabled = true;
            button81.Enabled = true;
            button80.Enabled = true;
            comboBox5.Enabled = true;
        }

        private void BackgroundWorker_Pansharpening_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaP, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                Process MyProcess1 = new Process();
                MyProcess1.StartInfo.FileName = PlikExePython;
                MyProcess1.StartInfo.Arguments = @"""" + katalogQGIS + @"\apps\Python39\Scripts\gdal_pansharpen.py"" """ + iPlik + @""" """ + sciezkaM + nPlik + @""" """ + sciezkaW + nPlik + @"""" + metodaP + " -of GTiff";
                MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess1.Start();
                MyProcess1.WaitForExit();
            }
        }

        private void TextBox91_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox91.Text = files[0];
                }
            }
        }

        private void TextBox91_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox90_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox90.Text = files[0];
                }
            }
        }

        private void TextBox90_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox89_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox89.Text = files[0];
                }
            }
        }

        private void TextBox89_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------MOZAIKA2DXF----------------------------------------------------------------------

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {
            plikDgn = textBox10.Text;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox10.Text = openFileDialog2.FileName;
            }
        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {
            plikPrj = textBox7.Text;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox7.Text = openFileDialog3.FileName;
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox3.Text;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            plikDgn = textBox10.Text;
            if (File.Exists(plikDgn) == false)
            {
                MessageBox.Show("Brak Pliku z Mozaikami !");
            }
            else
            {
                SumaPLIK = 1;
                plikPrj = textBox7.Text;
                if (File.Exists(plikPrj) == false)
                {
                    MessageBox.Show("Brak Pliku z Definicją Układu Współrzędnych !");
                }
                else
                {
                    sciezkaW = textBox3.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                    }
                    else
                    {
                        sciezkaW += @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        PlikExeGM = textBox65.Text;
                        if (File.Exists(PlikExeGM) == false)
                        {
                            MessageBox.Show(@"Wskaż ścieżkę do Global Mappera !" + Environment.NewLine + @"Pomarańczowa Gwiazdka w Lewym Górnym Rogu !");
                        }
                        else
                        {
                            PlikExeGM = @"""" + PlikExeGM + @"""";
                            Mozaika2DXF();
                        }
                    }
                }
            }
        }

        private void Mozaika2DXF()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button8.Enabled = false;
            textBox10.Enabled = false;
            textBox7.Enabled = false;
            textBox3.Enabled = false;
            button7.Enabled = false;
            button6.Enabled = false;
            button5.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_Mozaika2DXF_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_Mozaika2DXF_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_Mozaika2DXF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono Plik z Mozaikami: " + nPlik);
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button8.Enabled = true;
            textBox10.Enabled = true;
            textBox7.Enabled = true;
            textBox3.Enabled = true;
            button7.Enabled = true;
            button6.Enabled = true;
            button5.Enabled = true;
        }

        private void BackgroundWorker_Mozaika2DXF_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            ++progressBarVal;
            BackgroundWorker1.ReportProgress(progressBarVal);
            nPlik = Path.GetFileName(plikDgn);
            string[] gmsZawartosc =
            {
                "GLOBAL_MAPPER_SCRIPT VERSION=1.00",
                "UNLOAD_ALL",
                "SET_BG_COLOR COLOR=RGB(85,170,255)",
                @"LOAD_PROJECTION PROJ=""" + plikPrj + @"""",
                @"IMPORT FILENAME=""" + plikDgn + @""" PROJ=""" + plikPrj + @"""",
                @"COPY_ATTRS FROM_TYPE=POINTS TO_TYPE=AREAS ATTR_TO_COPY=""<Feature Name>"" MULTI_POINT=ALL",
                "EDIT_VECTOR SHAPE_TYPE=POINTS DELETE_FEATURES=YES",
                @"EDIT_VECTOR ATTR_VAL=""<Feature Desc>=SIMPLIFIED""",
                @"EXPORT_VECTOR FILENAME=""" + sciezkaW + @"\.dxf"" TYPE=DXF EXPORT_ELEV=NO SPLIT_BY_ATTR=YES FILENAME_ATTR=""<Feature Name>""",
            };
            string gmsPlik = sciezkaW + "Mozaika2DXF_GM.gms";
            File.WriteAllLines(gmsPlik, gmsZawartosc);
            Process MyProcess1 = new Process();
            MyProcess1.StartInfo.FileName = PlikExeGM;
            MyProcess1.StartInfo.Arguments = gmsPlik + " /showprogress";
            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess1.Start();
            MyProcess1.WaitForExit();
            File.Delete(gmsPlik);
        }

        private void TextBox10_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".dgn" || roz == ".DGN")
                    {
                        textBox10.Text = files[0];
                    }
                    else
                    {
                        textBox10.Text = "";
                    }
                }
            }
        }

        private void TextBox10_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox7_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".prj" || roz == ".PRJ")
                    {
                        textBox7.Text = files[0];
                    }
                    else
                    {
                        textBox7.Text = "";
                    }
                }
            }
        }

        private void TextBox7_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox3_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox3.Text = files[0];
                }
            }
        }

        private void TextBox3_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------TIFF->GeoTIFF--------------------------------------------------------------------

        private void TextBox19_TextChanged(object sender, EventArgs e)
        {
            sciezkaO = textBox19.Text;
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox19.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox18_TextChanged(object sender, EventArgs e)
        {
            plikPrj = textBox18.Text;
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox18.Text = openFileDialog3.FileName;
            }
        }

        private void TextBox17_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox17.Text;
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox17.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            sciezkaO = textBox19.Text;
            if (Directory.Exists(sciezkaO) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi - Pliki Tiff !");
            }
            else
            {
                sciezkaO += @"\";
                sciezkaO = Path.GetFullPath(sciezkaO);
                plikPrj = textBox18.Text;
                if (File.Exists(plikPrj) == false)
                {
                    MessageBox.Show("Brak Pliku z Definicją Układu Współrzędnych !");
                }
                else
                {
                    sciezkaW = textBox17.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu wynikowego !");
                    }
                    else
                    {
                        sciezkaW += @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        if (sciezkaO == sciezkaW)
                        {
                            MessageBox.Show("Ścieżka do Katalogu z Danymi Ta Sama Co Do Katalogu Wynikowego !");
                        }
                        else
                        {
                            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
                            SumaPLIK = 0;
                            foreach (string iPlik in Pliki)
                            {
                                ++SumaPLIK;
                            }
                            if (SumaPLIK == 0)
                            {
                                MessageBox.Show("Brak Tifów w Katalogu z Danymi !");
                            }
                            else
                            {
                                string[] Pliki2 = Directory.GetFiles(sciezkaO, "*.tfw");
                                int SumaPLIK2 = 0;
                                foreach (string iPlik in Pliki2)
                                {
                                    ++SumaPLIK2;
                                }
                                if (SumaPLIK2 == 0)
                                {
                                    MessageBox.Show("Brak Plików Tfw w Katalogu z Danymi !");
                                }
                                else
                                {
                                    if (SumaPLIK == SumaPLIK2)
                                    {
                                        PlikExeGDWarp = @"C:\Program Files\INDEOv2\bin\gdalwarp.exe";
                                        if (File.Exists(PlikExeGDWarp) == false)
                                        {
                                            MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin !");
                                        }
                                        else
                                        {
                                            System.Environment.SetEnvironmentVariable("GDAL_DATA", @"C:\Program Files\INDEOv2\bin");
                                            PlikExeGDWarp = @"""C:\Program Files\INDEOv2\bin\gdalwarp.exe""";
                                            TIFF2GeoTIFF();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ilość Tifów nie zgadza się z ilością Plików Tfw !");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void TIFF2GeoTIFF()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button26.Enabled = false;
            textBox19.Enabled = false;
            textBox18.Enabled = false;
            textBox17.Enabled = false;
            button25.Enabled = false;
            button24.Enabled = false;
            button22.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_TIFF2GeoTIFF_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_TIFF2GeoTIFF_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_TIFF2GeoTIFF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono: " + SumaPLIK.ToString() + " Tifów");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button26.Enabled = true;
            textBox19.Enabled = true;
            textBox18.Enabled = true;
            textBox17.Enabled = true;
            button25.Enabled = true;
            button24.Enabled = true;
            button22.Enabled = true;
        }

        private void BackgroundWorker_TIFF2GeoTIFF_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                if (File.Exists(sciezkaO + tPlik))
                {
                    if (File.Exists(sciezkaW + tPlik))
                    {
                        File.Delete(sciezkaW + tPlik);
                    }
                    Process MyProcess1 = new Process();
                    MyProcess1.StartInfo.FileName = PlikExeGDWarp;
                    MyProcess1.StartInfo.Arguments = @"-s_srs """ + plikPrj + @""" """ + iPlik + @""" """ + sciezkaW + nPlik + @""" -overwrite -cvmd '' -of GTiff";
                    MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    MyProcess1.Start();
                    MyProcess1.WaitForExit();
                    File.Copy(sciezkaO + tPlik, sciezkaW + tPlik);
                }
                else
                {
                    MessageBox.Show("Brak Pliku Tfw ! " + tPlik);
                }
            }
        }

        private void TextBox19_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox19.Text = files[0];
                }
            }
        }

        private void TextBox19_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox18_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".prj" || roz == ".PRJ")
                    {
                        textBox18.Text = files[0];
                    }
                    else
                    {
                        textBox18.Text = "";
                    }
                }
            }
        }

        private void TextBox18_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox17_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox17.Text = files[0];
                }
            }
        }

        private void TextBox17_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------ŁĄCZENIE KANAŁÓW-----------------------------------------------------------------

        private void TextBox22_TextChanged(object sender, EventArgs e)
        {
            plik1 = textBox22.Text;
        }

        private void Button29_Click(object sender, EventArgs e)
        {
            if (openFileDialog5.ShowDialog() == DialogResult.OK)
            {
                textBox22.Text = openFileDialog5.FileName;
            }
        }

        private void TextBox21_TextChanged(object sender, EventArgs e)
        {
            plik2 = textBox21.Text;
        }

        private void Button28_Click(object sender, EventArgs e)
        {
            if (openFileDialog5.ShowDialog() == DialogResult.OK)
            {
                textBox21.Text = openFileDialog5.FileName;
            }
        }

        private void TextBox20_TextChanged(object sender, EventArgs e)
        {
            plik3 = textBox20.Text;
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            if (openFileDialog5.ShowDialog() == DialogResult.OK)
            {
                textBox20.Text = openFileDialog5.FileName;
            }
        }

        private void TextBox25_TextChanged(object sender, EventArgs e)
        {
            plik4 = textBox25.Text;
        }

        private void Button32_Click(object sender, EventArgs e)
        {
            if (openFileDialog5.ShowDialog() == DialogResult.OK)
            {
                textBox25.Text = openFileDialog5.FileName;
            }
        }

        private void Button30_Click(object sender, EventArgs e)
        {
            plik1 = textBox22.Text;
            plik2 = textBox21.Text;
            plik3 = textBox20.Text;
            plik4 = textBox25.Text;
            SumaPLIK = 1;
            if ((File.Exists(plik1) == false) && (File.Exists(plik2) == false) && (File.Exists(plik3) == false) && (File.Exists(plik4) == false))
            {
                MessageBox.Show("Brak Plików do połączenia !");
            }
            else
            {
                katalogQGIS = textBox62.Text;
                PlikExePython = katalogQGIS + @"\bin\python-qgis.bat";
                if (File.Exists(PlikExePython) == false)
                {
                    MessageBox.Show(@"Wskaż ścieżkę do katalogu QGIS !" + Environment.NewLine + @"Pomarańczowa Gwiazdka w Lewym Górnym Rogu !");
                }
                else
                {
                    PlikExePython = @"""" + PlikExePython + @"""";
                    LaczKan();
                }
            }
        }

        private void LaczKan()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button30.Enabled = false;
            textBox20.Enabled = false;
            textBox21.Enabled = false;
            textBox22.Enabled = false;
            textBox25.Enabled = false;
            button27.Enabled = false;
            button28.Enabled = false;
            button29.Enabled = false;
            button32.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_LaczKan_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_LaczKan_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_LaczKan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Połączono");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button30.Enabled = true;
            textBox20.Enabled = true;
            textBox21.Enabled = true;
            textBox22.Enabled = true;
            textBox25.Enabled = true;
            button27.Enabled = true;
            button28.Enabled = true;
            button29.Enabled = true;
            button32.Enabled = true;
        }

        private void BackgroundWorker_LaczKan_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            ++progressBarVal;
            BackgroundWorker1.ReportProgress(progressBarVal);
            if (File.Exists(plik1) == false)
            {
                if (File.Exists(plik2) == false)
                {
                    if (File.Exists(plik3) == false)
                    {
                        wPlik = plik4.Remove(plik4.Length - 4) + "_NEW.tif";
                    }
                    else
                    {
                        wPlik = plik3.Remove(plik3.Length - 4) + "_NEW.tif";
                    }
                }
                else
                {
                    wPlik = plik2.Remove(plik2.Length - 4) + "_NEW.tif";
                }
            }
            else
            {
                wPlik = plik1.Remove(plik1.Length - 4) + "_NEW.tif";
            }
            Process MyProcess1 = new Process();
            MyProcess1.StartInfo.FileName = PlikExePython;
            MyProcess1.StartInfo.Arguments = @"""" + katalogQGIS + @"\apps\Python39\Scripts\gdal_merge.py"" -separate -of GTiff -o """ + wPlik + @""" """ + plik1 + @""" """ + plik2 + @""" """ + plik3 + @""" """ + plik4 + @"""";
            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess1.Start();
            MyProcess1.WaitForExit();
        }

        private void TextBox22_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".tif" || roz == ".TIF")
                    {
                        textBox22.Text = files[0];
                    }
                    else
                    {
                        textBox22.Text = "";
                    }
                }
            }
        }

        private void TextBox22_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox21_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".tif" || roz == ".TIF")
                    {
                        textBox21.Text = files[0];
                    }
                    else
                    {
                        textBox21.Text = "";
                    }
                }
            }
        }

        private void TextBox21_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox20_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".tif" || roz == ".TIF")
                    {
                        textBox20.Text = files[0];
                    }
                    else
                    {
                        textBox20.Text = "";
                    }
                }
            }
        }

        private void TextBox20_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox25_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".tif" || roz == ".TIF")
                    {
                        textBox25.Text = files[0];
                    }
                    else
                    {
                        textBox25.Text = "";
                    }
                }
            }
        }

        private void TextBox25_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------SAT------------------------------------------------------------------------------

        private void TextBox29_TextChanged(object sender, EventArgs e)
        {
            sciezkaO = textBox29.Text;
        }

        private void Button34_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox29.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox28_TextChanged(object sender, EventArgs e)
        {
            plikPrj = textBox28.Text;
        }

        private void Button33_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox28.Text = openFileDialog3.FileName;
            }
        }

        private void TextBox24_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox24.Text;
        }

        private void Button31_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox24.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                label59.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label59.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                label60.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label60.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                label62.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label62.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                label63.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label63.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                label92.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label92.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void Button35_Click(object sender, EventArgs e)
        {
            sciezkaO = textBox29.Text;
            if (Directory.Exists(sciezkaO) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi - Pliki Tiff !");
            }
            else
            {
                sciezkaO += @"\";
                sciezkaO = Path.GetFullPath(sciezkaO);
                plikPrj = textBox28.Text;
                if (File.Exists(plikPrj) == false)
                {
                    MessageBox.Show("Brak Pliku z Definicją Układu Współrzędnych !");
                }
                else
                {
                    sciezkaW = textBox24.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu wynikowego !");
                    }
                    else
                    {
                        sciezkaW += @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
                        SumaPLIK = 0;
                        foreach (string iPlik in Pliki)
                        {
                            ++SumaPLIK;
                        }
                        if (SumaPLIK == 0)
                        {
                            MessageBox.Show("Brak Tifów w Katalogu z Danymi !");
                        }
                        else
                        {
                            string[] Pliki2 = Directory.GetFiles(sciezkaO, "*.tfw");
                            int SumaPLIK2 = 0;
                            foreach (string iPlik in Pliki2)
                            {
                                ++SumaPLIK2;
                            }
                            if (SumaPLIK2 == 0)
                            {
                                MessageBox.Show("Brak Plików Tfw w Katalogu z Danymi !");
                            }
                            else
                            {
                                if (SumaPLIK == SumaPLIK2)
                                {
                                    PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                                    PlikExeGDWarp = @"C:\Program Files\INDEOv2\bin\gdalwarp.exe";
                                    PlikExeGDTrans = @"C:\Program Files\INDEOv2\bin\gdal_translate.exe";
                                    PlikExeINPHOPYR = @"C:\Program Files\INDEOv2\bin\make_pyr.exe";
                                    if (File.Exists(PlikExeGDWarp) == false || File.Exists(PlikExeGDTrans) == false || File.Exists(PlikExeEXIF) == false || File.Exists(PlikExeINPHOPYR) == false)
                                    {
                                        MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin !");
                                    }
                                    else
                                    {
                                        PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                                        System.Environment.SetEnvironmentVariable("GDAL_DATA", @"C:\Program Files\INDEOv2\bin");
                                        PlikExeGDWarp = @"""C:\Program Files\INDEOv2\bin\gdalwarp.exe""";
                                        PlikExeGDTrans = @"""C:\Program Files\INDEOv2\bin\gdal_translate.exe""";
                                        PlikExeINPHOPYR = @"""C:\Program Files\INDEOv2\bin\make_pyr.exe""";
                                        SAT();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Ilość Tifów nie zgadza się z ilością Plików Tfw !");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SAT()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button35.Enabled = false;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
            checkBox5.Enabled = false;
            radioButton8.Enabled = false;
            radioButton9.Enabled = false;
            textBox24.Enabled = false;
            textBox28.Enabled = false;
            textBox29.Enabled = false;
            button31.Enabled = false;
            button33.Enabled = false;
            button34.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_SAT_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_SAT_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_SAT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Wygenerowano: " + SumaPLIK.ToString() + " Tifów");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button35.Enabled = true;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
            radioButton8.Enabled = true;
            radioButton9.Enabled = true;
            textBox24.Enabled = true;
            textBox28.Enabled = true;
            textBox29.Enabled = true;
            button31.Enabled = true;
            button33.Enabled = true;
            button34.Enabled = true;
        }

        private void BackgroundWorker_SAT_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string sciezkaW1 = sciezkaW + @"\2_ORTO_8BIT\RGB";
            string sciezkaW2 = sciezkaW + @"\2_ORTO_8BIT\CIR";
            string sciezkaW3 = sciezkaW + @"\1_ORTO_16BIT\RGB";
            string sciezkaW4 = sciezkaW + @"\1_ORTO_16BIT\CIR";
            string sciezkaW5 = sciezkaW + @"\1_ORTO_16BIT\4K";
            if (checkBox1.Checked == true)
            {
                Directory.CreateDirectory(sciezkaW1);
                sciezkaW1 = sciezkaW1 + @"\";
                sciezkaW1 = Path.GetFullPath(sciezkaW1);
            }
            if (checkBox2.Checked == true)
            {
                Directory.CreateDirectory(sciezkaW2);
                sciezkaW2 = sciezkaW2 + @"\";
                sciezkaW2 = Path.GetFullPath(sciezkaW2);
            }
            if (checkBox3.Checked == true)
            {
                Directory.CreateDirectory(sciezkaW3);
                sciezkaW3 = sciezkaW3 + @"\";
                sciezkaW3 = Path.GetFullPath(sciezkaW3);
            }
            if (checkBox4.Checked == true)
            {
                Directory.CreateDirectory(sciezkaW4);
                sciezkaW4 = sciezkaW4 + @"\";
                sciezkaW4 = Path.GetFullPath(sciezkaW4);
            }
            if (checkBox5.Checked == true)
            {
                Directory.CreateDirectory(sciezkaW5);
                sciezkaW5 = sciezkaW5 + @"\";
                sciezkaW5 = Path.GetFullPath(sciezkaW5);
            }
            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                if (checkBox1.Checked == true)
                {
                    nPlik = Path.GetFileName(iPlik);
                    string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                    if (File.Exists(sciezkaO + tPlik))
                    {
                        label59.ForeColor = System.Drawing.Color.Yellow;
                        if (radioButton9.Checked == false)
                        {
                            string imgO = iPlik;
                            string imgW = sciezkaW1 + nPlik;
                            imgO = imgO.Replace(@"\", @"/");
                            imgW = imgW.Replace(@"\", @"/");
                            string jsxPlik = sciezkaW1 + "RGB.jsx";
                            string[] jsxZawartosc =
                            {
                            @"var plik = """ + imgO + @""";",
                            "var img = app.open(new File(plik));",
                            "img.bitsPerChannel = BitsPerChannelType.EIGHT;",
                            "img.changeMode(ChangeMode.MULTICHANNEL);",
                            "img.channels[3].remove();",
                            "img.channels[1].duplicate(img, ElementPlacement.PLACEATEND);",
                            "img.channels[0].duplicate(img, ElementPlacement.PLACEATEND);",
                            "img.channels[0].remove();",
                            "img.channels[0].remove();",
                            "img.changeMode(ChangeMode.RGB);",
                            @"var wynik = new File(""" + imgW + @""")",
                            "var opcje = new TiffSaveOptions();",
                            "img.saveAs(wynik, opcje, true, Extension.LOWERCASE);",
                            "img.close(SaveOptions.DONOTSAVECHANGES);",
                            @"var idquit = charIDToTypeID(""quit"");",
                            "executeAction(idquit, undefined, DialogModes.ALL);",
                            };
                            File.WriteAllLines(jsxPlik, jsxZawartosc);
                            Process MyProcess1 = new Process();
                            MyProcess1.StartInfo.FileName = "Photoshop.exe";
                            MyProcess1.StartInfo.Arguments = jsxPlik;
                            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            MyProcess1.Start();
                            MyProcess1.WaitForExit();
                            File.Delete(jsxPlik);
                        }
                        else
                        {
                            Process MyProcess1 = new Process();
                            MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                            MyProcess1.StartInfo.Arguments = @"""" + iPlik + @""" -ot byte -strict -scale 0 65535 -b 3 -b 2 -b 1 """ + sciezkaW1 + nPlik + @"""";
                            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            MyProcess1.Start();
                            MyProcess1.WaitForExit();
                        }
                        File.Copy(sciezkaO + tPlik, sciezkaW1 + tPlik);
                        label59.ForeColor = System.Drawing.Color.Red;
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeGDWarp;
                        MyProcess2.StartInfo.Arguments = @"-s_srs """ + plikPrj + @""" """ + sciezkaW1 + nPlik + @""" """ + sciezkaW1 + @"TempGeo.tif"" -overwrite -cvmd '' -of GTiff";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(sciezkaW1 + nPlik);
                        File.Delete(sciezkaW1 + tPlik);
                        label59.ForeColor = System.Drawing.Color.Blue;
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeINPHOPYR;
                        MyProcess3.StartInfo.Arguments = @"""" + sciezkaW1 + @"TempGeo.tif"" -out """ + sciezkaW1 + nPlik + @""" -TIFF -JPEG -QFACTOR 95 -tile 256";
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                        File.Delete(sciezkaW1 + "TempGeo.tif");
                        label59.ForeColor = System.Drawing.Color.ForestGreen;
                        Process MyProcess4 = new Process();
                        MyProcess4.StartInfo.FileName = PlikExeEXIF;
                        MyProcess4.StartInfo.Arguments = @" -XMP:All= -imagedescription= -software=""Trimble Germany GmbH"" -resolutionunit= -xresolution= -yresolution= -modifydate= -overwrite_original_in_place """ + sciezkaW1 + nPlik + @"""";
                        MyProcess4.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess4.Start();
                        MyProcess4.WaitForExit();
                    }
                    else
                    {
                        MessageBox.Show("Brak Pliku Tfw ! " + tPlik);
                    }
                }
                if (checkBox2.Checked == true)
                {
                    nPlik = Path.GetFileName(iPlik);
                    string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                    if (File.Exists(sciezkaO + tPlik))
                    {
                        label60.ForeColor = System.Drawing.Color.Yellow;
                        if (radioButton9.Checked == false)
                        {
                            string imgO = iPlik;
                            string imgW = sciezkaW2 + nPlik;
                            imgO = imgO.Replace(@"\", @"/");
                            imgW = imgW.Replace(@"\", @"/");
                            string jsxPlik = sciezkaW2 + "CIR.jsx";
                            string[] jsxZawartosc =
                            {
                            @"var plik = """ + imgO + @""";",
                            "var img = app.open(new File(plik));",
                            "img.bitsPerChannel = BitsPerChannelType.EIGHT;",
                            "img.changeMode(ChangeMode.MULTICHANNEL);",
                            "img.channels[0].remove();",
                            "img.channels[1].duplicate(img, ElementPlacement.PLACEATEND);",
                            "img.channels[0].duplicate(img, ElementPlacement.PLACEATEND);",
                            "img.channels[0].remove();",
                            "img.channels[0].remove();",
                            "img.changeMode(ChangeMode.RGB);",
                            @"var wynik = new File(""" + imgW + @""")",
                            "var opcje = new TiffSaveOptions();",
                            "img.saveAs(wynik, opcje, true, Extension.LOWERCASE);",
                            "img.close(SaveOptions.DONOTSAVECHANGES);",
                            @"var idquit = charIDToTypeID(""quit"");",
                            "executeAction(idquit, undefined, DialogModes.ALL);",
                            };
                            File.WriteAllLines(jsxPlik, jsxZawartosc);
                            Process MyProcess1 = new Process();
                            MyProcess1.StartInfo.FileName = "Photoshop.exe";
                            MyProcess1.StartInfo.Arguments = jsxPlik;
                            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            MyProcess1.Start();
                            MyProcess1.WaitForExit();
                            File.Delete(jsxPlik);
                        }
                        else
                        {
                            Process MyProcess1 = new Process();
                            MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                            MyProcess1.StartInfo.Arguments = @"""" + iPlik + @""" -ot byte -strict -scale 0 65535 -b 4 -b 3 -b 2 """ + sciezkaW2 + nPlik + @"""";
                            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            MyProcess1.Start();
                            MyProcess1.WaitForExit();
                        }
                        File.Copy(sciezkaO + tPlik, sciezkaW2 + tPlik);
                        label60.ForeColor = System.Drawing.Color.Red;
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeGDWarp;
                        MyProcess2.StartInfo.Arguments = @"-s_srs """ + plikPrj + @""" """ + sciezkaW2 + nPlik + @""" """ + sciezkaW2 + @"TempGeo.tif"" -overwrite -cvmd '' -of GTiff";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(sciezkaW2 + nPlik);
                        File.Delete(sciezkaW2 + tPlik);
                        label60.ForeColor = System.Drawing.Color.Blue;
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeINPHOPYR;
                        MyProcess3.StartInfo.Arguments = @"""" + sciezkaW2 + @"TempGeo.tif"" -out """ + sciezkaW2 + nPlik + @""" -TIFF -JPEG -QFACTOR 95 -tile 256";
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                        File.Delete(sciezkaW2 + "TempGeo.tif");
                        label60.ForeColor = System.Drawing.Color.ForestGreen;
                        Process MyProcess4 = new Process();
                        MyProcess4.StartInfo.FileName = PlikExeEXIF;
                        MyProcess4.StartInfo.Arguments = @" -XMP:All= -imagedescription= -software=""Trimble Germany GmbH"" -resolutionunit= -xresolution= -yresolution= -modifydate= -overwrite_original_in_place """ + sciezkaW2 + nPlik + @"""";
                        MyProcess4.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess4.Start();
                        MyProcess4.WaitForExit();
                    }
                    else
                    {
                        MessageBox.Show("Brak Pliku Tfw ! " + tPlik);
                    }
                }
                if (checkBox3.Checked == true)
                {
                    nPlik = Path.GetFileName(iPlik);
                    string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                    if (File.Exists(sciezkaO + tPlik))
                    {
                        label62.ForeColor = System.Drawing.Color.Yellow;
                        if (radioButton9.Checked == false)
                        {
                            string imgO = iPlik;
                            string imgW = sciezkaW3 + nPlik;
                            imgO = imgO.Replace(@"\", @"/");
                            imgW = imgW.Replace(@"\", @"/");
                            string jsxPlik = sciezkaW3 + "RGB.jsx";
                            string[] jsxZawartosc =
                            {
                            @"var plik = """ + imgO + @""";",
                            "var img = app.open(new File(plik));",
                            "img.changeMode(ChangeMode.MULTICHANNEL);",
                            "img.channels[3].remove();",
                            "img.channels[1].duplicate(img, ElementPlacement.PLACEATEND);",
                            "img.channels[0].duplicate(img, ElementPlacement.PLACEATEND);",
                            "img.channels[0].remove();",
                            "img.channels[0].remove();",
                            "img.changeMode(ChangeMode.RGB);",
                            @"var wynik = new File(""" + imgW + @""")",
                            "var opcje = new TiffSaveOptions();",
                            "img.saveAs(wynik, opcje, true, Extension.LOWERCASE);",
                            "img.close(SaveOptions.DONOTSAVECHANGES);",
                            @"var idquit = charIDToTypeID(""quit"");",
                            "executeAction(idquit, undefined, DialogModes.ALL);",
                            };
                            File.WriteAllLines(jsxPlik, jsxZawartosc);
                            Process MyProcess1 = new Process();
                            MyProcess1.StartInfo.FileName = "Photoshop.exe";
                            MyProcess1.StartInfo.Arguments = jsxPlik;
                            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            MyProcess1.Start();
                            MyProcess1.WaitForExit();
                            File.Delete(jsxPlik);
                        }
                        else
                        {
                            Process MyProcess1 = new Process();
                            MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                            MyProcess1.StartInfo.Arguments = @"""" + iPlik + @""" -strict -colorinterp red,green,blue -b 3 -b 2 -b 1 """ + sciezkaW3 + nPlik + @"""";
                            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            MyProcess1.Start();
                            MyProcess1.WaitForExit();
                        }
                        File.Copy(sciezkaO + tPlik, sciezkaW3 + tPlik);
                        label62.ForeColor = System.Drawing.Color.Red;
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeGDWarp;
                        MyProcess2.StartInfo.Arguments = @"-s_srs """ + plikPrj + @""" """ + sciezkaW3 + nPlik + @""" """ + sciezkaW3 + @"TempGeo.tif"" -overwrite -cvmd '' -of GTiff";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(sciezkaW3 + nPlik);
                        File.Delete(sciezkaW3 + tPlik);
                        label62.ForeColor = System.Drawing.Color.Blue;
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeINPHOPYR;
                        MyProcess3.StartInfo.Arguments = @"""" + sciezkaW3 + @"TempGeo.tif"" -out """ + sciezkaW3 + nPlik + @""" -TIFF -tile 256";
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                        File.Delete(sciezkaW3 + "TempGeo.tif");
                        label62.ForeColor = System.Drawing.Color.ForestGreen;
                        Process MyProcess4 = new Process();
                        MyProcess4.StartInfo.FileName = PlikExeEXIF;
                        MyProcess4.StartInfo.Arguments = @" -XMP:All= -imagedescription= -software=""Trimble Germany GmbH"" -resolutionunit= -xresolution= -yresolution= -modifydate= -overwrite_original_in_place """ + sciezkaW3 + nPlik + @"""";
                        MyProcess4.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess4.Start();
                        MyProcess4.WaitForExit();
                    }
                    else
                    {
                        MessageBox.Show("Brak Pliku Tfw ! " + tPlik);
                    }
                }
                if (checkBox4.Checked == true)
                {
                    nPlik = Path.GetFileName(iPlik);
                    string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                    if (File.Exists(sciezkaO + tPlik))
                    {
                        label63.ForeColor = System.Drawing.Color.Yellow;
                        if (radioButton9.Checked == false)
                        {
                            string imgO = iPlik;
                            string imgW = sciezkaW4 + nPlik;
                            imgO = imgO.Replace(@"\", @"/");
                            imgW = imgW.Replace(@"\", @"/");
                            string jsxPlik = sciezkaW4 + "CIR.jsx";
                            string[] jsxZawartosc =
                            {
                            @"var plik = """ + imgO + @""";",
                            "var img = app.open(new File(plik));",
                            "img.changeMode(ChangeMode.MULTICHANNEL);",
                            "img.channels[0].remove();",
                            "img.channels[1].duplicate(img, ElementPlacement.PLACEATEND);",
                            "img.channels[0].duplicate(img, ElementPlacement.PLACEATEND);",
                            "img.channels[0].remove();",
                            "img.channels[0].remove();",
                            "img.changeMode(ChangeMode.RGB);",
                            @"var wynik = new File(""" + imgW + @""")",
                            "var opcje = new TiffSaveOptions();",
                            "img.saveAs(wynik, opcje, true, Extension.LOWERCASE);",
                            "img.close(SaveOptions.DONOTSAVECHANGES);",
                            @"var idquit = charIDToTypeID(""quit"");",
                            "executeAction(idquit, undefined, DialogModes.ALL);",
                            };
                            File.WriteAllLines(jsxPlik, jsxZawartosc);
                            Process MyProcess1 = new Process();
                            MyProcess1.StartInfo.FileName = "Photoshop.exe";
                            MyProcess1.StartInfo.Arguments = jsxPlik;
                            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            MyProcess1.Start();
                            MyProcess1.WaitForExit();
                            File.Delete(jsxPlik);
                        }
                        else
                        {
                            Process MyProcess1 = new Process();
                            MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                            MyProcess1.StartInfo.Arguments = @"""" + iPlik + @""" -strict -colorinterp red,green,blue -b 4 -b 3 -b 2 """ + sciezkaW4 + nPlik + @"""";
                            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            MyProcess1.Start();
                            MyProcess1.WaitForExit();
                        }
                        File.Copy(sciezkaO + tPlik, sciezkaW4 + tPlik);
                        label63.ForeColor = System.Drawing.Color.Red;
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeGDWarp;
                        MyProcess2.StartInfo.Arguments = @"-s_srs """ + plikPrj + @""" """ + sciezkaW4 + nPlik + @""" """ + sciezkaW4 + @"TempGeo.tif"" -overwrite -cvmd '' -of GTiff";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(sciezkaW4 + nPlik);
                        File.Delete(sciezkaW4 + tPlik);
                        label63.ForeColor = System.Drawing.Color.Blue;
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeINPHOPYR;
                        MyProcess3.StartInfo.Arguments = @"""" + sciezkaW4 + @"TempGeo.tif"" -out """ + sciezkaW4 + nPlik + @""" -TIFF -tile 256";
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                        File.Delete(sciezkaW4 + "TempGeo.tif");
                        label63.ForeColor = System.Drawing.Color.ForestGreen;
                        Process MyProcess4 = new Process();
                        MyProcess4.StartInfo.FileName = PlikExeEXIF;
                        MyProcess4.StartInfo.Arguments = @" -XMP:All= -imagedescription= -software=""Trimble Germany GmbH"" -resolutionunit= -xresolution= -yresolution= -modifydate= -overwrite_original_in_place """ + sciezkaW4 + nPlik + @"""";
                        MyProcess4.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess4.Start();
                        MyProcess4.WaitForExit();
                    }
                    else
                    {
                        MessageBox.Show("Brak Pliku Tfw ! " + tPlik);
                    }
                }
                if (checkBox5.Checked == true)
                {
                    nPlik = Path.GetFileName(iPlik);
                    string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                    if (File.Exists(sciezkaO + tPlik))
                    {
                        label92.ForeColor = System.Drawing.Color.Yellow;
                        if (radioButton9.Checked == false)
                        {
                            string imgO = iPlik;
                            string imgW = sciezkaW5 + nPlik;
                            imgO = imgO.Replace(@"\", @"/");
                            imgW = imgW.Replace(@"\", @"/");
                            string jsxPlik = sciezkaW5 + "4K.jsx";
                            string[] jsxZawartosc =
                            {
                            @"var plik = """ + imgO + @""";",
                            "var img = app.open(new File(plik));",
                            "img.changeMode(ChangeMode.MULTICHANNEL);",
                            "img.channels[2].duplicate(img, ElementPlacement.PLACEATEND);",
                            "img.channels[1].duplicate(img, ElementPlacement.PLACEATEND);",
                            "img.channels[0].duplicate(img, ElementPlacement.PLACEATEND);",
                            "img.channels[3].duplicate(img, ElementPlacement.PLACEATEND);",
                            "img.channels[0].remove();",
                            "img.channels[0].remove();",
                            "img.channels[0].remove();",
                            "img.channels[0].remove();",
                            "img.changeMode(ChangeMode.RGB);",
                            @"var wynik = new File(""" + imgW + @""")",
                            "var opcje = new TiffSaveOptions();",
                            "img.saveAs(wynik, opcje, true, Extension.LOWERCASE);",
                            "img.close(SaveOptions.DONOTSAVECHANGES);",
                            @"var idquit = charIDToTypeID(""quit"");",
                            "executeAction(idquit, undefined, DialogModes.ALL);",
                            };
                            File.WriteAllLines(jsxPlik, jsxZawartosc);
                            Process MyProcess1 = new Process();
                            MyProcess1.StartInfo.FileName = "Photoshop.exe";
                            MyProcess1.StartInfo.Arguments = jsxPlik;
                            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            MyProcess1.Start();
                            MyProcess1.WaitForExit();
                            File.Delete(jsxPlik);
                        }
                        else
                        {
                            Process MyProcess1 = new Process();
                            MyProcess1.StartInfo.FileName = PlikExeGDTrans;
                            MyProcess1.StartInfo.Arguments = @"""" + iPlik + @""" -strict -colorinterp red,green,blue,alpha -b 3 -b 2 -b 1 -b 4 """ + sciezkaW5 + nPlik + @"""";
                            MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            MyProcess1.Start();
                            MyProcess1.WaitForExit();
                        }
                        File.Copy(sciezkaO + tPlik, sciezkaW5 + tPlik);
                        label92.ForeColor = System.Drawing.Color.Red;
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeGDWarp;
                        MyProcess2.StartInfo.Arguments = @"-s_srs """ + plikPrj + @""" """ + sciezkaW5 + nPlik + @""" """ + sciezkaW5 + @"TempGeo.tif"" -overwrite -cvmd '' -of GTiff";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        File.Delete(sciezkaW5 + nPlik);
                        File.Delete(sciezkaW5 + tPlik);
                        label92.ForeColor = System.Drawing.Color.Blue;
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeINPHOPYR;
                        MyProcess3.StartInfo.Arguments = @"""" + sciezkaW5 + @"TempGeo.tif"" -out """ + sciezkaW5 + nPlik + @""" -TIFF -tile 256";
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                        File.Delete(sciezkaW5 + "TempGeo.tif");
                        label92.ForeColor = System.Drawing.Color.ForestGreen;
                        Process MyProcess4 = new Process();
                        MyProcess4.StartInfo.FileName = PlikExeEXIF;
                        MyProcess4.StartInfo.Arguments = @" -XMP:All= -imagedescription= -software=""Trimble Germany GmbH"" -resolutionunit= -xresolution= -yresolution= -modifydate= -overwrite_original_in_place """ + sciezkaW5 + nPlik + @"""";
                        MyProcess4.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess4.Start();
                        MyProcess4.WaitForExit();
                    }
                    else
                    {
                        MessageBox.Show("Brak Pliku Tfw ! " + tPlik);
                    }
                }
            }
        }

        private void TextBox29_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox29.Text = files[0];
                }
            }
        }

        private void TextBox29_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox28_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".prj" || roz == ".PRJ")
                    {
                        textBox28.Text = files[0];
                    }
                    else
                    {
                        textBox28.Text = "";
                    }
                }
            }
        }

        private void TextBox28_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox24_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox24.Text = files[0];
                }
            }
        }

        private void TextBox24_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------ProRail--------------------------------------------------------------------------

        private void TextBox33_TextChanged(object sender, EventArgs e)
        {
            sciezkaO = textBox33.Text;
        }

        private void Button40_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox33.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox31_TextChanged(object sender, EventArgs e)
        {
            plikDgn = textBox31.Text;
        }

        private void Button38_Click(object sender, EventArgs e)
        {
            if (openFileDialog7.ShowDialog() == DialogResult.OK)
            {
                textBox31.Text = openFileDialog7.FileName;
            }
        }

        private void TextBox32_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox32.Text;
        }

        private void Button39_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox32.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button41_Click(object sender, EventArgs e)
        {
            sciezkaO = textBox33.Text;
            if (Directory.Exists(sciezkaO) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi - Pliki Tiff !");
            }
            else
            {
                sciezkaO += @"\";
                sciezkaO = Path.GetFullPath(sciezkaO);
                plikDgn = textBox31.Text;
                if (File.Exists(plikDgn) == false)
                {
                    MessageBox.Show("Brak Pliku z zakresem do cięcia !");
                }
                else
                {
                    sciezkaW = textBox32.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu wynikowego !");
                    }
                    else
                    {
                        sciezkaW += @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        if (sciezkaO == sciezkaW)
                        {
                            MessageBox.Show("Ścieżka do Katalogu z Danymi Ta Sama co do Katalogu Wynikowego !");
                        }
                        else
                        {
                            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
                            SumaPLIK = 0;
                            foreach (string iPlik in Pliki)
                            {
                                ++SumaPLIK;
                            }
                            if (SumaPLIK == 0)
                            {
                                MessageBox.Show("Brak Tifów w Katalogu z Danymi !");
                            }
                            else
                            {
                                string[] Pliki2 = Directory.GetFiles(sciezkaO, "*.tfw");
                                int SumaPLIK2 = 0;
                                foreach (string iPlik in Pliki2)
                                {
                                    ++SumaPLIK2;
                                }
                                if (SumaPLIK2 == 0)
                                {
                                    MessageBox.Show("Brak Plików Tfw w Katalogu z Danymi !");
                                }
                                else
                                {
                                    if (SumaPLIK == SumaPLIK2)
                                    {
                                        PlikExeGM = textBox65.Text;
                                        PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                                        PlikExeINPHOPYR = @"C:\Program Files\INDEOv2\bin\make_pyr.exe";
                                        if (File.Exists(PlikExeGM) == false || File.Exists(PlikExeEXIF) == false || File.Exists(PlikExeINPHOPYR) == false)
                                        {
                                            MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin lub wskaż ścieżkę do Global Mappera !" + Environment.NewLine + @"Pomarańczowa Gwiazdka w Lewym Górnym Rogu !");
                                        }
                                        else
                                        {
                                            PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                                            PlikExeINPHOPYR = @"""C:\Program Files\INDEOv2\bin\make_pyr.exe""";
                                            PlikExeGM = @"""" + PlikExeGM + @"""";
                                            ProRail();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ilość Tifów nie zgadza się z ilością Plików Tfw !");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ProRail()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button41.Enabled = false;
            textBox33.Enabled = false;
            textBox31.Enabled = false;
            textBox32.Enabled = false;
            button40.Enabled = false;
            button38.Enabled = false;
            button39.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_ProRail_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_ProRail_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_ProRail_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Wygenerowano dla Moni " + SumaPLIK.ToString() + " Tify ;-)");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button41.Enabled = true;
            textBox33.Enabled = true;
            textBox31.Enabled = true;
            textBox32.Enabled = true;
            button40.Enabled = true;
            button38.Enabled = true;
            button39.Enabled = true;
            label71.ForeColor = System.Drawing.Color.ForestGreen;
            label77.ForeColor = System.Drawing.Color.ForestGreen;
            label70.ForeColor = System.Drawing.Color.ForestGreen;
            label78.ForeColor = System.Drawing.Color.ForestGreen;
            label74.ForeColor = System.Drawing.Color.ForestGreen;
            label79.ForeColor = System.Drawing.Color.ForestGreen;
        }

        private void BackgroundWorker_ProRail_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                label71.ForeColor = System.Drawing.Color.Red;
                label77.ForeColor = System.Drawing.Color.Red;
                label70.ForeColor = System.Drawing.Color.Yellow;
                label78.ForeColor = System.Drawing.Color.Yellow;
                label74.ForeColor = System.Drawing.Color.Yellow;
                label79.ForeColor = System.Drawing.Color.Yellow;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                string[] gmsZawartosc =
                {
                "GLOBAL_MAPPER_SCRIPT VERSION=1.00",
                "UNLOAD_ALL",
                "SET_BG_COLOR COLOR=RGB(255,255,255)",
                @"DEFINE_PROJ PROJ_NAME=""DUTCH_GRD_DUTCH_RD""",
                "Projection     Dutch Grid (RD)",
                "Datum          AMERSFOORT",
                "Zunits         NO",
                "Units          METERS",
                "Xshift         0.000000",
                "Yshift         0.000000",
                "Parameters",
                "END_DEFINE_PROJ",
                @"IMPORT FILENAME=""" + iPlik + @""" TYPE=GEOTIFF PROJ=""DUTCH_GRD_DUTCH_RD"" CONTRAST_MODE=NONE CLIP_COLLAR=NONE",
                @"GENERATE_EQUAL_VAL_AREAS FILENAME=""" + nPlik + @""" LAYER_DESC=""BIALY"" EQUAL_COLORS=RGB(255,255,255) FORCE_RGB=YES",
                @"DEFINE_LAYER_STYLE NAME=""AREA_STYLE"" TYPE=""AREA""",
                "LayerStyle=1",
                "Type=0",
                "AreaStyleForAll=4278124097,0,81,270532608,0.0",
                "END_DEFINE_LAYER_STYLE",
                @"SET_LAYER_OPTIONS FILENAME=""BIALY"" AREA_STYLE_NAME=""AREA_STYLE"" CLIP_COLLAR=NONE",
                @"GENERATE_EQUAL_VAL_AREAS FILENAME=""" + nPlik + @""" LAYER_DESC=""CZARNY"" EQUAL_COLORS=RGB(0,0,0) FORCE_RGB=YES",
                @"DEFINE_LAYER_STYLE NAME=""AREA_STYLE"" TYPE=""AREA""",
                "LayerStyle=1",
                "Type=0",
                "AreaStyleForAll=16843073,0,81,270532608,0.0",
                "END_DEFINE_LAYER_STYLE",
                @"SET_LAYER_OPTIONS FILENAME=""CZARNY"" AREA_STYLE_NAME=""AREA_STYLE"" CLIP_COLLAR=NONE",
                @"EXPORT_RASTER FILENAME=""" + sciezkaW + @"TempGM.tif"" TYPE=GEOTIFF GEN_WORLD_FILE=YES POLYGON_CROP_FILE=""" + plikDgn + @""" POLYGON_CROP_FILE_PROJ=""DUTCH_GRD_DUTCH_RD"" INC_VECTOR_DATA=YES",
                @"SET_LAYER_OPTIONS FILENAME=""" + nPlik + @""" HIDDEN=YES",
                @"SET_LAYER_OPTIONS FILENAME=""BIALY"" HIDDEN=YES",
                @"SET_LAYER_OPTIONS FILENAME=""CZARNY"" HIDDEN=YES",
                @"IMPORT FILENAME=""" + sciezkaW + @"TempGM.tif"" TYPE=GEOTIFF PROJ=""DUTCH_GRD_DUTCH_RD"" CONTRAST_MODE=NONE CLIP_COLLAR=NONE",
                @"EXPORT_RASTER FILENAME=""" + sciezkaW + @"GM.tif"" TYPE=GEOTIFF GEN_WORLD_FILE=YES LAYER_BOUNDS=""" + nPlik + @""" USE_EXACT_BOUNDS=YES",
                "SET_BG_COLOR COLOR=RGB(255,255,255)",
                };
                string gmsPlik = sciezkaW + "GM.gms";
                File.WriteAllLines(gmsPlik, gmsZawartosc);
                Process MyProcess1 = new Process();
                MyProcess1.StartInfo.FileName = PlikExeGM;
                MyProcess1.StartInfo.Arguments = gmsPlik;
                MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess1.Start();
                MyProcess1.WaitForExit();
                label71.ForeColor = System.Drawing.Color.Blue;
                label77.ForeColor = System.Drawing.Color.Blue;
                label70.ForeColor = System.Drawing.Color.Red;
                label78.ForeColor = System.Drawing.Color.Red;
                label74.ForeColor = System.Drawing.Color.Yellow;
                label79.ForeColor = System.Drawing.Color.Yellow;
                File.Delete(gmsPlik);
                File.Delete(sciezkaW + "TempGM.tif");
                File.Delete(sciezkaW + "TempGM.tfw");
                Process MyProcess2 = new Process();
                MyProcess2.StartInfo.FileName = PlikExeINPHOPYR;
                MyProcess2.StartInfo.Arguments = @"""" + sciezkaW + @"GM.tif"" -out """ + sciezkaW + nPlik + @""" -TIFF -tile 256";
                MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess2.Start();
                MyProcess2.WaitForExit();
                label71.ForeColor = System.Drawing.Color.Blue;
                label77.ForeColor = System.Drawing.Color.Blue;
                label70.ForeColor = System.Drawing.Color.Blue;
                label78.ForeColor = System.Drawing.Color.Blue;
                label74.ForeColor = System.Drawing.Color.Red;
                label79.ForeColor = System.Drawing.Color.Red;
                File.Delete(sciezkaW + "GM.tif");
                string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                File.Move(sciezkaW + "GM.tfw", sciezkaW + tPlik);
                Process MyProcess3 = new Process();
                MyProcess3.StartInfo.FileName = PlikExeEXIF;
                MyProcess3.StartInfo.Arguments = @" -XMP:All= -imagedescription= -software=""Trimble Germany GmbH"" -resolutionunit= -xresolution= -yresolution= -modifydate= -overwrite_original_in_place """ + sciezkaW + nPlik + @"""";
                MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess3.Start();
                MyProcess3.WaitForExit();
                label71.ForeColor = System.Drawing.Color.Blue;
                label77.ForeColor = System.Drawing.Color.Blue;
                label70.ForeColor = System.Drawing.Color.Blue;
                label78.ForeColor = System.Drawing.Color.Blue;
                label74.ForeColor = System.Drawing.Color.Blue;
                label79.ForeColor = System.Drawing.Color.Blue;
            }
        }

        private void TextBox33_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox33.Text = files[0];
                }
            }
        }

        private void TextBox33_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox31_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".dgn" || roz == ".DGN" || roz == ".dxf" || roz == ".DXF")
                    {
                        textBox31.Text = files[0];
                    }
                    else
                    {
                        textBox31.Text = "";
                    }
                }
            }
        }

        private void TextBox31_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox32_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox32.Text = files[0];
                }
            }
        }

        private void TextBox32_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------TRANSF---------------------------------------------------------------------------

        private void TextBox47_TextChanged(object sender, EventArgs e)
        {
            sciezkaO = textBox47.Text;
        }

        private void Button85_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox47.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {
            plikPrjO = textBox9.Text;
        }
 
        private void Button14_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox9.Text = openFileDialog3.FileName;
            }
        }

        private void TextBox46_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox46.Text;
        }

        private void Button74_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox46.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox48_TextChanged(object sender, EventArgs e)
        {
            plikPrjW = textBox48.Text;
        }

        private void Button89_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox48.Text = openFileDialog3.FileName;
            }
        }

        private void TextBox49_TextChanged(object sender, EventArgs e)
        {
            WielkoscPix = textBox49.Text;
        }

        private void TextBox49_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && (((sender as TextBox).Text.IndexOf('.') > -1) || ((sender as TextBox).Text.Length == 0)))
            {
                e.Handled = true;
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                metodaT = " -r near ";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                metodaT = " -r bilinear ";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                metodaT = " -r cubic ";
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                metodaT = " -r cubicspline ";
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                metodaT = " -r lanczos ";
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                metodaT = " -r average ";
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                metodaT = " -r mode ";
            }

        }

        private void Button88_Click(object sender, EventArgs e)
        {
            sciezkaO = textBox47.Text;
            if (Directory.Exists(sciezkaO) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaO += @"\";
                sciezkaO = Path.GetFullPath(sciezkaO);
                plikPrjO = textBox9.Text;
                if (File.Exists(plikPrjO) == false)
                {
                    MessageBox.Show("Brak Pliku z Definicją Układu Współrzędnych Wejściowych !");
                }
                else
                {
                    sciezkaW = textBox46.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                    }
                    else
                    {
                        sciezkaW += @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        if (sciezkaO == sciezkaW)
                        {
                            MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Danymi !");
                        }
                        else
                        {                             
                            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
                            SumaPLIK = 0;
                            foreach (string iPlik in Pliki)
                            {
                                ++SumaPLIK;
                            }
                            if (SumaPLIK == 0)
                            {
                                MessageBox.Show("Brak Tifów w Katalogu z Danymi !");
                            }
                            else
                            {
                                plikPrjW = textBox48.Text;
                                if (File.Exists(plikPrjW) == false)
                                {
                                    MessageBox.Show("Brak Pliku z Definicją Układu Współrzędnych Wyjściowych !");
                                }
                                else
                                {
                                    PlikExeGDWarp = @"C:\Program Files\INDEOv2\bin\gdalwarp.exe";
                                    if (File.Exists(PlikExeGDWarp) == false) 
                                    {
                                        MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin (gdalwarp.exe) !");
                                    }
                                    else
                                    {
                                        System.Environment.SetEnvironmentVariable("GDAL_DATA", @"C:\Program Files\INDEOv2\bin");
                                        PlikExeGDWarp = @"""C:\Program Files\INDEOv2\bin\gdalwarp.exe""";
                                        SprPixTr();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SprPixTr()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            if (WielkoscPix == null)
            {
                MessageBox.Show("Wpisz Wielkość Piksela !");
            }
            else
            {
                bool wynik = decimal.TryParse(WielkoscPix, out _);
                if (wynik == true)
                {
                    if (radioButton15.Checked == true)
                    {
                        kolorMode = " -dstnodata 0 ";
                    }
                    else
                    {
                        kolorMode = " -dstnodata 255 ";
                    }
                    Transformacja();
                }
                else
                {
                    MessageBox.Show("Błędna Wielkość Piksela !");
                }
            }
        }

        private void Transformacja()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button88.Enabled = false;
            button85.Enabled = false;
            button14.Enabled = false;
            button74.Enabled = false;
            button89.Enabled = false;
            textBox47.Enabled = false;
            textBox9.Enabled = false;
            textBox46.Enabled = false;
            textBox48.Enabled = false;
            textBox49.Enabled = false;
            radioButton15.Enabled = false;
            radioButton14.Enabled = false;
            comboBox1.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_Transformacja_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_Transformacja_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_Transformacja_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetransformowano " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button88.Enabled = true;
            button85.Enabled = true;
            button14.Enabled = true;
            button74.Enabled = true;
            button89.Enabled = true;
            textBox47.Enabled = true;
            textBox9.Enabled = true;
            textBox46.Enabled = true;
            textBox48.Enabled = true;
            textBox49.Enabled = true;
            radioButton15.Enabled = true;
            radioButton14.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void BackgroundWorker_Transformacja_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                Process MyProcess = new Process();
                MyProcess.StartInfo.FileName = PlikExeGDWarp;
                MyProcess.StartInfo.Arguments = metodaT + kolorMode + @"-co TILED=YES -co TFW=YES -co PHOTOMETRIC=RGB -tr " + WielkoscPix + " -" + WielkoscPix + @" -tap -multi -overwrite -s_srs """ + plikPrjO + @""" -t_srs """ + plikPrjW + @""" """ + sciezkaO + nPlik + @""" """ + sciezkaW + nPlik + @"""";
                MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess.Start();
                MyProcess.WaitForExit();
            }
        }

        private void TextBox47_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox47.Text = files[0];
                }
            }
        }

        private void TextBox47_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox9_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".prj" || roz == ".PRJ")
                    {
                        textBox9.Text = files[0];
                    }
                    else
                    {
                        textBox9.Text = "";
                    }
                }
            }
        }

        private void TextBox9_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox46_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox46.Text = files[0];
                }
            }
        }

        private void TextBox46_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox48_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".prj" || roz == ".PRJ")
                    {
                        textBox48.Text = files[0];
                    }
                    else
                    {
                        textBox48.Text = "";
                    }
                }
            }
        }

        private void TextBox48_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------B/W_Pix_GM-----------------------------------------------------------------------

        private void TextBox53_TextChanged(object sender, EventArgs e)
        {
            sciezkaO = textBox53.Text;
        }

        private void Button93_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox53.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox51_TextChanged(object sender, EventArgs e)
        {
            plikPrj = textBox51.Text;
        }

        private void Button91_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox51.Text = openFileDialog3.FileName;
            }
        }

        private void TextBox52_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox52.Text;
        }

        private void Button92_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox52.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox54_TextChanged(object sender, EventArgs e)
        {
            plikShp = textBox54.Text;
        }

        private void Button95_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox54.Text = openFileDialog1.FileName;
            }
        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                textBox54.Enabled = true;
                button95.Enabled = true;
                groupBox7.Enabled = true;
                radioButton20.Enabled = true;
                radioButton16.Enabled = true;
            }
            else
            {
                textBox54.Enabled = false;
                button95.Enabled = false;
                groupBox7.Enabled = false;
                radioButton20.Enabled = false;
                radioButton16.Enabled = false;
            }
        }

        private void Button94_Click(object sender, EventArgs e)
        {
            sciezkaO = textBox53.Text;
            if (Directory.Exists(sciezkaO) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi - Pliki Tif !");
            }
            else
            {
                sciezkaO += @"\";
                sciezkaO = Path.GetFullPath(sciezkaO);
                plikPrj = textBox51.Text;
                if (File.Exists(plikPrj) == false)
                {
                    MessageBox.Show("Brak Pliku z definicja układu współrzędnych !");
                }
                else
                {
                    sciezkaW = textBox52.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu wynikowego !");
                    }
                    else
                    {
                        sciezkaW += @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        if (sciezkaO == sciezkaW)
                        {
                            MessageBox.Show("Ścieżka do Katalogu z Danymi Ta Sama co do Katalogu Wynikowego !");
                        }
                        else
                        {
                            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
                            SumaPLIK = 0;
                            foreach (string iPlik in Pliki)
                            {
                                ++SumaPLIK;
                            }
                            if (SumaPLIK == 0)
                            {
                                MessageBox.Show("Brak Tifów w Katalogu z Danymi !");
                            }
                            else
                            {
                                string[] Pliki2 = Directory.GetFiles(sciezkaO, "*.tfw");
                                int SumaPLIK2 = 0;
                                foreach (string iPlik in Pliki2)
                                {
                                    ++SumaPLIK2;
                                }
                                if (SumaPLIK2 == 0)
                                {
                                    MessageBox.Show("Brak Plików Tfw w Katalogu z Danymi !");
                                }
                                else
                                {
                                    if (SumaPLIK == SumaPLIK2)
                                    {
                                        PlikExeGM = textBox65.Text;
                                        PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                                        PlikExeGDWarp = @"C:\Program Files\INDEOv2\bin\gdalwarp.exe";
                                        PlikExeINPHOPYR = @"C:\Program Files\INDEOv2\bin\make_pyr.exe";
                                        if (File.Exists(PlikExeGM) == false || File.Exists(PlikExeEXIF) == false || File.Exists(PlikExeGDWarp) == false || File.Exists(PlikExeINPHOPYR) == false)
                                        {
                                            MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin lub wskaż ścieżkę do Global Mappera !" + Environment.NewLine + @"Pomarańczowa Gwiazdka w Lewym Górnym Rogu !");
                                        }
                                        else
                                        {
                                            PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                                            PlikExeGDWarp = @"""C:\Program Files\INDEOv2\bin\gdalwarp.exe""";
                                            PlikExeINPHOPYR = @"""C:\Program Files\INDEOv2\bin\make_pyr.exe""";
                                            PlikExeGM = @"""" + PlikExeGM + @"""";
                                            SprAOI();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ilość Tifów nie zgadza się z ilością Plików Tfw !");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SprAOI()
        {
            if (checkBox8.Checked == true)
            {
                georef = "TYPE=GEOTIFF GEN_WORLD_FILE=YES";
                geosrs = @" -t_srs """ + plikPrj + @""" ";
            }
            else
            {
                georef = "TYPE=GEOTIFF GEN_WORLD_FILE=YES TIFF_NO_GTIFF_HEADER=YES";
                geosrs = " ";
            }
            if (checkBox6.Checked == true)
            {
                if (radioButton20.Checked == true)
                {
                    kolorMode = " -dstnodata 0 ";
                }
                else
                {
                    kolorMode = " -dstnodata 255 ";
                }
                plikShp = textBox54.Text;
                if (File.Exists(plikShp) == false)
                {
                    MessageBox.Show("Brak Pliku z zakresem do cięcia !");
                }
                else
                {
                    BWPixGM();
                }
            }
            else
            {
                BWPixGM();
            }
        }

        private void BWPixGM()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button94.Enabled = false;
            textBox53.Enabled = false;
            button93.Enabled = false;
            textBox51.Enabled = false;
            button91.Enabled = false;
            textBox52.Enabled = false;
            button92.Enabled = false;
            textBox54.Enabled = false;
            button95.Enabled = false;
            groupBox7.Enabled = false;
            checkBox6.Enabled = false;
            checkBox7.Enabled = false;
            checkBox8.Enabled = false;
            radioButton20.Enabled = false;
            radioButton16.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_BWPixGM_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_BWPixGM_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_BWPixGM_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Wygenerowano " + SumaPLIK.ToString() + " Tify");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button94.Enabled = true;
            textBox53.Enabled = true;
            button93.Enabled = true;
            textBox51.Enabled = true;
            button91.Enabled = true;
            textBox52.Enabled = true;
            button92.Enabled = true;
            checkBox6.Enabled = true;
            checkBox7.Enabled = true;
            checkBox8.Enabled = true;
            if (checkBox6.Checked == true)
            {
                textBox54.Enabled = true;
                button95.Enabled = true;
                groupBox7.Enabled = true;
                radioButton20.Enabled = true;
                radioButton16.Enabled = true;
            }
            else
            {
                textBox54.Enabled = false;
                button95.Enabled = false;
                groupBox7.Enabled = false;
                radioButton20.Enabled = false;
                radioButton16.Enabled = false;
            }
        }

        private void BackgroundWorker_BWPixGM_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                string[] gmsZawartosc =
                {
                    "GLOBAL_MAPPER_SCRIPT VERSION=1.00",
                    "UNLOAD_ALL",
                    @"IMPORT FILENAME=""" + iPlik + @""" TYPE=GEOTIFF PROJ_FILENAME=""" + plikPrj + @""" CONTRAST_MODE=NONE CLIP_COLLAR=NONE",
                    @"GENERATE_EQUAL_VAL_AREAS FILENAME=""" + nPlik + @""" LAYER_DESC=""BIALY"" EQUAL_COLORS=RGB(255,255,255) FORCE_RGB=YES",
                    @"DEFINE_LAYER_STYLE NAME=""AREA_STYLE"" TYPE=""AREA""",
                    "LayerStyle=1",
                    "Type=0",
                    "AreaStyleForAll=4278124097,0,81,270532608,0.0",
                    "END_DEFINE_LAYER_STYLE",
                    @"SET_LAYER_OPTIONS FILENAME=""BIALY"" AREA_STYLE_NAME=""AREA_STYLE"" CLIP_COLLAR=NONE",
                    @"GENERATE_EQUAL_VAL_AREAS FILENAME=""" + nPlik + @""" LAYER_DESC=""CZARNY"" EQUAL_COLORS=RGB(0,0,0) FORCE_RGB=YES",
                    @"DEFINE_LAYER_STYLE NAME=""AREA_STYLE"" TYPE=""AREA""",
                    "LayerStyle=1",
                    "Type=0",
                    "AreaStyleForAll=16843073,0,81,270532608,0.0",
                    "END_DEFINE_LAYER_STYLE",
                    @"SET_LAYER_OPTIONS FILENAME=""CZARNY"" AREA_STYLE_NAME=""AREA_STYLE"" CLIP_COLLAR=NONE",
                    @"EXPORT_RASTER FILENAME=""" + sciezkaW + nPlik + @""" " + georef + @" INC_VECTOR_DATA=YES",
                };
                string gmsPlik = sciezkaW + "GM.gms";
                File.WriteAllLines(gmsPlik, gmsZawartosc);
                Process MyProcess1 = new Process();
                MyProcess1.StartInfo.FileName = PlikExeGM;
                MyProcess1.StartInfo.Arguments = gmsPlik;
                MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess1.Start();
                MyProcess1.WaitForExit();
                File.Delete(gmsPlik);
                if (checkBox6.Checked == true)
                {
                    tile = sciezkaW + nPlik;
                    if (File.Exists(tile) == false)
                    {
                        MessageBox.Show("Brak Pliku " + nPlik + " w katalogu po usuwaniu Białych / Czarnych pikseli !");
                    }
                    else
                    {
                        Process MyProcess2 = new Process();
                        MyProcess2.StartInfo.FileName = PlikExeEXIF;
                        MyProcess2.StartInfo.Arguments = @" -a -u -g1 -w txt """ + tile + @"""";
                        MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess2.Start();
                        MyProcess2.WaitForExit();
                        string txtPlik = tile.Remove(tile.Length - 3) + "txt";
                        decimal iSzerokosc = 0;
                        decimal iWysokosc = 0;
                        string[] zawartoscTxt = File.ReadAllLines(txtPlik);
                        foreach (string iLinia in zawartoscTxt)
                        {
                            int nr = 0;
                            nr = iLinia.IndexOf("Image Size                      :");
                            if (nr > -1)
                            {
                                string size = iLinia.Remove(0, 34);
                                nr = size.IndexOf("x");
                                string szerokosc = size.Remove(nr);
                                string wysokosc = size.Remove(0, nr + 1);
                                iSzerokosc = Convert.ToDecimal(szerokosc);
                                iWysokosc = Convert.ToDecimal(wysokosc);
                            }
                        }
                        string tfwPlik = tile.Remove(tile.Length - 3) + "tfw";
                        string[] zawartoscTfw = File.ReadAllLines(tfwPlik);
                        foreach (string iLinia in zawartoscTfw)
                        {
                        }
                        decimal pixel = Convert.ToDecimal(zawartoscTfw[0]);
                        iSzerokosc *= pixel;
                        iWysokosc *= pixel;
                        pixel /= 2;
                        decimal X = Convert.ToDecimal(zawartoscTfw[4]) - pixel;
                        decimal Y = Convert.ToDecimal(zawartoscTfw[5]) + pixel;
                        string TE = Convert.ToString(X) + " " + Convert.ToString(Y - iWysokosc) + " " + Convert.ToString(X + iSzerokosc) + " " + Convert.ToString(Y);
                        File.Delete(txtPlik);
                        Process MyProcess3 = new Process();
                        MyProcess3.StartInfo.FileName = PlikExeGDWarp;
                        MyProcess3.StartInfo.Arguments = kolorMode + @"-multi -overwrite -te " + TE + @" -cutline """ + plikShp + @"""" + geosrs + @"""" + sciezkaW + nPlik + @""" """ + sciezkaW + @"GDAL_" + nPlik + @"""";
                        MyProcess3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess3.Start();
                        MyProcess3.WaitForExit();
                        File.Delete(sciezkaW + nPlik);
                        File.Move(sciezkaW + @"GDAL_" + nPlik, sciezkaW + nPlik);
                    }
                }
                if (checkBox7.Checked == true)
                {
                    tile = sciezkaW + nPlik;
                    if (File.Exists(tile) == false)
                    {
                        MessageBox.Show("Brak Pliku " + nPlik + " w katalogu po usuwaniu Białych / Czarnych pikseli !");
                    }
                    else
                    {
                        Process MyProcess4 = new Process();
                        MyProcess4.StartInfo.FileName = PlikExeINPHOPYR;
                        MyProcess4.StartInfo.Arguments = @"""" + sciezkaW + nPlik + @""" -out """ + sciezkaW + @"PYR_" + nPlik + @""" -TIFF -tile 256";
                        MyProcess4.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        MyProcess4.Start();
                        MyProcess4.WaitForExit();
                        File.Delete(sciezkaW + nPlik);
                        File.Move(sciezkaW + @"PYR_" + nPlik, sciezkaW + nPlik);
                    }                        
                }
            }
        }

        private void TextBox53_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox53.Text = files[0];
                }
            }
        }

        private void TextBox53_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox51_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".prj" || roz == ".PRJ")
                    {
                        textBox51.Text = files[0];
                    }
                    else
                    {
                        textBox51.Text = "";
                    }
                }
            }
        }

        private void TextBox51_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox52_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox52.Text = files[0];
                }
            }
        }

        private void TextBox52_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox54_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".shp" || roz == ".SHP")
                    {
                        textBox54.Text = files[0];
                    }
                    else
                    {
                        textBox54.Text = "";
                    }
                }
            }
        }

        private void TextBox54_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------B/W_Pix--------------------------------------------------------------------------
        
        private void TextBox56_TextChanged(object sender, EventArgs e)
        {
            sciezkaO = textBox56.Text;
        }

        private void Button98_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox56.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox55_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox55.Text;
        }

        private void Button97_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox55.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button96_Click(object sender, EventArgs e)
        {
            sciezkaO = textBox56.Text;
            if (Directory.Exists(sciezkaO) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaO += @"\";
                sciezkaO = Path.GetFullPath(sciezkaO);
                sciezkaW = textBox55.Text;
                if (Directory.Exists(sciezkaW) == false)
                {
                    MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                }
                else
                {
                    sciezkaW += @"\";
                    sciezkaW = Path.GetFullPath(sciezkaW);
                    if (sciezkaO == sciezkaW)
                    {
                        MessageBox.Show("Te Same Katalogi !");
                    }
                    else
                    {
                        string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
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
                            katalogQGIS = textBox62.Text;
                            PlikExePython = katalogQGIS + @"\bin\python-qgis.bat";
                            if (File.Exists(PlikExePython) == false)
                            {
                                MessageBox.Show(@"Wskaż ścieżkę do Katalogu QGIS !" + Environment.NewLine + @"Pomarańczowa Gwiazdka w Lewym Górnym Rogu !");
                            }
                            else
                            {
                                PlikExePython = @"""" + PlikExePython + @""""; ;
                                BWPix();
                            }
                        }
                    }
                }
            }
        }

        private void BWPix()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button98.Enabled = false;
            textBox56.Enabled = false;
            button97.Enabled = false;
            textBox55.Enabled = false;
            button96.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_BWPix_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_BWPix_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_BWPix_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono " + SumaPLIK.ToString() + " Tify");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button98.Enabled = true;
            textBox56.Enabled = true;
            button97.Enabled = true;
            textBox55.Enabled = true;
            button96.Enabled = true;
        }

        private void BackgroundWorker_BWPix_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                Process MyProcess1 = new Process();
                MyProcess1.StartInfo.FileName = PlikExePython;
                MyProcess1.StartInfo.Arguments = @"""" + katalogQGIS + @"\apps\Python39\Scripts\gdal_calc.py"" -A """ + iPlik + @""" --allBands=A --outfile=""" + sciezkaW + nPlik + @""" --overwrite --calc=""(A!=255)*(A!=0)*A + (A==0)*1 + (A==255)*254""";
                MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess1.Start();
                MyProcess1.WaitForExit();
            }
        }

        private void TextBox56_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox56.Text = files[0];
                }
            }
        }

        private void TextBox56_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox55_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox55.Text = files[0];
                }
            }
        }

        private void TextBox55_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------TNIJ_POLIGONEM-------------------------------------------------------------------

        private void TextBox59_TextChanged(object sender, EventArgs e)
        {
            sciezkaO = textBox59.Text;
        }

        private void Button102_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox59.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox57_TextChanged(object sender, EventArgs e)
        {
            plikShp = textBox57.Text;
        }

        private void Button100_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox57.Text = openFileDialog1.FileName;
            }
        }

        private void TextBox58_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox58.Text;
        }

        private void Button101_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox58.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button99_Click(object sender, EventArgs e)
        {
            sciezkaO = textBox59.Text;
            if (Directory.Exists(sciezkaO) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi - Pliki Tif !");
            }
            else
            {
                sciezkaO += @"\";
                sciezkaO = Path.GetFullPath(sciezkaO);
                plikShp = textBox57.Text;
                if (File.Exists(plikShp) == false)
                {
                    MessageBox.Show("Brak Pliku .shp z zakresem do cięcia !");
                }
                else
                {
                    sciezkaW = textBox58.Text;
                    if (Directory.Exists(sciezkaW) == false)
                    {
                        MessageBox.Show("Błędna Ścieżka do Katalogu wynikowego !");
                    }
                    else
                    {
                        sciezkaW += @"\";
                        sciezkaW = Path.GetFullPath(sciezkaW);
                        if (sciezkaO == sciezkaW)
                        {
                            MessageBox.Show("Ścieżka do Katalogu z Danymi Ta Sama co do Katalogu Wynikowego !");
                        }
                        else
                        {
                            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
                            SumaPLIK = 0;
                            foreach (string iPlik in Pliki)
                            {
                                ++SumaPLIK;
                            }
                            if (SumaPLIK == 0)
                            {
                                MessageBox.Show("Brak Tifów w Katalogu z Danymi !");
                            }
                            else
                            {
                                string[] Pliki2 = Directory.GetFiles(sciezkaO, "*.tfw");
                                int SumaPLIK2 = 0;
                                foreach (string iPlik in Pliki2)
                                {
                                    ++SumaPLIK2;
                                }
                                if (SumaPLIK2 == 0)
                                {
                                    MessageBox.Show("Brak Plików Tfw w Katalogu z Danymi !");
                                }
                                else
                                {
                                    if (SumaPLIK == SumaPLIK2)
                                    {
                                        PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                                        PlikExeGDWarp = @"C:\Program Files\INDEOv2\bin\gdalwarp.exe";
                                        if (File.Exists(PlikExeEXIF) == false || File.Exists(PlikExeGDWarp) == false)
                                        {
                                            MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin !");
                                        }
                                        else
                                        {
                                            PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                                            PlikExeGDWarp = @"""C:\Program Files\INDEOv2\bin\gdalwarp.exe""";
                                            if (radioButton22.Checked == true)
                                            {
                                                kolorMode = " -dstnodata 0 ";
                                            }
                                            else
                                            {
                                                kolorMode = " -dstnodata 255 ";
                                            }
                                            Tnij_shp();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ilość Tifów nie zgadza się z ilością Plików Tfw !");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Tnij_shp()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button102.Enabled = false;
            textBox59.Enabled = false;
            button100.Enabled = false;
            textBox57.Enabled = false;
            button101.Enabled = false;
            textBox58.Enabled = false;
            radioButton21.Enabled = false;
            radioButton22.Enabled = false;
            button99.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_Tnij_shp_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_Tnij_shp_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_Tnij_shp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono " + SumaPLIK.ToString() + " Tify");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button102.Enabled = true;
            textBox59.Enabled = true;
            button100.Enabled = true;
            textBox57.Enabled = true;
            button101.Enabled = true;
            textBox58.Enabled = true;
            radioButton21.Enabled = true;
            radioButton22.Enabled = true;
            button99.Enabled = true;
        }

        private void BackgroundWorker_Tnij_shp_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                Process MyProcess1 = new Process();
                MyProcess1.StartInfo.FileName = PlikExeEXIF;
                MyProcess1.StartInfo.Arguments = @" -a -u -g1 -w txt """ + iPlik + @"""";
                MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess1.Start();
                MyProcess1.WaitForExit();
                string txtPlik = iPlik.Remove(iPlik.Length - 3) + "txt";
                decimal iSzerokosc = 0;
                decimal iWysokosc = 0;
                string[] zawartoscTxt = File.ReadAllLines(txtPlik);
                foreach (string iLinia in zawartoscTxt)
                {
                    int nr = 0;
                    nr = iLinia.IndexOf("Image Size                      :");
                    if (nr > -1)
                    {
                        string size = iLinia.Remove(0, 34);
                        nr = size.IndexOf("x");
                        string szerokosc = size.Remove(nr);
                        string wysokosc = size.Remove(0, nr + 1);
                        iSzerokosc = Convert.ToDecimal(szerokosc);
                        iWysokosc = Convert.ToDecimal(wysokosc);
                    }
                }
                string tfwPlik = iPlik.Remove(iPlik.Length - 3) + "tfw";
                string[] zawartoscTfw = File.ReadAllLines(tfwPlik);
                foreach (string iLinia in zawartoscTfw)
                {
                }
                decimal pixel = Convert.ToDecimal(zawartoscTfw[0]);
                iSzerokosc *= pixel;
                iWysokosc *= pixel;
                pixel /= 2;
                decimal X = Convert.ToDecimal(zawartoscTfw[4]) - pixel;
                decimal Y = Convert.ToDecimal(zawartoscTfw[5]) + pixel;
                string TE = Convert.ToString(X) + " " + Convert.ToString(Y - iWysokosc) + " " + Convert.ToString(X + iSzerokosc) + " " + Convert.ToString(Y);
                File.Delete(txtPlik);
                Process MyProcess2 = new Process();
                MyProcess2.StartInfo.FileName = PlikExeGDWarp;
                MyProcess2.StartInfo.Arguments = kolorMode + @"-multi -overwrite -te " + TE + @" -cutline """ + plikShp + @""" """ + iPlik + @""" """ + sciezkaW + nPlik + @"""";
                MyProcess2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess2.Start();
                MyProcess2.WaitForExit();
            }
        }

        private void TextBox59_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox59.Text = files[0];
                }
            }
        }

        private void TextBox59_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox57_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".shp" || roz == ".SHP")
                    {
                        textBox57.Text = files[0];
                    }
                    else
                    {
                        textBox57.Text = "";
                    }
                }
            }
        }

        private void TextBox57_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox58_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox58.Text = files[0];
                }
            }
        }

        private void TextBox58_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------OVERVIEW-INPHO-------------------------------------------------------------------

        private void TextBox61_TextChanged(object sender, EventArgs e)
        {
            sciezkaT = textBox61.Text;
        }

        private void Button105_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox61.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox60_TextChanged(object sender, EventArgs e)
        {
            sciezkaW = textBox60.Text;
        }

        private void Button103_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox60.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked == true)
            {
                textBox60.Enabled = true;
                button103.Enabled = true;
            }
            else
            {
                textBox60.Enabled = false;
                button103.Enabled = false;
                textBox60.Text = "";
            }
        }

        private void RadioButton27_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton27.Checked == true)
            {
                numericUpDown15.Enabled = true;
            }
            else
            {
                numericUpDown15.Enabled = false;
            }
        }

        private void RadioButton30_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton30.Checked == true)
            {
                numericUpDown14.Enabled = true;
            }
            else
            {
                numericUpDown14.Enabled = false;
            }
        }

        private void NumericUpDown14_ValueChanged(object sender, EventArgs e)
        {
            q = Convert.ToString(numericUpDown14.Value);
        }

        private void Button104_Click(object sender, EventArgs e)
        {
            sciezkaT = textBox61.Text;
            if (Directory.Exists(sciezkaT) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi !");
            }
            else
            {
                sciezkaT += @"\";
                sciezkaT = Path.GetFullPath(sciezkaT);
                string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
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
                    if (checkBox9.Checked == true)
                    {
                        sciezkaW = textBox60.Text;
                        if (Directory.Exists(sciezkaW) == false)
                        {
                            MessageBox.Show("Błędna Ścieżka do Katalogu Wynikowego !");
                        }
                        else
                        {
                            sciezkaW += @"\";
                            sciezkaW = Path.GetFullPath(sciezkaW);
                            if (sciezkaT == sciezkaW)
                            {
                                MessageBox.Show("Ścieżka do Katalogu Wynikowego ta sama co do Katalogu z Danymi !");
                            }
                            else
                            {
                                OvInphoExeki();
                            }
                        }
                    }
                    else
                    {
                        sciezkaW = sciezkaT;
                        OvInphoExeki();
                    }

                }
            }
        }

        private void OvInphoExeki()
        {
            PlikExeINPHOPYR = @"C:\Program Files\INDEOv2\bin\make_pyr.exe";
            if (File.Exists(PlikExeINPHOPYR) == false)
            {
                MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin !");
            }
            else
            {
                PlikExeINPHOPYR = @"""C:\Program Files\INDEOv2\bin\make_pyr.exe""";
                OvInphoPrzelaczniki();
            }
        }

        private void OvInphoPrzelaczniki()
        {
            if (radioButton38.Checked == true)
            {
                tifver = " -TIFF";
            }
            else if (radioButton37.Checked == true)
            {
                tifver = " -BIGTIFF";
            }
            if (radioButton31.Checked == true)
            {
                kompresja = "";
            }
            else if (radioButton30.Checked == true)
            {
                string st_q = Convert.ToString(numericUpDown14.Value);
                kompresja = " -JPEG -QFACTOR " + st_q;
            }
            if (radioButton33.Checked == true)
            {
                tile = " -tile 128";
            }
            else if (radioButton34.Checked == true)
            {
                tile = " -tile 256";
            }
            else if (radioButton36.Checked == true)
            {
                tile = " -tile 512";
            }
            else if (radioButton35.Checked == true)
            {
                tile = " -tile 1024";
            }
            if (radioButton28.Checked == true)
            {
                OverView = "";
                Ov_Inpho();
            }
            else if (radioButton27.Checked == true)
            {
                string ile_ov = Convert.ToString(numericUpDown15.Value);
                OverView = " -no_ov " + ile_ov;
                Ov_Inpho();
            }
        }

        private void Ov_Inpho()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button103.Enabled = false;
            button104.Enabled = false;
            button105.Enabled = false;
            textBox60.Enabled = false;
            textBox61.Enabled = false;
            checkBox9.Enabled = false;
            groupBox13.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_Ov_Inpho_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_Ov_Inpho_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_Ov_Inpho_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono " + SumaPLIK.ToString() + " plików.");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button104.Enabled = true;
            button105.Enabled = true;
            textBox61.Enabled = true;
            checkBox9.Enabled = true;
            if (checkBox9.Checked == true)
            {
                button103.Enabled = true;
                textBox60.Enabled = true;
            }
            groupBox13.Enabled = true;
        }

        private void BackgroundWorker_Ov_Inpho_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string[] Pliki = Directory.GetFiles(sciezkaT, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                if (checkBox9.Checked == true)
                {
                    string tPlik = nPlik.Remove(nPlik.Length - 4) + ".tfw";
                    if (File.Exists(sciezkaT + tPlik))
                    {
                        if (File.Exists(sciezkaW + tPlik))
                        {
                            File.Delete(sciezkaW + tPlik);
                        }
                        File.Copy(sciezkaT + tPlik, sciezkaW + tPlik);
                    }
                    if (File.Exists(sciezkaW + nPlik))
                    {
                        File.Delete(sciezkaW + nPlik);
                    }
                    Process MyProcess1 = new Process();
                    MyProcess1.StartInfo.FileName = PlikExeINPHOPYR;
                    MyProcess1.StartInfo.Arguments = @"""" + iPlik + @""" -out """ + sciezkaW + nPlik + @"""" + OverView + tifver  + kompresja + tile;
                    MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    MyProcess1.Start();
                    MyProcess1.WaitForExit();
                }
                else
                {
                    Process MyProcess1 = new Process();
                    MyProcess1.StartInfo.FileName = PlikExeINPHOPYR;
                    MyProcess1.StartInfo.Arguments = @"""" + iPlik + @""" -out """ + iPlik + @".pyr""" + OverView + tifver + kompresja + tile;
                    MyProcess1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    MyProcess1.Start();
                    MyProcess1.WaitForExit();
                    File.Delete(iPlik);
                    File.Move(iPlik + ".pyr", iPlik);
                }
            }
        }

        private void TextBox61_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox61.Text = files[0];
                }
            }
        }

        private void TextBox61_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox60_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox60.Text = files[0];
                }
            }
        }

        private void TextBox60_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }


        //---------------PLUM-----------------------------------------------------------------------------

        //---------------PLUM_1---------------------------------------------------------------------------

        private void TextBox30_TextChanged(object sender, EventArgs e)
        {
            plikINPHOPrj = textBox30.Text;
        }

        private void Button36_Click(object sender, EventArgs e)
        {
            textBox30.Text = openFileDialog3.FileName;
        }

        private void Button37_Click(object sender, EventArgs e)
        {
            plikINPHOPrj = textBox30.Text;
            if (File.Exists(plikINPHOPrj) == false)
            {
                MessageBox.Show("Brak Pliku !");
            }
            else
            {
                string plikINPHOPrjW = plikINPHOPrj.Remove(plikINPHOPrj.Length - 4) + "_NEW.prj";
                StreamReader sr = new StreamReader(plikINPHOPrj);
                StreamWriter sw = new StreamWriter(plikINPHOPrjW);
                string iLinia;
                int ni = 0;
                while ((iLinia = sr.ReadLine()) != null)
                {
                    ni = iLinia.IndexOf("$PHOTO_NUM :");
                    if (ni > -1)
                    {
                        iLinia = iLinia.Remove(18, 4);
                        sw.WriteLine(iLinia);
                    }
                    else
                    {
                        sw.WriteLine(iLinia);
                    }
                }
                sw.Close();
                sr.Close();
            }
        }

        private void TextBox30_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    string roz = files[0];
                    roz = roz.Remove(0, roz.Length - 4);
                    if (roz == ".prj" || roz == ".PRJ")
                    {
                        textBox30.Text = files[0];
                    }
                    else
                    {
                        textBox30.Text = "";
                    }
                }
            }
        }

        private void TextBox30_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------PLUM_2---------------------------------------------------------------------------

        private void TextBox34_TextChanged(object sender, EventArgs e)
        {
            sciezkaO = textBox34.Text;
        }

        private void Button43_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox34.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TextBox40_TextChanged(object sender, EventArgs e)
        {
            Info = textBox40.Text;
        }

        private void Button45_Click(object sender, EventArgs e)
        {
            sciezkaO = textBox34.Text;
            if (Directory.Exists(sciezkaO) == false)
            {
                MessageBox.Show("Błędna Ścieżka do Katalogu z Danymi - Pliki Tiff !");
            }
            else
            {
                sciezkaO = sciezkaO + @"\";
                sciezkaO = Path.GetFullPath(sciezkaO);
                string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
                SumaPLIK = 0;
                foreach (string iPlik in Pliki)
                {
                    ++SumaPLIK;
                }
                if (SumaPLIK == 0)
                {
                    MessageBox.Show("Brak Tifów w Katalogu z Danymi !");
                }
                else
                {
                    PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
                    if (File.Exists(PlikExeEXIF) == false)
                    {
                        MessageBox.Show(@"Brak plików w katalogu INDEOv2\bin !");
                    }
                    else
                    {
                        PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                        TifInfo();
                    }
                }
            }
        }

        private void TifInfo()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Maximum = SumaPLIK;
            progressBar1.Value = 0;
            button45.Enabled = false;
            BackgroundWorker1 = new BackgroundWorker();
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_TifInfo_DoWork);
            BackgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_TifInfo_RunWorkerCompleted);
            BackgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker_TifInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Przetworzono: " + SumaPLIK.ToString() + " Tifów");
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            label50.Text = "";
            button45.Enabled = true;
        }

        private void BackgroundWorker_TifInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            int progressBarVal = 0;
            string plikInfo = sciezkaO + "INFO.txt";
            string[] Pliki = Directory.GetFiles(sciezkaO, "*.tif");
            foreach (string iPlik in Pliki)
            {
                ++progressBarVal;
                BackgroundWorker1.ReportProgress(progressBarVal);
                nPlik = Path.GetFileName(iPlik);
                Process MyProcess = new Process();
                MyProcess.StartInfo.FileName = PlikExeEXIF;
                MyProcess.StartInfo.Arguments = @" -a -u -g1 -w txt """ + iPlik + @"""";
                MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                MyProcess.Start();
                MyProcess.WaitForExit();
                int ni = 0;
                string txtPlik = iPlik.Remove(iPlik.Length - 3) + "txt";
                string[] zawartosc = File.ReadAllLines(txtPlik);
                foreach (string iLinia in zawartosc)
                {
                    if (ni == 0)
                    {
                        int nr = 0;
                        nr = iLinia.IndexOf(Info);
                        if (nr > -1)
                        {
                            string[] InfoZawartosc =
                            {
                                nPlik.Remove(nPlik.Length - 4) + "   :   " + iLinia.Remove(0, 34),
                            };
                            File.AppendAllLines(plikInfo, InfoZawartosc);
                            ni = 1;
                        }
                    }
                }                
                File.Delete(txtPlik);
            }
        }

        private void TextBox34_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Directory.Exists(files[0]))
                {
                    textBox34.Text = files[0];
                }
            }
        }

        private void TextBox34_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //---------------GEOINFO--------------------------------------------------------------------------

        private void Button54_Click(object sender, EventArgs e)
        {
            Visible = false;
            new Form2().ShowDialog();
            Visible = true;
        }

        //---------------KONIEC--------------------------------------------------------------------------

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(gmwPlik) == true)
            {
                File.Delete(gmwPlik);
            }
 //           int W = 531;
 //           int H = 831;
 //           while (Opacity > 0.0)
 //           {
 //               Opacity -= 0.01;
 //               H = H - 7;
 //               Size = new Size(W, H);
 //               Thread.Sleep(5);
 //           }
        }

    }

}