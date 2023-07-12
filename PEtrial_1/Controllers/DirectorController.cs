using System.IO;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PEtrial_1.Models;

namespace PEtrial_1.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DirectorController : ControllerBase {

        private PE_PRN_Fall22B1Context db = new PE_PRN_Fall22B1Context();

        [HttpGet("{nationality}/{gender}")]
        public dynamic GetDirectors(string nationality, string gender)
        {
            bool male = gender == "male" ? true : false;
            var list = db.Directors.Where(x => x.Nationality == nationality && x.Male == male).Select(x =>
            new {
                id = x.Id,
                fullName = x.FullName,
                gender = x.Male ? "Male" : "Female",
                dob = x.Dob,
                dobString = x.Dob.ToString("M/d/yyyy"),
                nationality = x.Nationality,
                description = x.Description
            }).ToList();
            return list;
        }


        [HttpGet("{id}")]
        public dynamic GetDirector(int id)
        {
            var director = db.Directors.Include(x => x.Movies).Select(x =>
            new {
                id = x.Id,
                fullName = x.FullName,
                gender = x.Male ? "Male" : "Female",
                dob = x.Dob,
                dobString = x.Dob.ToString("M/d/yyyy"),
                nationality = x.Nationality,
                description = x.Description,
                movies = x.Movies.Select(x => new
                {
                    id = x.Id,
                    title = x.Title,
                    releaseDate = x.ReleaseDate,
                    description = x.Description,
                    language = x.Language,
                    producerId = x.ProducerId,
                    directorId = x.DirectorId,
                    genres = x.Genres,
                    stars = x.Stars,
                    releaseYear = x.ReleaseDate.GetValueOrDefault().Year,
                    producerName = x.Producer.Name,
                    directorName = x.Director.FullName,
                })
            }).FirstOrDefault(x => x.id == id);
            if(director == null)
            {
                return null;
            }
            return director;
        }

        public class DirectorDTO
        {
            public string fullName { get; set; }
            public bool male { get; set; }
            public string dob { get; set; }
            public string nationality { get; set; }
            public string description { get; set; }
        }

        [HttpPost]
        public IActionResult Create(DirectorDTO director)
        {
            try
            {
                Director d = new Director()
                {
                    Id = 7,
                    FullName = director.fullName,
                    Male = director.male,
                    Dob = Convert.ToDateTime(director.dob),
                    Nationality = director.nationality,
                    Description = director.description,
                };
                db.Directors.Add(d);
                db.SaveChanges();
                return Ok(1);
            }
            catch (Exception ex)
            {
                return StatusCode(409, "loi");
            }
        }
    }
}
