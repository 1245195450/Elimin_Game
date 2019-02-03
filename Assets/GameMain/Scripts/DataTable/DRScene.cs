//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月25日
//------------------------------------------------------------

using GameFramework;
using UnityGameFramework.Runtime;

namespace Assets.GameMain.Scripts.DataTable
{
    /// <summary>
    /// 场景表
    /// </summary>
    public class DRScene:DataRowBase
    {
        private int m_Id;

        /// <summary>
        /// ID
        /// </summary>
        public override int Id => m_Id;

        /// <summary>
        /// 资源名称
        /// </summary>
        public string AssetName { get; set; }
        
        public override bool ParseDataRow(GameFrameworkSegment<string> dataRowSegment)
        {
            string[] text = DataTableExtension.SplitDataRow(dataRowSegment);
            int index = 0;
            index++;
            m_Id = int.Parse(text[index++]);
            index++;
            AssetName = text[index++];
            return true;
        }
    }
}