using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Labb3.Models;

namespace API_Labb3.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // GET: api/Persons/5/Interests
        [HttpGet("{id}/Interests")]
        public async Task<ActionResult<IEnumerable<Interest>>> GetPersonInterests(int id)
        {
            var person = await _context.Persons.Include(p => p.Links).ThenInclude(l => l.Interest).FirstOrDefaultAsync(p => p.PersonId == id);

            if (person == null)
            {
                return NotFound();
            }

            return person.Links.Select(l => l.Interest).ToList();
        }

        // GET: api/Persons/5/Links
        [HttpGet("{id}/Links")]
        public async Task<ActionResult<IEnumerable<Link>>> GetPersonLinks(int id)
        {
            var person = await _context.Persons.Include(p => p.Links).FirstOrDefaultAsync(p => p.PersonId == id);

            if (person == null)
            {
                return NotFound();
            }

            return person.Links;
        }

        // POST: api/Persons/5/Interests
        [HttpPost("{id}/Interests")]
        public async Task<IActionResult> AddInterestToPerson(int id, Interest interest)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            var existingInterest = await _context.Interests.FindAsync(interest.InterestId);
            if (existingInterest == null)
            {
                return NotFound();
            }

            var link = new Link
            {
                PersonId = id,
                InterestId = interest.InterestId
            };

            _context.Links.Add(link);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Persons/5/Links
        [HttpPost("{id}/Links")]
        public async Task<IActionResult> AddLinkToPerson(int id, Link link)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            link.PersonId = id;
            _context.Links.Add(link);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Persons
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerson), new { id = person.PersonId }, person);
        }

        // PUT: api/Persons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.PersonId)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.PersonId == id);
        }
    }
}
