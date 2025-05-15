using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN231PE_SP24_123890_DangPhuongNam_FE.Models;

namespace PRN231PE_SP24_123890_DangPhuongNam_FE.Pages.Management
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public WatercolorsPainting WatercolorsPainting { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(string id)
        {
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
            var footballPlayerList = await Common.ReadT<List<WatercolorsPainting>>(response,true);
            WatercolorsPainting = footballPlayerList.FirstOrDefault();
            return Page();
        }
    }
}
