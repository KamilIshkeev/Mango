using MangoApi.Interfaces;
using MangoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangoApi.Controllers
{
    
        [ApiController]
        [Route("api/Levels")]
        public class LevelsController : ControllerBase
        {
            private readonly ILevels _LevelsService;

            public LevelsController(ILevels LevelsService)
            {
                _LevelsService = LevelsService;
            }

            // GET: api/Levelss
            [HttpGet]
            public IActionResult GetLevels()
            {
                var Levelss = _LevelsService.GetAllLevels();
                return Ok(Levelss);
            }

            // GET: api/Levelss/{id}
            [HttpGet("{id}")]
            public IActionResult GetLevels(int id)
            {
                var Levels = _LevelsService.GetLevelsById(id);
                if (Levels == null)
                {
                    return NotFound();
                }
                return Ok(Levels);
            }


            [HttpGet("titles/{title}")]
            public IActionResult GetLevelsTitle(string title)
            {
                var Levels = _LevelsService.GetLevelsByTitle(title);
                if (Levels == null)
                {
                    return NotFound();
                }
                return Ok(Levels);
            }

            // POST: api/Levelss
            [HttpPost]
            public IActionResult AddLevels([FromBody] Levels Levels)
            {
                if (Levels == null || string.IsNullOrEmpty(Levels.Name))
                {
                    return BadRequest("Invalid Levels data.");
                }

                var addedLevels = _LevelsService.AddLevels(Levels);
                return CreatedAtAction(nameof(GetLevels), new { id = addedLevels.Id }, addedLevels);
            }

            // PUT: api/Levelss/{id}
            [HttpPut("{id}")]
            public IActionResult UpdateLevels(int id, [FromBody] Levels updatedLevels)
            {
                var Levels = _LevelsService.UpdateLevels(id, updatedLevels);
                if (Levels == null)
                {
                    return NotFound();
                }

                return NoContent();
            }

            // DELETE: api/Levelss/{id}
            [HttpDelete("{id}")]
            public IActionResult DeleteLevels(int id)
            {
                _LevelsService.DeleteLevels(id);
                return NoContent();
            }
        }
    
}
