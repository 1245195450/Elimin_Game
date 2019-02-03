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
    /// UI表
    /// </summary>
    public class DRUIForm:DataRowBase
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
        
        /// <summary>
        /// 资源组名称
        /// </summary>
        public string GroupName { get; set; }
        
        /// <summary>
        /// 是否允许多个界面同时存在
        /// </summary>
        public bool AllowMultiInstance { get; set; }
        
        /// <summary>
        /// 是否暂停被其覆盖的界面
        /// </summary>
        public bool PauseCoveredUIForm { get; set; }
        
        public override bool ParseDataRow(GameFrameworkSegment<string> dataRowSegment)
        {
            string[] text = DataTableExtension.SplitDataRow(dataRowSegment);
            int index = 0;
            index++;
            m_Id = int.Parse(text[index++]);
            index++;
            AssetName = text[index++];
            GroupName = text[index++];
            AllowMultiInstance = bool.Parse(text[index++]);
            PauseCoveredUIForm = bool.Parse(text[index++]);
            return true;
        }
    }
}