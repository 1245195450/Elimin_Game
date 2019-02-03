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
    /// 子弹表
    /// </summary>
    public class DRBullect : DataRowBase
    {
        private int m_ID = 0;

        /// <summary>
        /// ID
        /// </summary>
        public override int Id => m_ID;

        /// <summary>
        /// 伤害
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// 子弹速度
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// 子弹声音编号
        /// </summary>
        public int MusicID { get; set; }


        public override bool ParseDataRow(GameFrameworkSegment<string> dataRowSegment)
        {
            string[] text = DataTableExtension.SplitDataRow(dataRowSegment);
            int index = 0;
            index++;
            m_ID = int.Parse(text[index++]);
            index++;
            Damage = int.Parse(text[index++]);
            Speed = int.Parse(text[index++]);
            MusicID = int.Parse(text[index++]);
            return true;
        }
    }
}