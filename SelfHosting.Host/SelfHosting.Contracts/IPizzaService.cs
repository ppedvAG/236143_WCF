using System.Collections.Generic;
using System.ServiceModel;

namespace SelfHosting.Contracts
{
    [ServiceContract]
    public interface IPizzaService
    {
        [OperationContract()]
        IEnumerable<Pizza> GetAllPizzas();

        [OperationContract()]
        void OrderPizza(int amount, Pizza pizza);
    }

}
