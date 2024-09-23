using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Text.Json;

namespace ProductApp.Pages
{
    public class IndexModel : PageModel
    {
        private const string SessionKey = "Products";

        public List<string> Products { get; set; } = new List<string>();

        [BindProperty]
        public string NewProduct { get; set; }

        public void OnGet()
        {
            var productsData = HttpContext.Session.GetString(SessionKey);
            if (productsData != null)
            {
                Products = JsonSerializer.Deserialize<List<string>>(productsData);
            }
        }

        public IActionResult OnPost()
        {
            var productsData = HttpContext.Session.GetString(SessionKey);
            if (productsData != null)
            {
                Products = JsonSerializer.Deserialize<List<string>>(productsData);
            }

            if (!string.IsNullOrWhiteSpace(NewProduct))
            {
                Products.Add(NewProduct);
            }

            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(Products));

            return RedirectToPage();
        }
    }
}
