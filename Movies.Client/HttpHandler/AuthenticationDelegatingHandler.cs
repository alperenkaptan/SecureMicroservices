using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;

namespace Movies.Client.HttpHandler;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ClientCredentialsTokenRequest _tokenRequestModel;
    public AuthenticationDelegatingHandler(IHttpClientFactory httpClientFactory, ClientCredentialsTokenRequest tokenRequestModel)
    {
        _httpClientFactory = httpClientFactory;
        _tokenRequestModel = tokenRequestModel;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient("IdentityServerClient");
        var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(_tokenRequestModel);
        if (tokenResponse.IsError)
        {
            throw new Exception("Problem encountered while fetching the access token", tokenResponse.Exception);
        }

        request.SetBearerToken(tokenResponse.AccessToken);
        return await base.SendAsync(request, cancellationToken);
    }

}
