//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月23日
//------------------------------------------------------------

using Assets.GameMain.Scripts.Entities;
using Assets.GameMain.Scripts.Entities.EntityData;
using Assets.GameMain.Scripts.Games;
using Assets.GameMain.Scripts.UI;
using Assets.GameMain.Scripts.Utility;
using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Scene;
using GameMain;
using GameMain.Scripts.Definition.Constant;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = Assets.GameMain.Scripts.Base.GameEntry;

namespace Assets.GameMain.Scripts.Procedures
{
    public class ProcedureGame : ProcedureBase
    {
        /// <summary>
        /// 游戏逻辑管理者
        /// </summary>
        private GameManager m_GameManager;

        /// <summary>
        /// 技能管理者
        /// </summary>
        private SkillManager m_SkillManager;

        /// <summary>
        /// 拿到游戏界面的引用
        /// </summary>
        public GamingForm m_GamingForm;


        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenGamingFormSuccess);
            GameEntry.UI.OpenUIForm(UIFormId.GamingForm, this);
        }

        private void OnOpenGamingFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs) e;
            if (ne.UserData != this) return;
            m_GamingForm = (GamingForm) ne.UIForm.Logic;
            m_GameManager = new GameManager(this);
            m_GameManager.Init();
            m_SkillManager = new SkillManager(this);
            m_SkillManager.Init();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds,
            float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            m_GameManager?.Update();
            m_SkillManager?.Update();
            if (GameEntry.DataNode.GetData<VarBool>(Constant.ProcedureRunnigData.CanChangeProcedure))
            {
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenGamingFormSuccess);
            m_GamingForm.RemoveAllSkills();
            m_GameManager.Leave();
            m_SkillManager.Leave();
            GameEntry.UI.CloseUIForm(m_GamingForm.UIForm);
        }
    }
}