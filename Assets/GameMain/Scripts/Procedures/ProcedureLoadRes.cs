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
    /// <summary>
    /// 加载资源流程
    /// </summary>
    public class ProcedureLoadRes : ProcedureBase
    {
        private IFsm<IProcedureManager> m_ProcedureOwner;
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            m_ProcedureOwner = procedureOwner;
            GameEntry.Resource.InitResources(OnInitResourceSuccess);
        }

        /// <summary>
        /// 初始化资源成功的回调函数
        /// </summary>
        private void OnInitResourceSuccess()
        {
            ChangeState<ProcedureLaunch>(m_ProcedureOwner);
        }
    }
}