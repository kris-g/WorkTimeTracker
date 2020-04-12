using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KrisG.TimeTracker.Repositories;

namespace KrisG.TimeTracker.Services
{
    public class TimeSlice
    {
        public string Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }

    public interface ITimeTrackerService
    {
        DateTime Start();
        Task<DateTime> Stop();
    }

    public class TimeTrackerService : ITimeTrackerService
    {
        private DateTime? _currentStartTime;
        
        private readonly TimeSliceRepository _repository;

        public TimeTrackerService(TimeSliceRepository repository)
        {
            _repository = repository;
        }

        public DateTime Start() => (DateTime) (_currentStartTime = DateTime.UtcNow);

        public async Task<DateTime> Stop()
        {
            var stopTime = DateTime.UtcNow;

            if (!_currentStartTime.HasValue)
            {
                return stopTime;
            }

            //await _repository.AddAsync(_currentStartTime.Value, stopTime);

            return stopTime;
        }
    }
}
