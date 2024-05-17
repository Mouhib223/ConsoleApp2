using Newtonsoft.Json;
using QuickFix;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class SimpleAcceptorApp : QuickFix.MessageCracker, QuickFix.IApplication
    {
        #region QuickFix.Application Methods

        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("IN: " + message);
            Crack(message, sessionID);

            // Convert message to JSON
            string json = message.ToJSON();
            Console.WriteLine("This is the JSON format: " + json);

            // Serialize JSON
            string JSONresult = JsonConvert.SerializeObject(json);

            // File path for JSON output
            string path = @"C:\Users\MHlaili\Desktop\json\fix.json";

            // Clear the content of the file before writing new JSON data
            File.WriteAllText(path, string.Empty);

            // Write JSON to file
            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine(JSONresult);
                tw.Close();
            }

            // Send JSON to API
            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                result = client.UploadString("http://localhost:5268/api/Rules/endpoint", "POST", JSONresult);
            }
            Console.WriteLine(result);



        }

        public void ToApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("OUT: " + message);
        }

        public void FromAdmin(Message message, SessionID sessionID)
        {
            Console.WriteLine("IN:  " + message);
        }

        public void ToAdmin(Message message, SessionID sessionID)
        {
            Console.WriteLine("OUT:  " + message);
        }

        public void OnCreate(SessionID sessionID) { }
        public void OnLogout(SessionID sessionID) { }
        public void OnLogon(SessionID sessionID) { }

        public void OnMessage(QuickFix.FIX44.NewOrderSingle order,SessionID sessionID)
        {
            Console.WriteLine("GGGGGGG");
            Console.WriteLine("Msg Recived");

            Session.SendToTarget(order, sessionID);
            ToApp(null, sessionID);
           
        }
        public void OnMessage(QuickFix.FIX44.OrderCancelRequest order,SessionID sessionID)
        {
            Console.WriteLine("FFFFFFFFF");
            Console.WriteLine("Msg Recived");
        }
        public void OnMessage(QuickFix.FIX44.OrderCancelReplaceRequest order,SessionID sessionID)
        {
            Console.WriteLine("BBBBBBBBBB");
            Console.WriteLine("Msg Recived");
        }
        #endregion
    }
}
