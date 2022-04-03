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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmBrans_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand ekle = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@b1)",bgl.baglanti());
            ekle.Parameters.AddWithValue("@b1",TxtBrans.Text);
            ekle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Branş Eklenmiştir.");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand delete = new SqlCommand("Delete From Tbl_Branslar where Bransİd=@d1", bgl.baglanti());
            delete.Parameters.AddWithValue("@d1", Txtİd.Text);
            delete.ExecuteNonQuery();
            MessageBox.Show("Kayutlı Branş Silinmiştir.");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand guncelle = new SqlCommand("Update Tbl_Branslar set  BransAd=@b1 where Bransİd=@b2", bgl.baglanti());
            guncelle.Parameters.AddWithValue("@b1", TxtBrans.Text);
            guncelle.Parameters.AddWithValue("@b2",Txtİd.Text);
            guncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Adı Güncellenmiştir.");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtİd.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }
    }
}
