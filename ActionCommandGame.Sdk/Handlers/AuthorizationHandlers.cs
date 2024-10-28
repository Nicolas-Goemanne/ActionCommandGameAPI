using ActionCommandGame.Authentication;
using ActionCommandGame.Sdk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionCommandGame.Sdk.Handlers
{
    public class AuthorizationHandler(IBearerTokenStore tokenStore) : DelegatingHandler
    {
        private readonly IBearerTokenStore _tokenStore = tokenStore;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _tokenStore.GetToken();
            request.Headers.AddAuthorization(token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
