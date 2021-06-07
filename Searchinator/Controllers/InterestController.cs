namespace Searchinator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Searchinator.Models;
    using Searchinator.Repositories;

    [ApiController]
    [Route("[controller]")]
    public class InterestController : ControllerBase
    {
        private readonly IInterestRepository interestRepository;

        private readonly IPersonRepository personRepository;

        public InterestController(IInterestRepository interestRepository, IPersonRepository personRepository)
        {
            this.interestRepository = interestRepository;
            this.personRepository = personRepository;
        }

        [Route("")]
        [HttpPost]
        [HttpPut]
        public IActionResult SaveInterest(Interest interest)
        {
            var personEntity = this.personRepository.GetPersonEntity(interest.PersonId);

            if (personEntity is null)
            {
                return this.BadRequest("Unable to add interest if interest is not linked to a person.");
            }

            var savedInterest = this.interestRepository.SaveInterest(interest, personEntity);
            return this.Ok(savedInterest);
        }
    }
}