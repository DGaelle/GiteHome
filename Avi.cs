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
    
    public partial class Avi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Avi()
        {
            this.Hebergements = new HashSet<Hebergement>();
        }
    
        public int IdAvi { get; set; }
        public int IdUtilisateur { get; set; }
        public int IdHebergement { get; set; }
        public int Note { get; set; }
        public string Commentaire { get; set; }
        public System.DateTime Date { get; set; }
        public string Reponse { get; set; }
    
        public virtual Utilisateur Utilisateur { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hebergement> Hebergements { get; set; }
    }
}
