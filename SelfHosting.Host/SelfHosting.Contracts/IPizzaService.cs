using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SelfHosting.Contracts
{
    [ServiceContract]
    public interface IPizzaService
    {
        [OperationContract()]
        [FaultContract(typeof(OutOfPizzaError))]
        IEnumerable<Pizza> GetAllPizzas();

        [OperationContract()]
        void OrderPizza(int amount, Pizza pizza);
    }

    [DataContract]
    public class OutOfPizzaError
    {
        [DataMember]
        public string Msg { get; set; }

        [DataMember]
        public string WerIstSchuld { get; set; }

        //public int InterneErrorId { get; set; }
    }

}
