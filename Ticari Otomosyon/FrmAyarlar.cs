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

namespace Ticari_Otomosyon
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele ()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da   = new SqlDataAdapter("Select * From TBL_ADMIN",bgl.baglanti());
            da.Fill (dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            TxtKullaniciadi.Text = "";
            TxtSifre.Text = "";
        }
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
        listele ();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (BtnKaydet.Text == "Kaydet")
            {
                SqlCommand komut1 = new SqlCommand("insert into TBL_ADMIN (KullaniciAd,Sifre) values (@p1,@p2)", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", TxtKullaniciadi.Text);
                komut1.Parameters.AddWithValue("@p2", TxtSifre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kullanıcı Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if (BtnKaydet.Text == "Güncelle")
            {
                SqlCommand komut = new SqlCommand("update TBL_ADMIN  set Sifre=@p2 where KullaniciAd=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtKullaniciadi.Text);
                komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kullanıcı Sistemde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }

           
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                TxtKullaniciadi.Text = dr["KullaniciAd"].ToString();
                TxtSifre.Text= dr["Sifre"].ToString() ;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            temizle ();
        }

        private void TxtKullaniciadi_TextChanged(object sender, EventArgs e)
        {
            if (TxtKullaniciadi.Text !="")
            {
                BtnKaydet.Text = "Güncelle";
                BtnKaydet.BackColor = Color.GreenYellow;
            }
            else 
            { 
            BtnKaydet.Text = "Kaydet";
                BtnKaydet.BackColor= Color.Azure;
            }
        }
    }
}
