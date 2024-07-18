using System.ComponentModel.DataAnnotations;

namespace Scrap_Project.Models
{
    public class user_data
    {
        public int id { get; set; }

        [Display(Name ="User Name")]
        public string u_name { get; set; }

        [Display(Name ="Email")]
        public string email_add { get; set; }

        [Display(Name ="Birth Date")]
        public DateTime b_date { get; set; }

        [Display(Name ="Contact No.")]
        public Int64 contact { get; set; }
        public string img_path { get; set; }

        [Display(Name ="Profile Photo")]
        public IFormFile imgfile { get; set; }
    }
}
