//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月1日
//------------------------------------------------------------

using GameFramework;
using GameFramework.ObjectPool;
using GameMain.Scripts.Entities.EntityLogic;

namespace GameMain.Scripts.Buffs
{
    public enum BuffKind
    {
        FreezeBuff = 1,
        GodDefendBuff = 2,
        SteelHeartBuff = 3,
    }


    public  class BuffBase:IReference
    {
        /// <summary>
        /// Buff的Id
        /// </summary>
        public BuffKind m_BuffKind;

        /// <summary>
        /// 计时器
        /// </summary>
        public float timer;

        /// <summary>
        /// Buff持续时间,我们约定,Buff时间为0,则为瞬时Buff,只执行OnAdd
        /// </summary>
        public float m_Length;

        /// <summary>
        /// 所归属的实体
        /// </summary>
        public CostumEntityLogic m_CostumEntityLogic;

        public BuffBase Fill(CostumEntityLogic costumEntityLogic, BuffKind buffKind, float length)
        {
            m_CostumEntityLogic = costumEntityLogic;
            m_BuffKind = buffKind;
            m_Length = length;
            return this;
        }
        
        /// <summary>
        /// 当添加到实体时执行逻辑
        /// </summary>
        public virtual void OnAdd()
        {
        }

        /// <summary>
        /// 跟随实体每帧更新
        /// </summary>
        public virtual void OnUpdate()
        {
        }

        /// <summary>
        /// 当从实体移除时
        /// </summary>
        public virtual void OnRemove()
        {
        }

        public void Clear()
        {
            timer = 0;
            m_Length = 0;
        }
    }
}