//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月24日
//------------------------------------------------------------

using Assets.GameMain.Scripts.Base;
using Assets.GameMain.Scripts.DataTable;
using GameFramework.DataTable;

namespace Assets.GameMain.Scripts.Entities.EntityData
{
    /// <summary>
    /// 道具信息
    /// </summary>
    public class ToolsData : EntityData
    {
        /// <summary>
        /// 升级
        /// </summary>
        public bool LvUp { get; set; }

        /// <summary>
        /// 无敌
        /// </summary>
        public bool GT { get; set; }

        /// <summary>
        /// 钢铁之心
        /// </summary>
        public bool SteelHeart { get; set; }

        /// <summary>
        /// 时间暂停
        /// </summary>
        public bool TF { get; set; }

        /// <summary>
        /// 全屏杀伤
        /// </summary>
        public bool ScreenH { get; set; }

        /// <summary>
        /// 加一命
        /// </summary>
        public bool LI { get; set; }

        public ToolsData(int entityId, int typeId) : base(entityId, typeId)
        {
            IDataTable<DRTools> m_toolsTable = GameEntry.DataTable.GetDataTable<DRTools>();
            DRTools toolsData = m_toolsTable.GetDataRow(typeId);
            IDataTable<DREntity> m_GroupTable = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity m_GroupData = m_GroupTable.GetDataRow(typeId);
            AssetName = m_GroupData.AssetName;
            GroupName = m_GroupData.GroupName;
            m_tag = m_GroupData.tag;
            LvUp = toolsData.LvUp;
            GT = toolsData.GT;
            SteelHeart = toolsData.SteelHeart;
            TF = toolsData.TF;
            ScreenH = toolsData.ScreenH;
            LI = toolsData.LI;
        }
    }
}