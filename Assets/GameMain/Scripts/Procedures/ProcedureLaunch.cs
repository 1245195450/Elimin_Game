//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月23日
//------------------------------------------------------------

using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameMain.Scripts.Definition.Constant;
using UnityGameFramework.Runtime;
using GameEntry = Assets.GameMain.Scripts.Base.GameEntry;
using Assets.GameMain.Scripts.DataTable;
using Assets.GameMain.Scripts.GameArgs;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Assets.GameMain.Scripts.Procedures
{
    /// <summary>
    /// 启动流程,开始初始化各种资源表
    /// </summary>
    public class ProcedureLaunch : ProcedureBase
    {
        private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
            //设置下一场景名称
            GameEntry.DataNode.GetOrAddNode(Constant.ProcedureRunnigData.NextSceneName).SetData<VarString>("Menu");
            //设置过渡界面文字
            GameEntry.DataNode.GetOrAddNode(Constant.ProcedureRunnigData.TransitionalMessage)
                .SetData<VarString>("Loading...");
            //设置不能改变流程
            GameEntry.DataNode.GetOrAddNode(Constant.ProcedureRunnigData.CanChangeProcedure).SetData<VarBool>(false);
            m_LoadedFlag.Clear();
            PreloadResources();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds,
            float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            IEnumerator<bool> iter = m_LoadedFlag.Values.GetEnumerator();
            while (iter.MoveNext())
            {
                if (!iter.Current)
                {
                    return;
                }
            }

            ChangeState<ProcedureChangeScene>(procedureOwner);
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);


            base.OnLeave(procedureOwner, isShutdown);
        }

        /// <summary>
        /// 资源表的预读取
        /// </summary>
        private void PreloadResources()
        {
            LoadDataTable("SkillMessages");
            LoadDataTable("Bullect");
            LoadDataTable("EnemyTank");
            LoadDataTable("Entity");
            LoadDataTable("Scene");
            LoadDataTable("Sound");
            LoadDataTable("Tools");
            LoadDataTable("UIForm");
        }

        private void LoadDataTable(string dataTableName)
        {
            m_LoadedFlag.Add(GameFramework.Utility.Text.Format("DataTable.{0}", dataTableName), false);
            GameEntry.DataTable.LoadDataTable(dataTableName, LoadType.Text, this);
        }

        private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
        {
            LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs) e;
            if (ne.UserData != this)
            {
                return;
            }

            m_LoadedFlag[GameFramework.Utility.Text.Format("DataTable.{0}", ne.DataTableName)] = true;
            Log.Info("Load data table '{0}' OK.", ne.DataTableName);
        }

        private void OnLoadDataTableFailure(object sender, GameEventArgs e)
        {
            LoadDataTableFailureEventArgs ne = (LoadDataTableFailureEventArgs) e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'.", ne.DataTableName,
                ne.DataTableAssetName, ne.ErrorMessage);
        }
    }
}