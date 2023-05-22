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

namespace WindowsFormsApplication3
{
    public partial class Form3 : Form
    {
        Form1 form1;
        string PlikExeEXIF;
        string PlikExeGDWarp;
        string PlikExeOrthoMaster;
        string PlikExeDtmTool;
        string plikINPHOPrj;
        string plikTif;
        string plikTfw;
        string plikS;
        string plikDTM;
        string plikBreak;
        string plikObiekt;
        string plikPunkty;
        string plikDTMopts;
        string sciezkaS;
        string sciezkaW;
        string tile;
        string Wysokosc;
        string WielkoscPix;
        string kolor;
        string nPlik;
        string Deh;
        string TrueOrto = "0";
        string TrueOrtoA = "1";
        string Time;
        int SumaPLIK;
        private BackgroundWorker BackgroundWorker1 = null;

        public Form3(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
        }

        //---------------OGOLNE---------------------------------------------------------------------------

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            form1.progressBar1.Value = e.ProgressPercentage;
            form1.label50.Text = form1.progressBar1.Value + "  /  " + SumaPLIK + "  -  " + nPlik;
            form1.label106.Text = form1.progressBar1.Value * 100 / SumaPLIK + " %";
        }

        //---------------ORTO-----------------------------------------------------------------------------

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Wysokosc = textBox1.Text;
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && (((sender as TextBox).Text.IndexOf('.') > -1) || ((sender as TextBox).Text.Length == 0)))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '-') && (((sender as TextBox).Text.IndexOf('-') > -1) || ((sender as TextBox).Text.Length == 1)))
            {
                e.Handled = true;
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = false;
            textBox2.Text = "";
            button2.Enabled = false;
            textBox3.Enabled = false;
            textBox3.Text = "";
            button3.Enabled = false;
            textBox4.Enabled = false;
            textBox4.Text = "";
            button4.Enabled = false;
            textBox5.Enabled = false;
            textBox5.Text = "";
            button5.Enabled = false;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Text = "";
            textBox2.Enabled = false;
            textBox2.Text = "";
            button2.Enabled = false;
            textBox3.Enabled = false;
            textBox3.Text = "";
            button3.Enabled = false;
            textBox4.Enabled = false;
            textBox4.Text = "";
            button4.Enabled = false;
            textBox5.Enabled = true;
            button5.Enabled = true;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Text = "";
            textBox2.Enabled = true;
            button2.Enabled = true;
            textBox3.Enabled = true;
            button3.Enabled = true;
            textBox4.Enabled = true;
            button4.Enabled = true;
            textBox5.Enabled = false;
            textBox5.Text = "";
            button5.Enabled = false;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            plikBreak = textBox2.Text;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            plikObiekt = textBox3.Text;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
            }
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            plikPunkty = textBox4.Text;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = openFileDialog2.FileName;
            }
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            plikDTM = textBox5.Text;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = openFileDialog3.FileName;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            SumaPLIK = 1;
            plikINPHOPrj = form1.textBox11.Text;
            sciezkaS = form1.textBox94.Text;
            sciezkaS = sciezkaS + @"\";
            sciezkaS = Path.GetFullPath(sciezkaS);
            sciezkaW = form1.textBox13.Text;
            sciezkaW = sciezkaW + @"\";
            sciezkaW = Path.GetFullPath(sciezkaW);
            tile = form1.textBox84.Text;
            plikS = sciezkaS + tile + ".tif";
            plikTfw = plikS.Remove(plikS.Length - 4) + ".tfw";
            plikTif = form1.textBox85.Text;
            if (form1.radioButton25.Checked == true)
            {
                kolor = "0";
            }
            else
            {
                kolor = "255";
            }
            PlikExeEXIF = @"C:\Program Files\INDEOv2\bin\exiftool.exe";
            PlikExeOrthoMaster = @"C:\Program Files\Trimble\Trimble Photogrammetry 8.0\bin\orthomaster.exe";
            PlikExeDtmTool = @"C:\Program Files\Trimble\Trimble Photogrammetry 8.0\bin\dtmtoolkit.exe";
            PlikExeGDWarp = @"C:\Program Files\INDEOv2\bin\gdalwarp.exe";
            if (File.Exists(PlikExeEXIF) == false || File.Exists(PlikExeOrthoMaster) == false || File.Exists(PlikExeDtmTool) == false || File.Exists(PlikExeGDWarp) == false)
            {
                PlikExeEXIF = @"C:\Program Files (x86)\INDEOv2\bin\exiftool.exe";
                PlikExeOrthoMaster = @"C:\Program Files\Trimble\Trimble Photogrammetry 8.0\bin\orthomaster.exe";
                PlikExeDtmTool = @"C:\Program Files\Trimble\Trimble Photogrammetry 8.0\bin\dtmtoolkit.exe";
                PlikExeGDWarp = @"C:\Program Files (x86)\INDEOv2\bin\gdalwarp.exe";
                if (File.Exists(PlikExeEXIF) == false || File.Exists(PlikExeOrthoMaster) == false || File.Exists(PlikExeDtmTool) == false || File.Exists(PlikExeGDWarp) == false)
                {
                    MessageBox.Show("Brak plików w katalogu INDEOv2\bin lub zainstalowanego Trimble Photogrammetry 8.0 !");
                }
                else
                {
                    PlikExeEXIF = @"""C:\Program Files (x86)\INDEOv2\bin\exiftool.exe""";
                    PlikExeOrthoMaster = @"""C:\Program Files\Trimble\Trimble Photogrammetry 8.0\bin\orthomaster.exe""";
                    PlikExeDtmTool = @"""C:\Program Files\Trimble\Trimble Photogrammetry 8.0\bin\dtmtoolkit.exe""";
                    System.Environment.SetEnvironmentVariable("GDAL_DATA", @"C:\Program Files (x86)\INDEOv2\bin");
                    PlikExeGDWarp = @"""C:\Program Files (x86)\INDEOv2\bin\gdalwarp.exe""";
                    GenDTM();
                }
            }
            else
            {
                PlikExeEXIF = @"""C:\Program Files\INDEOv2\bin\exiftool.exe""";
                PlikExeOrthoMaster = @"""C:\Program Files\Trimble\Trimble Photogrammetry 8.0\bin\orthomaster.exe""";
                PlikExeDtmTool = @"""C:\Program Files\Trimble\Trimble Photogrammetry 8.0\bin\dtmtoolkit.exe""";
                System.Environment.SetEnvironmentVariable("GDAL_DATA", @"C:\Program Files\INDEOv2\bin");
                PlikExeGDWarp = @"""C:\Program Files\INDEOv2\bin\gdalwarp.exe""";
                GenDTM();
            }            
        }

        private void GenDTM()
        {
            if (radioButton1.Checked == true)
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Wpisz wysokość terenu !");
                }
                else
                {
                    Wysokosc = textBox1.Text;
                    bool wynik = decimal.TryParse(Wysokosc, out decimal D);
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
            else if (radioButton2.Checked == true)
            {
                if (textBox5.Text == "")
                {
                    MessageBox.Show("Wskaż Plik DTM !");
                }
                else
                {
                    plikDTM = textBox5.Text;
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
                if (textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "")
                {
                    MessageBox.Show("Podaj pliki z terenem !");
                }
                else if (textBox2.Text == "" && textBox4.Text == "")
                {
                    MessageBox.Show("Podaj pliki z terenem na który będą rzutowane obiekty !");
                }
                else if (textBox2.Text != "" && textBox3.Text == "" && textBox4.Text == "")
                {
                    plikBreak = textBox2.Text;
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
                else if (textBox2.Text == "" && textBox3.Text == "" && textBox4.Text != "")
                {
                    plikPunkty = textBox4.Text;
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
                else if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text == "")
                {
                    plikBreak = textBox2.Text;
                    plikObiekt = textBox3.Text;
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
                else if (textBox2.Text == "" && textBox3.Text != "" && textBox4.Text != "")
                {
                    plikObiekt = textBox3.Text;
                    plikPunkty = textBox4.Text;
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
                else if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
                {
                    plikBreak = textBox2.Text;
                    plikObiekt = textBox3.Text;
                    plikPunkty = textBox4.Text;
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
            form1.progressBar1.Enabled = true;
            form1.progressBar1.Style = ProgressBarStyle.Continuous;
            form1.progressBar1.Maximum = SumaPLIK;
            form1.progressBar1.Value = 0;
            form1.button11.Enabled = false;
            form1.button19.Enabled = false;
            form1.button73.Enabled = false;
            form1.button87.Enabled = false;
            button1.Enabled = false;
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
            form1.progressBar1.Value = 0;
            form1.progressBar1.Style = ProgressBarStyle.Continuous;
            form1.label50.Text = "";
            form1.button11.Enabled = true;
            form1.button19.Enabled = true;
            form1.button73.Enabled = true;
            form1.button87.Enabled = true;
            button1.Enabled = true;
        }

        private void BackgroundWorker_GenOrto_DoWork(object sender, DoWorkEventArgs e)
        {
            Time = Convert.ToString(DateTime.Now);
            int no = 0;
            no = Time.IndexOf(" ");
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
            foreach (string iLinia in zawartoscTfw)
            {
            }
            decimal pixel = Convert.ToDecimal(zawartoscTfw[0]);
            WielkoscPix = Convert.ToString(pixel);
            iSzerokosc = iSzerokosc * pixel;
            iWysokosc = iWysokosc * pixel;
            pixel = pixel / 2;
            decimal X = Convert.ToDecimal(zawartoscTfw[4]) - pixel;
            decimal Y = Convert.ToDecimal(zawartoscTfw[5]) + pixel;
            string Xmin = Convert.ToString(X);
            string Ymin = Convert.ToString(Y - iWysokosc);
            string Xmax = Convert.ToString(X + iSzerokosc);
            string Ymax = Convert.ToString(Y);
            File.Delete(txtPlik);
            if (radioButton3.Checked == true)
            {
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
                }
                sw.Close();
                sr.Close();
            }
            else
            {
                StreamReader sr = new StreamReader(plikINPHOPrj);
                StreamWriter sw = new StreamWriter(plikPrjW);
                string iLinia;
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
                "  <OrthoAreas/>",
                "  <AreasOfInterest>",
                @"   <geomIndex value=""1""/>",
                @"   <Element name=""BlockArea Of Interest 1"" type=""46"">",
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
                "  </AreasOfInterest>",
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
                @"     <m_useAOI value=""1""/>",
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
                @"     <m_overlap value=""0.1""/>",
                @"     <m_overlapAbsolute value=""500""/>",
                @"     <m_selMethod value=""1""/>",
                @"     <m_resoMethod value=""2""/>",
                @"     <m_useWatermark value=""0""/>",
                @"     <m_watermarkFile value=""""/>",
                @"     <m_outputDatatype value=""2""/>",
                @"     <m_overviewType value=""0""/>",
                @"     <m_overviewWidth value=""1024""/>",
                @"     <m_isTiledOutput value=""1""/>",
                @"     <m_tileWidth value=""256""/>",
                @"     <m_tileHeight value=""256""/>",
                @"     <m_areaReplType value=""3""/>",
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
                @"     <m_MaxNumParallelProcesses value=""8""/>",
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
                int nr = 0;
                nr = iLinia.IndexOf(" orthoID=");
                if (nr > -1)
                {
                    plikTif = iLinia.Remove(0, nr + 10);
                    nr = plikTif.IndexOf(@"""");
                    plikTif = plikTif.Remove(nr);
                }
            }
            Process MyProcess4 = new Process();
            MyProcess4.StartInfo.FileName = "Photoshop.exe";
            MyProcess4.StartInfo.Arguments = sciezkaW + plikTif + ".tif";
            MyProcess4.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MyProcess4.Start();
        }

        private void TextBox2_DragDrop(object sender, DragEventArgs e)
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
                        textBox2.Text = files[0];
                    }
                    else
                    {
                        textBox2.Text = "";
                    }
                }
            }
        }

        private void TextBox2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox3_DragDrop(object sender, DragEventArgs e)
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
                        textBox3.Text = files[0];
                    }
                    else
                    {
                        textBox3.Text = "";
                    }
                }
            }
        }

        private void TextBox3_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox4_DragDrop(object sender, DragEventArgs e)
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
                        textBox4.Text = files[0];
                    }
                    else
                    {
                        textBox4.Text = "";
                    }
                }
            }
        }

        private void TextBox4_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TextBox5_DragDrop(object sender, DragEventArgs e)
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
                        textBox5.Text = files[0];
                    }
                    else
                    {
                        textBox5.Text = "";
                    }
                }
            }
        }

        private void TextBox5_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

    }
}