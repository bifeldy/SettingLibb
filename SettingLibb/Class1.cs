using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace SettingLibb
{
    public class Class1
    {
        public string IpWebService;
        public string conStrOra;
        private string prefix = "mujiyono";

        public string getConStrOra(string server, string username, string password)
        {
            return "Provider=MSDAORA.1;User ID=" + username + ";password=" + password + ";Data Source=" + server;
        }

        public string GetVariabel(string Key, string pathKunci)
        {
            string hostKunci = Environment.GetEnvironmentVariable("KUNCI_IP_DOMAIN");
            if (string.IsNullOrEmpty(hostKunci)) {
                hostKunci = "localhost";
            }
            var url = "http://" + hostKunci;
            if (!string.IsNullOrEmpty(pathKunci)) {
                url += "/" + pathKunci;
            }
            url += "/GetVariabel";

            var request = WebRequest.Create(url);
            request.Timeout = 600000;
            request.Method = "POST";
            request.ContentType = "application/json";
            Params asd = new Params();
            asd.Key = prefix+Key;
            string strJson = JsonConvert.SerializeObject(asd);
            if (string.IsNullOrEmpty(strJson))
            {
                //actSerUt.WriteProcessGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, "Serializing JSON results empty.");
                //goto loncat;
            }
            using (StreamWriter StreamWriter = new StreamWriter(request.GetRequestStream()))
            {
                StreamWriter.Write(strJson);
            }
            //actSerUt.WriteProcessGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, "Write string JSON done.");

            //actSerUt.WriteProcessGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, "Read string response...");
            string tempStr = "0";
            try
            {
                var response = request.GetResponse();
                var data = response.GetResponseStream();
                if (response is null || data is null)
                {
                    //actSerUt.WriteProcessGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, "Response or data results empty.");
                    //goto loncat;
                }
                using (StreamReader sr = new StreamReader(data))
                {
                    tempStr = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                //actSerUt.WriteErrorGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, "Waiting for response from 192.168.10.95 - " + ex.Message);
                return ex.Message;
            }
            //actSerUt.WriteProcessGeneral(System.Reflection.MethodBase.GetCurrentMethod().Name, tempStr + " received.");
            return tempStr;
        }
        public string getConStrOraODP(string server, string username, string password)
        {
            return "Data Source=" + server + "; User ID=" + username + ";password=" + password;
        }
    }
}
