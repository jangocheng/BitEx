using ProtoBuf;

namespace BitEx.IGrain.Entity.Cms
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class AssetsApplyVote
    {
        public int ApplyId { get; set; }
        public VoteItem Item { get; set; }
    }
    public enum VoteItem
    {
        /// <summary>
        /// 赞
        /// </summary>
        Praise = 0,
        /// <summary>
        /// 贬
        /// </summary>
        Belittle = 1
    }
}
