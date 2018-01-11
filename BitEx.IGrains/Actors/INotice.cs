using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitEx.IGrain.Entity.Notice;
using Coin.Core;
using Coin.Core.Notice;

namespace BitEx.IGrain.Actors
{
    public interface INotice : IGrainWithIntegerKey
    {
        Task Send(NoticeKey key, Dictionary<string, string> values, string targetId = null, LangType lang = LangType.ZH, NoticeType noticeType = NoticeType.All, string emailAddress = null, string phoneNumber = null, bool isVoice = false);
    }
}
