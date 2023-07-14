using System.IO;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

using PEtrial_1.DTOs;
using PEtrial_1.Models;

using static PEtrial_1.DTOs.DTO;

namespace PEtrial_1.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DirectorController : ControllerBase {

        private PE_PRN_Fall22B1Context db = new PE_PRN_Fall22B1Context();
        private readonly IMapper mapper;

        public DirectorController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet("{nationality}/{gender}")]
        public IActionResult GetDirectors(string nationality, string gender)
        {
            try
            {
                bool male = gender == "male" ? true : false;
                List<Director> list = db.Directors.Where(x => x.Nationality == nationality && x.Male == male).ToList();
                List<DirectorDTO> res = mapper.Map<List<DirectorDTO>>(list);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public DirectorDTO GetDirector(int id)
        {
            Director? director = db.Directors.Include(x => x.Movies).ThenInclude(x => x.Producer).FirstOrDefault(x => x.Id == id);
            DirectorDTO directorDTO = mapper.Map<DirectorDTO>(director);
            if(director == null)
            {
                return null;
            }
            return directorDTO;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetAllDirector()
        {
            List<Director> director = db.Directors.Include(x => x.Movies).ThenInclude(x => x.Producer).ToList();
            List<DirectorDTO> directorDTO = mapper.Map<List<DirectorDTO>>(director);
            if (director == null)
            {
                return null;
            }
            return Ok(directorDTO);
        }



        [HttpPost]
        public IActionResult Create(CreateDirectorDto director)
        {
            try
            {
                Director d = mapper.Map<Director>(director);
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
