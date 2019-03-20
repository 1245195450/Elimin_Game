//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年3月20日 13:45:27
//------------------------------------------------------------

using GameFramework.ObjectPool;
using GameMain.Scripts.Base;
using UnityEditor;
using UnityEngine;

namespace GameMain.Scripts.Buffs
{
    public class FreezeBuffPool:ObjectBase
    {
        public FreezeBuffPool(object target) : base(target)
        {
            
        }

        protected override void Release(bool isShutdown)
        {
            FreezeBuff freezeBuff = (FreezeBuff) Target;
            if (freezeBuff == null)
            {
                return;
            }

            freezeBuff = null;
        }
    }
    
    public class GodBuffPool:ObjectBase
    {
        public GodBuffPool(object target) : base(target)
        {
            
        }

        protected override void Release(bool isShutdown)
        {
            GodDefendBuff godDefendBuff = (GodDefendBuff) Target;
            if (godDefendBuff == null)
            {
                return;
            }

            godDefendBuff = null;
        }
    }
    
    public class SteelHeartBuffPool:ObjectBase
    {
        public SteelHeartBuffPool(object target) : base(target)
        {
            
        }

        protected override void Release(bool isShutdown)
        {
            SteelHeartBuff steelHeartBuff = (SteelHeartBuff) Target;
            if (steelHeartBuff == null)
            {
                return;
            }

            steelHeartBuff = null;
        }
    }
}