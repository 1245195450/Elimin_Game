//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月24日
//------------------------------------------------------------

using Assets.GameMain.Scripts.Entities.EntityData;
using UnityGameFramework.Runtime;
using GameEntry = Assets.GameMain.Scripts.Base.GameEntry;

namespace Assets.GameMain.Scripts.ProfileMessage
{
    public static class ProfileReader
    {
        /// <summary>
        /// 初始化玩家数据,并存入数据结点
        /// </summary>
        /// <param name="PlayerCount"></param>
        public static void Init()
        {
            TempData.Gold = GameEntry.Setting.HasSetting("Gold")
                ? GameEntry.Setting.GetInt("Gold")
                : SetAndGetData("Gold", TempData.Gold);
            GameEntry.DataNode.GetOrAddNode("Gold").SetData<VarInt>(TempData.Gold);

            TempData.Level = GameEntry.Setting.HasSetting("Level")
                ? GameEntry.Setting.GetInt("Level")
                : SetAndGetData("Level", TempData.Level);
            GameEntry.DataNode.GetOrAddNode("Level").SetData<VarInt>(TempData.Level);

            InitP1Data();
            InsertP1Data2DataNode();
            InitP2Data();
            InsertP2Data2DataNode();
        }

        /// <summary>
        /// 初始化P1存档信息
        /// </summary>
        public static void InitP1Data()
        {
            TempData.m_P1Model = GameEntry.Setting.HasSetting("P1Model")
                ? GameEntry.Setting.GetObject<PlayerDataModel>("P1Model")
                : SetAndGetData("P1Model", TempData.m_P1Model);
        }

        /// <summary>
        /// 初始化P2的存档信息
        /// </summary>
        public static void InitP2Data()
        {
            TempData.m_P2Model = GameEntry.Setting.HasSetting("P2Model")
                ? GameEntry.Setting.GetObject<PlayerDataModel>("P2Model")
                : SetAndGetData("P2Model", TempData.m_P2Model);
        }

        public static PlayerDataModel SetAndGetData(string path, PlayerDataModel toFill)
        {
            GameEntry.Setting.SetObject(path, toFill);
            return GameEntry.Setting.GetObject<PlayerDataModel>(path);
        }

        public static int SetAndGetData(string path, int value)
        {
            GameEntry.Setting.SetInt(path, value);
            return GameEntry.Setting.GetInt(path);
        }


        /// <summary>
        /// 把P1的数据存入数据结点里面
        /// </summary>
        public static void InsertP1Data2DataNode()
        {
            GameEntry.DataNode.GetOrAddNode("P1AttackCD").SetData<VarFloat>(TempData.m_P1Model.AttackCD);
            GameEntry.DataNode.GetOrAddNode("P1Lv").SetData<VarInt>(TempData.m_P1Model.Level);
            GameEntry.DataNode.GetOrAddNode("P1Lives").SetData<VarInt>(TempData.m_P1Model.Lives);
            GameEntry.DataNode.GetOrAddNode("P1GodDefendLv").SetData<VarInt>(TempData.m_P1Model.HGLv);
            GameEntry.DataNode.GetOrAddNode("P1SteelHeartLv").SetData<VarInt>(TempData.m_P1Model.SteelHLv);
            GameEntry.DataNode.GetOrAddNode("P1TimePauseLv").SetData<VarInt>(TempData.m_P1Model.TPLv);
            GameEntry.DataNode.GetOrAddNode("P1ScreenHurtLv").SetData<VarInt>(TempData.m_P1Model.ScreenHLv);
        }

        /// <summary>
        /// 把P2的数据存入数据结点里面
        /// </summary>
        public static void InsertP2Data2DataNode()
        {
            GameEntry.DataNode.GetOrAddNode("P2AttackCD").SetData<VarFloat>(TempData.m_P2Model.AttackCD);
            GameEntry.DataNode.GetOrAddNode("P2Lv").SetData<VarInt>(TempData.m_P2Model.Level);
            GameEntry.DataNode.GetOrAddNode("P2Lives").SetData<VarInt>(TempData.m_P2Model.Lives);
            GameEntry.DataNode.GetOrAddNode("P2GodDefendLv").SetData<VarInt>(TempData.m_P2Model.HGLv);
            GameEntry.DataNode.GetOrAddNode("P2SteelHeartLv").SetData<VarInt>(TempData.m_P2Model.SteelHLv);
            GameEntry.DataNode.GetOrAddNode("P2TimePauseLv").SetData<VarInt>(TempData.m_P2Model.TPLv);
            GameEntry.DataNode.GetOrAddNode("P2ScreenHurtLv").SetData<VarInt>(TempData.m_P2Model.ScreenHLv);
        }

        /// <summary>
        /// 设置玩家坦克数据
        /// </summary>
        /// <param name="playerTankData">玩家坦克数据类</param>
        /// <param name="playerTag">玩家标识符</param>
        public static void SetPlayerData(PlayerTankData playerTankData, int playerTag)
        {
            if (playerTag == 1)
            {
                playerTankData.AttackCD =
                    GameEntry.DataNode.GetData<VarFloat>("P1AttackCD") - TempData.m_P1Model.Level * 0.05f;
                playerTankData.Level = GameEntry.DataNode.GetData<VarInt>("P1Lv");
                playerTankData.Lives = GameEntry.DataNode.GetData<VarInt>("P1Lives");
                playerTankData.HGLv = GameEntry.DataNode.GetData<VarInt>("P1GodDefendLv");
                playerTankData.SteelHLv = GameEntry.DataNode.GetData<VarInt>("P1SteelHeartLv");
                playerTankData.TPLv = GameEntry.DataNode.GetData<VarInt>("P1TimePauseLv");
                playerTankData.ScreenHlV = GameEntry.DataNode.GetData<VarInt>("P1ScreenHurtLv");
            }
            else
            {
                playerTankData.AttackCD =
                    GameEntry.DataNode.GetData<VarFloat>("P2AttackCD") - TempData.m_P1Model.Level * 0.05f;
                playerTankData.Level = GameEntry.DataNode.GetData<VarInt>("P2Lv");
                playerTankData.Lives = GameEntry.DataNode.GetData<VarInt>("P2Lives");
                playerTankData.HGLv = GameEntry.DataNode.GetData<VarInt>("P2GodDefendLv");
                playerTankData.SteelHLv = GameEntry.DataNode.GetData<VarInt>("P2SteelHeartLv");
                playerTankData.TPLv = GameEntry.DataNode.GetData<VarInt>("P2TimePauseLv");
                playerTankData.ScreenHlV = GameEntry.DataNode.GetData<VarInt>("P2ScreenHurtLv");
            }
        }
    }
}