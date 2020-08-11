using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiteHouse.Models
{
    public class SessionUser
    {
        public Utilisateur User { get; set; }
        public Hebergement Hebergement { get; set; }
    }
}