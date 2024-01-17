using ppedv.WcfChat.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Threading;

namespace ppedv.WcfChat.ServerNetFramework
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    class WcfChatServer : IChatServer
    {

        public WcfChatServer()
        {
            Log("NEUE SERVER INSTANZ");
        }
        void Log(string msg, [CallerMemberName] string caller = "")
        {
            Console.WriteLine($"{DateTime.Now:G} [{caller}] {msg}");
        }

        Dictionary<string, IChatClient> clients = new Dictionary<string, IChatClient>();

        public void Login(string username)
        {
            Log($"Login: {username} {OperationContext.Current.SessionId}");

            var client = OperationContext.Current.GetCallbackChannel<IChatClient>();

            ICommunicationObject com = (ICommunicationObject)client;

            com.Faulted += Com_Faulted;

            if (clients.ContainsKey(username))
            {
                client.LoginResponse(false, $"{username} ist bereits angemeldet!");
            }
            else
            {
                clients.Add(username, client);
                client.LoginResponse(true, $"Hallo {username}");
            }

            SendtoAllClient(x => x.ShowUserlist(clients.Select(c => c.Key)));
        }

        private void Com_Faulted(object sender, EventArgs e)
        {
            Log("FAULT");
        }

        private void SendtoAllClient(Action<IChatClient> clientAction, IChatClient sender = null)
        {
            foreach (var client in clients.ToList())
            {
                //if (client.Value == sender)
                //    continue;

                try
                {
                    clientAction.Invoke(client.Value);
                }
                catch (Exception ex)
                {
                    Log($"ERROR: {ex.Message}");
                    clients.Remove(client.Key);
                    SendtoAllClient(x => x.ShowUserlist(clients.Select(c => c.Key)));
                }
            }
        }

        public void Logout()
        {
            Log($"Logout: ?");

            var client = OperationContext.Current.GetCallbackChannel<IChatClient>();

            var sender = clients.FirstOrDefault(x => x.Value == client);

            if (sender.Key != null)
            {
                Log($"{sender.Key} logging out");
                clients.Remove(sender.Key);
                SendtoAllClient(x => x.ShowUserlist(clients.Select(c => c.Key)));
            }
        }

        public void SendImage(Stream stream)
        {
            Log($"SendImage: ?");

            var ms = new MemoryStream();
            stream.CopyTo(ms);

            SendtoAllClient(x =>
            {
                ms.Position = 0;
                x.ShowImage(ms);
            });
        }

        public void SendText(string text)
        {
            Log($"SendText: {text}");
            var client = OperationContext.Current.GetCallbackChannel<IChatClient>();
            var sender = clients.FirstOrDefault(x => x.Value == client);

            SendtoAllClient(x => x.ShowText($"{DateTime.Now:t} [{sender.Key}]: T:[{Thread.CurrentThread.ManagedThreadId}]  {text}"),sender.Value);
        }

        public void StartMultiSend()
        {
            for (int i = 0; i < 10000; i++)
            {
                SendtoAllClient(x => x.ShowText($"{DateTime.Now:u} {i} T:[{Thread.CurrentThread.ManagedThreadId}] {RandomString()}"));
            }
        }

        public static string RandomString()
        {
            var random = new Random();
            var length = random.Next(100, 10000);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
