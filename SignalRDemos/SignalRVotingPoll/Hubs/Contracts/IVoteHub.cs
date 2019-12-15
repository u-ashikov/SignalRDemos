namespace SignalRVotingPoll.Hubs.Contracts
{
    using Models;
    using System.Threading.Tasks;

    public interface IVoteHub
    {
        Task ReceiveVotingResultAsync(VoteResult result);
    }
}
