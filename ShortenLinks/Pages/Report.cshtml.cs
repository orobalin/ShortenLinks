using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ShortenLinks.Pages
{
    public class ReportModel : PageModel
    {
        private readonly AppDbContext _db;

        public ReportModel(AppDbContext db) { _db = db; }

        public IList<ShortenLink> ShortenLinks { get; private set; }
        public IList<ShortenLinkLog> ShortenLinkLogs { get; private set; }

        public async Task OnGetAsync()
        {
            try
            {
                ShortenLinks = await _db.ShortenLinks.AsNoTracking().ToListAsync();
                ShortenLinkLogs = await _db.ShortenLinkLogs.AsNoTracking().ToListAsync();
            }
            catch 
            {

            }
        }
    }
}