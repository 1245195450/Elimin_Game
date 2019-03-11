//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月25日
//------------------------------------------------------------

using GameFramework;
using GameMain.Scripts.Entities.EntityData;
using GameMain.Scripts.GameArgs;
using UnityEngine;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.Entities.EntityLogic
{
    public class BullectLogic : UnityGameFramework.Runtime.EntityLogic
    {
        private BullectData m_BullectData;


        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_BullectData = userData as BullectData;
            transform.position = m_BullectData.Position;
            transform.rotation = m_BullectData.Rotation;
            transform.tag = m_BullectData.m_tag;
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            transform.Translate(Vector3.up * m_BullectData.Speed * Time.deltaTime, Space.Self);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Air"))
            {
                if (Entity.isActiveAndEnabled)
                {
                    GameEntry.Entity.HideEntity(Entity);
                }

                return;
            }

            switch (transform.tag)
            {
                case "EnemyBullect":
                    if (Entity.isActiveAndEnabled)
                    {
                        switch (collision.transform.tag)
                        {
                            case "Steel":
                            case "PlayerHomeSteel":
                                GameEntry.Entity.HideEntity(Entity);
                                break;
                            case "Heart":
                                GameEntry.Entity.HideEntity(Entity);
                                collision.transform.GetComponent<MapItemsLogic>().LoseHome();
                                break;
                            case "PlayerHomeBrick":
                            case "Brick":
                                GameEntry.Entity.HideEntity(Entity);
                                if (collision.transform.GetComponent<UnityGameFramework.Runtime.EntityLogic>().Entity
                                    .isActiveAndEnabled)
                                    GameEntry.Entity.HideEntity(collision.transform
                                        .GetComponent<UnityGameFramework.Runtime.EntityLogic>().Entity);
                                break;
                            case "P1":
                            case "P2":
                                GameEntry.Entity.HideEntity(Entity);
                                if (collision.transform.GetComponent<PlayerTankLogic>().Entity
                                    .isActiveAndEnabled)
                                {
                                    collision.transform.GetComponent<PlayerTankLogic>()
                                        .BeHited(m_BullectData.Damage);
                                }
                                break;
                        }
                    }

                    break;
                case "P1Bullect":
                case "P2Bullect":
                    switch (collision.transform.tag)
                    {
                        case "PlayerHomeBrick":
                        case "PlayerHomeSteel":
                            if (Entity.isActiveAndEnabled)
                            {
                                GameEntry.Entity.HideEntity(Entity);
                            }

                            break;

                        case "Brick":
                            if (collision.transform.GetComponent<UnityGameFramework.Runtime.EntityLogic>().Entity
                                .isActiveAndEnabled)
                                GameEntry.Entity.HideEntity(collision.transform
                                    .GetComponent<UnityGameFramework.Runtime.EntityLogic>().Entity);
                            if (Entity.isActiveAndEnabled)
                            {
                                GameEntry.Entity.HideEntity(Entity);
                            }

                            break;
                        case "Steel":
                            if (m_BullectData.Damage > 3)
                            {
                                GameEntry.Entity.HideEntity(collision.transform
                                    .GetComponent<UnityGameFramework.Runtime.EntityLogic>().Entity);
                            }
                            else if (Entity.isActiveAndEnabled)
                            {
                                GameEntry.Entity.HideEntity(Entity);
                            }

                            break;
                        case "EnemyBullect":
                            if (m_BullectData.Damage > 2)
                            {
                                m_BullectData.Damage--;
                                GameEntry.Entity.HideEntity(collision.transform
                                    .GetComponent<UnityGameFramework.Runtime.EntityLogic>().Entity);
                            }
                            else if (Entity.isActiveAndEnabled)
                            {
                                GameEntry.Entity.HideEntity(Entity);
                                GameEntry.Entity.HideEntity(collision.transform
                                    .GetComponent<UnityGameFramework.Runtime.EntityLogic>().Entity);
                            }

                            break;
                        case "Enemy":
                            collision.transform.GetComponent<EnemyTankLogic>()
                                .BeHited(m_BullectData.Damage);
                            if (Entity.isActiveAndEnabled)
                                GameEntry.Entity.HideEntity(Entity);
                            break;
                    }

                    break;
            }
        }
    }
}