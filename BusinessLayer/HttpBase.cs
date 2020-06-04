using RestSharp;

namespace BusinessLayer
{
    public static class HttpBase
    {

        public static IRestResponse Get(string Url, string Token)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.GET);
            if (!string.IsNullOrEmpty(Token))
            {
                request.AddParameter("Authorization", string.Format("Bearer " + Token), ParameterType.HttpHeader);
            }

            request.AddHeader("Accept", "*/*");
            IRestResponse res = client.Execute(request);
            return res;
        }

        public static IRestResponse Put(string Url, string Token, string json)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.PUT);
            if (!string.IsNullOrEmpty(Token))
            {
                request.AddParameter("Authorization", string.Format("Bearer " + Token), ParameterType.HttpHeader);
            }
            if (json != null)
            {
                request.AddJsonBody(json);
            }

            IRestResponse res = client.Execute(request);
            return res;
        }

        public static IRestResponse Post(string Url, string Token, string json)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.POST);
            if (!string.IsNullOrEmpty(Token))
            {
                request.AddParameter("Authorization", string.Format("Bearer " + Token), ParameterType.HttpHeader);
            }
            if (json != null)
            {
                request.AddJsonBody(json);
            }
            IRestResponse res = client.Execute(request);
            return res;
        }

        public static IRestResponse Delete(string Url, string Token)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.DELETE);
            if (!string.IsNullOrEmpty(Token))
            {
                request.AddParameter("Authorization", string.Format("Bearer " + Token), ParameterType.HttpHeader);
            }
            IRestResponse res = client.Execute(request);
            return res;
        }

    }
}
