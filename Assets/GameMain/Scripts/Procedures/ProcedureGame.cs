//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月23日
//------------------------------------------------------------

using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameMain.Scripts.Buffs;
using GameMain.Scripts.Definition.Constant;
using GameMain.Scripts.Games;
using GameMain.Scripts.UI;
using UnityGameFramework.Runtime;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.Procedures
{
    public class ProcedureGame : ProcedureBase
    {
        /// <summary>
        /// 游戏逻辑管理者
        /// </summary>
        private GameManager m_GameManager;

        /// <summary>
        /// 拿到游戏界面的引用
        /// </summary>
        public GamingForm m_GamingForm;


        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            // 订阅打开游戏UI界面事件
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenGamingFormSuccess);
            // 打开游戏UI界面
            GameEntry.UI.OpenUIForm(UIFormId.GamingForm, this);
        }

        
        private void OnOpenGamingFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs) e;
            if (ne.UserData != this) return;
            
            // 拿到游戏UI引用
            m_GamingForm = (GamingForm) ne.UIForm.Logic;
            
            if (m_GameManager == null)
                m_GameManager = new GameManager(this);
            m_GameManager?.Init();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds,
            float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            m_GameManager?.Update();
            if (GameEntry.DataNode.GetData<VarBool>(Constant.ProcedureRunnigData.CanChangeProcedure))
            {
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenGamingFormSuccess);
            GameEntry.UI.CloseUIForm(m_GamingForm.UIForm);
        }
    }
}