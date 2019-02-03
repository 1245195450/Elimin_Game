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
    /// <summary>
    /// 家园无敌Buff
    /// </summary>
    public class SteelHeartBuff : BuffBase
    {
        public SteelHeartBuff(CostumEntityLogic costumEntityLogic, float length) : base(costumEntityLogic, length)
        {
        }

        public override void OnAdd()
        {
            base.OnAdd();
            m_CostumEntityLogic.GetSteelHeart();
        }


        public override void OnUpdate()
        {
            base.OnUpdate();
            if (timer >= m_Length)
            {
                m_CostumEntityLogic.RemoveBuff(this);
                m_CostumEntityLogic.LoseSteelHeart();
            }

            timer += Time.fixedDeltaTime;
        }

        public override void OnRemove()
        {
            base.OnRemove();
            GC.Collect();
        }
    }
}