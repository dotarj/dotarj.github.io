using System.Web.Mvc;
using UsingViewModelsWithAspNetMvc.Repositories;
using UsingViewModelsWithAspNetMvc.ViewModels.Home;

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

            return View(new IndexViewModel(company));
        }
    }
}