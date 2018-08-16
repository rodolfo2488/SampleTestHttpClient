using System;
using MSTest;
using Moq;
using Testing.HttpClient;
using Services.TestWiremock;
using System.Net;
using System.Threading.Tasks;

namespace TestwiremockTests
{
    [TestClass]
    public class MySuperServiceTest
    {
        [TestMethod]
        public async Task Test1()
        {
            using (var httpFactory = new HttpClientTestingFactory())
            {
                var baseUrl = "http://some-website.com/some-path";
                var service = new MySuperService(httpFactory.HttpClient, baseUrl);

                var request = httpFactory.Expect(baseUrl);
                request.Respond(HttpStatusCode.OK, "123");

                var result = await service.MakeAsyncCall();
                Assert.Equal("123", result.Content.ToString());
            }
        }
    }
}
