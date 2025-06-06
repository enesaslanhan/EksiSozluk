﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Infrastructure
{
    public static class QueueFactory
    {
        public static void SendMessageToExchange(string exchangeName,string exchangeType,string queueName,object obj)
        {
            var channel = CreateBasicConsumer()
                            .EnsureExchange(exchangeName,exchangeType)
                            .EnsureQueue(queueName,exchangeName)
                            .Model;

            var body=Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));

            channel.BasicPublish(exchange:exchangeName,routingKey:queueName,basicProperties:null,body:body);
        }
        public static EventingBasicConsumer CreateBasicConsumer()
        {
            var factory = new ConnectionFactory() { HostName = SozlukContants.RabbitMQHost ,Port=5672};
            var connection=factory.CreateConnection();
            var channel = connection.CreateModel();

            return new EventingBasicConsumer(channel);
        }
        public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer,string exchangeName,string exchangeType = SozlukContants.DefaultExchangeType)
        {
            consumer.Model.ExchangeDeclare(exchange: exchangeName, type: exchangeType, durable: false, autoDelete: false);
            return consumer;
        }

        public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer, string queueName, string exchangeName)
        {
            consumer.Model.QueueDeclare(queue:queueName,durable:false,exclusive:false,autoDelete:false,null);

            consumer.Model.QueueBind(queueName,exchangeName,queueName);
            return consumer;
        }
    }
}
