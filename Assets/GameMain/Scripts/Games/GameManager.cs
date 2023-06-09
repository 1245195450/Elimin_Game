//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月26日
//------------------------------------------------------------

using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using GameFramework.Resource;
using GameMain.Scripts.CostumAssets;
using GameMain.Scripts.Definition.Constant;
using GameMain.Scripts.Entities;
using GameMain.Scripts.Entities.EntityData;
using GameMain.Scripts.GameArgs;
using GameMain.Scripts.Procedures;
using GameMain.Scripts.ProfileMessage;
using GameMain.Scripts.UI;
using GameMain.Scripts.Utility;
using UnityEngine;
using UnityGameFramework.Runtime;
using Object = System.Object;
using Random = UnityEngine.Random;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.Games
{
    /// <summary>
    /// 游戏逻辑类
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// 从asset文件拿到技能图标
        /// </summary>
        public static SpritesAsset m_SpritesAsset { get; set; }

        /// <summary>
        /// 加载图标成功的回调函数
        /// </summary>
        public LoadAssetCallbacks
            OnLoadSpriteAssetSuccess = new LoadAssetCallbacks(loadSpritsAssetCallback);

        /// <summary>
        /// 加载实体成功标识字典,int为id,bool为成功与否的布尔类型
        /// </summary>
        private Dictionary<int, bool> m_LoadedFlag = new Dictionary<int, bool>();

        /// <summary>
        /// 游戏流程的引用
        /// </summary>
        public ProcedureGame m_ProcedureGame;

        /// <summary>
        /// 加载实体已全部完成1
        /// </summary>
        private bool LoadSuccess;

        /// <summary>
        /// 生成敌人计时器
        /// </summary>
        private float m_SpwanEnemyTimer;

        /// <summary>
        /// 敌人出生点数组
        /// </summary>
        private Vector3[] m_EnemySpwanPos = new Vector3[3];

        /// <summary>
        /// 已经点击了退出按钮
        /// </summary>
        private bool HasExit;

        /// <summary>
        /// 技能管理者
        /// </summary>
        private SkillManager m_SkillManager;

        public GameManager(Object userData)
        {
            m_ProcedureGame = userData as ProcedureGame;
        }


        /// <summary>
        /// 初始化游戏
        /// </summary>
        public void Init()
        {
            GameEntry.Resource.LoadAsset(AssetUtility.GetSpriteAsset(), OnLoadSpriteAssetSuccess);
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Subscribe(GameOverArgs.EventId, OnGameOver);
            GameEntry.Event.Subscribe(PlayerLivesChangeArgs.EventId, OnPlayerLivesChanged);

            if (m_SkillManager == null)
                m_SkillManager = new SkillManager(m_ProcedureGame);
            m_SkillManager?.Init();

            HasExit = false;
            LoadSuccess = false;

            InitEnemySpwanPos();
            InitHeart();
            InitCom();
        }

        /// <summary>
        /// 更新游戏逻辑
        /// </summary>
        public void Update()
        {
            m_SkillManager.Update();
            
            if (!LoadSuccess)
            {
                IEnumerator<bool> iter = m_LoadedFlag.Values.GetEnumerator();
                while (iter.MoveNext())
                {
                    if (!iter.Current)
                    {
                        return;
                    }
                }

                InitPlayer();
                GameEntry.Event.Fire(this, ReferencePool.Acquire<LoadNextResourcesSuccessArgs>().Fill(true));
                HasExit = false;
                LoadSuccess = true;
            }

            if (HasExit) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HasExit = true;
                GameEntry.Base.GameSpeed = 0;
                GameEntry.UI.OpenDialog(new DialogParams()
                {
                    Mode = 2,
                    Title = "暂停游戏",
                    Message = "游戏已暂停",
                    ConfirmText = "返回主菜单",
                    CancelText = "返回游戏",
                    OnClickConfirm = Return2Menu,
                    OnClickCancel = Return2Game
                });
            }

            SpwanEnemy();
        }

        /// <summary>
        /// 离开游戏
        /// </summary>
        public void Leave()
        {
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Unsubscribe(GameOverArgs.EventId, OnGameOver);
            GameEntry.Event.Unsubscribe(PlayerLivesChangeArgs.EventId, OnPlayerLivesChanged);
            GameEntry.Entity.HideAllLoadedEntities();
            m_SkillManager.Leave();
        }

        /// <summary>
        /// 玩家生命值改变的回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPlayerLivesChanged(object sender, GameEventArgs e)
        {
            PlayerLivesChangeArgs ne = (PlayerLivesChangeArgs) e;
            //如果是加命，下面的逻辑不需要执行
            if (ne.changeValue > 0) return;

            if (GameEntry.DataNode.GetData<VarInt>("PlayerMode") == 2)
            {
                if (GameEntry.DataNode.GetData<VarInt>("P1Lives") <= 0 &&
                    GameEntry.DataNode.GetData<VarInt>("P2Lives") <= 0)
                {
                    GameEntry.Event.Fire(this, ReferencePool.Acquire<GameOverArgs>().Fill(false));
                    return;
                }
            }
            else
            {
                if (GameEntry.DataNode.GetData<VarInt>("P1Lives") <= 0)
                {
                    GameEntry.Event.Fire(this, ReferencePool.Acquire<GameOverArgs>().Fill(false));
                    return;
                }
            }

            if (ne.playerFlag == "P1")
            {
                GameEntry.Entity.ShowPlayerTank(
                    new PlayerTankData(GameEntry.Entity.GenerateSerialId(), 10000)
                    {
                        Position = new Vector3(-2.8f, -4.5f, 1.0f),
                    });
            }
            else
            {
                GameEntry.Entity.ShowPlayerTank(
                    new PlayerTankData(GameEntry.Entity.GenerateSerialId(), 10001)
                    {
                        Position = new Vector3(-0.24f, -4.5f, 1.0f),
                    });
            }
        }

        /// <summary>
        /// 游戏结束回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGameOver(object sender, GameEventArgs e)
        {
            GameOverArgs ne = (GameOverArgs) e;
            if (HasExit) return;

            if (ne.m_PassGame)
            {
                HasExit = true;
                GameEntry.DataNode.SetData<VarInt>("Level", GameEntry.DataNode.GetData<VarInt>("Level") + 1);
                ProfileSaver.SaveData();
                GameEntry.DataNode.SetData<VarString>(Constant.ProcedureRunnigData.TransitionalMessage,
                    "Scene " + GameEntry.DataNode.GetData<VarInt>("Level"));
                GameEntry.Entity.HideAllLoadedEntities();
                GameEntry.Entity.HideAllLoadingEntities();
                GameEntry.UI.OpenUIForm(UIFormId.TransitionalForm);
                ResetGame();
            }
            else
            {
                HasExit = true;
                GameEntry.Base.GameSpeed = 0;
                GameEntry.UI.OpenDialog(new DialogParams()
                {
                    Mode = 1,
                    Title = "游戏结束",
                    Message = "胜败乃兵家常事",
                    ConfirmText = "回到主菜单",
                    OnClickConfirm = Return2Menu
                });
            }
        }


        private void ResetGame()
        {
            LoadSuccess = false;
            m_SpwanEnemyTimer = 0;
            m_LoadedFlag.Clear();
            InitEnemySpwanPos();
            InitHeart();
            InitCom();
        }


        /// <summary>
        /// 回到主菜单
        /// </summary>
        /// <param name="obj"></param>
        private void Return2Menu(object obj)
        {
            GameEntry.Base.GameSpeed = 1.0f;

            Leave();
            GameEntry.DataNode.SetData<VarInt>("P1Lives", 3);
            GameEntry.DataNode.SetData<VarInt>("P2Lives", 3);
            GameEntry.DataNode.SetData<VarString>(Constant.ProcedureRunnigData.NextSceneName, "Menu");
            GameEntry.DataNode.SetData<VarBool>(Constant.ProcedureRunnigData.CanChangeProcedure, true);
        }

        /// <summary>
        /// 返回游戏
        /// </summary>
        /// <param name="obj"></param>
        private void Return2Game(object obj)
        {
            GameEntry.Base.GameSpeed = 1.0f;
            HasExit = false;
        }

        /// <summary>
        /// 加载自定义asset成功回调函数
        /// </summary>
        /// <param name="assetname"></param>
        /// <param name="asset"></param>
        /// <param name="duration"></param>
        /// <param name="userdata"></param>
        private static void loadSpritsAssetCallback(string assetname, object asset, float duration, object userdata)
        {
            m_SpritesAsset = asset as SpritesAsset;
        }

        /// <summary>
        /// 显示敌人成功的回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ne"></param>
        private void OnShowEntitySuccess(object sender, GameEventArgs ne)
        {
            ShowEntitySuccessEventArgs e = (ShowEntitySuccessEventArgs) ne;
            EntityData m_entityData = e.UserData as EntityData;
            m_LoadedFlag[m_entityData.Id] = true;
        }

        /// <summary>
        /// 初始化敌人位置
        /// </summary>
        private void InitEnemySpwanPos()
        {
            for (int i = 0; i < 3; i++)
            {
                m_EnemySpwanPos[i] = new Vector3(-6.00f + 4.88f * i, 4.48f, 1.0f);
            }
        }

        /// <summary>
        /// 生成敌人
        /// </summary>
        private void SpwanEnemy()
        {
            m_SpwanEnemyTimer += Time.fixedDeltaTime;
            if (m_SpwanEnemyTimer >= 5.0f)
            {
                GameEntry.Entity.ShowEnemy(new EnemyTankData(GameEntry.Entity.GenerateSerialId(),
                    20000 + Random.Range(0, 2))
                {
                    Position = m_EnemySpwanPos[Random.Range(0, 3)],
                    Rotation = Quaternion.Euler(0, 0, 0),
                    Level = Random.Range(1, 5)
                });
                m_SpwanEnemyTimer = 0;
            }
        }

        /// <summary>
        /// 生成玩家
        /// </summary>
        private void InitPlayer()
        {
            if (GameEntry.DataNode.GetOrAddNode("PlayerMode").GetData<VarInt>() == 1)
            {
                GameEntry.Entity.ShowPlayerTank(
                    new PlayerTankData(GameEntry.Entity.GenerateSerialId(), 10000)
                    {
                        Position = new Vector3(-2.8f, -4.5f, 1.0f),
                    });
            }
            else
            {
                GameEntry.Entity.ShowPlayerTank(
                    new PlayerTankData(GameEntry.Entity.GenerateSerialId(), 10000)
                    {
                        Position = new Vector3(-2.8f, -4.5f, 1.0f),
                    });
                m_LoadedFlag.Add(EntityExtension.s_SerialId, false);
                GameEntry.Entity.ShowPlayerTank(
                    new PlayerTankData(GameEntry.Entity.GenerateSerialId(), 10001)
                    {
                        Position = new Vector3(-0.24f, -4.5f, 1.0f),
                    });
            }
        }

        /// <summary>
        /// 创建普通地图部分
        /// </summary>
        public void InitCom()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    int typeId = 30000 + Random.Range(-1, 5);
                    if (typeId != 29999 && typeId != 30004)
                    {
                        GameEntry.Entity.ShowMapItems(
                            new MapItemsDatas(GameEntry.Entity.GenerateSerialId(), typeId)
                            {
                                Position = new Vector3(-5.36f + j * 0.64f, -3.22f + i * 0.64f, 1.0f),
                            });
                        m_LoadedFlag.Add(EntityExtension.s_SerialId, false);
                    }
                }
            }

            //空气墙
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 17; i++)
                {
                    GameEntry.Entity.ShowMapItems(
                        new MapItemsDatas(GameEntry.Entity.GenerateSerialId(), 30005)
                        {
                            Position = new Vector3(-6.64f + i * 0.64f, -5.14f + j * 10.24f, 1.0f),
                        });
                    m_LoadedFlag.Add(EntityExtension.s_SerialId, false);
                }
            }

            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 15; i++)
                {
                    GameEntry.Entity.ShowMapItems(
                        new MapItemsDatas(GameEntry.Entity.GenerateSerialId(), 30005)
                        {
                            Position = new Vector3(-6.64f + 10.88f * j, -4.5f + i * 0.64f, 1.0f),
                        });
                    m_LoadedFlag.Add(EntityExtension.s_SerialId, false);
                }
            }
        }

        /// <summary>
        /// 初始化家园
        /// </summary>
        public void InitHeart()
        {
            GameEntry.Entity.ShowMapItems(
                new MapItemsDatas(GameEntry.Entity.GenerateSerialId(), 30006)
                {
                    Position = new Vector3(-2.16f, -4.5f, 1.0f),
                });
            m_LoadedFlag.Add(EntityExtension.s_SerialId, false);
            GameEntry.Entity.ShowMapItems(
                new MapItemsDatas(GameEntry.Entity.GenerateSerialId(), 30006)
                {
                    Position = new Vector3(-2.16f, -3.86f, 1.0f),
                });
            m_LoadedFlag.Add(EntityExtension.s_SerialId, false);
            GameEntry.Entity.ShowMapItems(
                new MapItemsDatas(GameEntry.Entity.GenerateSerialId(), 30006)
                {
                    Position = new Vector3(-1.52f, -3.86f, 1.0f),
                });
            m_LoadedFlag.Add(EntityExtension.s_SerialId, false);
            GameEntry.Entity.ShowMapItems(
                new MapItemsDatas(GameEntry.Entity.GenerateSerialId(), 30006)
                {
                    Position = new Vector3(-0.88f, -3.86f, 1.0f),
                });
            m_LoadedFlag.Add(EntityExtension.s_SerialId, false);
            GameEntry.Entity.ShowMapItems(
                new MapItemsDatas(GameEntry.Entity.GenerateSerialId(), 30006)
                {
                    Position = new Vector3(-0.88f, -4.5f, 1.0f),
                });
            m_LoadedFlag.Add(EntityExtension.s_SerialId, false);
            GameEntry.Entity.ShowMapItems(
                new MapItemsDatas(GameEntry.Entity.GenerateSerialId(), 30004)
                {
                    Position = new Vector3(-1.52f, -4.5f, 1.0f),
                });
            m_LoadedFlag.Add(EntityExtension.s_SerialId, false);
        }
    }
}