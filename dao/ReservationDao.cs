using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiteHouse.dao
{
    public class ReservationDao
    {
        private GiteHouseEntities db = new GiteHouseEntities();

        public List<Reservation> GetReservations(int idUtilisateur)
        {
            List<Reservation> reservations = db.Reservations.Where(r => r.IdUtilisateur == idUtilisateur).ToList();
            return reservations;
        }
    }
}