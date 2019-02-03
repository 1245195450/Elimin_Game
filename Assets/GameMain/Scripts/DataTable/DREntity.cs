//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月12日 20:56:13
//------------------------------------------------------------

using GameFramework;
using UnityGameFramework.Runtime;

namespace Assets.GameMain.Scripts.DataTable
{
    /// <summary>
    /// 实体表。
    /// </summary>
    public class DREntity : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 实体编号。
        /// </summary>
        public override int Id
        {
            get { return m_Id; }
        }

        /// <summary>
        /// 资源名称。
        /// </summary>
        public string AssetName { get; private set; }

        /// <summary>
        /// 资源组名称
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// tag标识
        /// </summary>
        public string tag { get; set; }

        public override bool ParseDataRow(GameFrameworkSegment<string> dataRowSegment)
        {
            string[] text = DataTableExtension.SplitDataRow(dataRowSegment);
            int index = 0;
            index++;
            m_Id = int.Parse(text[index++]);
            index++;
            AssetName = text[index++];
            GroupName = text[index++];
            tag = text[index++];
            return true;
        }
    }
}