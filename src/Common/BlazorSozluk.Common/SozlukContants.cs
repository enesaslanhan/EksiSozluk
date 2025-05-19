using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common
{
    public class SozlukContants
    {
        public const string RabbitMQHost = "localhost";
        public const string DefaultExchangeType = "direct";


        public const string UserExchangedName = "UserExchanged";
        public const string UserEmailExchangedQueueName = "UserEmailExchangedQueue";
    }
}
