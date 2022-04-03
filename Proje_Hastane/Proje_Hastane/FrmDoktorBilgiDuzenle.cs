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
namespace Proje_Hastane
{
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string TCNO;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCNO;

            SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar where DoktorTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTC.Text);
            SqlDataReader rd = komut.ExecuteReader();
            while (rd.Read())
            {
                TxtAd.Text = rd[1].ToString();
                TxtSoyad.Text = rd[2].ToString();
                CmbBrans.Text = rd[3].ToString();
                TxtSifre.Text = rd[5].ToString();
            }
            bgl.baglanti().Close();

        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand update = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 where DoktorTC=@p5",bgl.baglanti());
            update.Parameters.AddWithValue("@p1",TxtAd.Text);
            update.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            update.Parameters.AddWithValue("@p3",CmbBrans.Text);
            update.Parameters.AddWithValue("@p4", TxtSifre.Text);
            update.Parameters.AddWithValue("@p5", MskTC.Text);
            update.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Tebrikler! Kayıt Güncellenmiştir");
        }
    }
}
