//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月29日
//------------------------------------------------------------

using System.IO;
using GameMain.Scripts.CostumAssets;
using UnityEditor;
using UnityEngine;

namespace Assets.GameMain.Editor
{
    public static class PathSetting
    {
        /// <summary>
        /// .asset文件储存路径
        /// </summary>
        public static readonly string SavePath = "Assets/GameMain/CostumAssets";
    }

    public class CreateCostumSpritesAsset : UnityEditor.Editor
    {
        [MenuItem("CreateCostumSpritesAsset/CreateSpritesAssets")]
        static void Create()
        {
            //先创建需要被序列化为.asset类实例
            ScriptableObject m_SpritesAsset = ScriptableObject.CreateInstance<SpritesAsset>();
            //是否存在保存路径,如果不存在,就创建
            if (!Directory.Exists(PathSetting.SavePath))
            {
                Directory.CreateDirectory(PathSetting.SavePath);
            }

            //带有asset文件名的储存路径
            string m_SavePath = string.Format("{0}/{1}.asset", PathSetting.SavePath, "SpritesAsset");
            //创建.asset文件
            AssetDatabase.CreateAsset(m_SpritesAsset, m_SavePath);
            
        }
    }
}