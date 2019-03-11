//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月31日
//------------------------------------------------------------

using GameFramework.Event;

namespace GameMain.Scripts.GameArgs
{
    /// <summary>
    /// 释放技能事件
    /// </summary>
    public class SkillReleaseArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(SkillReleaseArgs).GetHashCode();

        /// <summary>
        /// 道具名称
        /// </summary>
        public string m_ToolName;

        public override void Clear()
        {
            m_ToolName = null;
        }

        /// <summary>
        /// 填充事件
        /// </summary>
        /// <param name="toolName">道具名称</param>
        /// <returns></returns>
        public SkillReleaseArgs Fill(string toolName)
        {
            m_ToolName = toolName;
            return this;
        }

        public override int Id => EventId;
    }
}