//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月30日
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using Assets.GameMain.Scripts.Base;
using Assets.GameMain.Scripts.GameArgs;
using Assets.GameMain.Scripts.Procedures;
using Assets.GameMain.Scripts.UI;
using GameFramework;
using GameFramework.Event;
using GameFramework.Scene;
using UnityEngine;

namespace Assets.GameMain.Scripts.Games
{
    /// <summary>
    /// 技能管理类
    /// </summary>
    public class SkillManager
    {
        /// <summary>
        /// 拿到游戏流程的引用
        /// </summary>
        private ProcedureGame m_ProcedureGame;

        /// <summary>
        /// 拿到游戏界面UI的引用
        /// </summary>
        private GamingForm m_GamingForm;

        /// <summary>
        /// 玩家一的技能栏
        /// </summary>
        private List<Sprite> m_P1SkillList;

        /// <summary>
        /// 玩家二的技能栏
        /// </summary>
        private List<Sprite> m_P2SkillList;

        public SkillManager(object userData)
        {
            m_ProcedureGame = userData as ProcedureGame;
            m_GamingForm = m_ProcedureGame.m_GamingForm;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init()
        {
            m_P1SkillList = m_GamingForm.P1SkillList;
            m_P1SkillList = m_GamingForm.P2SkillList;
            GameEntry.Event.Subscribe(GetToolsArgs.EventId, InitOnSkill);
        }

        /// <summary>
        /// 显示到技能栏上面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void InitOnSkill(object sender, GameEventArgs e)
        {
            GetToolsArgs ne = (GetToolsArgs) e;
            m_GamingForm.PutOnSkillPanel(ne.m_OwnerName, ne.m_ToolName);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.J)&&m_GamingForm.CheckSkillToNullName(1,2))
            {
                GameEntry.Event.Fire(this,
                    ReferencePool.Acquire<SkillReleaseArgs>().Fill("P1."+m_GamingForm.GetSkillToNullName(1, 2)));
                m_GamingForm.SetSkillToNull(m_GamingForm.m_P1Skills, 1, 2);
            }

            if (Input.GetKeyDown(KeyCode.K)&&m_GamingForm.CheckSkillToNullName(1,3))
            {
                GameEntry.Event.Fire(this,
                    ReferencePool.Acquire<SkillReleaseArgs>().Fill("P1."+m_GamingForm.GetSkillToNullName(1, 3)));
                m_GamingForm.SetSkillToNull(m_GamingForm.m_P1Skills, 1, 3);
            }

            if (Input.GetKeyDown(KeyCode.U)&&m_GamingForm.CheckSkillToNullName(1,0))
            {
                GameEntry.Event.Fire(this,
                    ReferencePool.Acquire<SkillReleaseArgs>().Fill("P1."+m_GamingForm.GetSkillToNullName(1, 0)));
                m_GamingForm.SetSkillToNull(m_GamingForm.m_P1Skills, 1, 0);
            }

            if (Input.GetKeyDown(KeyCode.I)&&m_GamingForm.CheckSkillToNullName(1,1))
            {
                GameEntry.Event.Fire(this,
                    ReferencePool.Acquire<SkillReleaseArgs>().Fill("P1."+m_GamingForm.GetSkillToNullName(1, 1)));
                m_GamingForm.SetSkillToNull(m_GamingForm.m_P1Skills, 1, 1);
            }

            if (Input.GetKeyDown(KeyCode.Keypad1)&&m_GamingForm.CheckSkillToNullName(2,2))
            {
                m_GamingForm.SetSkillToNull(m_GamingForm.m_P2Skills, 2, 2);
                GameEntry.Event.Fire(this,
                    ReferencePool.Acquire<SkillReleaseArgs>().Fill("P2."+m_GamingForm.GetSkillToNullName(2, 2)));
            }

            if (Input.GetKeyDown(KeyCode.Keypad2)&&m_GamingForm.CheckSkillToNullName(2,3))
            {
                GameEntry.Event.Fire(this,
                    ReferencePool.Acquire<SkillReleaseArgs>().Fill("P2."+m_GamingForm.GetSkillToNullName(2, 3)));
                m_GamingForm.SetSkillToNull(m_GamingForm.m_P2Skills, 2, 3);
            }

            if (Input.GetKeyDown(KeyCode.Keypad4)&&m_GamingForm.CheckSkillToNullName(2,0))
            {
                GameEntry.Event.Fire(this,
                    ReferencePool.Acquire<SkillReleaseArgs>().Fill("P2."+m_GamingForm.GetSkillToNullName(2, 0)));
                m_GamingForm.SetSkillToNull(m_GamingForm.m_P2Skills, 2, 0);
            }

            if (Input.GetKeyDown(KeyCode.Keypad5)&&m_GamingForm.CheckSkillToNullName(2,1))
            {
                GameEntry.Event.Fire(this,
                    ReferencePool.Acquire<SkillReleaseArgs>().Fill("P2."+m_GamingForm.GetSkillToNullName(2, 1)));
                m_GamingForm.SetSkillToNull(m_GamingForm.m_P2Skills, 2, 1);
            }
        }

        public void Leave()
        {
            GameEntry.Event.Unsubscribe(GetToolsArgs.EventId, InitOnSkill);
        }
    }
}