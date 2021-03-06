﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiteHouse.dao
{
    public class HebergementDao
    {
        private GiteHouseEntities db = new GiteHouseEntities();

        // GET: Hebergements
        public List<Hebergement> GetHebergements()
        {
            var hebergements = db.Hebergements.ToList();
            return hebergements.ToList();
        }
    }
}