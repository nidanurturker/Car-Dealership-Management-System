using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Araba_Otomasyonu_1
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ArabaDbContext db = new ArabaDbContext();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var Araba = db.Arabas.ToList();
                dataGridView1.DataSource = db.Arabas.ToList();
                dataGridView1.DataSource = Araba;

                dataGridView1.Columns["Araba_Id"].HeaderText = "Araba ID";
                dataGridView1.Columns["Araba_Marka"].HeaderText = "Araba Marka";
                dataGridView1.Columns["Araba_Model"].HeaderText = "Araba Model";
                dataGridView1.Columns["Araba_Model_Yili"].HeaderText = "Araba Model Yılı";
                dataGridView1.Columns["Araba_Fiyat"].HeaderText = "Araba Fiyat";
                dataGridView1.Columns["Araba_Id"].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Hata = {ex.Message} ");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Araba newAraba = new Araba()
                {
                    Araba_Marka = txt_1.Text,
                    Araba_Model = txt_2.Text,
                    Araba_Model_Yili = txt_3.Text,
                    Araba_Fiyat = decimal.Parse(txt_4.Text)
                };
                db.Arabas.Add(newAraba);
                db.SaveChanges();

                MessageBox.Show("Yeni Araba Eklendi!");
                button1.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Hata = {ex.Message} ");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Araba_Id"].Value);
                    Araba araba = db.Arabas.Find(selectedId);
                    if (araba != null)
                    {
                        araba.Araba_Marka = txt_1.Text;
                        araba.Araba_Model = txt_2.Text;
                        araba.Araba_Model_Yili = txt_3.Text;
                        decimal fiyat;
                        if (decimal.TryParse(txt_4.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out fiyat))
                        {
                            araba.Araba_Fiyat = fiyat;
                        }
                        else
                        {
                            MessageBox.Show("Lütfen geçerli bir fiyat giriniz.");
                        }

                        db.SaveChanges();
                        MessageBox.Show("Araba Bilgileri Güncellendi!");
                        button1.PerformClick();
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata ={ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Araba_Id"].Value);
                    Araba araba = db.Arabas.Find(selectedId);
                    if (araba != null)
                    {
                        db.Arabas.Remove(araba);
                        db.SaveChanges();
                        MessageBox.Show("Araba Bilgileri Silindi!");
                        button1 .PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {

                txt_1.Text = dataGridView1.Rows[e.RowIndex].Cells["Araba_Marka"].Value.ToString();
                txt_2.Text = dataGridView1.Rows[e.RowIndex].Cells["Araba_Model"].Value.ToString();
                txt_3.Text = dataGridView1.Rows[e.RowIndex].Cells["Araba_Model_Yili"].Value.ToString();
                txt_4.Text = dataGridView1.Rows[e.RowIndex].Cells["Araba_Fiyat"].Value.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form_ArabaGaleri form1_ag = new Form_ArabaGaleri();
            this.Hide();
            form1_ag.ShowDialog();
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
