using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MegaDesk.Models;

namespace MegaDesk.Data
{
    public class MegaDeskContext : IdentityDbContext
    {
        public MegaDeskContext(DbContextOptions<MegaDeskContext> options)
            : base(options)
        {
        }

        public DbSet<MegaDesk.Models.Desk_Quote> Desk_Quote { get; set; } = default!;

        public DbSet<MegaDesk.Models.SurfaceMaterial> SurfaceMaterial { get; set; }

        public DbSet<MegaDesk.Models.RushOrder> RushOrder { get; set; }

        public DbSet<MegaDesk.Models.UploadedFile> UploadedFile { get; set; }

    }
}
