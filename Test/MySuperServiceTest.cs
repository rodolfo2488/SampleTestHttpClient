using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.TestWiremock;
using Testing.HttpClient;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class MySuperServiceTest
    {
        [TestMethod]
        public async Task ShouldMockMyHttpCall()
        {
            using (var httpFactory = new HttpClientTestingFactory())
            {
                var baseUrl = "http://some-website.com/some-path";
                var service = new MySuperService(httpFactory.HttpClient, baseUrl);

                var response = service.MakeAsyncCall();

                var request = httpFactory.Expect(baseUrl);

                Assert.AreEqual(request.Request.Method, HttpMethod.Get);

                Assert.AreEqual("Value", GetHeaderValueFromRequest(request, "CustomHeader"));

                request.Respond(HttpStatusCode.OK, "123");

                var result = await response;

                Assert.AreEqual("123", result);
            }
        }

        private string GetHeaderValueFromRequest(TestRequest request, string headerKey)
        {
            Assert.IsTrue(request.Request.Headers.Contains(headerKey), $"Key provided {headerKey} it is not contained in Headers");

            var sequence = request.Request.Headers.GetValues(headerKey).GetEnumerator();
            while (sequence.MoveNext())
            {
                return sequence.Current;
            }

            return null;
        }
    }
}
