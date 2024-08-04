using Asp.Versioning;
using CrossCuttingLayer;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.SharedObject;


namespace Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{userId}/{parentObject}/sharedobjects")]
    [ApiController]
    public class SharedObjectController : ControllerBase,IConsumer<QueueMessage> 
    {
        private readonly IServiceManager _service;
        private readonly IBusControl _bus;
        public SharedObjectController(IServiceManager service)
        {
            _service = service;
            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
                {
                    h.Username(RabbitMqConsts.UserName);
                    h.Password(RabbitMqConsts.Password);
                });
            });
            _bus.StartAsync();
        }
        [HttpGet("{id:int}", Name = "Get_SharedObject_By_Id")]
        public async Task<IActionResult> Get_SharedObject_By_Id(int id)
        {
            var result = await _service.SharedObjectService.Get_SharedObject_Async(id, trackChanges: false);
            return Ok(result);
        }
        public async Task Consume(ConsumeContext<QueueMessage> context)
        {

            var result = await _service.SharedObjectService.Create_SharedObject(context.Message.UserId, context.Message.ParentObject, false, context.Message.SharedObjectForCreationDto);
            if (result != null)
            {
                Uri uri = new Uri(RabbitMqConsts.RabbitMqUriNotifyClient);
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(result);
            }
            //return CreatedAtRoute("Get_SharedObject_By_Id", new { context.Message.UserId, context.Message.ParentObject, id = result.Id }, result);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Create_SharedObject(int userId, string parentObject, [FromBody] SharedObjectForCreationDto sharedObjectForCreationDto)
        {
            var result = await _service.SharedObjectService.Create_SharedObject(userId,parentObject,  false,sharedObjectForCreationDto);
            return CreatedAtRoute("Get_SharedObject_By_Id", new {userId , parentObject, id = result.Id}, result);
        }
    }
}
