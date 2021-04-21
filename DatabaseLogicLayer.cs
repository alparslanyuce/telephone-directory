using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Entities;

namespace Udemy.Core
{
    public class DatabaseLogicLayer
    {
        List<RehberKayit> kayitlarim;



        public DatabaseLogicLayer()
        {
            kayitlarim = new List<RehberKayit>();
            VeriTabanıKontrol();
        }




      private void VeriTabanıKontrol()
        {
           bool klasorKontrol = Directory.Exists(@"c:\TelefonRehberDB\");
            if (klasorKontrol == false)
            {
                Directory.CreateDirectory(@"c:\TelefonRehberDB\");
                Kullanici demo = new Kullanici();
                demo.ID = Guid.NewGuid();
                demo.KullaniciAdi = "Demo";
                demo.Sifre = "Demo";

                string JsonKullaniciText = Newtonsoft.Json.JsonConvert.SerializeObject(demo);
                File.WriteAllText(@"c:\TelefonRehberDB\kullanici.json", JsonKullaniciText);



            }

            


        }
        public int yenikayit(RehberKayit K)
        {
            int sonuc = 0;
            try
            {
                rehberkayitlarigetir();//class seviyesinde oluşturmuş olduğum koleksiyonum içerisine datamı (varsa)doldurdum
                //yok ise zaten bellekte bir değeri yoktu o şekilde değer eklemek üzere bekliyor.

                kayitlarim.Add(K);//koleksiyonumuza değerimizi ekledik

                JsonDBGuncelle();//var ise üzerine yazdı yoksa yeni json oluşturdu.
                sonuc = 1;
            }
            catch  (Exception ex)
            {
                //log
                sonuc = 0;
            }
            return sonuc;
        }


        public int kayitguncelle(RehberKayit K)
        {
            int sonuc = 0;
            try
            {
                rehberkayitlarigetir();
                int index = kayitlarim.FindIndex(I => I.ID == K.ID);
                if(index > -1)
                {
                    kayitlarim[index].isim = K.isim;
                    kayitlarim[index].soyisim = K.soyisim;
                    kayitlarim[index].telefonI = K.telefonI;
                    kayitlarim[index].telefonII = K.telefonII;
                    kayitlarim[index].telefonIII = K.telefonIII;
                    kayitlarim[index].emailadres = K.emailadres;
                    kayitlarim[index].Website = K.Website;
                    kayitlarim[index].adres = K.adres;
                    kayitlarim[index].aciklama = K.aciklama;
                }
                JsonDBGuncelle();
                sonuc = 1;
            }
            catch(Exception ex)
            {

            }
            return sonuc;
        }


        public int kayitsil(Guid ID)
        {
            int sonuc = 0;
            try
            {
                rehberkayitlarigetir();
                RehberKayit silinecekdeger = kayitlarim.Find(I => I.ID == ID);
                if(silinecekdeger!= null)
                {
                    kayitlarim.Remove(silinecekdeger);
                }
                JsonDBGuncelle();
                sonuc = 1;
            }
            catch(Exception ex)
            {

            }

            return sonuc;
        }



        public List<RehberKayit> rehberkayitlarigetir()
        {
            if (File.Exists(@"c:\TelefonRehberDB\Rehber.json"))
            {
               string JsonDBText = File.ReadAllText(@"c:\TelefonRehberDB\Rehber.json");
                kayitlarim = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RehberKayit>>(JsonDBText);

            }
            return kayitlarim;
        }

        public int kullanicikontrol(Kullanici kullanici)
        {
            int kullanicisonuc = 0;
            if(File.Exists(@"c:\TelefonRehberDB\kullanici.json"))
            {
                string JsonKullaniciText = File.ReadAllText(@"c:\TelefonRehberDB\kullanici.json");
                List<Kullanici> kullanicilar = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Kullanici>>(JsonKullaniciText);
                kullanicisonuc =  kullanicilar.FindAll(I => I.KullaniciAdi == kullanici.KullaniciAdi && I.Sifre == kullanici.Sifre).ToList().Count();



            }
            return kullanicisonuc;
        }

        #region yardimci metotlar

        private void JsonDBGuncelle()
        {
            if(kayitlarim != null && kayitlarim.Count > 0)
            {
                string JsonDB = Newtonsoft.Json.JsonConvert.SerializeObject(kayitlarim);
                File.WriteAllText(@"c:\TelefonRehberDB\Rehber.json", JsonDB);
            }
        }



        #endregion



    }
}
