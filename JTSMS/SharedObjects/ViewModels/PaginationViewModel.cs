using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ViewModels
{
    public class PaginationViewModel
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
