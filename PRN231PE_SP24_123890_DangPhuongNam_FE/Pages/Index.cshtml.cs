using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace PRN231PE_SP24_123890_DangPhuongNam_FE.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await Common.SendRequestAsync($"{Common.BaseURL}/odata/Auth", HttpMethod.Post, new { password = Password, email = EmailAddress });

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                TempData["error"] = await Common.ReadError(response);
                return Page();
            }

            var token = await Common.ReadToken(response);

            Common.DecodeJwtToken(token, out string id, out string role);

            HttpContext.Session.SetString("token", token);

            if (Common.CheckPermission(token, [3, 2]))
            {
                return RedirectToPage("./Management/Index");
            }
            TempData["error"] = Common.NoPermissionMessage;
            return Page();
        }
    }
}
