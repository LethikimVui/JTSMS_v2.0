using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class MasterStatus
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusColour { get; set; }
        public int? IsActive { get; set; }
    }
}
