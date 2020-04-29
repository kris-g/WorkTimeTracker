using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using KrisG.TimeTracker.Entities;
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

    public class TimeSliceRepository : RepositoryBase<TimeSlice>, ITimeSliceRepository
    {
        protected override string JsonPath => @".\TimeSlices.json";
    }
}