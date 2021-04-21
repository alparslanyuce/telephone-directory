using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Udemy.Entities;

namespace Udemy.BLL
{
    public class BusinessLogicLayer
    {
        Udemy.Core.DatabaseLogicLayer DLL;

        public BusinessLogicLayer()
        {
            DLL = new Core.DatabaseLogicLayer();
        }

        public int kullanicikontrol(string kullaniciadi, string sifre)
        {
            int sonuc = 0;
            if(!string.IsNullOrEmpty(kullaniciadi) && !string.IsNullOrEmpty(sifre))
            {
                Kullanici kullanici = new Kullanici();
                kullanici.KullaniciAdi = kullaniciadi;
                kullanici.Sifre = sifre;
                sonuc = DLL.kullanicikontrol(kullanici);
            }


            else
            {
                sonuc = -100;//eksik parametre hatası
            }
            return sonuc;
        }

        public int yenikayit(Guid ID, string isim, string soyisim, string telefonI, string telefonII, string telefonIII, string adres, string emailadres, string Website, string aciklama)
        {



            int sonuc = 0;
            if (ID != Guid.Empty && !string.IsNullOrEmpty(isim) && !string.IsNullOrEmpty(soyisim) && !string.IsNullOrEmpty(telefonI))
            {
                RehberKayit kayit = new RehberKayit();
                kayit.ID = ID;
                kayit.isim = isim;
                kayit.soyisim = soyisim;
                kayit.telefonI = telefonI;
                kayit.telefonII = telefonII;
                kayit.telefonIII = telefonIII;
                kayit.adres = adres;
                kayit.emailadres = emailadres;
                kayit.Website = Website;
                kayit.aciklama = aciklama;

                sonuc = DLL.yenikayit(kayit);

            }

            else
            {

                sonuc = -100; //eksik parametre
            }
            return sonuc;


        }


        public int kayitguncelle(Guid ID, string isim, string soyisim, string telefonI, string telefonII, string telefonIII, string adres, string emailadres, string Website, string aciklama)
        {
            int sonuc = 0;
            if(ID != Guid.Empty && !string.IsNullOrEmpty(isim) && !string.IsNullOrEmpty(soyisim) && !string.IsNullOrEmpty(telefonI))
            {
                RehberKayit kayit = new RehberKayit();
                kayit.ID = ID;
                kayit.isim = isim;
                kayit.soyisim = soyisim;
                kayit.telefonI = telefonI;
                kayit.telefonII = telefonII;
                kayit.telefonIII = telefonIII;
                kayit.adres = adres;
                kayit.emailadres = emailadres;
                kayit.Website = Website;
                kayit.aciklama = aciklama;

                sonuc = DLL.kayitguncelle(kayit);
            }

            else
            {
                sonuc = -100;
            }
            return sonuc;
        }

        public int kayıtsil(Guid ID)
        {
            return DLL.kayitsil(ID);
        }




        public List<RehberKayit> RehberKayitGetir()
        {
            return DLL.rehberkayitlarigetir();
        }

        public int XMLDataVer()
        {
            int sonuc = 0;

            try
            {
              List<RehberKayit> kayitlarim =  DLL.rehberkayitlarigetir();
                XDocument Doc = new XDocument(new XDeclaration("1.0.0.1", "UTF-8", "yes"),
                    new XElement("RehberKayitlar", kayitlarim.Select
                    (I => new XElement("kayit",
                    new XElement("ID",I.ID),
                    new XElement("Isim",I.isim),
                    new XElement("soyisim",I.soyisim),
                    new XElement("telefonI",I.telefonI),
                    new XElement("telefonII", I.telefonII),
                    new XElement("telefonIII", I.telefonIII),
                    new XElement("Emailadres",I.emailadres),
                    new XElement("adres",I.adres),
                    new XElement("Website",I.Website),
                    new XElement("Açıklama",I.aciklama)

                    )
                    )
                    )
                    );
                Doc.Save(@"c:\TelefonRehberDB\DataVerXML.xml");
                sonuc = 1;
            }

            catch(Exception ex)
            {
                sonuc = 0;
            }




            return sonuc;
        }
        
        public int CSVDataver()
        {
            int sonuc = 0;
            try
            {
                List<RehberKayit> kayitlar = DLL.rehberkayitlarigetir();
                StreamWriter sw = new StreamWriter(@"c:\TelefonRehberDB\DataVerCSV.csv");
                CsvHelper.CsvWriter write = new CsvHelper.CsvWriter(sw);
                write.WriteHeader(typeof(RehberKayit));

                foreach(var item in kayitlar)
                {
                    write.WriteRecord(item);
                }
                sw.Close();
                sonuc = 1;

            }

            catch(Exception ex)
            {
                sonuc = 0;
            }



            return sonuc;
        }

        public int JsonDataVer()
        {
            int sonuc = 0;
            try
            {
                List<RehberKayit> kayitlar = DLL.rehberkayitlarigetir();
                string jsontext =  Newtonsoft.Json.JsonConvert.SerializeObject(kayitlar);
                File.WriteAllText(@"c:\TelefonRehberDB\DataVerJSON.json",jsontext);
                sonuc = 1;
            }
            catch(Exception ex)
            {

            }

            return sonuc;
        }




    }
}
