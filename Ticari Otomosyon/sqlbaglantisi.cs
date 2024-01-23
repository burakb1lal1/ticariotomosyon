using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Ticari_Otomosyon
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=BURAKB1LAL1\SQLEXPRESS;Initial Catalog=Dbo TicariOtomosyon;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
