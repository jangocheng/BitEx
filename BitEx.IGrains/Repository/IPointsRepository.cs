using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coin.Model.User;

namespace BitEx.IGrain.Repository
{
    public interface IPointsRepository
    {
        Task<bool> CreateAsync(MyPoints point);
        Task<List<MyPoints>> GetTopListAsync(string userid);
    }
}
