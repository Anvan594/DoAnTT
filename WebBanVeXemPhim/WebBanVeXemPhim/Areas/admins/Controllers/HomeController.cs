using Microsoft.AspNetCore.Mvc;
using WebBanVeXemPhim.Areas.admins.Controllers;


namespace Web_Api.Areas.admins.Controllers
{
    public class HomeController : BaseController
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
