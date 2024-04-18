using System.Runtime.CompilerServices;
using HttpHelper;

namespace RandomSayings;

public class Saying
{
    public RestClientHelper HttpHelper { get; set; }

    public Saying(string host)
    {
        HttpHelper = new RestClientHelper(host);
    }

    public string? Get(string endpoint)
    {
        return HttpHelper.ExecuteGetRequest(endpoint);
    }
}