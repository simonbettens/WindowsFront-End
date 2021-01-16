using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace WindowsFront_end.Controllers
{
    public class HttpInterceptorHandler : DelegatingHandler
    {
        public HttpInterceptorHandler()
        {
            InnerHandler = new HttpClientHandler();
        }
        //adds the bearer to the header
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", $"Bearer {ApplicationData.Current.LocalSettings.Values["token"]}");
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
