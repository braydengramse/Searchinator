namespace Searchinator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Searchinator.Models;
    using Searchinator.Repositories;

    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAllPeople()
        {
            var people = this.personRepository.GetPeople();
            return this.Ok(people);
        }

        [Route("{personId}")]
        [HttpGet]
        public IActionResult GetPerson(int personId)
        {
            var person = this.personRepository.GetPerson(personId);

            if (person is null)
            {
                return this.NotFound();
            }

            return this.Ok(person);
        }

        [Route("")]
        [HttpPost]
        [HttpPut]
        public IActionResult SavePerson(Person person)
        {
            var savedPerson = this.personRepository.SavePerson(person);
            return this.Ok(savedPerson);
        }

        [Route("{searchInput}")]
        [HttpGet]
        public IActionResult SearchPeople(string searchInput)
        {
            var people = this.personRepository.SearchPeople(searchInput);

            return this.Ok(people);
        }

        [Route("{personId}")]
        [HttpDelete]
        public IActionResult DeletePerson(int personId)
        {
            this.personRepository.DeletePerson(personId);

            return this.Ok();
        }
    }
}