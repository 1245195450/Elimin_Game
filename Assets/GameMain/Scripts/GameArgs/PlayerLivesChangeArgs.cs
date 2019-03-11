//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月3日
//------------------------------------------------------------

using GameFramework.Event;

namespace GameMain.Scripts.GameArgs
{
    public class PlayerLivesChangeArgs:GameEventArgs
    {
        public static readonly int EventId = typeof(UpSkillArgs).GetHashCode();

        /// <summary>
        /// P1为玩家一,P2为玩家2
        /// </summary>
        public string playerFlag;
        
        /// <summary>
        /// 改变的值,1为增加,-1为死亡
        /// </summary>
        public int changeValue;

        public PlayerLivesChangeArgs Fill(string flag,int value)
        {
            playerFlag = flag;
            changeValue = value;
            return this;
        }
        
        public override void Clear()
        {
            playerFlag = null;
            changeValue = 0;
        }

        public override int Id => EventId;
    }
}