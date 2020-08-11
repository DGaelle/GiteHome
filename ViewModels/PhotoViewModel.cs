using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiteHouse.ViewModels
{
    public class PhotoViewModel : Photo
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}