using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessValueProviders;
using MVCWebAPI.Models;

namespace MVCWebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusinessValueProvider _businessValueProvider;

        public HomeController(IBusinessValueProvider businessValueProvider)
        {
            _businessValueProvider = businessValueProvider;
        }

        public ActionResult Index()
        {
            var viewModel = new HomeViewModel {BusinessValue = _businessValueProvider.ProvideValue()};

            return View(viewModel);
        }
    }
}
