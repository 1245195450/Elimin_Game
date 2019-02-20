//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月26日
//------------------------------------------------------------

using Assets.GameMain.Scripts.Buffs;
using Assets.GameMain.Scripts.Entities.EntityData;
using Assets.GameMain.Scripts.GameArgs;
using Assets.GameMain.Scripts.Games;
using DefaultNamespace;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = Assets.GameMain.Scripts.Base.GameEntry;

namespace Assets.GameMain.Scripts.Entities.EntityLogic
{
    public class MapItemsLogic : CostumEntityLogic
    {
        /// <summary>
        /// 地图数据
        /// </summary>
        private MapItemsDatas m_MapItemsDatas;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_MapItemsDatas = userData as MapItemsDatas;
            transform.position = m_MapItemsDatas.Position;
            transform.tag = m_MapItemsDatas.m_tag;
            if (m_MapItemsDatas.m_tag == "PlayerHomeBrick")
            {
                GameEntry.Event.Subscribe(SkillReleaseArgs.EventId, OnSkillRelease);
            }
        }

        /// <summary>
        /// 当技能释放时的回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSkillRelease(object sender, GameEventArgs e)
        {
            SkillReleaseArgs ne = (SkillReleaseArgs) e;
            string skillName = ne.m_ToolName.Split('.')[1];
            if (skillName == "SteelHeart")
            {
                string senderName = ne.m_ToolName.Split('.')[0];
                if (senderName == "P1")
                    AddBuff(
                        new SteelHeartBuff(this, BuffKind.SteelHeartBuff,
                            1.5f + GameEntry.DataNode.GetData<VarInt>("P1SteelHeartLv") * 0.5f));
                else
                {
                    AddBuff(
                        new SteelHeartBuff(this, BuffKind.SteelHeartBuff,
                            1.5f + GameEntry.DataNode.GetData<VarInt>("P2SteelHeartLv") * 0.5f));
                }
            }
        }

        public override void GetSteelHeart()
        {
            base.GetSteelHeart();
            transform.tag = "PlayerHomeSteel";
            transform.GetComponent<SpriteRenderer>().sprite = GameManager.m_SpritesAsset.m_SpritesAssets[5].Sprite;
        }

        public override void LoseSteelHeart()
        {
            base.LoseSteelHeart();
            transform.tag = "PlayerHomeBrick";
            transform.GetComponent<SpriteRenderer>().sprite = GameManager.m_SpritesAsset.m_SpritesAssets[4].Sprite;
        }

        public void LoseHome()
        {
            transform.GetComponent<SpriteRenderer>().sprite = GameManager.m_SpritesAsset.m_SpritesAssets[6].Sprite;
        }

        protected override void OnHide(object userData)
        {
            base.OnHide(userData);
            if (m_MapItemsDatas.m_tag == "PlayerHomeBrick")
            {
                GameEntry.Event.Unsubscribe(SkillReleaseArgs.EventId, OnSkillRelease);
            }
        }
    }
}