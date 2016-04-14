using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BusinessValueProviders;

namespace MVCWebAPI.Controllers
{
    [RoutePrefix("api/businessvalue")]
    public class WebApiController: ApiController
    {
        private readonly IBusinessValueProvider _businessValueProvider;


        public WebApiController(IBusinessValueProvider businessValueProvider)
        {
            _businessValueProvider = businessValueProvider;
        }

        [HttpGet]
        public string Get()
        {
            return _businessValueProvider.ProvideValue();
        }
    }
}