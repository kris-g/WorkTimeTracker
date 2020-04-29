using System;

namespace KrisG.TimeTracker.Entities
{
    public class TimeSlice : EntityBase
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}