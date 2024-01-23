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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl =new sqlbaglantisi();

        void giderlistesi()
        {
            DataTable dt = new DataTable();
           SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_GIDERLER",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        void temizle()
        {
            CmbAy.Text = "";
            CmbYıl.Text = "";
            TxId.Text = "";
            TxtElektirik.Text = "";
            TxtDoğalgaz.Text = "";
            TxtEkstra.Text = "";
            TxtInternet.Text = "";
            TxtMaaslar.Text = "";
            TxtSu.Text = "";
            RchNot.Text = "";
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
        giderlistesi();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
        SqlCommand komut= new SqlCommand("insert into TBL_GIDERLER (AY,YIL,ELEKTIRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbAy.Text);
            komut.Parameters.AddWithValue("@p2", CmbYıl.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse (TxtElektirik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse (TxtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse (TxtDoğalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse (TxtInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse (TxtMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse (TxtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", RchNot.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Giderler Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderlistesi();
            temizle();



        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
        SqlCommand komutsil = new SqlCommand("Delete From TBL_GIDERLER where ID=@P1",bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", TxId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Listeden Silindi","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Stop);
            giderlistesi();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxId.Text = dr["ID"].ToString();
                CmbAy.Text = dr["AY"].ToString();
                CmbYıl.Text = dr["YIL"].ToString();
                TxtElektirik.Text = dr["ELEKTIRIK"].ToString();
                TxtSu.Text = dr["SU"].ToString();
                TxtDoğalgaz.Text = dr["DOGALGAZ"].ToString();
                TxtElektirik.Text = dr["INTERNET"].ToString();
                TxtMaaslar.Text = dr["MAASLAR"].ToString();
                TxtEkstra.Text = dr["EKSTRA"].ToString();
                RchNot.Text = dr["NOTLAR"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_GIDERLER set AY=@P1,YIL=@P2,ELEKTIRIK=@P3,SU=@P4,DOGALGAZ=@P5,INTERNET=@P6,MAASLAR=@P7,EKSTRA=@P8,NOTLAR=@P9 where ID=@P10", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbAy.Text);
            komut.Parameters.AddWithValue("@p2", CmbYıl.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektirik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(TxtDoğalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", RchNot.Text);
            komut.Parameters.AddWithValue("@p10", TxId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Giderler Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            giderlistesi();
            temizle();
        }
    }
}
