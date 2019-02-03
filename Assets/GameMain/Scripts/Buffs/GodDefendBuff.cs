//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月1日
//------------------------------------------------------------

using System;
using DefaultNamespace;
using UnityEngine;

namespace Assets.GameMain.Scripts.Buffs
{
    public class GodDefendBuff : BuffBase
    {
        public GodDefendBuff(CostumEntityLogic costumEntityLogic, float length) : base(costumEntityLogic, length)
        {
        }

        public override void OnAdd()
        {
            base.OnAdd();
            m_CostumEntityLogic.IsGodDefend = true;
            m_CostumEntityLogic.GetGodDefend();
        }


        public override void OnUpdate()
        {
            base.OnUpdate();
            timer += Time.fixedDeltaTime;
            if (timer >= m_Length)
            {
                m_CostumEntityLogic.IsGodDefend = false;
                m_CostumEntityLogic.LoseGodDefend();
                m_CostumEntityLogic.RemoveBuff(this);
            }
        }

        public override void OnRemove()
        {
            base.OnRemove();
            GC.Collect();
        }
    }
}