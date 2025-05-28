using MangoApi.Interfaces;
using MangoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangoApi.Controllers
{
    
        [ApiController]
        [Route("api/Passinglvl")]
        public class PassinglvlController : ControllerBase
        {
            private readonly IPassinglvl _Passinglvlervice;

            public PassinglvlController(IPassinglvl Passinglvlervice)
            {
                _Passinglvlervice = Passinglvlervice;
            }

            // GET: api/Passinglvl
            [HttpGet]
            public IActionResult GetPassinglvl()
            {
                var Passinglvl = _Passinglvlervice.GetAllPassinglvls();
                return Ok(Passinglvl);
            }

            // GET: api/Passinglvl/{id}
            [HttpGet("{id}")]
            public IActionResult GetPassinglvl(int id)
            {
                var Passinglvl = _Passinglvlervice.GetPassinglvlById(id);
                if (Passinglvl == null)
                {
                    return NotFound();
                }
                return Ok(Passinglvl);
            }


            [HttpGet("titles/{title}")]
            public IActionResult GetPassinglvlUserId(int user_id, int lvl_id)
            {
                var Passinglvl = _Passinglvlervice.GetPassinglvlByUserId(user_id, lvl_id);
                if (Passinglvl == null)
                {
                    return NotFound();
                }
                return Ok(Passinglvl);
            }

            // POST: api/Passinglvl
            [HttpPost]
            public IActionResult AddPassinglvl([FromBody] Passinglvl Passinglvl)
            {
                if (Passinglvl == null)
                {
                    return BadRequest("Invalid Passinglvl data.");
                }

                var addedPassinglvl = _Passinglvlervice.AddPassinglvl(Passinglvl);
                return CreatedAtAction(nameof(GetPassinglvl), new { id = addedPassinglvl.Id }, addedPassinglvl);
            }

            // PUT: api/Passinglvl/{id}
            [HttpPut("{id}")]
            public IActionResult UpdatePassinglvl(int id, [FromBody] Passinglvl updatedPassinglvl)
            {
                var Passinglvl = _Passinglvlervice.UpdatePassinglvl(id, updatedPassinglvl);
                if (Passinglvl == null)
                {
                    return NotFound();
                }

                return NoContent();
            }

            // DELETE: api/Passinglvl/{id}
            [HttpDelete("{id}")]
            public IActionResult DeletePassinglvl(int id)
            {
                _Passinglvlervice.DeletePassinglvl(id);
                return NoContent();
            }
        }
    
}
