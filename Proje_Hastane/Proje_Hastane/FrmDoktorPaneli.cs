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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter dl = new SqlDataAdapter("Select * From Tbl_Doktorlar", bgl.baglanti());
            dl.Fill(dt1);
            dataGridView1.DataSource = dt1;

            SqlCommand kmt = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader brans = kmt.ExecuteReader();
            while (brans.Read())
            {
                CmbBrans.Items.Add(brans[0]);
            }
            bgl.baglanti().Close();

            //Branşları combobox'a aktarma.
            SqlCommand kmt2 = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader brs = kmt2.ExecuteReader();
            while (brs.Read())
            {
                CmbBrans.Items.Add(brs[0]);
            }
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand ekle = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@e1,@e2,@e3,@e4,@e5)", bgl.baglanti());
            ekle.Parameters.AddWithValue("@e1", TxtAd.Text);
            ekle.Parameters.AddWithValue("@e2", TxtSoyad.Text);
            ekle.Parameters.AddWithValue("@e3",CmbBrans.Text);
            ekle.Parameters.AddWithValue("@e4",MskTC.Text);
            ekle.Parameters.AddWithValue("@e5",TxtSifre.Text);
            ekle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Kaydı Eklenmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from Tbl_Doktorlar where DoktorTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.ExecuteNonQuery();

            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silinmiştir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex; //seçilen hücrenin 0. indexine göre alsın.
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); //datagrid'in satıları içerisinde secilen satırın hücreleri içerisinde 1. hücreyi al.
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString(); //datagrid'in satıları içerisinde secilen satırın hücreleri içerisinde 2. hücreyi al.
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorTC=@d4,DoktorSifre=@d5",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", TxtAd.Text);
            komut.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@d3", CmbBrans.Text);
            komut.Parameters.AddWithValue("@d4", MskTC.Text);
            komut.Parameters.AddWithValue("@d5", TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Bilgileri Güncellenmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
