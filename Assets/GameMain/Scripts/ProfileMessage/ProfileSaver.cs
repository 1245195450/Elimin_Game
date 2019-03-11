//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月24日
//------------------------------------------------------------

using UnityGameFramework.Runtime;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.ProfileMessage
{
    public static class ProfileSaver
    {
        /// <summary>
        /// 保存玩家的数据
        /// </summary>
        public static void SaveData()
        {
            TempData.Level = GameEntry.DataNode.GetData<VarInt>("Level");
            GameEntry.Setting.SetInt("Level", TempData.Level);
            
            TempData.Gold = GameEntry.DataNode.GetData<VarInt>("Gold");
            GameEntry.Setting.SetInt("Gold", TempData.Gold);

            TempData.m_P1Model.AttackCD = GameEntry.DataNode.GetData<VarFloat>("P1AttackCD");
            TempData.m_P1Model.Level = GameEntry.DataNode.GetData<VarInt>("P1Lv");
            TempData.m_P1Model.Lives = GameEntry.DataNode.GetData<VarInt>("P1Lives");
            TempData.m_P1Model.HGLv = GameEntry.DataNode.GetData<VarInt>("P1GodDefendLv");
            TempData.m_P1Model.SteelHLv = GameEntry.DataNode.GetData<VarInt>("P1SteelHeartLv");
            TempData.m_P1Model.TPLv = GameEntry.DataNode.GetData<VarInt>("P1TimePauseLv");
            TempData.m_P1Model.ScreenHLv = GameEntry.DataNode.GetData<VarInt>("P1ScreenHurtLv");
            GameEntry.Setting.SetObject("P1Model", TempData.m_P1Model);

            TempData.m_P2Model.AttackCD = GameEntry.DataNode.GetData<VarFloat>("P2AttackCD");
            TempData.m_P2Model.Level = GameEntry.DataNode.GetData<VarInt>("P2Lv");
            TempData.m_P2Model.Lives = GameEntry.DataNode.GetData<VarInt>("P2Lives");
            TempData.m_P2Model.HGLv = GameEntry.DataNode.GetData<VarInt>("P2GodDefendLv");
            TempData.m_P2Model.SteelHLv = GameEntry.DataNode.GetData<VarInt>("P2SteelHeartLv");
            TempData.m_P2Model.TPLv = GameEntry.DataNode.GetData<VarInt>("P2TimePauseLv");
            TempData.m_P2Model.ScreenHLv = GameEntry.DataNode.GetData<VarInt>("P2ScreenHurtLv");
            GameEntry.Setting.SetObject("P2Model", TempData.m_P2Model);
        }
    }
}