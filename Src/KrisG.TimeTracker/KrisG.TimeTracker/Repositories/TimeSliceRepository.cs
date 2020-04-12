using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using KrisG.TimeTracker.Services;

namespace KrisG.TimeTracker.Repositories
{
    public interface ITimeSliceRepository
    {
        Task<IEnumerable<TimeSlice>> GetAll();
        Task<TimeSlice> AddAsync(TimeSlice timeSlice);
        Task UpdateAsync(TimeSlice timeSlice);
        Task DeleteAllAsync();
    }

    public class TimeSliceRepository : ITimeSliceRepository
    {
        private const string TimeSlicesJsonPath = @".\TimeSlices.json";
        private IList<TimeSlice> _timeSlices = new List<TimeSlice>();

        public TimeSliceRepository()
        {
            LoadTimeSlices().Wait();
        }

        public async Task<IEnumerable<TimeSlice>> GetAll()
        {
            await LoadTimeSlices();
            return _timeSlices.AsEnumerable();
        }

        public async Task<TimeSlice> AddAsync(TimeSlice timeSlice)
        {
            timeSlice.Id = Guid.NewGuid().ToString("N");
            _timeSlices.Add(timeSlice);
            await SaveTimeSlices();

            return timeSlice;
        }

        public async Task UpdateAsync(TimeSlice timeSlice)
        {
            var toUpdate = _timeSlices.FirstOrDefault(x => x.Id == timeSlice.Id);

            if (toUpdate != null)
            {
                _timeSlices.Remove(toUpdate);
            }

            _timeSlices.Add(timeSlice);
            await SaveTimeSlices();
        }

        public async Task DeleteAllAsync()
        {
            _timeSlices.Clear();
            await SaveTimeSlices();
        }

        private async Task SaveTimeSlices()
        {
            var json = JsonSerializer.Serialize(_timeSlices, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(TimeSlicesJsonPath, json);
        }

        private async Task LoadTimeSlices()
        {
            if (File.Exists(TimeSlicesJsonPath))
            {
                var json = await File.ReadAllTextAsync(TimeSlicesJsonPath);
                _timeSlices = JsonSerializer.Deserialize<IList<TimeSlice>>(json);
            }
        }
    }
}