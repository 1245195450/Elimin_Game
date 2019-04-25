//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月1日
//------------------------------------------------------------

using System.Collections.Generic;
using GameMain.Scripts.Buffs;

namespace GameMain.Scripts.Entities.EntityLogic
{
    /// <summary>
    /// 为了添加Buff设计的自定义EntityLogic
    /// </summary>
    public class CostumEntityLogic : UnityGameFramework.Runtime.EntityLogic
    {
        public List<BuffBase> m_Buffs = new List<BuffBase>();

        /// <summary>
        /// 被冰冻
        /// </summary>
        public bool IsFreeze { get; set; }

        /// <summary>
        /// 是否处于无敌状态
        /// </summary>
        public bool IsGodDefend { get; set; }


        /// <summary>
        /// 得到无敌状态
        /// </summary>
        public virtual void GetGodDefend()
        {
            IsGodDefend = true;
        }

        /// <summary>
        /// 失去无敌状态
        /// </summary>
        public virtual void LoseGodDefend()
        {
        }

        /// <summary>
        /// 得到钢铁之心状态
        /// </summary>
        public virtual void GetSteelHeart()
        {
        }
        
        /// <summary>
        /// 失去钢铁之心状态
        /// </summary>
        public virtual void LoseSteelHeart()
        {
        }
        
        public void AddBuff(BuffBase buffNeed2Add)
        {
            m_Buffs.Add(buffNeed2Add);
            buffNeed2Add.OnAdd();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            ReFreshBuff();
        }

        public void ReFreshBuff()
        {
            for (int i = m_Buffs.Count - 1; i >= 0; i--)
            {
                m_Buffs[i].OnUpdate();
            }
        }

        public void RemoveBuff(BuffBase buffNeed2Remove)
        {
            m_Buffs.Remove(buffNeed2Remove);
            buffNeed2Remove.OnRemove();
        }
    }
}