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

namespace Ticari_Otomosyon
{
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }
         sqlbaglantisi bgl = new sqlbaglantisi();
        public string urunid;
        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
        TxtUrunid.Text = urunid;


            SqlCommand komut = new SqlCommand("Select * From TBL_FATURADETAY where FATURAURUNID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", urunid);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
            TxtFiyat.Text= dr[3].ToString();
            TxtMiktar.Text= dr[2].ToString();
            TxtTutar.Text= dr[4].ToString();
            TxtUrünad.Text= dr[1].ToString();
                bgl.baglanti().Close();
            }


        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_FATURADETAY set URUNAD=@P1,MIKTAR=@P2,FIYAT=@P3,TUTAR=@P4 WHERE FATURAURUNID=@P5", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtUrünad.Text);
            komut.Parameters.AddWithValue("@p2", TxtMiktar.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse (TxtFiyat.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse (TxtTutar.Text));
            komut.Parameters.AddWithValue("@p5", TxtUrunid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close() ;
            MessageBox.Show("Ürün Başarıyla Güncellendi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From TBL_FATURADETAY where FATURAURUNID=@P1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtUrunid.Text);
            komut.ExecuteNonQuery() ;
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Başarıyla Silindi", "Uyrarı", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}
