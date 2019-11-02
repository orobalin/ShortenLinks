using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShortenLinks.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db) { _db = db; }

        [TempData]
        public string Message { get; set; }

        [TempData]
        public string Sid { get; set; }

        [TempData]
        public string Slink { get; set; }

        [BindProperty]
        public ShortenLink ShortenLink { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            try
            {
                String sLink = Guid.NewGuid().ToString("N").Substring(0, 7).ToLower();

                if (!string.IsNullOrEmpty(sLink))
                {
                    var slk = _db.ShortenLinks.SingleOrDefault(a => a.Id == sLink);

                    if (slk == null)
                    {
                        string lLink = ShortenLink.LongLink;
                        if ((lLink.StartsWith("http://") == false) || (lLink.StartsWith("https://") == false))
                        {
                            lLink = "http://" + lLink;
                        }
                        var llk = _db.ShortenLinks.SingleOrDefault(a => a.LongLink == lLink);
                        if (llk == null)
                        {
                            ShortenLink.Id = sLink;
                            ShortenLink.LongLink = lLink;
                            ShortenLink.ShortLink = "http://" + HttpContext.Request.Host.Value.ToString() + "/SL/" + sLink;
                            ShortenLink.DateCreated = DateTime.Now;
                            ShortenLink.AccessCount = 0;
                            _db.ShortenLinks.Add(ShortenLink);
                            await _db.SaveChangesAsync();
                            Message = $"Enlace reducido para: {ShortenLink.LongLink}  ha sido generado.";
                            Sid = ShortenLink.Id;
                            Slink = ShortenLink.ShortLink;
                            return RedirectToPage("/Index");
                        }
                        else
                        {
                            Message = $"Enlace reducido para: {llk.LongLink} ha sido generado previamente.";
                            Sid = llk.Id;
                            Slink = llk.ShortLink;
                            return Page();
                        }
                    }
                    else
                    {
                        Message = $"Enlace reducido para: {slk.LongLink} ha sido generado previamente.";
                        Sid = slk.Id;
                        Slink = slk.ShortLink;
                        return Page();
                    }
                }
                else
                {
                    return Page();
                }
            }
            catch (Exception e)
            {
                Message = $"Se produjo un error. " + e.Message;
                return Page();
            }
            
        }
        
    }
}
