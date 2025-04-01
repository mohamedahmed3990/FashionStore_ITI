using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.BLL.Common
{
    public class GeneralApiResult
    {
        public bool Success { get; set; }
        public ApiErrorResult[] Errors { get; set; } = [];
    }

    public class ApiErrorResult
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

}
