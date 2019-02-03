//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月26日
//------------------------------------------------------------

using System.Collections.Generic;
using Assets.GameMain.Scripts.CostumAssets;
using Assets.GameMain.Scripts.GameArgs;
using Assets.GameMain.Scripts.Games;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = Assets.GameMain.Scripts.Base.GameEntry;

namespace Assets.GameMain.Scripts.UI
{
    public class GamingForm : UGuiForm
    {
        /// <summary>
        /// 主要内容
        /// </summary>
        private Transform m_MainContent;

        /// <summary>
        /// 游戏数据,关卡数,P1生命数,P2生命数
        /// </summary>
        private Transform m_GamingData;

        /// <summary>
        /// 任务栏
        /// </summary>
        private Transform m_Messions;

        /// <summary>
        /// 玩家一的技能栏
        /// </summary>
        public Transform m_P1Skills;

        /// <summary>
        /// 玩家二的技能栏
        /// </summary>
        public Transform m_P2Skills;


        /// <summary>
        /// P1的技能图标数组
        /// </summary>
        public List<Sprite> P1SkillList = new List<Sprite>();

        /// <summary>
        /// P2的技能图标数组
        /// </summary>
        public List<Sprite> P2SkillList = new List<Sprite>();

        /// <summary>
        /// 任务需求量
        /// </summary>
        private int m_MessionRequest;

        /// <summary>
        /// 当前已完成任务量
        /// </summary>
        private int m_MessionCurrent;

        /// <summary>
        /// 对UI进行初始化
        /// </summary>
        /// <param name="userData"></param>
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_MainContent = transform.Find("MainContent");
            m_GamingData = m_MainContent.Find("GamingData");
            m_Messions = m_MainContent.Find("Messions");
            m_P1Skills = m_MainContent.Find("P1Skills");
            m_P2Skills = m_MainContent.Find("P2Skills");
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            for (int i = 1; i <= 4; i++)
            {
                P1SkillList.Add(m_P1Skills.Find("Skill" + i + "(Back)/MainContent")
                    .GetComponent<Image>()
                    .sprite);
                P2SkillList.Add(m_P2Skills.Find("Skill" + i + "(Back)/MainContent")
                    .GetComponent<Image>()
                    .sprite);
            }

            GameEntry.Event.Subscribe(MessionTouchArgs.EventId, OnMessionTouch);
            GameEntry.Event.Subscribe(GameOverArgs.EventId, OnGameOver);
            GameEntry.Event.Subscribe(PlayerLivesChangeArgs.EventId,OnPlayerLivesChanged);
            m_MessionCurrent = 0;
            m_MessionRequest = 25;
            SetGameData();
        }

        private void OnPlayerLivesChanged(object sender, GameEventArgs e)
        {
            PlayerLivesChangeArgs ne = (PlayerLivesChangeArgs) e;
            SetGameData();
        }

        protected override void OnClose(object userData)
        {
            base.OnClose(userData);
            GameEntry.Event.Unsubscribe(MessionTouchArgs.EventId, OnMessionTouch);
            GameEntry.Event.Unsubscribe(GameOverArgs.EventId, OnGameOver);
        }

        private void OnGameOver(object sender, GameEventArgs e)
        {
            m_MessionCurrent = 0;
            m_MessionRequest = 25;
            SetGameData();
        }

        private void OnMessionTouch(object sender, GameEventArgs e)
        {
            ++m_MessionCurrent;
            m_Messions.Find("CompleteSituation").GetComponent<Text>().text = m_MessionCurrent + "/" + m_MessionRequest;
            if (m_MessionCurrent >= m_MessionRequest)
            {
                GameEntry.DataNode.SetData<VarInt>("Gold", GameEntry.DataNode.GetData<VarInt>("Gold") + 1000);
                ProfileMessage.ProfileSaver.SaveData();
                GameEntry.Event.Fire(this, ReferencePool.Acquire<GameOverArgs>().Fill(true));
            }
        }

        public void SetGameData()
        {
            m_GamingData.Find("Lv/Text").GetComponent<Text>().text =
                GameEntry.DataNode.GetData<VarInt>("Level").ToString();
            m_GamingData.Find("P1RemainLives/Text").GetComponent<Text>().text =
                GameEntry.DataNode.GetData<VarInt>("P1Lives").ToString();
            m_GamingData.Find("P2RemainLives/Text").GetComponent<Text>().text =
                GameEntry.DataNode.GetData<VarInt>("P2Lives").ToString();
            m_Messions.Find("CompleteSituation").GetComponent<Text>().text = m_MessionCurrent + "/" + m_MessionRequest;
        }

        /// <summary>
        /// 显示到技能面板
        /// </summary>
        /// <param name="ownerName">技能拥有者</param>
        /// <param name="toolName">技能名称</param>
        public void PutOnSkillPanel(string ownerName, string toolName)
        {
            foreach (SpriteItem t in GameManager.m_SpritesAsset.m_SpritesAssets)
            {
                if (t.m_SpriteName == toolName)
                {
                    switch (ownerName)
                    {
                        case "P1":
                            for (int i = 0; i < 4; i++)
                            {
                                if (CheckAndSetSkills(P1SkillList, m_P1Skills, i, t.Sprite, toolName))
                                {
                                    return;
                                }
                            }

                            break;
                        case "P2":
                            for (int i = 0; i < 4; i++)
                            {
                                if (CheckAndSetSkills(P2SkillList, m_P2Skills, i, t.Sprite, toolName))
                                {
                                    return;
                                }
                            }

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 检查背包的某一格是否为空,如果为空,就设置上技能图标
        /// </summary>
        /// <param name="playerSkillList">玩家技能列表</param>
        /// <param name="playerTransform">玩家技能栏</param>
        /// <param name="i">指定数组中的技能</param>
        /// <param name="skillIcon">技能图标</param>
        /// <param name="skillIcon">技能名称</param>
        /// <returns></returns>
        public bool CheckAndSetSkills(List<Sprite> playerSkillList, Transform playerTransform, int i, Sprite skillIcon,
            string spriteName)
        {
            if (playerSkillList[i] == null)
            {
                playerSkillList[i] = skillIcon;
                playerSkillList[i].name = spriteName;
                playerTransform.Find("Skill" + (i + 1) + "(Back)/MainContent")
                    .GetComponent<Image>()
                    .sprite = skillIcon;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 清空某一技能格的技能
        /// </summary>
        /// <param name="playerTransform">玩家技能栏</param>
        /// <param name="playerFlag">玩家标识</param>
        /// <param name="i">指定要置空的技能</param>
        public void SetSkillToNull(Transform playerTransform, int playerFlag, int i)
        {
            if (playerFlag == 1)
            {
                P1SkillList[i] = null;
                m_P1Skills.Find("Skill" + (i + 1) + "(Back)/MainContent")
                    .GetComponent<Image>()
                    .sprite = null;
            }
            else
            {
                P2SkillList[i] = null;
                m_P2Skills.Find("Skill" + (i + 1) + "(Back)/MainContent")
                    .GetComponent<Image>()
                    .sprite = null;
            }
        }

        /// <summary>
        /// 得到将要被置空的技能名
        /// </summary>
        /// <param name="playerFlag"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetSkillToNullName(int playerFlag, int i)
        {
            return playerFlag == 1 ? P1SkillList[i].name : P2SkillList[i].name;
        }

        /// <summary>
        /// 检查将要被置空的技能是否存在
        /// </summary>
        /// <param name="playerFlag"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public bool CheckSkillToNullName(int playerFlag, int i)
        {
            if (playerFlag == 1)
            {
                return P1SkillList[i] != null;
            }

            return P2SkillList[i] != null;
        }

        public void RemoveAllSkills()
        {
            for (int i = 0; i < 4; i++)
            {
                P1SkillList[i] = null;
                m_P1Skills.Find("Skill" + (i + 1) + "(Back)/MainContent")
                    .GetComponent<Image>()
                    .sprite = null;
                P2SkillList[i] = null;
                m_P2Skills.Find("Skill" + (i + 1) + "(Back)/MainContent")
                    .GetComponent<Image>()
                    .sprite = null;
            }
        }
    }
}