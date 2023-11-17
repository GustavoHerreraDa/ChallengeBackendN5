using Challenge.Application.Common.Models;
using Challenge.Application.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Services
{
    public class ProducerService : IProducerService
    {
        private readonly ILogger<ProducerService> _logger;
        private readonly ProducerConfig _producerConfig;

        public const string CONST_TOPIC = "myTopic";
        public ProducerService(ILogger<ProducerService> logger, ProducerConfig producerConfig)
        {
            _logger = logger;
            _producerConfig = producerConfig;
        }

        public async Task SendEventAsync(PermisionEvent PermissionEvent)
        {
            try
            {
                using (var producer = new ProducerBuilder<string, PermisionEvent>(_producerConfig).Build())
                {


                    producer.Produce(CONST_TOPIC, new Message<string, PermisionEvent> { Value = PermissionEvent },
                        (deliveryReport) =>
                        {
                            if (deliveryReport.Error.Code != ErrorCode.NoError)
                            {
                                _logger.LogError($"Failed to deliver message: {deliveryReport.Error.Reason}");
                            }
                            else
                            {
                                _logger.LogError($"Produced event to topic {CONST_TOPIC}: id = {PermissionEvent.Id} value = {PermissionEvent.OperationName}");
                            }
                        });     
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
