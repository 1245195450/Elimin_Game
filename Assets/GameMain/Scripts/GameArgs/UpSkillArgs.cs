//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月2日
//------------------------------------------------------------

using GameFramework.Event;

namespace GameMain.Scripts.GameArgs
{
    public class UpSkillArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(UpSkillArgs).GetHashCode();

        /// <summary>
        /// 花费的金钱
        /// </summary>
        public int m_Price;

        public UpSkillArgs Fill(int price)
        {
            m_Price = price;
            return this;
        }
        
        public override void Clear()
        {
            m_Price = 0;
        }

        public override int Id => EventId;
    }
}