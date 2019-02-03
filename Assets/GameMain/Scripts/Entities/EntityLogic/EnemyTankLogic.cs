//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月24日
//------------------------------------------------------------

using System.Collections.Generic;
using Assets.GameMain.Scripts.Buffs;
using Assets.GameMain.Scripts.Entities.EntityData;
using Assets.GameMain.Scripts.GameArgs;
using DefaultNamespace;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = Assets.GameMain.Scripts.Base.GameEntry;

namespace Assets.GameMain.Scripts.Entities.EntityLogic
{
    /// <summary>
    /// 敌人坦克逻辑类
    /// </summary>
    public class EnemyTankLogic : TankBaseLogic
    {
        /// <summary>
        /// 坦克数据
        /// </summary>
        public EnemyTankData m_EnemyTankData { get; set; }

        /// <summary>
        /// 变向计时器,大于4秒变向一次
        /// </summary>
        private float timeValChangeDirection = 0;

        private static readonly int Level = Animator.StringToHash("Level");
        private static readonly int Blend = Animator.StringToHash("Blend");

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_EnemyTankData = userData as EnemyTankData;
            CD = m_EnemyTankData.AttackCD;
            if (m_EnemyTankData.Level > m_EnemyTankData.MaxLevel)
            {
                m_EnemyTankData.Level = m_EnemyTankData.MaxLevel;
            }

            m_Animator.SetInteger(Level, m_EnemyTankData.Level);
            GameEntry.Event.Subscribe(SkillReleaseArgs.EventId, OnSkillRelease);
        }

        private void OnSkillRelease(object sender, GameEventArgs e)
        {
            SkillReleaseArgs ne = (SkillReleaseArgs) e;
            string skillName = ne.m_ToolName.Split('.')[1];
            if (skillName == "TimePause")
            {
                string senderName = ne.m_ToolName.Split('.')[0];
                if (senderName == "P1")
                    AddBuff(new FreezeBuff(this, 1.5f + GameEntry.DataNode.GetData<VarInt>("P1TimePauseLv") * 0.5f));
                else
                {
                    AddBuff(new FreezeBuff(this, 1.5f + GameEntry.DataNode.GetData<VarInt>("P2TimePauseLv") * 0.5f));
                }
            }
            else if (skillName == "ScreenHurt")
            {
                string senderName = ne.m_ToolName.Split('.')[0];
                if (senderName == "P1")
                    BeHited(GameEntry.DataNode.GetData<VarInt>("P1ScreenHurtLv"));
                else
                {
                    BeHited(GameEntry.DataNode.GetData<VarInt>("P2ScreenHurtLv"));
                }
            }
        }


        protected override void OnHide(object userData)
        {
            base.OnHide(userData);
            GameEntry.Event.Unsubscribe(SkillReleaseArgs.EventId, OnSkillRelease);
        }

        void FixedUpdate()
        {
            if (IsFreeze) return;
            Move();
            Shoot();
        }

        public override void Move()
        {
            base.Move();

            if (timeValChangeDirection >= 4)
            {
                int num = Random.Range(0, 8);
                if (num > 5)
                {
                    v = -1;
                    h = 0;
                    m_Animator.SetFloat(Blend, 0.3f);
                }
                else if (num == 0)
                {
                    v = 1;
                    h = 0;
                    m_Animator.SetFloat(Blend, 0);
                }
                else if (num > 0 && num <= 2)
                {
                    h = -1;
                    v = 0;
                    m_Animator.SetFloat(Blend, 0.6f);
                }
                else if (num > 2 && num <= 4)
                {
                    h = 1;
                    v = 0;
                    m_Animator.SetFloat(Blend, 1.0f);
                }

                timeValChangeDirection = 0;
            }
            else
            {
                timeValChangeDirection += Time.fixedDeltaTime;
            }


            transform.Translate(Vector3.up * v * 3 * Time.fixedDeltaTime, Space.World);

            if (v < 0)
            {
                transformRotation.eulerAngles = new Vector3(0, 0, 180);
                return;
            }

            else if (v > 0)
            {
                transformRotation.eulerAngles = new Vector3(0, 0, 0);
                return;
            }

            transform.Translate(Vector3.right * h * 3 * Time.fixedDeltaTime, Space.World);
            if (h < 0)
            {
                transformRotation.eulerAngles = new Vector3(0, 0, 90);
            }

            else if (h > 0)
            {
                transformRotation.eulerAngles = new Vector3(0, 0, -90);
            }
        }

        public override void Shoot()
        {
            base.Shoot();
            m_Timer += Time.fixedDeltaTime;
            if (m_Timer >= CD)
            {
                GameEntry.Entity.ShowBullect(new BullectData(GameEntry.Entity.GenerateSerialId(), 50002)
                {
                    Position = transform.position,
                    Rotation = transformRotation
                });
                m_Timer = 0f;
            }
        }

        public void BeHited(int damgeValue)
        {
            TryDropTool();
            m_EnemyTankData.Level -= damgeValue;
            m_Animator.SetInteger(Level, m_EnemyTankData.Level);
            //要死可以,先把道具留下
            Invoke(nameof(TryDie), 0.167f);
        }

        public override void TryDie()
        {
            base.TryDie();
            if (m_EnemyTankData.Level <= 0)
            {
                if (Entity.isActiveAndEnabled)
                {
                    GameEntry.Event.Fire(this, ReferencePool.Acquire<MessionTouchArgs>().Fill());
                    GameEntry.Entity.HideEntity(Entity);
                }
            }
        }

        /// <summary>
        /// 尝试掉落道具
        /// </summary>
        public void TryDropTool()
        {
            if (m_EnemyTankData.Level % 2 == 0)
            {
                m_EnemyTankData.HasSp = true;
            }

            if (m_EnemyTankData.HasSp)
            {
                m_EnemyTankData.HasSp = false;
                GameEntry.Entity.ShowTools(new ToolsData(GameEntry.Entity.GenerateSerialId(),
                    40000 + Random.Range(0, 6))
                {
                    Position = transform.position
                });
            }
        }
    }
}