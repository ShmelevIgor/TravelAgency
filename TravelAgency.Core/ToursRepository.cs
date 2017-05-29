using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Core
{
    public class ToursRepository
    {
        List<Tour> tours = new List<Tour>();

        public ToursRepository()
        {
            using (StreamReader streamReader = new StreamReader(@"..\..\..\TravelAgency.Core\Tours.json"))
            {
                string json = streamReader.ReadToEnd();
                tours = JsonConvert.DeserializeObject<List<Tour>>(json);
            }
        }
        
        public List<Tour> GetTours()
        {
            return tours;
        }

        public List<Tour> FindTours(Tour tour)
        {
            List<Tour> foundTours = tours;

            if (tour.Direction != null)
            {
                foundTours = foundTours.FindAll(t => t.Direction == tour.Direction);
            }

            if (tour.ToDate != new DateTime(1970, 1, 1))
            {
                foundTours = foundTours.FindAll(t => t.ToDate == tour.ToDate);
            }

            if (tour.FromDate != new DateTime(1970, 1, 1))
            {
                foundTours = foundTours.FindAll(t => t.FromDate == tour.FromDate);
            }

            if (tour.Hotel != null)
            {
                foundTours = foundTours.FindAll(t => t.Hotel == tour.Hotel);
            }

            if (tour.Rating != 0)
            {
                foundTours = foundTours.FindAll(t => t.Rating >= tour.Rating);
            }

            if (tour.Price != 0)
            {
                foundTours = foundTours.FindAll(t => t.Price >= tour.Price);
            }

            return foundTours;
        }

        public void AddTour(Tour tour)
        {
            tour.Id = tours.Count();
            tours.Add(tour);
        }

        public void UpdateTour(Tour tour)
        {
            tours[tour.Id] = tour;
        }

        public void DeleteTour(int id)
        {
            tours.RemoveAt(id);

            foreach (Tour tour in tours)
                if (tour.Id > id)
                    tour.Id -= 1;
        }

        public void SaveChanges()
        {
            using (StreamWriter streamWriter = new StreamWriter(@"..\..\..\TravelAgency.Core\Tours.json"))
            {
                using (JsonWriter writer = new JsonTextWriter(streamWriter))
                {
                    JsonSerializer serizalizer = new JsonSerializer();
                    serizalizer.Serialize(writer, tours);
                }
            }
        }
    }
}
