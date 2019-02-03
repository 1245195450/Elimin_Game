//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月1日
//------------------------------------------------------------

namespace Assets.GameMain.Scripts.ProfileMessage
{
    /// <summary>
    /// 玩家数据模型
    /// </summary>
    public class PlayerDataModel
    {
        /// <summary>
        /// 攻速
        /// </summary>
        public float AttackCD;

        /// <summary>
        /// 自身等级
        /// </summary>
        public int Level;

        /// <summary>
        /// 命
        /// </summary>
        public int Lives;

        /// <summary>
        /// 无敌等级
        /// </summary>
        public int HGLv;

        /// <summary>
        /// 钢铁之心等级
        /// </summary>
        public int SteelHLv;

        /// <summary>
        /// 时间暂停等级
        /// </summary>
        public int TPLv;

        /// <summary>
        /// 全屏轰炸等级
        /// </summary>
        public int ScreenHLv;

        public PlayerDataModel(float attackCD, int level, int lives, int hGLv, int steelHLv, int tpLv, int screenHLv)
        {
            AttackCD = attackCD;
            Level = level;
            Lives = lives;
            HGLv = hGLv;
            SteelHLv = steelHLv;
            TPLv = tpLv;
            ScreenHLv= screenHLv;
        }
    }
}