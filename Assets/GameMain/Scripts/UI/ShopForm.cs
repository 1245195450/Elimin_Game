//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月2日
//------------------------------------------------------------

using System.Collections.Generic;
using DG.Tweening;
using GameFramework.Event;
using GameMain.Scripts.GameArgs;
using GameMain.Scripts.ProfileMessage;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.UI
{
    public class ShopForm : UGuiForm
    {
        /// <summary>
        /// 技能商品
        /// </summary>
        public List<Transform> m_skills;

        /// <summary>
        /// 回到菜单的按钮
        /// </summary>
        private Button m_Return2MenuButton;

        private Transform m_Gold;

        private CanvasGroup m_CanvasGroup;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Gold = transform.Find("MainContent/MyGold/GoldCount");
            m_CanvasGroup = GetComponent<CanvasGroup>();
            m_Return2MenuButton = transform.Find("MainContent/Return2Menu").GetComponent<Button>();
            m_Return2MenuButton.onClick.AddListener(OnReturn2MenuClick);

        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            
            GameEntry.Event.Subscribe(UpSkillArgs.EventId, OnUpSkill);
            m_Gold.GetComponent<Text>().text = GameEntry.DataNode.GetData<VarInt>("Gold").ToString();
            foreach (Transform t in m_skills)
            {
                t.GetComponent<SkillFormBase>().OnLoaded();
            }
            m_CanvasGroup.alpha = 1.0f;
        }

        private void OnReturn2MenuClick()
        {
            m_CanvasGroup.DOFade(0, 1.0f).OnComplete(() => GameEntry.UI.CloseUIForm(UIForm));
        }

        private void OnUpSkill(object sender, GameEventArgs e)
        {
            ProfileSaver.SaveData();
            m_Gold.GetComponent<Text>().text = GameEntry.DataNode.GetData<VarInt>("Gold").ToString();
        }

        protected override void OnClose(object userData)
        {
            base.OnClose(userData);
            GameEntry.Event.Unsubscribe(UpSkillArgs.EventId, OnUpSkill);
        }
    }
}