using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDesk.Data;
using MegaDesk.Models;

namespace MegaDesk.Pages.UploadedFiles
{
    public class DetailsModel : PageModel
    {
        private readonly MegaDesk.Data.MegaDeskContext _context;

        public DetailsModel(MegaDesk.Data.MegaDeskContext context)
        {
            _context = context;
        }

      public UploadedFile UploadedFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UploadedFile == null)
            {
                return NotFound();
            }

            var uploadedfile = await _context.UploadedFile.FirstOrDefaultAsync(m => m.ID == id);
            if (uploadedfile == null)
            {
                return NotFound();
            }
            else 
            {
                UploadedFile = uploadedfile;
            }
            return Page();
        }
    }
}
