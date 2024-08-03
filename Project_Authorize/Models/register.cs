using System.ComponentModel.DataAnnotations;

namespace Project_Authorize.Models
{
    public class register
    {
        public int Id { get; set; }

        [Display(Name ="User Name")]
        [Required(ErrorMessage = "User name must be required")]
        [MaxLength(15)]
        public string u_name { get; set; }
        public string email { get; set; }
        public string pswd { get; set; }
        public string con_pswd { get; set; }
        public Int64 contact { get; set; }
        public string img_path { get; set; }    
        public IFormFile profile_img { get; set; }

    }
}
