namespace SignalRVotingPoll.Hubs
{
    using Contracts;
    using Microsoft.AspNetCore.SignalR;
    using Models;
    using System.Threading.Tasks;

    public class VoteHub : Hub<IVoteHub>
    {
        public async Task Vote(VoteResult result)
        {
            result.SelectedTeam = result.VoteChoice.ToString();

            await this.Clients.Caller.ReceiveVotingResultAsync(result);
        }
    }
}
