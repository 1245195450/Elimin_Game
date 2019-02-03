//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月29日
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameMain.Scripts.CostumAssets
{
    public class SpritesAsset : ScriptableObject
    {
        /// <summary>
        /// 精灵数组
        /// </summary>
        [SerializeField] public List<SpriteItem> m_SpritesAssets = new List<SpriteItem>();
    }
}