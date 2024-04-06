using RestSharp;

namespace HttpHelper;

public class RestClientHelper
{
    public string BaseRequestAdress { get; set; }
    private RestClient Client { get; set; }

    public RestClientHelper(string uri)
    {
        BaseRequestAdress = uri;
        Client = new RestClient(BaseRequestAdress);
    }

    public RestResponse ExecuteGetRequest(string endpoint)
    {
        var request = new RestRequest($"{BaseRequestAdress}/{endpoint}/random", Method.Get);
        request.AddHeader("Accept", "*/*");
        RestResponse response = Client.Execute(request);
        return response;
    }

}