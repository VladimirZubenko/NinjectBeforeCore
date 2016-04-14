using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace MVCWebAPI.Tests.Controllers
{
    public class ControllersTestContext: IntegrationTestsContext
    {
        protected void GivenControllersTestContext()
        {
            this.GivenIntegrationTestsContext();
        }

        protected void ShouldReturnAValuableValue(string iAmAValue)
        {
            Response.Content.ReadAsAsync<string>().Result.Should().Contain(iAmAValue);
        }
    }
}
