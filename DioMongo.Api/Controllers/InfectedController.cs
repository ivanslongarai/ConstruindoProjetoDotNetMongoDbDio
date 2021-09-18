using System;
using DioMongo.Api.Data.Collections;
using DioMongo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DioMongo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectedController : ControllerBase
    {
        DioMongo.Api.Data.MongoDB _mongoDB;

        IMongoCollection<Infected> _infectedCollection;

        public InfectedController(DioMongo.Api.Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectedCollection =
                _mongoDB
                    .DB
                    .GetCollection<Infected>(typeof (Infected).Name.ToLower());
        }

        [HttpPost]
        public ActionResult InsertInfected([FromBody] InfectedDTO dto)
        {
            var infected =
                new Infected(dto.Birthday,
                    dto.Gender,
                    dto.Latitude,
                    dto.Longitude);

            _infectedCollection.InsertOne (infected);

            return StatusCode(201, "Infected added successfully");
        }

        [HttpGet]
        public ActionResult GetInfectedList()
        {
            var infected =
                _infectedCollection
                    .Find(Builders<Infected>.Filter.Empty)
                    .ToList();

            return Ok(infected);
        }

        [HttpPut]
        public ActionResult
        UpdateInfected([FromBody] InfectedUpdateGenderDTO dto)
        {
            var infected = new Infected(dto.Birthday, dto.Gender, 0, 0);

            _infectedCollection
                .UpdateOne(Builders<Infected>
                    .Filter
                    .Where(i => i.Birthday == dto.Birthday),
                Builders<Infected>.Update.Set("gender", dto.Gender));

            return Ok("Updated successfully");
        }

        [HttpDelete("{birthday}")]
        public ActionResult DeleteInfected(DateTime birthday)
        {
            _infectedCollection
                .DeleteOne(Builders<Infected>
                    .Filter
                    .Where(i => i.Birthday == Convert.ToDateTime(birthday)));

            return Ok("Deleted successfully");
        }
    }
}
