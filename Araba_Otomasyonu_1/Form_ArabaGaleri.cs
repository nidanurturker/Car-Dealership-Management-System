using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Araba_Otomasyonu_1
{
    public partial class Form_ArabaGaleri : Form
    {
        public Form_ArabaGaleri()
        {
            InitializeComponent();


        }
        ArabaDbContext db = new ArabaDbContext();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var list = db.ArabaGaleris
                    .Include(ag => ag.Araba).Include(ag => ag.Galeri.Satis)
                      .Select(ag => new
                      {


                          ag.AraGal_Id,

                          ArabaMarka = ag.Araba.Araba_Marka,
                          ArabaModel = ag.Araba.Araba_Model,
                          ArabaModelYılı = ag.Araba.Araba_Model_Yili,
                          ArabaFiyat = ag.Araba.Araba_Fiyat,
                          Galeri = ag.Galeri.Galeri_Isim,
                          Satis = ag.Galeri.Satis.Satis_Isim,


                      })
                      .ToList();
                dataGridView1.DataSource = list;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["AraGal_Id"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata ={ex.Message}");

            }
        }

        private void Form_ArabaGaleri_Load(object sender, EventArgs e)
        {


            try
            {
                combogaleri.DataSource = db.Arabas // Table_Customers tablosunu seçiyoruz
                .OrderBy(c => c.Araba_Marka) // OrderBy metodu ile müşterinin adına göre alfabetik şekilde sıralıyoruz
                .Select(c => new
                {
                    c.Araba_Id,
                    // combobox'ta gösterilecek değerler arka planda bu ID bilgisine göre seçilecek
                    Fullname = c.Araba_Marka + " " + c.Araba_Model + " " + c.Araba_Model_Yili // combobox'ta değerler bu şekilde listelenecek
                })
                .ToList();
                combogaleri.DisplayMember = "FullName";
                combogaleri.ValueMember = "Araba_Id";// Combobox'ta bize gösterilecek isimler FullName'den seçiliyor (yukarıdaki değişken)
                // Combobox'ta gösterilen değerler arka planda ID bilgisinden çekiliyor

                // aynı şeyler aşağıdakiler için de geçerli
                combosatis.DataSource = db.Galeris
                    .Include(p => p.Satis)
                    .OrderBy(p => p.Galeri_Isim)
                    .Select(p => new
                    {
                        p.Galeri_Id,
                        Info = p.Galeri_Isim + " - " + p.Satis.Satis_Isim
                    })
                    .ToList();
                combosatis.DisplayMember = "Info";
                combosatis.ValueMember = "Galeri_Id";

                button1.PerformClick();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // ComboBox'lardan seçilen ID'lerini alıyoruz
                int selectedArabaId = (int)combogaleri.SelectedValue;
                int selectedGaleriId = (int)combosatis.SelectedValue;

                // Yeni CustomerProduct nesnesi oluşturuyoruz
                var newRecord = new ArabaGaleri
                {
                    Araba_Id = selectedArabaId,
                    Galeri_Id = selectedGaleriId
                };

                // CustomerProduct tablosuna yeni kaydımızı ekliyoruz
                db.ArabaGaleris.Add(newRecord);
                db.SaveChanges();

                MessageBox.Show("Kayıt başarıyla eklendi.");

                // Listeleme işleminin tanımlı olduğu btn_listele'nin kodlarını çalıştırıyoruz
                button1.PerformClick();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {



            try
            {
                DialogResult sonuc = MessageBox.Show(
                            "Silmek istediğinize emin misiniz?",
                            "Silme Onayı",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    // DataGridView’da bir satır seçili mi?
                    if (dataGridView1.CurrentRow != null)
                    {
                        // Seçilen satırdaki CusPro_Id değerini alıyoruz
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["AraGal_Id"].Value);

                        // İlgili veritabanı kaydını buluyoruz
                        var recordToDelete = db.ArabaGaleris.Find(selectedId);

                        if (recordToDelete != null)
                        {
                            // Kaydı siliyoruz
                            db.ArabaGaleris.Remove(recordToDelete);
                            db.SaveChanges();

                            MessageBox.Show("Kayıt başarıyla silindi.");

                            // DatagridvieW'daki listeyi güncelliyoruz
                            button1.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }












        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Eğer radioButton1 seçiliyse işlem yap
                if (radioButton1.Checked)
                {
                    // CustomerProduct tablosunu müşteriye göre grupla
                    var result = db.ArabaGaleris
                        .GroupBy(ag => new
                        {
                            ag.Araba.Araba_Id,
                            ag.Araba.Araba_Marka,
                            ag.Araba.Araba_Model,
                        })
                        // Her grup için müşteri adı, soyadı ve toplam ürün sayısını al
                        .Select(g => new
                        {
                            FullName = g.Key.Araba_Marka + " " + g.Key.Araba_Model,
                            TotalCount = g.Count()
                        })
                        // Ürün sayısına göre azalan sırala (en çok ürünü alan en üstte)
                        .OrderByDescending(x => x.TotalCount)
                        // En çok ürün alan müşteriyi al
                        .FirstOrDefault();

                    // Eğer sonuç varsa label1'e yaz
                    if (result != null)
                    {
                        label3.Text = $"{result.FullName} Bulunur.";
                    }
                    else
                    {
                        label3.Text = "Kayıt bulunamadı.";
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Eğer radioButton1 seçiliyse işlem yap
                if (radioButton2.Checked)
                {
                    // CustomerProduct tablosunu müşteriye göre grupla
                    var result = db.ArabaGaleris
                        .GroupBy(ag => new
                        {
                            ag.Galeri.Galeri_Id,
                            ag.Galeri.Galeri_Isim,

                        })
                        // Her grup için müşteri adı, soyadı ve toplam ürün sayısını al
                        .Select(g => new
                        {
                            FullName = g.Key.Galeri_Isim,
                            TotalCount = g.Count()
                        })
                        // Ürün sayısına göre azalan sırala (en çok ürünü alan en üstte)
                        .OrderByDescending(x => x.TotalCount)
                        // En çok ürün alan müşteriyi al
                        .FirstOrDefault();

                    // Eğer sonuç varsa label1'e yaz
                    if (result != null)
                    {
                        label4.Text = $"{result.FullName}'i En Çok Arabaya Sahiptir.";
                    }
                    else
                    {
                        label4.Text = "Kayıt bulunamadı.";
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
