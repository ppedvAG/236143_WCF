using ppedv.WcfChat.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;

namespace ppedv.WcfChat.ServerNetFramework
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
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

        private void SendtoAllClient(Action<IChatClient> clientAction)
        {
            foreach (var client in clients.ToList())
            {
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

            SendtoAllClient(x => x.ShowText($"{DateTime.Now:t} [{sender.Key}]: {text}"));
        }
    }
}
