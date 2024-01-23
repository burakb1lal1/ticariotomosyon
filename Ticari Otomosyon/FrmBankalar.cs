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
using DevExpress.XtraBars;
using DevExpress.Data.Linq.Helpers;


namespace Ticari_Otomosyon
{
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }
         sqlbaglantisi bgl = new sqlbaglantisi();
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
            TxtBankaAd.Text = "";
            TxId.Text = "";
            lookUpEdit1.Text = "";
            TxtHesapno.Text = "";
            TxtHesaptütü.Text = "";
            TxtIban.Text = "";
            TxtSube.Text = "";
            TxtYetkili.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
            MskTarih.Text = "";
            MskTelefon.Text = "";
        }

        void bankalarlistele()
        {
            SqlDataAdapter da= new SqlDataAdapter("Execute BankaBilgileri",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource= dt;
        }

        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD From TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        }

        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            bankalarlistele();
            sehirlistesi();
            firmalistesi();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_BANKALAR (BANKADI,IL,ILCE,SUBE,IBAN,HESAPNO,HESAPTURU,YETKILI,TELEFON,TARIH,FIRMAID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", Cmbil.Text);
            komut.Parameters.AddWithValue("@p3", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", TxtIban.Text);
            komut.Parameters.AddWithValue("@p6", TxtHesapno.Text);
            komut.Parameters.AddWithValue("@p7", TxtHesaptütü.Text);
            komut.Parameters.AddWithValue("@p8", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p9", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p10", MskTarih.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgileri Kaydedildi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            bankalarlistele();

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

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxId.Text = dr["ID"].ToString();
                TxtBankaAd.Text = dr["BANKADI"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                TxtSube.Text = dr["SUBE"].ToString();
                TxtIban.Text = dr["IBAN"].ToString();
                TxtHesapno.Text = dr["HESAPNO"].ToString();
                TxtHesaptütü.Text = dr["HESAPTURU"].ToString();
                TxtYetkili.Text = dr["YETKILI"].ToString();
                MskTelefon.Text = dr["TELEFON"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
              
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete from TBL_BANKALAR where ID=@P1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@P1", TxId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgileri Silindi.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Stop);
            bankalarlistele();

        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_BANKALAR set BANKADI=@P1,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5,HESAPNO=@P6,HESAPTURU=@P7,YETKILI=@P8,TELEFON=@P9,TARIH=@P10,FIRMAID=@P11 where ID=@P12", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", Cmbil.Text);
            komut.Parameters.AddWithValue("@p3", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", TxtIban.Text);
            komut.Parameters.AddWithValue("@p6", TxtHesapno.Text);
            komut.Parameters.AddWithValue("@p7", TxtHesaptütü.Text);
            komut.Parameters.AddWithValue("@p8", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p9", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p10", MskTarih.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p12", TxId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgileri Güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            bankalarlistele();
        }

        private void MskTarih_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
