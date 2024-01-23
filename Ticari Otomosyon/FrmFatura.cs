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
using DevExpress.XtraExport.Xls;
using System.Data.SqlTypes;


namespace Ticari_Otomosyon
{
    public partial class FrmFatura : Form
    {
        public FrmFatura()
        {
            InitializeComponent();
        }

      sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter  da = new SqlDataAdapter("Select * from TBL_FATURABILGI",bgl.baglanti());
            DataTable dt= new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;       
        }

        void temizle()
        {
            TxtAlıcı.Text = "";
            TxId.Text = "";
            TxtFaturaid.Text = "";
            TxtFiyat.Text = "";
            TxtMiktar.Text = "";
            TxtSeri.Text = "";
            TxtSırano.Text = "";
            TxtTeslimalan.Text = "";
            TxtTeslimeden.Text = "";
            TxtTutar.Text = "";
            TxtUrunid.Text = "";
            TxtUrünad.Text = "";
            TxtVergid.Text = "";
            MskSaat.Text = "";
            MskTarih.Text = "";
            
        }

        private void FrmFatura_Load(object sender, EventArgs e)
        {
         listele();

            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxId.Text = dr["FATURABILGIID"].ToString();
                TxtSeri.Text = dr["SERI"].ToString();
                TxtSırano.Text = dr["SIRANO"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtVergid.Text = dr["VERGIDAIRE"].ToString();
                TxtAlıcı.Text = dr["ALICI"].ToString();
                TxtTeslimeden.Text = dr["TESLIMEDEN"].ToString();
                TxtTeslimalan.Text = dr["TESLIMALAN"].ToString();
         


            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtFaturaid.Text == "")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@p2", TxtSırano.Text);
                komut.Parameters.AddWithValue("@p3", MskTarih.Text);
                komut.Parameters.AddWithValue("@p4", MskSaat.Text);
                komut.Parameters.AddWithValue("@p5", TxtVergid.Text);
                komut.Parameters.AddWithValue("@p6", TxtAlıcı.Text);
                komut.Parameters.AddWithValue("@p7", TxtTeslimeden.Text);
                komut.Parameters.AddWithValue("@p8", TxtTeslimalan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Sisteme Başarıyla Kaydedildi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }

            if (TxtFaturaid.Text != "")
            {
                double miktar, tutar, fiyat;
                fiyat= Convert.ToDouble(TxtFiyat.Text);
                miktar= Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text=tutar.ToString();


                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@P1,@P2,@P3,@P4,@P5)",bgl.baglanti());
                komut2.Parameters.AddWithValue("@P1", TxtUrünad.Text);
                komut2.Parameters.AddWithValue("@P2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@P3", TxtFiyat.Text);
                komut2.Parameters.AddWithValue("@P4", TxtTutar.Text);
                komut2.Parameters.AddWithValue("@P5", TxtFaturaid.Text);
                komut2.ExecuteNonQuery(); 
                bgl.baglanti().Close();
                MessageBox.Show("Faturaya Ait Ürün kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                listele();
            }



        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From TBL_FATURABILGI where FATURABILGIID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1",TxId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Kaydı Başarıyla Silindi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Question);
            listele();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_FATURABILGI set SERI=@P1,SIRANO=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7,TESLIMALAN=@P8 where FATURABILGIID=@P9", bgl.baglanti());
            
            komut.Parameters.AddWithValue("@p1", TxtSeri.Text);
            komut.Parameters.AddWithValue("@p2", TxtSırano.Text);
            komut.Parameters.AddWithValue("@p3", MskTarih.Text);
            komut.Parameters.AddWithValue("@p4", MskSaat.Text);
            komut.Parameters.AddWithValue("@p5", TxtVergid.Text);
            komut.Parameters.AddWithValue("@p6", TxtAlıcı.Text);
            komut.Parameters.AddWithValue("@p7", TxtTeslimeden.Text);
            komut.Parameters.AddWithValue("@p8", TxtTeslimalan.Text);
            komut.Parameters.AddWithValue("@p9", TxId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay fr = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }
    }
}
