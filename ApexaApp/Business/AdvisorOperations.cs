using ApexaApp.Business.DataContracts;
using ApexaApp.Business.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using Support;
using System.Net;
using System.Xml.Linq;


namespace ApexaApp.Business
{
    public class AdvisorOperations : IAdvisorOperations
    {
        #region Properties&Attributes
#if DEBUG
        private string AdvisorServiceURL = "https://localhost:7274/api/Advisor";

#else
        private string AdvisorServiceURL = "http://localhost:8090/api/Advisor";
#endif
        #endregion Properties&Attributes

        #region Operations
        /// <summary>
        /// Return the advisors from the Apexa REST Service
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Advisor>?> GetAdvisors()
        {
            List<Advisor>? Advisors = null; 

            try
            {                
                using (HttpClient webClient = new HttpClient())
                {
                    var URL = string.Format("{0}/GetAllAdvisors", AdvisorServiceURL);

                    if (UrlIsValid())
                    {
                        webClient.BaseAddress = new Uri(URL);
                        Task<string> responseBody = webClient.GetStringAsync("");
                        responseBody.Wait();

                        var xml = responseBody.Result;

                        Advisors = JsonConvert.DeserializeObject<List<Advisor>>(xml);
                    }
                }
            }
            catch (WebException ex)
            {                
            }

            return Advisors;
        }

        /// <summary>
        /// Create an advisor using the Apexa REST service
        /// </summary>
        /// <param name="SIN"></param>
        /// <param name="Name"></param>
        /// <param name="Address"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public async Task<Response> CreateAdvisor(
            string SIN,
            string Name,
            string Address,
            string Phone
            )
        {
            Response result = new Response();
            Task<HttpResponseMessage> Create;

            using (HttpClient webClient = new HttpClient())
            {
                try
                {                
                    var URL = string.Format("{0}/Create/{1}/{2}/{3}/{4}",
                        AdvisorServiceURL,
                        SIN,
                        Name,
                        Address,
                        Phone);
                    
                    if (UrlIsValid())
                    {
                        webClient.BaseAddress = new Uri(URL);
                        Create = webClient.PostAsync(URL, null);
                        Create.Wait();

                        var Response = Create.Result.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<Response>(Response.Result);                        
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Message = "Cannot access Apexa Service";
                    }
                }
                catch (WebException ex)
                {
                    result.Succeeded = false;
                    result.Message = ex.Message;
                }
            }

            return result;
        }

        public async Task<Advisor>? GetAdvisor(string SIN)
        {
            Advisor? Result = null;

            var URL = string.Format("{0}/Get/{1}", AdvisorServiceURL,SIN);

            using (HttpClient webClient = new HttpClient())
            {                
                if (UrlIsValid())
                {
                    webClient.BaseAddress = new Uri(URL);
                    var Create = webClient.PostAsync(URL, null);
                    Task<string> responseBody = webClient.GetStringAsync("");
                    responseBody.Wait();
                    var xml = responseBody.Result;
                    Result = JsonConvert.DeserializeObject<Advisor>(xml);
                }
            }

            return Result;
        }

        /// <summary>
        /// Create an advisor using the Apexa REST service
        /// </summary>
        /// <param name="SIN"></param>
        /// <param name="Name"></param>
        /// <param name="Address"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public async Task<Response> UpdateAdvisor(
            string SIN,
            string Name,
            string Address,
            string Phone
            )
        {
            Response result = new Response();
            Task<HttpResponseMessage> Update;

            using (HttpClient webClient = new HttpClient())
            {
                try
                {
                    var URL = string.Format("{0}/Update/{1}/{2}/{3}/{4}",
                        AdvisorServiceURL,
                        SIN,
                        Name,
                        Address,
                        Phone);

                    if (UrlIsValid())
                    {
                        webClient.BaseAddress = new Uri(URL);
                        Update = webClient.PutAsync(URL, null);
                        Update.Wait();
                        var Response = Update.Result.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<Response>(Response.Result);                        
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Message = "Cannot access Apexa Service";
                    }
                }
                catch (WebException ex)
                {
                    result.Succeeded = false;
                    result.Message = ex.Message;
                }
            }

            return result;
        }


        public async Task<bool> DeleteAdvisor(string SIN)
        {
            bool Result = false;
            Task<HttpResponseMessage> Response;

            using (HttpClient webClient = new HttpClient())
            {
                var URL = string.Format("{0}/Delete/{1}",AdvisorServiceURL,SIN);
                webClient.BaseAddress = new Uri(URL);
                Response = webClient.DeleteAsync(URL);
                Response.Wait();
                Result = true;
            }

            return Result;
        }



        public bool UrlIsValid()
        {
            bool Result = false;

            string url = string.Format("{0}/Check", AdvisorServiceURL);

            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 500; //set the timeout to 5 seconds to keep the user from waiting too long for the page to load
                request.Method = "HEAD"; //Get only the header information -- no need to download any content
                request.Method = "GET";
                request.ContentType = "application/json";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    if (statusCode >= 100 && statusCode < 400) //Good requests
                    {
                        Result  = true;
                    }
                    else if (statusCode >= 500 && statusCode <= 510) //Server Errors
                    {                        
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {                    
                }
                else
                {                    
                }
            }
            catch (Exception ex)
            {                
            }
            return Result;
        }

#endregion Operations
    }
}
