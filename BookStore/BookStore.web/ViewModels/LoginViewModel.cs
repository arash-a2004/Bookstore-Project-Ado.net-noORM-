using System.ComponentModel.DataAnnotations;

namespace BookStore.web.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name ="User Name")]
        public required string userName { get; set; }
        [DataType (DataType.Password)]
        public required string password { get; set; }
    }
}
