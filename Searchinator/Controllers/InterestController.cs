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

        public InterestController(IInterestRepository interestRepository)
        {
            this.interestRepository = interestRepository;
        }

        [Route("")]
        [HttpPost]
        [HttpPut]
        public IActionResult SaveInterest(Interest interest)
        {
            var savedInterest = this.interestRepository.SaveInterest(interest);
            return this.Ok(savedInterest);
        }
    }
}