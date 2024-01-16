using ppedv.WcfChat.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.ServiceModel;

namespace ppedv.WcfChat.ServerNetFramework
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    class WcfChatServer : IChatServer
    {
        void Log(string msg, [CallerMemberName] string caller = "")
        {
            Console.WriteLine($"{DateTime.Now:G} [{caller}] {msg}");
        }

        Dictionary<string, IChatClient> clients = new Dictionary<string, IChatClient>();

        public void Login(string username)
        {
            Log($"Login: {username} {OperationContext.Current.SessionId}");

            var client = OperationContext.Current.GetCallbackChannel<IChatClient>();

            if (clients.ContainsKey(username))
            {
                client.LoginResponse(false, $"{username} ist bereits angemeldet!");
            }
            else
            {
                clients.Add(username, client);
                client.LoginResponse(true, $"Hallo {username}");
            }
        }

        public void Logout()
        {
            Log($"Logout: ?");
        }

        public void SendImage(Stream stream)
        {
            Log($"SendImage: ?");

        }

        public void SendText(string text)
        {
            Log($"SendText: {text}");
        }
    }
}
