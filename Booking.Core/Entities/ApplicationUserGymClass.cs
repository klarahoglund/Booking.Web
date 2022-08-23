using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Entities
{
   public class ApplicationUserGymClass
    {
        public string ApplicationUserId { get; set; }
        public int GymClassId { get; set; }

        public GymClass GymClass { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }
}
