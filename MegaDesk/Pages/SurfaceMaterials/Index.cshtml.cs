using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDesk.Data;
using MegaDesk.Models;

namespace MegaDesk.Pages.SurfaceMaterials
{
    public class IndexModel : PageModel
    {
        private readonly MegaDesk.Data.MegaDeskContext _context;

        public IndexModel(MegaDesk.Data.MegaDeskContext context)
        {
            _context = context;
        }

        public IList<SurfaceMaterial> SurfaceMaterial { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.SurfaceMaterial != null)
            {
                SurfaceMaterial = await _context.SurfaceMaterial.ToListAsync();
            }
        }
    }
}
