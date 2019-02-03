//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月11日 16:00:28
//------------------------------------------------------------

namespace GameMain.Scripts.Definition.Constant
{
    public class Constant
    {
        /// <summary>
        /// 流程运行时的数据
        /// </summary>
        public static class ProcedureRunnigData
        {
            /// <summary>
            /// 需要加载的下一场景名称
            /// </summary>
            public const string NextSceneName = "NextSceneName";

            /// <summary>
            /// 游戏模式,单人或双人
            /// </summary>
            public const string PlayerMode = "PlayerMode";

            /// <summary>
            /// 是否可以改变游戏流程
            /// </summary>
            public const string CanChangeProcedure = "CanChangeProcedure";

            /// <summary>
            /// 过渡界面文字提示
            /// </summary>
            public const string TransitionalMessage = "TransitionalMessage";

            /// <summary>
            /// 当前关卡
            /// </summary>
            public const string CurrentLevel = "CurrentLevel";
        }
    }
}