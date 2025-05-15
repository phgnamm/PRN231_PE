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
    public class EditModel : PageModel
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

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var token = HttpContext.Session.GetString("token");
            if (!Common.CheckPermission(token, [3]))
            {
                TempData["error"] = Common.NoPermissionMessage;
                return RedirectToPage("./Index");
            }
            await GetClubs(token);
            var url = $"{Common.BaseURL}/odata/WatercolorsPainting?$expand=Style&$filter=PaintingId eq '{id}'";
            var response = await Common.SendRequestAsync(url, HttpMethod.Get, null, token);
            if (!response.IsSuccessStatusCode)
            {
                TempData["errror"] = await Common.ReadError(response);
                return Page();
            }
            var WatercolorsPaintingList = await Common.ReadT<List<WatercolorsPainting>>(response, true);
            WatercolorsPainting = WatercolorsPaintingList.FirstOrDefault();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
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

            var response = await Common.SendRequestAsync($"{Common.BaseURL}/api/WatercolorsPainting/{WatercolorsPainting.PaintingId}", HttpMethod.Put, WatercolorsPainting, token);
            if (!response.IsSuccessStatusCode)
            {
                TempData["errror"] = await Common.ReadError(response);
                return Page();
            }
            TempData["info"] = "Updated successfully";
            return RedirectToPage("./Index");
        }
    }
}
