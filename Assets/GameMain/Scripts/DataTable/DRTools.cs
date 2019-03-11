//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月24日
//------------------------------------------------------------

using GameFramework;
using UnityGameFramework.Runtime;

namespace GameMain.Scripts.DataTable
{
    /// <summary>
    /// 道具表
    /// </summary>
    public class DRTools : DataRowBase
    {
        /// <summary>
        /// ID
        /// </summary>
        private int m_ID = 0;

        public override int Id => m_ID;

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
        
        public override bool ParseDataRow(GameFrameworkSegment<string> dataRowSegment)
        {
            string[] text = DataTableExtension.SplitDataRow(dataRowSegment);
            int index = 0;
            index++;
            m_ID = int.Parse(text[index++]);
            index++;
            LvUp = bool.Parse(text[index++]);
            GT = bool.Parse(text[index++]);
            SteelHeart = bool.Parse(text[index++]);
            TF = bool.Parse(text[index++]);
            ScreenH = bool.Parse(text[index++]);
            LI = bool.Parse(text[index++]);
            return true;
        }
    }
}