//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月24日
//------------------------------------------------------------

using System.Collections;
using Assets.GameMain.Scripts.Buffs;
using Assets.GameMain.Scripts.Entities.EntityData;
using Assets.GameMain.Scripts.GameArgs;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = Assets.GameMain.Scripts.Base.GameEntry;

namespace Assets.GameMain.Scripts.Entities.EntityLogic
{
    /// <summary>
    /// 玩家基类
    /// </summary>
    public class PlayerTankLogic : TankBaseLogic
    {
        /// <summary>
        /// 水平控制名
        /// </summary>
        private string ControlHor = "Horizontal";

        /// <summary>
        /// 垂直控制名
        /// </summary>
        private string ControlVer = "Vertical";

        /// <summary>
        /// 开火控制名
        /// </summary>
        private string ControlFire = "Fire";

        /// <summary>
        /// 玩家标识
        /// </summary>
        private string PlayerFlag;

        /// <summary>
        /// 坦克数据
        /// </summary>
        public PlayerTankData m_PlayerTankData;

        /// <summary>
        /// 将要被解除关系的子实体数据
        /// </summary>
        private EntityData.EntityData m_ChildEntity2Detach_Data;

        private Entity m_ChildEntity2Detach_Entity;


        private static readonly int Level = Animator.StringToHash("Level");
        private static readonly int Blend = Animator.StringToHash("Blend");


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Animator = GetComponent<Animator>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_PlayerTankData = userData as PlayerTankData;

            CD = m_PlayerTankData.AttackCD;
            PlayerFlag = (m_PlayerTankData.TypeId - 9999).ToString();
            //根据typeId判定玩家类型,从而设置不同的操控变量
            ControlHor = string.Concat(ControlHor, PlayerFlag);
            ControlVer = string.Concat(ControlVer, PlayerFlag);
            ControlFire = string.Concat(ControlFire, PlayerFlag);
            m_Animator.SetInteger(Level, m_PlayerTankData.Level);
            //送一个3秒钟的无敌buff
            AddBuff(new GodDefendBuff(this, 3.0f));
            GameEntry.Event.Subscribe(SkillReleaseArgs.EventId, OnSkillRelease);
        }

        private void OnSkillRelease(object sender, GameEventArgs e)
        {
            SkillReleaseArgs ne = (SkillReleaseArgs) e;
            string skillOwner = ne.m_ToolName.Split('.')[0];
            string skillName = ne.m_ToolName.Split('.')[1];
            if (skillOwner == m_PlayerTankData.m_tag)
            {
                if (skillName == "GodTime")
                    AddBuff(new GodDefendBuff(this,
                        1.5f + GameEntry.DataNode.GetData<VarInt>(m_PlayerTankData.m_tag + "GodDefendLv") * 0.5f));
            }
        }

        protected override void OnAttached(UnityGameFramework.Runtime.EntityLogic childEntity,
            Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);
            m_ChildEntity2Detach_Data = userData as EntityData.EntityData;
            m_ChildEntity2Detach_Entity = childEntity.Entity;
        }

        public override void GetGodDefend()
        {
            base.GetGodDefend();
            GameEntry.Entity.ShowPlayerGodDefend(new GodDefendData(GameEntry.Entity.GenerateSerialId(), 60000,
                m_PlayerTankData.Id));
        }

        public override void LoseGodDefend()
        {
            GameEntry.Entity.DetachEntity(m_ChildEntity2Detach_Data.Id);
            GameEntry.Entity.HideEntity(m_ChildEntity2Detach_Entity);
        }

        /// <summary>
        /// 使用一波unity的生命周期函数(懒得在GF拓展了)
        /// </summary>
        private void FixedUpdate()
        {
            Move();
            Shoot();
        }

        public override void Move()
        {
            base.Move();
            h = Input.GetAxisRaw(ControlHor);
            transform.Translate(Vector2.right * h * 4 * Time.fixedDeltaTime, Space.World);
            if (h > 0)
            {
                m_Animator.SetFloat(Blend, 1.0f);
                transformRotation.eulerAngles = new Vector3(0, 0, -90);
                return;
            }

            else if (h < 0)
            {
                m_Animator.SetFloat(Blend, 0.6f);
                transformRotation.eulerAngles = new Vector3(0, 0, 90);
                return;
            }

            v = Input.GetAxisRaw(ControlVer);
            transform.Translate(Vector2.up * v * 4 * Time.fixedDeltaTime, Space.World);
            if (v > 0)
            {
                m_Animator.SetFloat(Blend, 0);
                transformRotation.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (v < 0)
            {
                m_Animator.SetFloat(Blend, 0.3f);
                transformRotation.eulerAngles = new Vector3(0, 0, 180);
            }
        }

        public override void Shoot()
        {
            base.Shoot();
            m_Timer += Time.fixedDeltaTime;
            if (!(m_Timer >= CD)) return;
            if (!Input.GetButtonDown(ControlFire)) return;
            GameEntry.Entity.ShowBullect(
                new BullectData(GameEntry.Entity.GenerateSerialId(), 49999 + int.Parse(PlayerFlag),
                    m_PlayerTankData.Level)
                {
                    Position = transform.position,
                    Rotation = transformRotation
                });
            m_Timer = 0f;
        }

        public void LevelUp()
        {
            if (m_PlayerTankData.Level + 1 > 5) return;
            m_PlayerTankData.Level++;
            m_Animator.SetInteger(Level, m_PlayerTankData.Level);
        }


        public void IncreaseLife()
        {
            GameEntry.DataNode.SetData<VarInt>(transform.tag + "Lives",
                GameEntry.DataNode.GetData<VarInt>(transform.tag + "Lives") + 1);
            GameEntry.Event.Fire(this, ReferencePool.Acquire<PlayerLivesChangeArgs>().Fill(transform.tag, 1));
        }


        public void BeHited(int damgeValue)
        {
            if (IsGodDefend) return;
            m_PlayerTankData.Level -= damgeValue;
            m_Animator.SetInteger(Level, m_PlayerTankData.Level);
            Invoke(nameof(TryDie), 0.167f);
        }


        public override void TryDie()
        {
            base.TryDie();
            if (m_PlayerTankData.Level <= 0)
            {
                if (Entity.isActiveAndEnabled)
                {
                    GameEntry.DataNode.SetData<VarInt>(transform.tag + "Lives",
                        GameEntry.DataNode.GetData<VarInt>(transform.tag + "Lives") - 1);
                    GameEntry.Event.Fire(this, ReferencePool.Acquire<PlayerLivesChangeArgs>().Fill(transform.tag, -1));
                    GameEntry.Entity.HideEntity(Entity);
                }
            }
        }

        protected override void OnHide(object userData)
        {
            base.OnHide(userData);
            ControlHor = "Horizontal";
            ControlVer = "Vertical";
            ControlFire = "Fire";
            GameEntry.Event.Unsubscribe(SkillReleaseArgs.EventId, OnSkillRelease);
            GameEntry.Entity.DetachChildEntities(m_PlayerTankData.Id);
            m_Buffs.Clear();
        }
    }
}