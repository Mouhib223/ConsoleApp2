using Newtonsoft.Json;
using QuickFix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class SimpleAcceptorApp : QuickFix.MessageCracker, QuickFix.IApplication
    {
        #region QuickFix.Application Methods

        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("IN:  " + message);
            Crack(message, sessionID);
            string json = message.ToJSON(); // Convert message to JSON
            Console.WriteLine("IN: " + json);

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
