//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月26日
//------------------------------------------------------------

using GameFramework.DataTable;
using GameMain.Scripts.DataTable;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.Entities.EntityData
{
    public class MapItemsDatas : EntityData
    {
        public MapItemsDatas(int entityId, int typeId) : base(entityId, typeId)
        {
            IDataTable<DREntity> m_GroupTable = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity m_GroupData = m_GroupTable.GetDataRow(typeId);
            AssetName = m_GroupData.AssetName;
            GroupName = m_GroupData.GroupName;
            m_tag = m_GroupData.tag;
        }
    }
}