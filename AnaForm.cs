using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Udemy.Entities;

namespace Udemy.WFUI
{
    public partial class AnaForm : Form
    {
        Udemy.BLL.BusinessLogicLayer BLL;



        public AnaForm()
        {
            InitializeComponent();
            BLL = new Udemy.BLL.BusinessLogicLayer();
        }

        private void btn_yeni_kayit_Click(object sender, EventArgs e)
        {
           int sonuc = BLL.yenikayit(Guid.NewGuid(),txt_isim.Text,txt_soyisim.Text,txt_telefonI.Text,
                txt_telefonII.Text,txt_telefonIII.Text,txt_adres.Text,txt_emailadres.Text,txt_website.Text,txt_aciklama.Text);

            if (sonuc > 0)
            {
                MessageBox.Show("kaydınız başarılı bir şekilde eklendi");
                doldur();
            }
            else if (sonuc == -100)
            {
                MessageBox.Show("eksik parametre hatası lütfen isim soyisim telefon1 alanlarını doldurunuz");
            }

            else
            {
                MessageBox.Show("kayıt ekleme işleminde hata oluştu");
            }
        }

        private void doldur()
        {
            List<RehberKayit> rehberkayitlarim = BLL.RehberKayitGetir();
            if(rehberkayitlarim != null && rehberkayitlarim.Count > 0)
            {
                lst_liste.DataSource = rehberkayitlarim;
            }
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            doldur();
        }

        private void lst_liste_DoubleClick(object sender, EventArgs e)
        {
            ListBox L = (ListBox)sender;
            RehberKayit secilendeger =  (RehberKayit)L.SelectedItem;
            txt_isim.Text = secilendeger.isim;
            txt_soyisim.Text = secilendeger.soyisim;
            txt_telefonI.Text = secilendeger.telefonI;
            txt_telefonII.Text = secilendeger.telefonII;
            txt_telefonIII.Text = secilendeger.telefonIII;
            txt_emailadres.Text = secilendeger.emailadres;
            txt_website.Text = secilendeger.Website;
            txt_adres.Text = secilendeger.adres;
            txt_aciklama.Text = secilendeger.aciklama;

            grpbox_kayit.Text = "Rehber Kayıt Güncelle";

        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
           if( lst_liste.SelectedItems != null)
            {
               //kısa kullanım ((RehberKayit)lst_liste.SelectedItem).ID

               RehberKayit K  = (RehberKayit)lst_liste.SelectedItem;
                int sonuc = BLL.kayitguncelle(K.ID, txt_isim.Text, txt_soyisim.Text, txt_telefonI.Text, txt_telefonII.Text, txt_telefonIII.Text, txt_adres.Text, txt_emailadres.Text, txt_website.Text, txt_aciklama.Text);

                if(sonuc > 0)
                {
                    MessageBox.Show("kaydınız başarılı bir şekilde güncellendi");
                    doldur();
                }
                else if (sonuc== -100)
                {
                    MessageBox.Show("eksik parametre hatası");
                }
                else
                {
                    MessageBox.Show("kayıt ekleme işleminde hata oluştu");
                }
            }

        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
           Guid silinecekID = ((RehberKayit)lst_liste.SelectedItem).ID;
            int sonuc = BLL.kayıtsil(silinecekID);
            if(sonuc > 0)
            {
                MessageBox.Show("kaydınız başarılı bir şekilde silindi");
                doldur();
            }
            else
            {
                MessageBox.Show("kayıt silme işleminde hata oluştu");
            }



        }

        

        private void btn_xmlver_Click(object sender, EventArgs e)
        {
            int sonuc = BLL.XMLDataVer();
            if(sonuc > 0)
            {
                lbldataalverdurum.Text = "xml data alver işlemi tamamlandı";
            }
            else
            {
                lbldataalverdurum.Text = "xml data alver işlemi hatalı";
            }
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            int sonuc = BLL.CSVDataver();

            if (sonuc > 0)
            {
                lbldataalverdurum.Text = "csv data ver işlemi tamamlandı";
            }
            else
            {
                lbldataalverdurum.Text = "csv data ver işlemi hatalı";
            }
        }

        private void btn_jsonver_Click(object sender, EventArgs e)
        {
           int sonuc = BLL.JsonDataVer();

            if (sonuc > 0)
            {
                lbldataalverdurum.Text = "json data ver işlemi tamamlandı";
            }
            else
            {
                lbldataalverdurum.Text = "json data ver işlemi hatalı";
            }
        }
    }
}
