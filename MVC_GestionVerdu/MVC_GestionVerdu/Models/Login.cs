using System.ComponentModel.DataAnnotations;

namespace MVC_GestionVerdu.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

       
        public string? NickName { get; set; }



    }
}
