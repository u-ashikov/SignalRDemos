namespace SignalRVotingPoll.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Vote;

    public class VoteController : Controller
    {
        public IActionResult Index()
        {
            var model = new VoteFormModel()
            {
                Question = "Which is your favorite football team?"
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Index2(VoteFormModel model)
        {
            return null;
        }
    }
}
