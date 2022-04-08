using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.ComponentModel;
using System.Collections;
using Microsoft.Win32;

namespace ProfilMetinCek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static SqlConnection baglanti = new SqlConnection("Data Source = 185.141.33.104\\SQLEXPRESS;" + "Initial Catalog = facebook;" + "User id = sa;" + "Password=1236asd;");
        System.Collections.ArrayList arr1 = new System.Collections.ArrayList(); // listbox1
        System.Collections.ArrayList arr2 = new System.Collections.ArrayList(); // listbox2
        System.Collections.ArrayList arr3 = new System.Collections.ArrayList(); // listbox2
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string kayit = "insert into Gonderiler(gonderiIcerik,gonderiTip) values (@gonderiIcerik, @gonderiTip)";
                SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
                komutpaylasim.Parameters.AddWithValue("@gonderiIcerik", listBox1.Items[i].ToString());
                komutpaylasim.Parameters.AddWithValue("@gonderiTip", "0");
                komutpaylasim.ExecuteNonQuery();

            }
            baglanti.Close();
            // AsagiKaydirTmr.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("");
        }
        int islemSaniyesi = 0;
        private void AsagiKaydirTmr_Tick(object sender, EventArgs e)
        {
            islemSaniyesi++;
            label1.Text = islemSaniyesi.ToString();
            if (islemSaniyesi < 100)
            {
                webBrowser1.Document.Window.ScrollTo(0, webBrowser1.Document.Body.ScrollRectangle.Height);
            }
            if (islemSaniyesi == 105)
            {
                string html = webBrowser1.Document.Body.InnerHtml.ToString();
                string[] parcalar;
                parcalar = html.Split('<');
                arr1.Clear();
                foreach (string i in parcalar)
                {
                    arr1.Add(i);
                }
                arr2.Clear();
                for (int i = 0; i < arr1.Count; i++)
                {
                    string Metin = arr1[i].ToString();
                    int sonuc = Metin.IndexOf("_5pbx userContent _22jv _3576");
                    if (sonuc > 0)
                    {
                        string sayfa = (arr1[i + 2].ToString());
                        listBox1.Items.Add(sayfa.Remove(0, 2));
                    }
                }
            }
            if (islemSaniyesi == 110)
            {
                string yol = "F:\\Durumlar.txt";
                StreamWriter s = new StreamWriter(yol);
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    s.WriteLine(listBox1.Items[i].ToString());
                }
                s.Close();
                islemSaniyesi = 0;
                AsagiKaydirTmr.Stop();
                //baglanti.Open();
                //for (int i = 0; i < listBox1.Items.Count; i++)
                //{
                //    string kayit = "insert into Gezinti(GezintiList) values (@GezintiList)";
                //    SqlCommand komutpaylasim = new SqlCommand(kayit, baglanti);
                //    komutpaylasim.Parameters.AddWithValue("@GezintiList", listBox1.Items[i].ToString());
                //    komutpaylasim.ExecuteNonQuery();

                //}
                //baglanti.Close();
            }
        }
        void SayfaAdresiCek()
        {
            string html = webBrowser1.Document.Body.InnerHtml.ToString();
            string[] parcalar;
            parcalar = html.Split('<');
            arr1.Clear();
            foreach (string i in parcalar)
            {
                arr1.Add(i);
            }
            arr2.Clear();
            for (int i = 0; i < arr1.Count; i++)
            {
                string Metin = arr1[i].ToString();
                int sonuc = Metin.IndexOf("_4-u3 _5l2c");
                if (sonuc > 0)
                {
                    string sayfa = (arr1[i + 2].ToString().Remove(0, 34));
                    string[] parcalar2;
                    parcalar2 = sayfa.Split('"');
                    arr3.Clear();
                    foreach (string i2 in parcalar2)
                    {
                        arr3.Add(i2);
                    }
                    listBox1.Items.Add(arr3[0].ToString());
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string yol = "F:\\Durumlar.txt";
            StreamReader oku;
            oku = File.OpenText(yol);
            string yazi;
            while ((yazi = oku.ReadLine()) != null)
            {
                listBox1.Items.Add(yazi.ToString());
            }
            oku.Close();
        }
    }
}
