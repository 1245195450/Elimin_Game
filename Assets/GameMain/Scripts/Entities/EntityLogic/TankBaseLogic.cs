//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月28日
//------------------------------------------------------------

using Assets.GameMain.Scripts.Base;
using Assets.GameMain.Scripts.Entities.EntityData;
using DefaultNamespace;
using GameFramework;
using UnityEngine;

namespace Assets.GameMain.Scripts.Entities.EntityLogic
{
    public abstract class TankBaseLogic : CostumEntityLogic
    {
        /// <summary>
        /// 动画机
        /// </summary>
        public Animator m_Animator;

        /// <summary>
        /// 垂直方向控制变量
        /// </summary>
        public float v = 0;

        /// <summary>
        /// 水平方向控制变量
        /// </summary>
        public float h = 0;

        /// <summary>
        /// 是否可以攻击
        /// </summary>
        public bool enableAttack;

        /// <summary>
        /// 攻击CD
        /// </summary>
        public float CD;

        /// <summary>
        /// 计时器
        /// </summary>
        public float m_Timer = 0f;

        /// <summary>
        /// 自身旋转角度
        /// </summary>
        public Quaternion transformRotation;


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Animator = GetComponent<Animator>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            EntityData.EntityData m_EntityData = (EntityData.EntityData) userData;
            transform.tag = m_EntityData.m_tag;
            transform.position = m_EntityData.Position;
            transform.rotation = m_EntityData.Rotation;
            transformRotation = transform.rotation;
            v = 0;
            h = 0;
        }


        public virtual void Move()
        {
        }


        public virtual void Shoot()
        {
        }

        /// <summary>
        /// 掉血或死亡
        /// </summary>
        public virtual void TryDie()
        {
        }

    }
}