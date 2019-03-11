//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月23日
//------------------------------------------------------------

using GameFramework.Fsm;
using GameFramework.Procedure;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.Procedures
{
    public class ProcedureLoadRes : ProcedureBase
    {
        private IFsm<IProcedureManager> m_ProcedureOwner;
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            m_ProcedureOwner = procedureOwner;
            GameEntry.Resource.InitResources(OnInitResourceSuccess);
        }

        private void OnInitResourceSuccess()
        {
            ChangeState<ProcedureLaunch>(m_ProcedureOwner);
        }
    }
}