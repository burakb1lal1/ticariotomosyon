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
using DevExpress.Utils.CodedUISupport;


namespace Ticari_Otomosyon
{
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void firmalistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_FIRMALAR", bgl.baglanti());
            DataTable dt = new DataTable(); 
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select Sehir From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void temizle()
        {
            TxtAd.Text = "";
            TxId.Text = "";
            TxtKod1.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";
            TxtMail.Text = "";
            TxtSektör.Text = "";
            TxtVergiD.Text = "";
            TxtYetkili.Text = "";
            TxtYetkiliG.Text = "";
            MskFax.Text = "";
            MskTc.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            MskTelefon3.Text = "";
            RchAdres.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
            TxtAd.Focus();
            
        }
        
        void carikodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("Select FIRMAKOD1 from TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                RchKod1.Text= dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
        firmalistesi();

        sehirlistesi();

        temizle();

        carikodaciklamalar();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtYetkiliG.Text = dr["YETKILISTATU"].ToString();
                TxtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                MskTelefon1.Text = dr["TELEFON1"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                MskTelefon3.Text = dr["TELEFON3"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                MskFax.Text = dr["FAX"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                TxtVergiD.Text = dr["VERGIDAIRE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                MskTc.Text = dr["YETKILITC"].ToString();
                TxtKod1.Text = dr["OZELKOD1"].ToString();
                TxtKod2.Text = dr["OZELKOD2"].ToString();
                TxtKod3.Text = dr["OZELKOD3"].ToString();
                TxtSektör.Text = dr["SEKTOR"].ToString();
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_FIRMALAR(AD,YETKILISTATU,YETKILIADSOYAD,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,YETKILITC,OZELKOD1,OZELKOD2,OZELKOD3,SEKTOR) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17)" , bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtYetkiliG.Text);
            komut.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P5", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@P6", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@P7", TxtMail.Text);
            komut.Parameters.AddWithValue("@P8", MskFax.Text);
            komut.Parameters.AddWithValue("@P9", Cmbil.Text);
            komut.Parameters.AddWithValue("@P10", Cmbilce.Text);
            komut.Parameters.AddWithValue("@P11", TxtVergiD.Text);
            komut.Parameters.AddWithValue("@P12", RchAdres.Text);
            komut.Parameters.AddWithValue("@P13", MskTc.Text);
            komut.Parameters.AddWithValue("@P14", TxtKod1.Text);
            komut.Parameters.AddWithValue("@P15", TxtKod2.Text);
            komut.Parameters.AddWithValue("@P16", TxtKod3.Text);
            komut.Parameters.AddWithValue("@P17", TxtSektör.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            temizle();
            


        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ilce from TBL_ILCELER where Sehir=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From TBL_FIRMALAR where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            firmalistesi();
            MessageBox.Show("Firma listeden silindi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Hand);
            temizle() ;
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_FIRMALAR set AD=@P1,YETKILISTATU=@P2,YETKILIADSOYAD=@P3,TELEFON1=@P4,TELEFON2=@P5,TELEFON3=@P6,MAIL=@P7,FAX=@P8,IL=@P9,ILCE=@P10,VERGIDAIRE=@P11,ADRES=@P12,YETKILITC=@P13,OZELKOD1=@P14,OZELKOD2=@P15,OZELKOD3=@P16,SEKTOR=@P17 WHERE ID=@P18", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtYetkiliG.Text);
            komut.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P5", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@P6", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@P7", TxtMail.Text);
            komut.Parameters.AddWithValue("@P8", MskFax.Text);
            komut.Parameters.AddWithValue("@P9", Cmbil.Text);
            komut.Parameters.AddWithValue("@P10", Cmbilce.Text);
            komut.Parameters.AddWithValue("@P11", TxtVergiD.Text);
            komut.Parameters.AddWithValue("@P12", RchAdres.Text);
            komut.Parameters.AddWithValue("@P13", MskTc.Text);
            komut.Parameters.AddWithValue("@P14", TxtKod1.Text);
            komut.Parameters.AddWithValue("@P15", TxtKod2.Text);
            komut.Parameters.AddWithValue("@P16", TxtKod3.Text);
            komut.Parameters.AddWithValue("@P17", TxtSektör.Text);
            komut.Parameters.AddWithValue("@p18",TxId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            firmalistesi();
            temizle();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }

}
