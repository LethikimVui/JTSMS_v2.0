using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models2
{
    public partial class jtestsmsContext : DbContext
    {
        public jtestsmsContext()
        {
        }

        public jtestsmsContext(DbContextOptions<jtestsmsContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Data Source=vnhcmm0teapp03;Initial Catalog=jtestsms;User Id=hcm_vui_usr;Password=hcm_vui_usr;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
