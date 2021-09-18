using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace DioMongo.Api.Data.Collections
{
    public class Infected
    {
        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public GeoJson2DGeographicCoordinates Location { get; set; }

        public Infected(
            DateTime birthday,
            string gender,
            double longitude,
            double latitude
        )
        {
            this.Birthday = birthday;
            this.Gender = gender;
            this.Location =
                new GeoJson2DGeographicCoordinates(longitude, latitude);
        }
    }
}
