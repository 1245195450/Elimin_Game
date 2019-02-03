//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月24日
//------------------------------------------------------------

using Assets.GameMain.Scripts.Base;
using Assets.GameMain.Scripts.DataTable;
using GameFramework;
using GameFramework.DataTable;

namespace Assets.GameMain.Scripts.Entities.EntityData
{
    /// <summary>
    /// 敌方坦克信息
    /// </summary>
    public class EnemyTankData : EntityData
    {
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 被击中是否会掉落道具
        /// </summary>
        public bool HasSp { get; set; }

        /// <summary>
        /// 攻速
        /// </summary>
        public int AttackCD { get; set; }

        /// <summary>
        /// 最高等级
        /// </summary>
        public int MaxLevel { get; set; }

        public EnemyTankData(int entityId, int typeId) : base(entityId, typeId)
        {
            IDataTable<DREnemyTank> m_EnemyTankTable = GameEntry.DataTable.GetDataTable<DREnemyTank>();
            DREnemyTank enemyTankData = m_EnemyTankTable.GetDataRow(typeId);
            IDataTable<DREntity> m_GroupTable = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity m_GroupData = m_GroupTable.GetDataRow(typeId);
            AssetName = m_GroupData.AssetName;
            GroupName = m_GroupData.GroupName;
            m_tag = m_GroupData.tag;
            MaxLevel = enemyTankData.MaxLevel;
            HasSp = enemyTankData.HasSp;
            AttackCD = enemyTankData.AttackCD;
        }
    }
}