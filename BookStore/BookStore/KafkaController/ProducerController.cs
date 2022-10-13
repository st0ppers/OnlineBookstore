using BookStore.BL.Kafka;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.KafkaController
{
    [ApiController]
    [Route("[controller]")]
    public class ProducerController : ControllerBase
    {
        private readonly ProducerServices<int, int> _producerServices;

        public ProducerController(ProducerServices<int, int> producerServices)
        {
            _producerServices = producerServices;
        }

        [HttpPost(nameof(SendMessage))]
        public  Task SendMessage([FromQuery] int p, int i)
        {
            return  _producerServices.Producer(p, i);
        }
    }
}
