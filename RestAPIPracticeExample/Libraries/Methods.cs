
namespace Elabor8_PracticeTest_Arjun.Libraries
{
    /// <summary>
    /// Rest API Methods covering GET, POST, PUT, PATCH and DELETE
    /// </summary>
    public class Methods
    {

        public RestResponse GetMethod(string url)
        {
            var client = new RestClient();

            var _restRequest = new RestRequest(url, Method.Get);
            _restRequest.AddHeader("Accept", "application/json");

            RestResponse response = client.Execute(_restRequest);

            return response;
        }


        public RestResponse PostMethod(string url, string body)
        {
            var client = new RestClient();

            var _restRequest = new RestRequest(url, Method.Post);

            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddStringBody(body, DataFormat.Json);

            RestResponse response = client.Execute(_restRequest);

            return response;
        }


        public RestResponse PutMethod(string url, string body)
        {
            var client = new RestClient();

            var _restRequest = new RestRequest(url, Method.Put);

            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddStringBody(body, DataFormat.Json);

            RestResponse response = client.Execute(_restRequest);

            return response;
        }


        public RestResponse PatchMethod(string url, string body)
        {
            var client = new RestClient();

            var _restRequest = new RestRequest(url, Method.Patch);

            _restRequest.AddStringBody(body, DataFormat.Json);
            _restRequest.AddHeader("Accept", "application/json");

            RestResponse response = client.Execute(_restRequest);

            return response;
        }


        public RestResponse DeleteMethod(string url)
        {
            var client = new RestClient();

            var _restRequest = new RestRequest(url, Method.Delete);
            _restRequest.AddHeader("Accept", "application/json");

            RestResponse response = client.Execute(_restRequest);

            return response;
        }

    }
}
