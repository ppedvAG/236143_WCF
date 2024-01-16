﻿using System;
using System.IO;
using System.ServiceModel;

namespace ppedv.WcfChat.Contracts
{
    [ServiceContract(Namespace = "http://ppedv.de/WcfKurs/2024/WcfChat)", CallbackContract = typeof(IChatClient))]
    public interface IChatServer
    {
        [OperationContract(IsOneWay = true)]
        void Login(string username);

        [OperationContract(IsOneWay = true)]
        void Logout();

        [OperationContract(IsOneWay = true)]
        void SendText(string text);

        [OperationContract(IsOneWay = true)]
        void SendImage(Stream stream);
    }

}
