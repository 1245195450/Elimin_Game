//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月25日
//------------------------------------------------------------

using Assets.GameMain.Scripts.Base;
using Assets.GameMain.Scripts.DataTable;
using GameFramework;
using GameFramework.DataTable;
using UnityEngine;

namespace Assets.GameMain.Scripts.Entities.EntityData
{
    [SerializeField]
    public class BullectData : EntityData
    {
        /// <summary>
        /// 伤害
        /// </summary>
        public int Damage;

        /// <summary>
        /// 速度
        /// </summary>
        public int Speed;


        /// <summary>
        /// 子弹声音编号
        /// </summary>
        public int MusicID;

        public BullectData(int entityId, int typeId, int ownerLevel = 0) : base(entityId, typeId)
        {
            IDataTable<DRBullect> m_bulLectDataTable = GameEntry.DataTable.GetDataTable<DRBullect>();
            DRBullect m_bullectData = m_bulLectDataTable.GetDataRow(typeId);
            IDataTable<DREntity> m_GroupTable = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity m_GroupData = m_GroupTable.GetDataRow(typeId);
            AssetName = m_GroupData.AssetName;
            GroupName = m_GroupData.GroupName;
            m_tag = m_GroupData.tag;
            //缺省为0,代表为敌人子弹,伤害为1
            if (ownerLevel == 0)
            {
                Damage = m_bullectData.Damage;
            }
            else
            {
                Damage = ownerLevel;
            }
            Speed = m_bullectData.Speed;
            MusicID = m_bullectData.MusicID;
        }
    }
}