//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月24日
//------------------------------------------------------------

using GameMain.Scripts.Entities.EntityData;
using GameMain.Scripts.Entities.EntityLogic;
using GameMain.Scripts.Utility;
using UnityGameFramework.Runtime;

namespace GameMain.Scripts.Entities
{
    public enum ToolsId
    {
        /// <summary>
        /// 时间暂停
        /// </summary>
        TimePause = 40000,

        /// <summary>
        /// 升级
        /// </summary>
        LevelUp = 40001,

        /// <summary>
        /// 续命
        /// </summary>
        LifeIncrease = 40002,

        /// <summary>
        /// 全屏杀伤
        /// </summary>
        ScreenHurt = 40003,

        /// <summary>
        /// 钢铁之心
        /// </summary>
        SteelHeart = 40004,

        /// <summary>
        /// 无敌时间
        /// </summary>
        GodTime = 40005
    }

    public static class EntityExtension
    {
        // 关于 EntityId 的约定：
        // 0 为无效
        // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
        // 负值用于本地生成的临时实体（如特效、FakeObject等）
        public static int s_SerialId = 0;

        /// <summary>
        /// 生成玩家
        /// </summary>
        /// <param name="entityComponent">所拓展的组件</param>
        /// <param name="playerTankData">玩家数据</param>
        public static void ShowPlayerTank(this EntityComponent entityComponent, PlayerTankData playerTankData)
        {
            entityComponent.ShowEntity<PlayerTankLogic>(playerTankData.Id,
                AssetUtility.GetEntityAsset(playerTankData.AssetName),
                playerTankData.GroupName, playerTankData);
        }
        /// <summary>
        /// 生成玩家无敌子实体
        /// </summary>
        /// <param name="entityComponent">所拓展的组件</param>
        /// <param name="playerTankData">玩家数据</param>
        public static void ShowPlayerGodDefend(this EntityComponent entityComponent, GodDefendData godDefendData)
        {
            entityComponent.ShowEntity<GodDefendLogic>(godDefendData.Id,
                AssetUtility.GetEntityAsset(godDefendData.AssetName),
                godDefendData.GroupName, godDefendData);
        }
        /// <summary>
        /// 生成子弹
        /// </summary>
        /// <param name="entityComponent">所拓展的组件</param>
        /// <param name="bullectData">子弹数据</param>
        public static void ShowBullect(this EntityComponent entityComponent, BullectData bullectData)
        {
            entityComponent.ShowEntity<BullectLogic>(bullectData.Id, AssetUtility.GetEntityAsset(bullectData.AssetName),
                bullectData.GroupName, bullectData);
        }

        /// <summary>
        /// 生成敌人
        /// </summary>
        /// <param name="entityComponent">所拓展的组件</param>
        /// <param name="enemyTankData">敌人数据</param>
        public static void ShowEnemy(this EntityComponent entityComponent, EnemyTankData enemyTankData)
        {
            entityComponent.ShowEntity<EnemyTankLogic>(enemyTankData.Id,
                AssetUtility.GetEntityAsset(enemyTankData.AssetName),
                enemyTankData.GroupName, enemyTankData);
        }

        /// <summary>
        /// 生成地图组件
        /// </summary>
        /// <param name="entityComponent">所拓展的组件</param>
        /// <param name="mapItemsDatas">地图组件</param>
        public static void ShowMapItems(this EntityComponent entityComponent, MapItemsDatas mapItemsDatas)
        {
            entityComponent.ShowEntity<MapItemsLogic>(mapItemsDatas.Id,
                AssetUtility.GetEntityAsset(mapItemsDatas.AssetName), mapItemsDatas.GroupName, mapItemsDatas);
        }

        /// <summary>
        /// 生成道具
        /// </summary>
        /// <param name="entityComponent">所拓展的组件</param>
        /// <param name="mapItemsDatas">道具数据</param>
        public static void ShowTools(this EntityComponent entityComponent, ToolsData toolsData)
        {
            entityComponent.ShowEntity<ToolsLogic>(toolsData.Id,
                AssetUtility.GetEntityAsset(toolsData.AssetName), toolsData.GroupName, toolsData);
        }

        /// <summary>
        /// 生成连续ID
        /// </summary>
        /// <param name="entityComponent">实体组件</param>
        /// <returns>返回设置的ID</returns>
        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --s_SerialId;
        }
    }
}