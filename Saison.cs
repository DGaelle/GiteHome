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
    
    public partial class Saison
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Saison()
        {
            this.Tarifications = new HashSet<Tarification>();
        }
    
        public int IdSaison { get; set; }
        public int IdUtilisateur { get; set; }
        public System.DateTime DateDebut { get; set; }
        public System.DateTime DateFin { get; set; }
        public string Nom { get; set; }
        public decimal PrixNuit { get; set; }
    
        public virtual Utilisateur Utilisateur { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tarification> Tarifications { get; set; }
    }
}