using System;
using System.Threading.Tasks;
using KrisG.TimeTracker.Repositories;

namespace KrisG.TimeTracker.Services
{
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

        public Task<DateTime> Stop()
        {
            var stopTime = DateTime.UtcNow;

            if (!_currentStartTime.HasValue)
            {
                return Task.FromResult(stopTime);
            }

            //await _repository.AddAsync(_currentStartTime.Value, stopTime);

            return Task.FromResult(stopTime);
        }
    }
}
