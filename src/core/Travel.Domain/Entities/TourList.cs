using System.Collections.Generic;
using System.Reflection;

namespace Travel.Domain.Entities
{
    public class TourList
    {

        public TourList()
        {
            Tours = new List<TourPackage>();
        }
        // property 
        public IList<TourPackage>? Tours { get; set; }
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string About { get; set; }
    }
}