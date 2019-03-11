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
    /// 敌人表
    /// </summary>
    public class DREnemyTank : DataRowBase
    {
        private int m_ID = 0;

        /// <summary>
        /// ID
        /// </summary>
        public override int Id => m_ID;


        /// <summary>
        /// 被击中是否会掉落道具
        /// </summary>
        public bool HasSp { get; set; }
        
        /// <summary>
        /// 攻速
        /// </summary>
        public int AttackCD { get; set; }
        
        /// <summary>
        /// 最高等级
        /// </summary>
        public int MaxLevel { get; set; }

        public override bool ParseDataRow(GameFrameworkSegment<string> dataRowSegment)
        {
            string[] text = DataTableExtension.SplitDataRow(dataRowSegment);
            int index = 0;
            index++;
            m_ID = int.Parse(text[index++]);
            index++;
            HasSp = bool.Parse(text[index++]);
            AttackCD = int.Parse(text[index++]);
            MaxLevel = int.Parse(text[index++]);
            return true;
        }
    }
}