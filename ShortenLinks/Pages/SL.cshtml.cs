using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace ShortenLinks.Pages
{
    [Route("SL/{id}")]
    public class SLModel : PageModel
    {
        private readonly AppDbContext _db;
        private StringValues originValues;

        public SLModel(AppDbContext db) { _db = db; }
               
        [BindProperty]
        public ShortenLink ShortenLink { get; set; }

        [BindProperty]
        public ShortenLinkLog ShortenLinkLog { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                ShortenLink = await _db.ShortenLinks.FindAsync(id);
                if (ShortenLink != null)
                {
                    Request.Headers.TryGetValue("Referer", out originValues);

                    ShortenLinkLog = new ShortenLinkLog();
                    ShortenLinkLog.ShortLinkId = ShortenLink.Id;
                    ShortenLinkLog.LongLink = ShortenLink.LongLink;
                    ShortenLinkLog.ShortLink = ShortenLink.ShortLink;
                    ShortenLinkLog.Origen = originValues.ToString();
                    ShortenLinkLog.DateAccess = DateTime.Now;

                    _db.ShortenLinkLogs.Add(ShortenLinkLog);

                    ShortenLink.AccessCount++;
                    _db.Attach(ShortenLink).State = EntityState.Modified;

                    await _db.SaveChangesAsync();
                    return Redirect(ShortenLink.LongLink);
                }
                else
                {
                    return Page();
                }
            }
            catch
            {
                return Page();
            }
            
        }
        
    }
}