//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月2日
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using GameMain.Scripts.DataTable;
using GameMain.Scripts.GameArgs;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.UI
{
    /// <summary>
    /// 商品基类
    /// </summary>
    public class SkillFormBase : UGuiForm
    {
        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// 等级文本
        /// </summary>
        private Text m_Lv;

        /// <summary>
        /// 金钱文本
        /// </summary>
        private Text m_Price;

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 每级增长数
        /// </summary>
        public int Factor = 1000;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string SkillName;

        /// <summary>
        /// 购买按钮
        /// </summary>
        private Button m_BuyButton;

        /// <summary>
        /// 对应的技能ID
        /// </summary>
        public int m_TypeId;

        public void OnLoaded()
        {
            m_BuyButton = transform.GetComponent<Button>();
            m_Lv = transform.Find("Lv").GetComponent<Text>();
            m_Price = transform.Find("Price").GetComponent<Text>();
            SkillName = gameObject.name;
            ReFreshData();
            m_BuyButton.onClick.AddListener(ShowEnsureForm);
        }

        private void ReFreshData()
        {
            Level = GameEntry.DataNode.GetData<VarInt>(SkillName);
            Price = Level * Factor;
            m_Lv.text = "Lv " + Level.ToString();
            m_Price.text = "$ " + Price.ToString();
        }

        private void ShowEnsureForm()
        {
            IDataTable<DRSkillMessages> m_SkillTable = GameEntry.DataTable.GetDataTable<DRSkillMessages>();
            DRSkillMessages skillMessage = m_SkillTable.GetDataRow(m_TypeId);
            GameEntry.UI.OpenDialog(new DialogParams()
            {
                Mode = 2,
                Title = "购买确认",
                Message = "确定要购买" + skillMessage.m_Describe[GameEntry.DataNode.GetData<VarInt>(SkillName)] + "吗?",
                ConfirmText = "确定",
                CancelText = "再想想",
                OnClickConfirm = OnClickConfirm,
            });
        }

        private void OnClickConfirm(object obj)
        {
            IDataTable<DRSkillMessages> m_SkillTable = GameEntry.DataTable.GetDataTable<DRSkillMessages>();
            DRSkillMessages skillMessage = m_SkillTable.GetDataRow(m_TypeId);
            if (GameEntry.DataNode.GetData<VarInt>("Gold") >= Price)
            {
                GameEntry.DataNode.SetData<VarInt>("Gold", GameEntry.DataNode.GetData<VarInt>("Gold") - Price);
                GameEntry.DataNode.SetData<VarInt>(SkillName, GameEntry.DataNode.GetData<VarInt>(SkillName) + 1);
                GameEntry.UI.OpenDialog(new DialogParams()
                {
                    Mode = 1,
                    Title = "购买成功",
                    Message = "购买" + skillMessage.m_Describe[GameEntry.DataNode.GetData<VarInt>(SkillName)-1] + "成功",
                    ConfirmText = "好的"
                });
                GameEntry.Event.Fire(this, ReferencePool.Acquire<UpSkillArgs>().Fill(Price));
                ReFreshData();
            }
            else
            {
                GameEntry.UI.OpenDialog(new DialogParams()
                {
                    Mode = 1,
                    Title = "购买失败",
                    Message = "购买升级" + skillMessage.m_Describe[GameEntry.DataNode.GetData<VarInt>(SkillName)] +
                              "失败,金钱不足",
                    ConfirmText = "好的"
                });
            }
        }
    }
}