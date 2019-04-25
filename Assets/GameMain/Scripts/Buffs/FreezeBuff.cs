//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月1日
//------------------------------------------------------------

using System;
using GameFramework;
using GameMain.Scripts.Base;
using GameMain.Scripts.Entities.EntityLogic;
using UnityEngine;

namespace GameMain.Scripts.Buffs
{
    /// <summary>
    /// 冰冻Buff
    /// </summary>
    public class FreezeBuff : BuffBase
    {
        public override void OnAdd()
        {
            base.OnAdd();
            m_CostumEntityLogic.IsFreeze = true;
        }


        public override void OnUpdate()
        {
            base.OnUpdate();
            if (timer >= m_Length)
            {
                m_CostumEntityLogic.IsFreeze = false;
                m_CostumEntityLogic.RemoveBuff(this);
            }

            timer += Time.fixedDeltaTime;
        }

        public override void OnRemove()
        {
            base.OnRemove();
            ReferencePool.Release(this);
        }
    }
}