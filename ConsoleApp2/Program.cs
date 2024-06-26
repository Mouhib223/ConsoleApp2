﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;
using QuickFix.Fields;
using QuickFix.Transport;
using Newtonsoft.Json;

namespace ConsoleApp2
{

    

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            


            string logo = @"                             _             
     /\                     | |            
    /  \   ___ ___ ___ _ __ | |_ ___  _ __ 
   / /\ \ / __/ __/ _ \ '_ \| __/ _ \| '__|
  / ____ \ (_| (_|  __/ |_) | || (_) | |   
 /_/    \_\___\___\___| .__/ \__\___/|_|   
                      | |                  
                      |_|                  ";
            Console.WriteLine(logo);
        
            Console.WriteLine("===================================");
            /*Console.WriteLine("This is only an example program.");
            Console.WriteLine("It's a simple server (e.g. Acceptor) app that will let clients (e.g. Initiators)");
            Console.WriteLine("connect to it.  It will accept and display any application-level messages that it receives.");
            Console.WriteLine("Connecting clients should set TargetCompID to 'SIMPLE' and SenderCompID to 'CLIENT1' or 'CLIENT2'.");
            Console.WriteLine("Port is 5001.");
            Console.WriteLine("(see simpleacc.cfg for configuration details)");*/
            Console.WriteLine("                 Acceptor Ready !!");
            Console.WriteLine("Ready and waiting for your command. Let's make magic happen!");
            Console.WriteLine("************************************************************");
            Console.WriteLine("************************************************************");
            Console.WriteLine("************************************************************");
            Console.WriteLine("************************************************************");
            Console.WriteLine("************************************************************");
            Console.WriteLine("===================================");

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
                Console.WriteLine("if you are Done, press <enter> to quit");
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
