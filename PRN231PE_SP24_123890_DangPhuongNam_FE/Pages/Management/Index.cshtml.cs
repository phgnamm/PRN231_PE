using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN231PE_SP24_123890_DangPhuongNam_FE.Models;

namespace PRN231PE_SP24_123890_DangPhuongNam_FE.Pages.Management
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<WatercolorsPainting> WatercolorsPainting { get; set; } = default!;

        [BindProperty]
        public int? PublishYear { get; set; }

        [BindProperty]
        public string? PaintingAuthor { get; set; } = "";

        string url = $"{Common.BaseURL}/odata/WatercolorsPainting?$expand=Style";

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var token = HttpContext.Session.GetString("token");
                if (!Common.CheckPermission(token, [3, 2]))
                {
                    TempData["error"] = Common.NoPermissionMessage;
                    return RedirectToPage("../index");
                }
                var response = await Common.SendRequestAsync(url, HttpMethod.Get, null, token);
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["error"] = "Something was wrong.";
                }
                WatercolorsPainting = await Common.ReadT<IList<WatercolorsPainting>>(response, true);
                return Page();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var token = HttpContext.Session.GetString("token");
                if (!Common.CheckPermission(token, [3, 2]))
                {
                    TempData["error"] = Common.NoPermissionMessage;
                    return RedirectToPage("../index");
                }
                var filters = new List<string>();

                if (!string.IsNullOrEmpty(PaintingAuthor))
                {
                    filters.Add($"contains(tolower(PaintingAuthor),'{PaintingAuthor.ToLower()}')");
                }

                if (PublishYear > 0)
                {
                    filters.Add($"PublishYear eq {PublishYear}");
                }

                // Add filters to the URL if any
                if (filters.Any())
                {
                    url += $"&$filter={string.Join(" and ", filters)}";
                }
                var response = await Common.SendRequestAsync(url, HttpMethod.Get, null, token);
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["error"] = "Something was wrong.";
                    return Page();
                }
                WatercolorsPainting = await Common.ReadT<IList<WatercolorsPainting>>(response, true);
                return Page();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return Page();
            }
        }
    }
}
