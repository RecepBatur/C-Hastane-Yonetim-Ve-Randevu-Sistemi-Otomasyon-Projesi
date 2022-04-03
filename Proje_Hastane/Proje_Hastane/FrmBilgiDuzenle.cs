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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }
        public string TCno;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCno;

            //bilgi düzenle formuna hasta bilgilerini girilen TC no'ya göre çekiyoruz.
            SqlCommand komut = new SqlCommand("Select * From Tbl_Hastalar where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text=dr[1].ToString();
                TxtSoyad.Text=dr[2].ToString();
                MskTelefon.Text=dr[4].ToString();
                TxtSifre.Text=dr[5].ToString();
                CmbCinsiyet.Text=dr[6].ToString();

            }
            bgl.baglanti().Close();

        }

        private void TxtSoyad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MskTC_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void TxtAd_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtSifre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand guncelle =new SqlCommand("Update Tbl_Hastalar set HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 where HastaTc=@p6",bgl.baglanti());
            guncelle.Parameters.AddWithValue("@p1",TxtAd.Text);
            guncelle.Parameters.AddWithValue("@p2",TxtSoyad.Text);
            guncelle.Parameters.AddWithValue("@p3",MskTelefon.Text);
            guncelle.Parameters.AddWithValue("@p4",TxtSifre.Text);
            guncelle.Parameters.AddWithValue("@p5",CmbCinsiyet.Text);
            guncelle.Parameters.AddWithValue("@p6",MskTC.Text);
            guncelle.ExecuteNonQuery();

            bgl.baglanti().Close();

            MessageBox.Show("Bilgileriniz Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
