using System.Web.Mvc;
using UsingViewModelsWithAspNetMvc.Repositories;
using UsingViewModelsWithAspNetMvc.ViewModels;

namespace UsingViewModelsWithAspNetMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly CompanyRepository companyRepository;

        public HomeController()
        {
            companyRepository = new CompanyRepository();
        }

        public ActionResult Index()
        {
            var company = companyRepository.Get();

            var viewModel = new CompanyViewModel(company);

            return View(viewModel);
        }
    }
}