//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月28日
//------------------------------------------------------------

using Assets.GameMain.Scripts.Entities.EntityData;
using Assets.GameMain.Scripts.GameArgs;
using GameFramework;
using UnityEngine;
using GameEntry = Assets.GameMain.Scripts.Base.GameEntry;

namespace Assets.GameMain.Scripts.Entities.EntityLogic
{
    public class ToolsLogic : UnityGameFramework.Runtime.EntityLogic
    {
        /// <summary>
        /// 道具数据
        /// </summary>
        private ToolsData m_ToolsData;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_ToolsData = userData as ToolsData;
            transform.position = m_ToolsData.Position;
            transform.tag = m_ToolsData.m_tag;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.transform.tag)
            {
                case "P1":
                case "P2":
                    switch (transform.tag)
                    {
                        case "LevelUp":
                            collision.transform.GetComponent<PlayerTankLogic>().LevelUp();
                            if (Entity.isActiveAndEnabled)
                            {
                                GameEntry.Entity.HideEntity(Entity);
                            }

                            break;
                        case "LifeIncrease":
                            collision.transform.GetComponent<PlayerTankLogic>().IncreaseLife();
                            if (Entity.isActiveAndEnabled)
                            {
                                GameEntry.Entity.HideEntity(Entity);
                            }

                            break;
                        case "TimePause":
                        case "ScreenHurt":
                        case "SteelHeart":
                        case "GodTime":
                            GameEntry.Event.Fire(this,
                                ReferencePool.Acquire<GetToolsArgs>().Fill(collision.transform.tag, transform.tag));
                            if (Entity.isActiveAndEnabled)
                            {
                                GameEntry.Entity.HideEntity(Entity);
                            }
                            break;
                    }
                    break;
            }
        }
    }
}