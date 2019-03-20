//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年3月20日 15:44:11
//------------------------------------------------------------

using GameFramework.ObjectPool;
using GameMain.Scripts.Base;
using GameMain.Scripts.Entities.EntityLogic;
using UnityEditor;

namespace GameMain.Scripts.Buffs
{
    public class BuffPoolManager : ScriptableSingleton<BuffPoolManager>
    {
        public IObjectPool<FreezeBuffPool> m_FreezeBuffPool;

        public IObjectPool<GodBuffPool> m_GodBuffPool;

        public IObjectPool<SteelHeartBuffPool> m_SteelHeartBuffPool;

        public bool hasInit = false;


        public void InitPool()
        {
            m_FreezeBuffPool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<FreezeBuffPool>("FreezeBuffPool");

            m_GodBuffPool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<GodBuffPool>("GodBuffPool");

            m_SteelHeartBuffPool =
                GameEntry.ObjectPool.CreateSingleSpawnObjectPool<SteelHeartBuffPool>("SteelBuffPool");
        }

        public GodDefendBuff CreateGodDefendBuff(CostumEntityLogic costumEntityLogic, BuffKind buffKind, float length)
        {
            GodDefendBuff godDefendBuff = null;
            GodBuffPool buffPool = m_GodBuffPool.Spawn();
            if (buffPool != null)
            {
                godDefendBuff = (GodDefendBuff) buffPool.Target;
            }
            else
            {
                godDefendBuff = new GodDefendBuff(costumEntityLogic, buffKind, length);
                m_GodBuffPool.Register(new GodBuffPool(godDefendBuff), true);
            }

            godDefendBuff.ResetBuff();
            godDefendBuff.m_CostumEntityLogic = costumEntityLogic;
            godDefendBuff.m_BuffKind = buffKind;
            godDefendBuff.m_Length = length;
            return godDefendBuff;
        }

        public SteelHeartBuff CreateSteelHeartBuff(CostumEntityLogic costumEntityLogic, BuffKind buffKind, float length)
        {
            SteelHeartBuff steelHeartBuff = null;
            SteelHeartBuffPool buffPool = m_SteelHeartBuffPool.Spawn();
            if (buffPool != null)
            {
                steelHeartBuff = (SteelHeartBuff) buffPool.Target;
            }
            else
            {
                steelHeartBuff = new SteelHeartBuff(costumEntityLogic, buffKind, length);
                m_SteelHeartBuffPool.Register(new SteelHeartBuffPool(steelHeartBuff), true);
            }

            steelHeartBuff.ResetBuff();
            steelHeartBuff.m_CostumEntityLogic = costumEntityLogic;
            steelHeartBuff.m_BuffKind = buffKind;
            steelHeartBuff.m_Length = length;
            return steelHeartBuff;
        }


        public FreezeBuff CreateFreezeBuff(CostumEntityLogic costumEntityLogic, BuffKind buffKind, float length)
        {
            FreezeBuff freezeBuff = null;
            FreezeBuffPool buffPool = m_FreezeBuffPool.Spawn();
            if (buffPool != null)
            {
                freezeBuff = (FreezeBuff) buffPool.Target;
            }
            else
            {
                freezeBuff = new FreezeBuff(costumEntityLogic, buffKind, length);
                m_FreezeBuffPool.Register(new FreezeBuffPool(freezeBuff), true);
            }

            freezeBuff.ResetBuff();
            freezeBuff.m_CostumEntityLogic = costumEntityLogic;
            freezeBuff.m_BuffKind = buffKind;
            freezeBuff.m_Length = length;
            return freezeBuff;
        }
    }
}