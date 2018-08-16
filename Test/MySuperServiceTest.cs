using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.TestWiremock;
using Testing.HttpClient;
using System.Threading.Tasks;
using System.Net;

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
                request.Respond(HttpStatusCode.OK, "123");

                var result = await response;

                Assert.AreEqual("123", result);
            }
        }
    }
}
