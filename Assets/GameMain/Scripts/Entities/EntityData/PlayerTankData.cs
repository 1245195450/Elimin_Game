//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月24日
//------------------------------------------------------------

using Assets.GameMain.Scripts.Base;
using Assets.GameMain.Scripts.DataTable;
using Assets.GameMain.Scripts.ProfileMessage;
using GameFramework;
using GameFramework.DataTable;

namespace Assets.GameMain.Scripts.Entities.EntityData
{
    /// <summary>
    /// 玩家坦克信息
    /// </summary>
    public class PlayerTankData : EntityData
    {
        /// <summary>
        /// 攻速
        /// </summary>
        public float AttackCD;

        /// <summary>
        /// 自身等级
        /// </summary>
        public int Level = 1;

        /// <summary>
        /// 命
        /// </summary>
        public int Lives = 3;

        /// <summary>
        /// 无敌等级
        /// </summary>
        public int HGLv = 1;

        /// <summary>
        /// 钢铁之心等级
        /// </summary>
        public int SteelHLv = 1;

        /// <summary>
        /// 时间暂停等级
        /// </summary>
        public int TPLv = 1;

        /// <summary>
        /// 全屏轰炸等级
        /// </summary>
        public int ScreenHlV = 1;

        public PlayerTankData(int entityId, int typeId) : base(entityId, typeId)
        {
            IDataTable<DREntity> m_GroupTable = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity m_GroupData = m_GroupTable.GetDataRow(typeId);
            AssetName = m_GroupData.AssetName;
            GroupName = m_GroupData.GroupName;
            m_tag = m_GroupData.tag;
            ProfileReader.SetPlayerData(this, typeId - 9999);
        }
    }
}