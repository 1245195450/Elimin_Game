//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月24日
//------------------------------------------------------------

using Assets.GameMain.Scripts.Entities.EntityData;

namespace Assets.GameMain.Scripts.ProfileMessage
{
    /// <summary>
    /// 用来存档的临时数据类
    /// </summary>
    public static class TempData
    {
        public static PlayerDataModel m_P1Model = new PlayerDataModel(0.7f, 1, 3, 1, 1, 1, 1);
        public static PlayerDataModel m_P2Model = new PlayerDataModel(0.7f, 1, 3, 1, 1, 1, 1);
        public static int Gold = 2000;
        /// <summary>
        /// 玩到第几关了
        /// </summary>
        public static int Level = 1;
    }
}