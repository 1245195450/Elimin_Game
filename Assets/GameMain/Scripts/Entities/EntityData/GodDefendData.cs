//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月30日
//------------------------------------------------------------

using GameFramework.DataTable;
using GameMain.Scripts.DataTable;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.Entities.EntityData
{
    public class GodDefendData : EntityData
    {
        /// <summary>
        /// 持有者ID
        /// </summary>
        public int OwnerId;

        public GodDefendData(int entityId, int typeId, int ownerId) : base(entityId, typeId)
        {
            IDataTable<DREntity> m_GroupTable = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity m_GroupData = m_GroupTable.GetDataRow(typeId);
            AssetName = m_GroupData.AssetName;
            GroupName = m_GroupData.GroupName;
            m_tag = m_GroupData.tag;
            OwnerId = ownerId;
        }
    }
}