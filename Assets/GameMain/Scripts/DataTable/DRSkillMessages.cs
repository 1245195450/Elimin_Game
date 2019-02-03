//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月2日
//------------------------------------------------------------

using Boo.Lang;
using GameFramework;
using UnityGameFramework.Runtime;

namespace Assets.GameMain.Scripts.DataTable
{
    public class DRSkillMessages : DataRowBase
    {
        public override int Id => m_Id;

        public int m_Id;
        
        /// <summary>
        /// 技能描述
        /// </summary>
        public List<string> m_Describe = new List<string>();
        
        /// <summary>
        /// 对应属性
        /// </summary>
        public List<string> m_Attribute { get; set; }
        
        
        public override bool ParseDataRow(GameFrameworkSegment<string> dataRowSegment)
        {
            string[] text = DataTableExtension.SplitDataRow(dataRowSegment);
            int index = 0;
            index++;
            m_Id = int.Parse(text[index++]);
            index++;
            m_Describe.Add(text[index++]);
            m_Describe.Add(text[index++]);
            m_Describe.Add(text[index++]);
            m_Describe.Add(text[index++]);
            m_Describe.Add(text[index++]);
            return true;
        }
    }
}