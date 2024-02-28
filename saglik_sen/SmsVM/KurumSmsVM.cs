using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using saglik_sen.Models;

namespace saglik_sen.SmsVM
{
    public class KurumSmsVM
    {
       
        public List<UYELER> UYELERs { get; set; }
        [Required]
        public string mesaj { get; set; }
        [Required]
        public int KURUM_ID { get; set; }

    }
}