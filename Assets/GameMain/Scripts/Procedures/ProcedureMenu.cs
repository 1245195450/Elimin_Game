//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月23日
//------------------------------------------------------------

using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameMain.Scripts.Definition.Constant;
using GameMain.Scripts.UI;
using UnityGameFramework.Runtime;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.Procedures
{
    public class ProcedureMenu : ProcedureBase
    {
        /// <summary>
        /// 进入游戏流程的标识
        /// </summary>
        public bool ToGame { get; set; }

        /// <summary>
        /// 拿到主菜单UI引用
        /// </summary>
        private MenuForm m_MenuForm;


        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.DataNode.SetData<VarBool>(Constant.ProcedureRunnigData.CanChangeProcedure, false);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.UI.OpenUIForm(UIFormId.MenuForm, this);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds,
            float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (GameEntry.DataNode.GetData<VarBool>(Constant.ProcedureRunnigData.CanChangeProcedure))
            {
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.UI.CloseUIForm(m_MenuForm.UIForm);
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs) e;
            if (ne.UserData != this)
            {
                return;
            }

            m_MenuForm = (MenuForm) ne.UIForm.Logic;
            GameFrameworkLog.Info("Has get the MenuForm");
        }
    }
}