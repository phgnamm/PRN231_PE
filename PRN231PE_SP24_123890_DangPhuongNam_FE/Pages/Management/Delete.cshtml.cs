using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN231PE_SP24_123890_DangPhuongNam_FE.Models;

namespace PRN231PE_SP24_123890_DangPhuongNam_FE.Pages.Management
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public WatercolorsPainting WatercolorsPainting { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var token = HttpContext.Session.GetString("token");
            if (!Common.CheckPermission(token, [3]))
            {
                TempData["error"] = Common.NoPermissionMessage;
                return RedirectToPage("./Index");
            }
            var url = $"{Common.BaseURL}/odata/WatercolorsPainting?$expand=Style&$filter=PaintingId eq '{id}'";
            var response = await Common.SendRequestAsync(url, HttpMethod.Get, null, token);
            if (!response.IsSuccessStatusCode)
            {
                TempData["errror"] = await Common.ReadError(response);
                return Page();
            }
            var waterColorList = await Common.ReadT<List<WatercolorsPainting>>(response, true);
            WatercolorsPainting = waterColorList.FirstOrDefault();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("token");
            if (!Common.CheckPermission(token, [3]))
            {
                return RedirectToPage("./Index");
            }
            var response = await Common.SendRequestAsync($"{Common.BaseURL}/api/WatercolorsPainting/{WatercolorsPainting.PaintingId}", HttpMethod.Delete, null, token);
            if (!response.IsSuccessStatusCode)
            {
                TempData["errror"] = await Common.ReadError(response);
                return Page();
            }
            TempData["info"] = "Deleted successfully";
            return RedirectToPage("./Index");
        }
    }
}
