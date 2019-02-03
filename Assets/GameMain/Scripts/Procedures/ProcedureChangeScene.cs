//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月11日 15:48:36
//------------------------------------------------------------

using Assets.GameMain.Scripts.Utility;
using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameMain;
using GameMain.Scripts.Definition.Constant;
using UnityGameFramework.Runtime;
using GameEntry = Assets.GameMain.Scripts.Base.GameEntry;

namespace Assets.GameMain.Scripts.Procedures
{
    public class ProcedureChangeScene : ProcedureBase
    {
        /// <summary>
        /// 所有已加载的场景
        /// </summary>
        private string[] loadedSceneAssetNames;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            GameEntry.Event.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            GameEntry.Event.Subscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
            // 获取所有已加载场景名称
            GameEntry.DataNode.SetData<VarBool>(Constant.ProcedureRunnigData.CanChangeProcedure, false);
            loadedSceneAssetNames = GameEntry.Scene.GetLoadedSceneAssetNames();
            GameEntry.Scene.LoadScene(
                AssetUtility.GetSceneAsset(
                    GameEntry.DataNode.GetData<VarString>(Constant.ProcedureRunnigData.NextSceneName)), this);
        }

        private void OnLoadSceneUpdate(object sender, GameEventArgs e)
        {
            LoadSceneUpdateEventArgs ne = (LoadSceneUpdateEventArgs) e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' update, progress '{1}'.", ne.SceneAssetName, ne.Progress.ToString("P2"));
        }

        private void OnLoadSceneFailure(object sender, GameEventArgs e)
        {
            LoadSceneFailureEventArgs ne = (LoadSceneFailureEventArgs) e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Load scene '{0}' failure, error message '{1}'.", ne.SceneAssetName, ne.ErrorMessage);
        }

        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs) e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' OK.", ne.SceneAssetName);

            foreach (string t in loadedSceneAssetNames)
            {
                GameEntry.Scene.UnloadScene(t);
            }
            GameEntry.DataNode.SetData<VarBool>(Constant.ProcedureRunnigData.CanChangeProcedure, true);
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            GameEntry.Event.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            GameEntry.Event.Unsubscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
            
            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds,
            float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (!GameEntry.DataNode.GetData<VarBool>(Constant.ProcedureRunnigData.CanChangeProcedure))
            {
                return;
            }

            GameEntry.DataNode.SetData<VarBool>(Constant.ProcedureRunnigData.CanChangeProcedure, false);
            switch (GameEntry.DataNode.GetData<VarString>(Constant.ProcedureRunnigData.NextSceneName))
            {
                case "Menu":
                    ChangeState<ProcedureMenu>(procedureOwner);
                    break;
                case "Game":
                    ChangeState<ProcedureGame>(procedureOwner);
                    break;
            }
        }
    }
}