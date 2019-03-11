//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月3日
//------------------------------------------------------------

using GameFramework.Event;

namespace GameMain.Scripts.GameArgs
{
    /// <summary>
    /// 任务事件
    /// </summary>
    public class MessionTouchArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(MessionTouchArgs).GetHashCode();


        public override void Clear()
        {
        }

        /// <summary>
        /// 填充事件
        /// </summary>
        public MessionTouchArgs Fill()
        {
            return this;
        }

        public override int Id => EventId;
    }
}