using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.BLL.DTOs
{
    public class ChangePasswordDTO
    {
        
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
