using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using uyg04.Models;
using uyg04.ViewModel;
namespace uyg04.Controllers
{
    public class ServisController : ApiController
    {
        DB03Entities db = new DB03Entities();
        SonucModel sonuc = new SonucModel();

        #region User
        [HttpGet]
        [Route("api/userliste")]
        public List<UserModel> UserListe()
        {

            List<UserModel> liste = db.User.Select(x => new UserModel()
            {
                userId = x.userId,
                userNo = x.userNo,
                userAdSoyad = x.userAdSoyad,
                userDogTarih = x.userDogTarih,
                userFoto = x.userFoto,
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/userbyid/{userId}")]
        public UserModel UserById(string userId)
        {

            UserModel kayit = db.User.Where(s => s.userId == userId).Select(x => new UserModel()
            {
                userId = x.userId,
                userNo = x.userNo,
                userAdSoyad = x.userAdSoyad,
                userDogTarih = x.userDogTarih,
                userFoto = x.userFoto,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/userekle")]
        public SonucModel UserEkle(UserModel model)
        {
            if (db.User.Count(c => c.userNo == model.userNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen User Numarası Kayıtlıdır!";
                return sonuc;
            }

            User yeni = new User();
            yeni.userId = Guid.NewGuid().ToString();
            yeni.userNo = model.userNo;
            yeni.userAdSoyad = model.userAdSoyad;
            yeni.userDogTarih = model.userDogTarih;
            yeni.userFoto = model.userFoto;
            db.User.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "User Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/userduzenle")]
        public SonucModel UserDuzenle(UserModel model)
        {

            User kayit = db.User.Where(s => s.userId == model.userId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }


            kayit.userNo = model.userNo;
            kayit.userAdSoyad = model.userAdSoyad;
            kayit.userDogTarih = model.userDogTarih;
            kayit.userFoto = model.userFoto;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "User Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/usersil/{userId}")]
        public SonucModel UserSil(string userId)
        {

            User kayit = db.User.Where(s => s.userId == userId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            if (db.Kayit.Count(c => c.kayituserId == userId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Numara Kaydı Olan User Silinemez!";
                return sonuc;
            }


            db.User.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "User Silindi";

            return sonuc;
        }
        #endregion

        #region Mesaj

        [HttpGet]
        [Route("api/mesajliste")]
        public List<MesajModel> MesajListe()
        {

            List<MesajModel> liste = db.Mesaj.Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                mesajKodu = x.mesajKodu,
                mesajAdi = x.mesajAdi,
                mesajUserSayisi = x.Kayit.Count()
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/mesajbyid/{mesajId}")]
        public MesajModel MesajById(string mesajId)
        {

            MesajModel kayit = db.Mesaj.Where(s => s.mesajId == mesajId).Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                mesajKodu = x.mesajKodu,
                mesajAdi = x.mesajAdi,
                mesajUserSayisi = x.Kayit.Count()
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/mesajekle")]
        public SonucModel MesajEkle(MesajModel model)
        {
            if (db.Mesaj.Count(c => c.mesajKodu == model.mesajKodu) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Mesaj Kodu Kayıtlıdır!";
                return sonuc;
            }

            Mesaj yeni = new Mesaj();
            yeni.mesajId = Guid.NewGuid().ToString();
            yeni.mesajKodu = model.mesajKodu;
            yeni.mesajAdi = model.mesajAdi;
            db.Mesaj.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/mesajduzenle")]
        public SonucModel MesajDuzenle(MesajModel model)
        {

            Mesaj kayit = db.Mesaj.Where(s => s.mesajId == model.mesajId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Mesaj Bulunamadı!";
                return sonuc;
            }


            kayit.mesajKodu = model.mesajKodu;
            kayit.mesajAdi = model.mesajAdi;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/mesajsil/{mesajId}")]
        public SonucModel MesajSil(string mesajId)
        {

            Mesaj kayit = db.Mesaj.Where(s => s.mesajId == mesajId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            if (db.Kayit.Count(c => c.kayitMesajId == mesajId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "User Kayıdı Silinmeden Mesaj Silemezsiniz!";
                return sonuc;
            }

            db.Mesaj.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Silindi";

            return sonuc;
        }

        #endregion

        #region Kayıt

        [HttpGet]
        [Route("api/mesajuserliste/{mesajId}")]
        public List<KayitModel> MesajUserListe(string mesajId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitMesajıd == mesajId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitMesajId = x.kayitMesajId,
                kayituserId = x.kayituserId
            }
            ).ToList();

            foreach (var kayit in liste)
            {
                kayit.userBilgi = userById(kayit.kayituserId);
                kayit.mesajBilgi = MesajById(kayit.kayitmesajId);
            }

            return liste;
        }
        [HttpGet]
        [Route("api/mesajuserliste/{userId}")]
        public List<KayitModel> MesajUserListe(string userId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayituserId == userId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitMesajId = x.kayitmesajId,
                kayituserId = x.kayituserId
            }
            ).ToList();

            foreach (var kayit in liste)
            {
                kayit.userBilgi = UserbyId(kayit.kayituserId);
                kayit.mesajBilgi = MesajById(kayit.kayitMesajId);
            }

            return liste;
        }
        #endregion
    }
}
