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
    public class DetailsModel : PageModel
    {
        private readonly MegaDesk.Data.MegaDeskContext _context;

        public DetailsModel(MegaDesk.Data.MegaDeskContext context)
        {
            _context = context;
        }

      public SurfaceMaterial SurfaceMaterial { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SurfaceMaterial == null)
            {
                return NotFound();
            }

            var surfacematerial = await _context.SurfaceMaterial.FirstOrDefaultAsync(m => m.ID == id);
            if (surfacematerial == null)
            {
                return NotFound();
            }
            else 
            {
                SurfaceMaterial = surfacematerial;
            }
            return Page();
        }
    }
}
