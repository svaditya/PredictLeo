using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PredictionBot
{

    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public class Program
    {
        //    static void Main(string[] args)
        //    {
        //        InvokeRequestResponseService().Wait();
        //    }

        public static async Task<string> InvokeRequestResponseService(string strRet1)
        {
            string strRet = string.Empty;
          
            char[] separatingChars = { ' ' , ',' };
			string[] Values = strRet1.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"WallArea", "RoofArea", "OverallHeight", "GlazingArea", "HeatingLoad"},
                                Values = new string[,] {  { "0", Values[0], Values[1], Values[2], Values[3] } }
                                // Convert.ToString(PredictionBot.InputForm.WallArea), Convert.ToString(PredictionBot.InputForm.RoofArea), Convert.ToString(PredictionBot.InputForm.OverallHeight), Convert.ToString(PredictionBot.InputForm.GlazingArea), Convert.ToString(PredictionBot.InputForm.HeatingLoad)
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "abc123"; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/50bca197e8fb4568a83d4da3662674fe/services/4d871264a4784dd49c6a795241ad14f4/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    strRet = await response.Content.ReadAsStringAsync();
                    // Convert to Json Object for Parsing
                    JObject data = JObject.Parse(strRet);
                    return (string)data["Results"]["output1"]["value"]["Values"][0][5];

                }
                else
                {
                    //Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    //    Console.WriteLine(response.Headers.ToString());

                    strRet = string.Format("Please enter Correct Values");
                    return strRet;
                    // Console.WriteLine(responseContent);
                }
            }
        }

    }
}
