//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GiteHouse
{
    using System;
    using System.Collections.Generic;
    
    public partial class VilleCure
    {
        public int IdVilleCure { get; set; }
        public string Nom { get; set; }
        public int IdRegion { get; set; }
        public string SiteWeb { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string SiteWebTherme { get; set; }
        public string TelephoneTherme { get; set; }
        public string EmailTherme { get; set; }
        public string AdresseTherme { get; set; }
    
        public virtual Region Region { get; set; }
    }
}