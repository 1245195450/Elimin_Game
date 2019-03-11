//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月2日
//------------------------------------------------------------

using GameFramework.Event;

namespace GameMain.Scripts.GameArgs
{
    public class GameOverArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(GameOverArgs).GetHashCode();

        /// <summary>
        /// 是否是通过完成任务来让游戏结束的
        /// </summary>
        public bool m_PassGame;

        public GameOverArgs Fill(bool passGame)
        {
            m_PassGame = passGame;
            return this;
        }

        public override void Clear()
        {
            m_PassGame = false;
        }

        public override int Id => EventId;
    }
}