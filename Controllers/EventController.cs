using Abstraction.Services;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [Route("GetAllLocations")]
        public async Task<ActionResult> Get()
        {
            ResponsePayload<EventLocation> response = new ResponsePayload<EventLocation>();
           
                try
                {
                    response.StatusCode = 200;
                    response.Message = "success";
                    response.DataList = await _eventService.GetAllLocations();
                }
                catch (Exception exception)
                {
                    response.StatusCode = 400;
                    response.Message = "Can't get any location,please try again " + exception.Message;
                }
           
            return Ok(response);

        }

        [HttpGet]
        [Route("GetAllEventByLocation")]
        public async Task<ActionResult> GetAllEventByLocation(int id)
        {
            ResponsePayload<EventDetails> response = new ResponsePayload<EventDetails>();

            try
            {
                response.StatusCode = 200;
                response.Message = "success";
                response.DataList = await _eventService.GetAllEventByLocation(id);
            }
            catch (Exception exception)
            {
                response.StatusCode = 400;
                response.Message = "Can't get any location,please try again " + exception.Message;
            }

            return Ok(response);

        }
        [HttpPost]
        [Route("addBooking")]
        public async Task<ActionResult> AddBooking([FromBody] EventBooking booking)
        {
            ResponsePayload<string> response = new ResponsePayload<string>();

            try
            {
                response.StatusCode = 200;
                response.AddedIdentity = await _eventService.AddBooking(booking);
                response.Message = "Booking Details added successfully";
                
            }
            catch (Exception exception)
            {
                response.StatusCode = 400;
                response.Message = "Adding Booking details failed" + exception.Message;
            }

            return Ok(response);

        }
        [HttpPost]
        [Route("updateBooking")]
        public async Task<ActionResult> UpdateBooking([FromBody] EventBooking booking)
        {
            ResponsePayload<string> response = new ResponsePayload<string>();

            try
            {
                response.StatusCode = 200;
                response.Message = await _eventService.UpdateBooking(booking);

            }
            catch (Exception exception)
            {
                response.StatusCode = 400;
                response.Message = "Adding Booking details failed" + exception.Message;
            }

            return Ok(response);

        }
    }
}
