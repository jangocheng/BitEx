using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitEx.IGrain.Actors.Base
{
    public interface INodeManager : IGrainWithStringKey
    {
        /// <summary>
        /// 初始化节点管理器
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        Task Init(List<string> nodeList);
        /// <summary>
        /// 获取一个节点名称作为当前节点的名称
        /// </summary>
        /// <returns></returns>
        Task<string> GetNodeName();
        /// <summary>
        /// 保持节点活动状态(超时节点会被回收)
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns>是否续活成功</returns>
        Task<bool> KeepLive(string nodeName);
    }
}
