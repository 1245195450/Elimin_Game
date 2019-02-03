//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月30日
//------------------------------------------------------------

using Assets.GameMain.Scripts.Base;
using Assets.GameMain.Scripts.Entities.EntityData;
using UnityEngine;

namespace Assets.GameMain.Scripts.Entities.EntityLogic
{
    public class GodDefendLogic : UnityGameFramework.Runtime.EntityLogic
    {
        /// <summary>
        /// 拿到无敌的数据
        /// </summary>
        public GodDefendData m_GodDefendData;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_GodDefendData = userData as GodDefendData;
            GameEntry.Entity.AttachEntity(m_GodDefendData.Id, m_GodDefendData.OwnerId, m_GodDefendData);
        }

        protected override void OnAttachTo(UnityGameFramework.Runtime.EntityLogic parentEntity,
            Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);
            transform.localPosition = new Vector3(0,0,0);
        }

        
    }
}