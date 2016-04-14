using System.Net.Http;
using System.Web.Mvc;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCWebAPI;
using MVCWebAPI.Controllers;
using NUnit.Framework;
using TestStack.BDDfy;

namespace MVCWebAPI.Tests.Controllers
{
    public class When_Retrieving_Value : ControllersTestContext
    {
        [Test]
        public void Run()
        {
            this.Given(x => x.GivenControllersTestContext())
                .And(x => x.GivenRequest(HttpMethod.Get, "api/businessvalue/get"))
                .When(x => x.WhenPerformRequest())
                .Then(x => x.ThenShouldReturnSuccessStatusCode())
                .And(x => x.ShouldReturnAValuableValue("I am a value"))
                .BDDfy();
        }
    }
}
