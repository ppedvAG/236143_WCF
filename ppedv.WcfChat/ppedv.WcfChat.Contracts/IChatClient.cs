using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace ppedv.WcfChat.Contracts
{
    [ServiceContract(Namespace = "http://ppedv.de/WcfKurs/2024/WcfChat)")]
    public interface IChatClient
    {
        [OperationContract(IsOneWay = true)]
        void LoginResponse(bool ok, string msg);

        [OperationContract(IsOneWay = true)]
        void ShowUserlist(IEnumerator<string> users);

        [OperationContract(IsOneWay = true)]
        void ShowText(string msg);

        [OperationContract(IsOneWay = true)]
        void ShowImage(Stream steam);
    }

}
