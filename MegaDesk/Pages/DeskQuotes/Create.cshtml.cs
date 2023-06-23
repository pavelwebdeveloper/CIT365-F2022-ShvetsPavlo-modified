using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MegaDesk.Data;
using MegaDesk.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MegaDesk.Pages.DeskQuotes
{
    public class CreateModel : PageModel
    {
        private readonly MegaDesk.Data.MegaDeskContext _context;

        public CreateModel(MegaDesk.Data.MegaDeskContext context)
        {
            _context = context;
        }

        //public SelectList? DesktopMaterials { get; set; }
        public List<SelectListItem> DesktopMaterials { get; set; }
        public SelectList? RushOrders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Use LINQ to get list of surface material.
            /*IQueryable<string> surfaceMaterials = from s in _context.SurfaceMaterial
                                            select s.SurfaceMaterialName;

            DesktopMaterials = new SelectList(await surfaceMaterials.Distinct().ToListAsync());*/

            DesktopMaterials = _context.SurfaceMaterial.Select(a =>
                            new SelectListItem
                            {
                                Value = a.SurfaceMaterialName.ToString(),
                                Text = a.SurfaceMaterialName.ToString()
                            }).ToList();

            /*List<SelectListItem> surfaceMaterials = _context.SurfaceMaterial.AsNoTracking()
                .OrderBy(n => n.SurfaceMaterialName)
                .Select(n => new SelectListItem
                { 
                    Value = n.SurfaceMaterialName.ToString(),
                    Text = n.SurfaceMaterialName
                }).ToList();
            var surfaceMaterialTip = new SelectListItem()
            {
                Value = null,
                Text = "Select desktop material"
            };
            surfaceMaterials.Insert(0, surfaceMaterialTip);
            DesktopMaterial = new SelectList(surfaceMaterials, "Value", "Text");
            */

            // Use LINQ to get list of rush orders.
            IQueryable<int> rushOrders = from r in _context.RushOrder
                                                  select r.RushOrderOption;

            RushOrders = new SelectList(await rushOrders.Distinct().ToListAsync());

            return Page();
        }

        [BindProperty]
        public Desk_Quote DeskQuote { get; set; }
        private decimal RushOrderPrice { get; set; }
        private decimal[,] rushOrderPrices;
        private decimal PricePerInch;
        private decimal DrawersTotalPrice;
        public static decimal pricePerDrawer = 50;
        private IList<SurfaceMaterial> DesktopMaterial { get; set; } = default!;
        /*public string SelectedMaterial { get; set; }
        public IEnumerable<SelectListItem> DesktopMaterial { get; set; }*/
        private decimal DesktopSurfaceMaterialPrice;
        public static decimal basicDeskPrice = 200;
        //public string JSONVALUe { get; set; }





        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

          DeskQuote.QuoteDate = DateTime.Now;
            GetRushOrder();
            PricePerInchCalculation();
            RushOrderPriceCalculation();
            DrawersTotalPriceCalculation();
            await DesktopSurfaceMaterialPriceCalculation();
            DeskQuotePriceCalculation();

            _context.Desk_Quote.Add(DeskQuote);
            await _context.SaveChangesAsync();

            //return RedirectToPage("./Index");
            return RedirectToPage("./Index");
        }


        public void RushOrderPriceCalculation()
        {
            switch (DeskQuote.ProductionNumberOfDays)
            {
                case 3:
                    if (DeskQuote.DeskDepth * DeskQuote.DeskWidth < 1000)
                    {
                        RushOrderPrice = rushOrderPrices[0, 0];
                    }
                    else if (DeskQuote.DeskDepth * DeskQuote.DeskWidth >= 1000 && DeskQuote.DeskDepth * DeskQuote.DeskWidth <= 2000)
                    {
                        RushOrderPrice = rushOrderPrices[0, 1];
                    }
                    else if (DeskQuote.DeskDepth * DeskQuote.DeskWidth > 2000)
                    {
                        RushOrderPrice = rushOrderPrices[0, 2];
                    }
                    break;
                case 5:
                    if (DeskQuote.DeskDepth * DeskQuote.DeskWidth < 1000)
                    {
                        RushOrderPrice = rushOrderPrices[1, 0];
                    }
                    else if (DeskQuote.DeskDepth * DeskQuote.DeskWidth >= 1000 && DeskQuote.DeskDepth * DeskQuote.DeskWidth <= 2000)
                    {
                        RushOrderPrice = rushOrderPrices[1, 1];
                    }
                    else if (DeskQuote.DeskDepth * DeskQuote.DeskWidth > 2000)
                    {
                        RushOrderPrice = rushOrderPrices[1, 2];
                    }
                    break;
                case 7:
                    if (DeskQuote.DeskDepth * DeskQuote.DeskWidth < 1000)
                    {

                        RushOrderPrice = rushOrderPrices[2, 0];
                    }
                    else if (DeskQuote.DeskDepth * DeskQuote.DeskWidth >= 1000 && DeskQuote.DeskDepth * DeskQuote.DeskWidth <= 2000)
                    {

                        RushOrderPrice = rushOrderPrices[2, 1];
                    }
                    else if (DeskQuote.DeskDepth * DeskQuote.DeskWidth > 2000)
                    {

                        RushOrderPrice = rushOrderPrices[2, 2];
                    }
                    break;
                    default:
                        RushOrderPrice = 0;
                    break;

            }
        }

        public void PricePerInchCalculation()
        {
            if (DeskQuote.DeskDepth * DeskQuote.DeskWidth > 1000)
            {
                PricePerInch = 1;
            }
            else
            {
                PricePerInch = 0;
            }
        }

        public void DrawersTotalPriceCalculation()
        {
            DrawersTotalPrice = DeskQuote.NumberOfDrawers * pricePerDrawer;
        }

        public async Task DesktopSurfaceMaterialPriceCalculation()
        {
            var desktopMaterials = from i in _context.SurfaceMaterial
                                       select i;

            DesktopMaterial = await desktopMaterials.ToListAsync();

            for (int i = 0; i < DesktopMaterial.Count; i++) {
                if (DesktopMaterial[i].SurfaceMaterialName == DeskQuote.SurfaceMaterial) {
                    DesktopSurfaceMaterialPrice = DesktopMaterial[i].Price;
                }
            }
        }

        public void DeskQuotePriceCalculation()
        {

            DeskQuote.Price = basicDeskPrice + DeskQuote.DeskWidth * DeskQuote.DeskDepth * PricePerInch
                + DrawersTotalPrice + DesktopSurfaceMaterialPrice + RushOrderPrice;
        }


        public void GetRushOrder()
        {
            
            rushOrderPrices = new decimal[3, 3];        

            string[] fileContents = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\rushOrderPrices.json");

            var k = 0;
            for (int i = 0; i < rushOrderPrices.GetLength(0); i++)
            {
                for (int j = 0; j < rushOrderPrices.GetLength(1); j++)
                {
                    rushOrderPrices[i, j] = decimal.Parse(fileContents[k]);
                    k++;
                }
            }

        }
    }
}
