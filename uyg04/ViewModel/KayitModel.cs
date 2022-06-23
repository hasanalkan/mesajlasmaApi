using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uyg04.ViewModel
{
    public class KayitModel
    {
        public string kayitId { get; set; }
        public string kayituserId { get; set; }
        public string kayitMesajId{ get; set; }
        public UserModel userBilgi { get; set; }
        public MesajModel mesajBilgi { get; set; }
    }
}