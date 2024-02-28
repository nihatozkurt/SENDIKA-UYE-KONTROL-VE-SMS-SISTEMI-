using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using saglik_sen.Controllers;

namespace saglik_sen.dll
{
    public class bildirim
    {
        public static int onlinesayisi()
        {
            return (new UyelerController ().dogumsayidondur());
            
        }
        public static int bugungonderilensms()
        {
            return (new UyelerController().bugungonderilen());
        }
        public static int buhaftagonderilensms()
        {
            return (new UyelerController().buhaftagonderilen());
        }

    }
}