namespace SignalRVotingPoll.Hubs.Models
{
    using SignalRVotingPoll.Common.Enums;

    public class VoteResult
    {
        public string User { get; set; }

        public FootballClub VoteChoice { get; set; }

        public string SelectedTeam { get; set; }
    }
}
