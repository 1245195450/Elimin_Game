//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月28日
//------------------------------------------------------------

using GameFramework.Event;

namespace Assets.GameMain.Scripts.GameArgs
{
    /// <summary>
    /// 得到道具事件
    /// </summary>
    public class GetToolsArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(GetToolsArgs).GetHashCode();

        /// <summary>
        /// 道具名称
        /// </summary>
        public string m_ToolName;

        /// <summary>
        /// 持有者名称
        /// </summary>
        public string m_OwnerName;

        public override void Clear()
        {
            m_ToolName = null;
            m_OwnerName = null;
        }

        /// <summary>
        /// 填充事件
        /// </summary>
        /// <param name="m_OwnerName">持有者名称</param>
        /// <param name="toolName">道具名称</param>
        /// <returns></returns>
        public GetToolsArgs Fill(string ownerName,string toolName)
        {
            m_OwnerName = ownerName;
            m_ToolName = toolName;
            return this;
        }

        public override int Id => EventId;
    }
}