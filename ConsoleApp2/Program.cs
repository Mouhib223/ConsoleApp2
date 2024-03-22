using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;
using QuickFix.Fields;
using QuickFix.Transport;

namespace ConsoleApp2
{

    public class SimpleAcceptorApp : /*QuickFix.MessageCracker,*/ QuickFix.IApplication
    {
        #region QuickFix.Application Methods

        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("IN:  " + message);
            //Crack(message, sessionID);
            string msgType = message.Header.GetString(Tags.MsgType);
            if (msgType.Equals(MsgType.EXECUTION_REPORT))
            {
                string account = message.GetString(Tags.Account);
                decimal price = message.GetDecimal(Tags.Price);
            }
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
        #endregion
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("=============");
            Console.WriteLine("This is only an example program.");
            Console.WriteLine("It's a simple server (e.g. Acceptor) app that will let clients (e.g. Initiators)");
            Console.WriteLine("connect to it.  It will accept and display any application-level messages that it receives.");
            Console.WriteLine("Connecting clients should set TargetCompID to 'SIMPLE' and SenderCompID to 'CLIENT1' or 'CLIENT2'.");
            Console.WriteLine("Port is 5001.");
            Console.WriteLine("(see simpleacc.cfg for configuration details)");
            Console.WriteLine("=============");

            if (args.Length != 1)
            {
                Console.WriteLine("usage: SimpleAcceptor CONFIG_FILENAME");
                System.Environment.Exit(2);
            }

            try
            {
                SessionSettings settings = new SessionSettings(args[0]);
                IApplication app = new SimpleAcceptorApp();
                IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
                ILogFactory logFactory = new FileLogFactory(settings);
                IAcceptor acceptor = new ThreadedSocketAcceptor(app, storeFactory, settings, logFactory);

                acceptor.Start();
                Console.WriteLine("press <enter> to quit");
                Console.Read();
                acceptor.Stop();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("==FATAL ERROR==");
                Console.WriteLine(e.ToString());
            }
        }
    }
}
