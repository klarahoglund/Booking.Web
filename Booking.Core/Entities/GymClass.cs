using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Entities
{
    public class GymClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndTime => StartDate + Duration;
        public string Description { get; set; } = String.Empty;

        public ICollection<ApplicationUserGymClass> ApplicationUserGymClasses { get; set; } = new List<ApplicationUserGymClass>();
    }
}
