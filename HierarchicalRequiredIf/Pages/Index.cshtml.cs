using HierarchicalRequiredIf.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HierarchicalRequiredIf.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> logger;
        private readonly ApiContext apiInMemoryDbContext;


        public string MessageKey= "HierarchicalRequiredIf.Pages.Message";



        public IndexModel(ILogger<IndexModel> logger, ApiContext apiInMemoryDbContext) {

            this.logger = logger;
            this.apiInMemoryDbContext = apiInMemoryDbContext;

        }

        public void OnGet() {


            this.Container = apiInMemoryDbContext.Container;

        }

                
        public IActionResult OnPost() {


            if (!ModelState.IsValid) {

                string detailsMsg= string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                this.AddNotification($"Form not valid: {detailsMsg}");
                return Page();
            }


            this.apiInMemoryDbContext.Container = this.Container;
            this.apiInMemoryDbContext.SaveChanges();

            return Page();

        }


        private void AddNotification(string message) {
            this.TempData[MessageKey] = message;
        }

        [BindProperty]
        public ContainerViewModel Container { get; set; }
    }
}