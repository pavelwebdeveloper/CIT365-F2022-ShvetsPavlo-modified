using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDesk.Data;
using MegaDesk.Models;

namespace MegaDesk.Pages.RushOrders
{
    public class DeleteModel : PageModel
    {
        private readonly MegaDesk.Data.MegaDeskContext _context;

        public DeleteModel(MegaDesk.Data.MegaDeskContext context)
        {
            _context = context;
        }

        [BindProperty]
      public RushOrder RushOrder { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RushOrder == null)
            {
                return NotFound();
            }

            var rushorder = await _context.RushOrder.FirstOrDefaultAsync(m => m.ID == id);

            if (rushorder == null)
            {
                return NotFound();
            }
            else 
            {
                RushOrder = rushorder;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.RushOrder == null)
            {
                return NotFound();
            }
            var rushorder = await _context.RushOrder.FindAsync(id);

            if (rushorder != null)
            {
                RushOrder = rushorder;
                _context.RushOrder.Remove(RushOrder);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
