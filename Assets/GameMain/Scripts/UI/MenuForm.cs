//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月23日
//------------------------------------------------------------

using System.Collections.Generic;
using DG.Tweening;
using GameFramework;
using GameMain.Scripts.Definition.Constant;
using GameMain.Scripts.ProfileMessage;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.UI
{
    public class MenuForm : UGuiForm
    {
        /// <summary>
        /// 菜单UI的主要内容
        /// </summary>
        private Transform m_MainContent;

        /// <summary>
        /// 选择指示器
        /// </summary>
        private Transform m_Selector;

        /// <summary>
        /// 单人模式
        /// </summary>
        private Transform m_One;

        /// <summary>
        /// 双人模式
        /// </summary>
        private Transform m_Two;

        /// <summary>
        /// 商城入口
        /// </summary>
        private Transform m_Shop;

        /// <summary>
        /// 储存选项的数组
        /// </summary>
        private List<Transform> m_ListToSelect = new List<Transform>();

        /// <summary>
        /// 用于调节透明度实现渐变效果
        /// </summary>
        private CanvasGroup m_CanvasGroup;


        /// <summary>
        /// 已经点击过space
        /// </summary>
        private bool m_HasHit;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_MainContent = transform.Find("MainContent");
            m_Selector = m_MainContent.Find("Selector");
            m_One = m_MainContent.Find("ONE");
            m_ListToSelect.Add(m_One);
            m_Two = m_MainContent.Find("TWO");
            m_ListToSelect.Add(m_Two);
            m_Shop = m_MainContent.Find("Shop");
            m_ListToSelect.Add(m_Shop);
            m_CanvasGroup = transform.GetComponent<CanvasGroup>();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_CanvasGroup.alpha = 0;
            m_CanvasGroup.DOFade(1.0f, 1.0f);
            m_HasHit = false;
            GameFrameworkLog.Info("Enter Menu");
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            //如果空格被点击过一次,就把所有操作屏蔽
            if (m_HasHit) return;
            if (Input.GetKeyDown(KeyCode.W))
            {
                ChangePosition(-1);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ChangePosition(1);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_HasHit = true;
                if (m_Selector.position == m_One.position)
                {
                    GameEntry.DataNode.GetOrAddNode("PlayerMode").SetData<VarInt>(1);
                    ProfileReader.Init();
                    m_CanvasGroup.DOFade(0, 1.0f)
                        .OnComplete(ToGame);
                }
                else if (m_Selector.position == m_Two.position)
                {
                    GameEntry.DataNode.GetOrAddNode("PlayerMode").SetData<VarInt>(2);
                    ProfileReader.Init();
                    m_CanvasGroup.DOFade(0, 1.0f)
                        .OnComplete(ToGame);
                }
                else if (m_Selector.position == m_Shop.position)
                {
                    ProfileReader.Init();
                    m_CanvasGroup.DOFade(0, 1.0f)
                        .OnComplete(() => GameEntry.UI.OpenUIForm(UIFormId.ShopForm));
                }
            }
        }

        private void ToGame()
        {
            GameEntry.DataNode.SetData<VarString>(Constant.ProcedureRunnigData.TransitionalMessage,
                "Scene " + GameEntry.DataNode.GetData<VarInt>("Level"));
            
            //打开过渡界面
            GameEntry.UI.OpenUIForm(UIFormId.TransitionalForm);

            //设置下一场景名称
            GameEntry.DataNode.SetData<VarString>(Constant.ProcedureRunnigData.NextSceneName, "Game");
            
            //可以切换流程了
            GameEntry.DataNode.SetData<VarBool>(Constant.ProcedureRunnigData.CanChangeProcedure, true);
        }

        protected override void OnResume()
        {
            base.OnResume();
            m_HasHit = false;
            m_CanvasGroup.DOFade(1, 0.5f);
        }

        /// <summary>
        /// 切换选项位置
        /// </summary>
        /// <param name="flag">规定,上为-1,下为+1</param>
        private void ChangePosition(int flag)
        {
            int i;
            for (i = 0; i < m_ListToSelect.Count; i++)
            {
                if (m_Selector.position == m_ListToSelect[i].position)
                {
                    break;
                }
            }

            if (i + flag < m_ListToSelect.Count && i + flag >= 0)
            {
                m_Selector.position = m_ListToSelect[i + flag].position;
            }
        }
    }
}