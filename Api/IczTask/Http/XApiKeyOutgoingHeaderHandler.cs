namespace IczTask.Http;

public sealed class XApiKeyOutgoingHeaderHandler : DelegatingHandler
{
    private const string HeaderName = "X-Api-Key";

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        request.Headers.Remove(HeaderName);
        request.Headers.TryAddWithoutValidation(HeaderName, string.Empty);
        return base.SendAsync(request, cancellationToken);
    }
}
