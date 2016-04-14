using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using FluentAssertions;
using Microsoft.Owin.Testing;
using MVCWebAPI.Ninject;
using Ninject;
using NUnit.Framework;

namespace MVCWebAPI.Tests
{ 
    [TestFixture]
    public abstract class IntegrationTestsContext : IDisposable
    {
        protected string stringResponse;
        private const string BaseUri = "http://mvcwebapi.integration.tests/";

        public TestServer TestServer { get; set; }

        public IKernel Kernel { get; set; }

        protected HttpRequestMessage Request { get; set; }

        protected HttpResponseMessage Response { get; set; }

        public void GivenRequest(HttpMethod method, string relativeUri)
        {
            this.Request = this.CreateHttpRequestMessage(method, relativeUri);
        }

        public void GivenRequest<T>(HttpMethod method, string relativeUri, T content)
        {
            this.Request = this.CreateHttpRequestMessage(method, relativeUri, content);
        }

        public void GivenRequest(HttpMethod method, string relativeUri, FormUrlEncodedContent content)
        {
            this.Request = this.CreateHttpRequestMessage(method, relativeUri, content);
        }

        public void WhenPerformRequest()
        {
            var client = this.TestServer.HttpClient;
            client.Timeout = TimeSpan.FromMilliseconds((double) int.MaxValue);
            this.Response = client.SendAsync(this.Request).Result;
        }

        public void ThenShouldReturnSuccessStatusCode()
        {
            this.Response.EnsureSuccessStatusCode();
        }

        public void ThenPersistResult<T>(out T r)
        {
            r = this.GetContentValue<T>();
        }

        protected void GivenIntegrationTestsContext()
        {
            this.GivenDependencyResolver();
            this.GivenTestServer();
        }

        protected T GetContentValue<T>()
        {
            return this.Response.Content.ReadAsAsync<T>().Result;
        }

        private HttpRequestMessage CreateHttpRequestMessage(HttpMethod method, string relativeUri)
        {
            var requestUrl = new Uri(BaseUri + relativeUri);

            HttpContext.Current = new HttpContext(
                new HttpRequest(string.Empty, requestUrl.ToString(), string.Empty),
                new HttpResponse(new StringWriter()));

            var request = new HttpRequestMessage { RequestUri = requestUrl };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = method;

            return request;
        }

        private HttpRequestMessage CreateHttpRequestMessage<T>(HttpMethod method, string relativeUri, T value)
        {
            var requestUrl = new Uri(BaseUri + relativeUri);

            HttpContext.Current = new HttpContext(
                new HttpRequest(string.Empty, requestUrl.ToString(), string.Empty),
                new HttpResponse(new StringWriter()));

            var request = new HttpRequestMessage
            {
                RequestUri = requestUrl,
                Content = new ObjectContent<T>(value, new JsonMediaTypeFormatter())
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = method;

            return request;
        }

        private HttpRequestMessage CreateHttpRequestMessage(HttpMethod method, string relativeUri, FormUrlEncodedContent content)
        {
            var requestUrl = new Uri(BaseUri + relativeUri);

            HttpContext.Current = new HttpContext(
                new HttpRequest(string.Empty, requestUrl.ToString(), string.Empty),
                new HttpResponse(new StringWriter()));

            var request = new HttpRequestMessage
            {
                RequestUri = requestUrl,
                Content = content
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            request.Method = method;

            return request;
        }

        public void Dispose()
        {
            this.DisposeRequestMessage();
            this.DisposeResponseMessage();

            NinjectCommon.Stop();
        }

        private void DisposeResponseMessage()
        {
            if (this.Response != null)
            {
                this.Response.Dispose();
            }
        }

        private void DisposeRequestMessage()
        {
            if (this.Request != null)
            {
                this.Request.Dispose();
            }
        }

        protected void ThenShouldReturnBadRequest()
        {
            this.Response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
        }
    }
}
