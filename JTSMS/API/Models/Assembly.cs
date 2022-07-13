using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class Assembly
    {
        public int Id { get; set; }
        public int RegId { get; set; }
        public string AssemblyNumber { get; set; }
        public string AssemblyRevision { get; set; }
    }
}
