using BitEx.Core;
using BitEx.Core.Utils;
using Ray.RabbitMQ;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitEx.Framework.Notice
{
    public class NoticeHub
    {
        public const string Exchange = "BitEx.Message";
        public const string Queue = "Notice";
        public const int QueueCount = 3;
        static RabbitPubAttribute mqInfo;
        public static RabbitPubAttribute NoticeMQPublisher
        {
            get
            {
                if (mqInfo == null)
                {
                    mqInfo = new RabbitPubAttribute(Exchange, Queue, QueueCount);
                    mqInfo.Init();
                }
                return mqInfo;
            }
        }
        public static Task Send(NoticeTplKey key, Dictionary<string, string> values, Lang lang, string emailAddress, string targetId = null)
        {
            var data = new NoticeInfo()
            {
                Id = OGuid.GenerateNewId().ToString(),
                Key = key.ToString(),
                Values = values,
                LangType = lang,
                TargetId = targetId,
                EmailAddress = emailAddress
            };
            return RabbitMQClient.Publish(data, mqInfo.Exchange, mqInfo.GetQueue(key.ToString()), false);
        }
    }
}
