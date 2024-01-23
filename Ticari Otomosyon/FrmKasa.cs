using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.Charts;


namespace Ticari_Otomosyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void lsitele()
        {
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3= new SqlDataAdapter("Select * From TBL_GIDERLER",bgl.baglanti());
            da3.Fill(dt3);
            gridControl2.DataSource = dt3;  

        }
        void musterihareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da= new SqlDataAdapter("Execute MusteriHareketler",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void firmahareket()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Execute FirmaHareketler", bgl.baglanti());
            da2.Fill(dt2);
            gridControl3.DataSource = dt2;
        }

        public string ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            LblAktifKullanici.Text = ad;




            lsitele();
            musterihareket();
            firmahareket();


            //Toplam Tutarı Hesaplama 
            SqlCommand komut1 = new SqlCommand("Select Sum(Tutar) From TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblKasaToplam.Text = dr1[0].ToString()+ "₺";
            }
            bgl.baglanti().Close();

            // son ayın faturaları
            SqlCommand komut2 = new SqlCommand("Select(ELEKTIRIK + SU + DOGALGAZ + INTERNET + EKSTRA ) From TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr2= komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblOdemeler.Text = dr2[0].ToString() + "₺";
            }
            bgl.baglanti().Close();

            //son ayın personel maaşları
            SqlCommand komut3 = new SqlCommand("Select Maaslar From TBL_GIDERLER order by ID asc ", bgl.baglanti());
            SqlDataReader d3= komut3.ExecuteReader();
            while (d3.Read())
            {
                LblPersonelMaas.Text = d3[0].ToString() + "₺";
            }
            bgl.baglanti().Close();

            //son ay müsteri sayısı
            SqlCommand komut4 = new SqlCommand("Select count(*) from TBL_MUSTERILER ", bgl.baglanti());
            SqlDataReader d4 = komut4.ExecuteReader();
            while (d4.Read())
            {
                LblMusteriSayisi.Text = d4[0].ToString() ;
            }
            bgl.baglanti().Close();

            //son ay firma sayısı
            SqlCommand komut5 = new SqlCommand("Select count(*) from TBL_FIRMALAR ", bgl.baglanti());
            SqlDataReader d5 = komut5.ExecuteReader();
            while (d5.Read())
            {
                LblFirmaSayisi.Text = d5[0].ToString();
            }
            bgl.baglanti().Close();

            //son PERSONELsayısı
            SqlCommand komut6 = new SqlCommand("Select count(*) from TBL_PERSONELLER ", bgl.baglanti());
            SqlDataReader d6 = komut6.ExecuteReader();
            while (d6.Read())
            {
             LblPersonelSayisi.Text = d6[0].ToString();
            }
            bgl.baglanti().Close();

            
            //son STOK sayısı
            SqlCommand komut7 = new SqlCommand("Select sum(ADET) from TBL_URUNLER", bgl.baglanti());
            SqlDataReader d7 = komut7.ExecuteReader();
            while (d7.Read())
            {
                LblStokSayisi.Text = d7[0].ToString();
            }
            bgl.baglanti().Close();

            //son firma sehir sayısı
            SqlCommand komut8 = new SqlCommand("Select count(Distinct(IL)) from TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader d8 = komut8.ExecuteReader();
            while (d8.Read())
            {
                LblSehirSayısı.Text = d8[0].ToString();
            }
            bgl.baglanti().Close();

            //son müsteri sehir sayısı
            SqlCommand komut9 = new SqlCommand("Select count(Distinct(IL)) from TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader d9 = komut9.ExecuteReader();
            while (d9.Read())
            {
                LblMusterisehir.Text = d9[0].ToString();
            }
            bgl.baglanti().Close();

          
        }

        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if (sayac>0 && sayac<=10)
            {
                //chart Elektirik Faturası son 4 ay listele
                groupControl11.Text = "Elektirik";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 AY,ELEKTIRIK from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();

                
            }
            if (sayac>10 &&  sayac<=20)
            {
                //chart su faturası son 4 ay listeleme 
                groupControl11.Text = "Su";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select top 4 AY,SU from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 20 && sayac <= 30)
            {
                //chart doğalgaz faturası son 4 ay listeleme 
                groupControl11.Text = "Doğalgaz";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select top 4 AY,DOGALGAZ from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 30 && sayac <= 40)
            {
                //chart internet faturası son 4 ay listeleme 
                groupControl11.Text = "İnternet";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select top 4 AY,INTERNET from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 40 && sayac <= 50)
            {
                //chart EKSTRE faturası son 4 ay listeleme 
                groupControl11.Text = "Ekstra";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select top 4 EKSTRA,SU from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac == 51)
            {
                sayac= 0;
            }

        }
        int sayac2= 0;
        private void timer2_Tick(object sender, EventArgs e)
        {

            sayac2++;
            if (sayac2 > 0 && sayac2 <= 5)
            {
                //chart Elektirik Faturası son 4 ay listele
                groupControl10.Text = "Elektirik";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 AY,ELEKTIRIK from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();


            }
            if (sayac2 > 5 && sayac2 <= 10)
            {
                //chart su faturası son 4 ay listeleme 
                groupControl10.Text = "Su";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select top 4 AY,SU from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 10 && sayac2 <= 15)
            {
                //chart doğalgaz faturası son 4 ay listeleme 
                groupControl10.Text = "Doğalgaz";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select top 4 AY,DOGALGAZ from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 15 && sayac2 <= 20)
            {
                //chart internet faturası son 4 ay listeleme 
                groupControl10.Text = "İnternet";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select top 4 AY,INTERNET from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 20 && sayac2 <= 25)
            {
                //chart EKSTRE faturası son 4 ay listeleme 
                groupControl10.Text = "Ekstra";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select top 4 EKSTRA,SU from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
}
