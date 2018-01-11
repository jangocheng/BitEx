using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coin.Framework.EventSourcing;

namespace BitEx.IGrain.Actors.Denormalize
{
    public interface IUserDenormalize : IDenormalize, IGrainWithStringKey
    {
        Task UpdatePoints(string userId, decimal points);
    }
}
