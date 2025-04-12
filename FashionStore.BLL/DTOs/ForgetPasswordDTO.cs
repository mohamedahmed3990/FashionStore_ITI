using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.BLL.DTOs
{
    public class ForgetPasswordDTO
    {
        [EmailAddress(ErrorMessage = "must be in Email Format")]
        [Required(ErrorMessage = "Email cant be empty")]
        public string Email { get; set; }
    }
}
