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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string TCnumara;
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = TCnumara;

            //Sekreterin girilen label'da ki TC Numarasına göre formda adı ve soyadını getirdik.
            SqlCommand komut = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter where SekreterTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            //Branşları Datagride Aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;
            
            //Doktorları listeye aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter dl = new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as 'Doktorlar',DoktorBrans From Tbl_Doktorlar", bgl.baglanti());
            dl.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşları combobxa aktarma
            SqlCommand kmt = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader brans = kmt.ExecuteReader();
            while (brans.Read())
            {
                CmbBrans.Items.Add(brans[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand kaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)",bgl.baglanti());
            kaydet.Parameters.AddWithValue("@r1", MskTarih.Text);
            kaydet.Parameters.AddWithValue("@r2", MskSaat.Text);
            kaydet.Parameters.AddWithValue("@r3", CmbBrans.Text);
            kaydet.Parameters.AddWithValue("@r4", CmbDoktor.Text);
            kaydet.ExecuteNonQuery();

            bgl.baglanti().Close();

            MessageBox.Show("Tebrikler! Randevunuz Oluşturulmuştur.");
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();

            SqlCommand dktr = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@d1", bgl.baglanti());
            dktr.Parameters.AddWithValue("@d1", CmbBrans.Text);
            SqlDataReader dk = dktr.ExecuteReader();
            while (dk.Read())
            {
                CmbDoktor.Text = (dk[0] + " " + dk[1]);
            }
            bgl.baglanti().Close();

        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@s1)",bgl.baglanti());
            komut.Parameters.AddWithValue("@s1",RchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drt = new FrmDoktorPaneli();
            drt.Show();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans frb = new FrmBrans();
            frb.Show();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi rdv = new FrmRandevuListesi();
            rdv.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular dy = new FrmDuyurular();
            dy.Show();
        }
    }
}
