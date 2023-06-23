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
    public class IndexModel : PageModel
    {
        private readonly MegaDesk.Data.MegaDeskContext _context;

        public IndexModel(MegaDesk.Data.MegaDeskContext context)
        {
            _context = context;
        }

        public IList<UploadedFile> UploadedFile { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UploadedFile != null)
            {
                UploadedFile = await _context.UploadedFile.ToListAsync();
            }
        }
    }
}
