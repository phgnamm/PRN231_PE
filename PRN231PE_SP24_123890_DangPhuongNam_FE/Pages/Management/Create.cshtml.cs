using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN231PE_SP24_123890_DangPhuongNam_FE.Models;

namespace PRN231PE_SP24_123890_DangPhuongNam_FE.Pages.Management
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public WatercolorsPainting WatercolorsPainting { get; set; } = default!;

        [BindProperty]
        public List<Style> Style { get; set; } = default!;

        private async Task GetClubs(string token)
        {
            var response = await Common.SendRequestAsync($"{Common.BaseURL}/odata/Style", HttpMethod.Get, null, token);
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = await Common.ReadError(response);
                return;
            }
            Style = await Common.ReadT<List<Style>>(response);
        }

        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("token");
            if (!Common.CheckPermission(token, [3]))
            {
                TempData["error"] = Common.NoPermissionMessage;
                return RedirectToPage("./Index");
            }
            await GetClubs(token);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("token");
            if (!Common.CheckPermission(token, [3]))
            {
                return RedirectToPage("./Index");
            }
            if (!ModelState.IsValid)
            {
                await GetClubs(token);
                return Page();
            }
            var response = await Common.SendRequestAsync($"{Common.BaseURL}/api/WatercolorsPainting", HttpMethod.Post, WatercolorsPainting, token);
            if (!response.IsSuccessStatusCode)
            {
                TempData["errror"] = await Common.ReadError(response);
                await GetClubs(token);
                return Page();
            }
            TempData["info"] = "Added successfully";
            return RedirectToPage("./Index");
        }
    }
}
