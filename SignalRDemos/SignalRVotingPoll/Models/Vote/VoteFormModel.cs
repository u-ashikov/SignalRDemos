namespace SignalRVotingPoll.Models.Vote
{
    using System.ComponentModel;

    public class VoteFormModel
    {
        public string User { get; set; }

        [ReadOnly(true)]
        public string Question { get; set; }

        public int Choice { get; set; }
    }
}
