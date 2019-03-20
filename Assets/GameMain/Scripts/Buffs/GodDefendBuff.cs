//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月1日
//------------------------------------------------------------

using System;
using GameFramework;
using GameMain.Scripts.Entities.EntityLogic;
using UnityEngine;

namespace GameMain.Scripts.Buffs
{
    public class GodDefendBuff : BuffBase
    {
        public GodDefendBuff(CostumEntityLogic costumEntityLogic, BuffKind buffKind, float length) : base(
            costumEntityLogic, buffKind, length)
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
            BuffPoolManager.instance.m_GodBuffPool.Unspawn(this);
            GameFrameworkLog.Info("啊，我被回收了");
        }

    }
}