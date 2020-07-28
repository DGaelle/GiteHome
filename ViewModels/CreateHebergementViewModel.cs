using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GiteHouse.ViewModels
{
    public class CreateHebergementViewModel
    {
        [Required]
        [Display(Name = "Region")]
        public string SelectedRegionCode { get; set; }
        public IEnumerable<SelectListItem> Regions { get; set; }
    }
}