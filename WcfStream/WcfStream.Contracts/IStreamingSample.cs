using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace WcfStream.Contracts
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IStreamingSample
    {
        [OperationContract()]
        Stream GetStream(string data);

        [OperationContract()]
        string GetString();

        [OperationContract()]
        IEnumerable<Pizza> GetPizzas();


        [OperationContract]
        bool UploadStream(Stream stream);

        [OperationContract]
        Stream EchoStream(Stream stream);

        [OperationContract]
        Stream GetReversedStream();
    }

    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Preis { get; set; }
    }
}
