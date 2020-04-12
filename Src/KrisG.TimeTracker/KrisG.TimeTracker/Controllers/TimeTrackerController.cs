using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KrisG.TimeTracker.Repositories;
using KrisG.TimeTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KrisG.TimeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTrackerController : ControllerBase
    {
        private readonly ITimeSliceRepository _timeSliceRepository;

        public TimeTrackerController(ITimeSliceRepository timeSliceRepository)
        {
            _timeSliceRepository = timeSliceRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TimeSlice>> GetAll()
        {
            return await _timeSliceRepository.GetAll();
        }

        [HttpPost]
        public async Task<TimeSlice> Add(TimeSlice item)
        {
            return await _timeSliceRepository.AddAsync(item);
        }

        [HttpPut]
        public async Task<TimeSlice> Update(TimeSlice item)
        {
            await _timeSliceRepository.UpdateAsync(item);

            return item;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await _timeSliceRepository.DeleteAllAsync();
            return Ok();
        }
    }
}