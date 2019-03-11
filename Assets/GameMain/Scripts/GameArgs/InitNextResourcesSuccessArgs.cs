//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月27日
//------------------------------------------------------------

using GameFramework.Event;

namespace GameMain.Scripts.GameArgs
{
    /// <summary>
    /// 下一场景已初始化完毕事件
    /// </summary>
    public class LoadNextResourcesSuccessArgs : GameEventArgs
    {
        /// <summary>
        /// 事件ID
        /// </summary>
        public static readonly int EventId = typeof(LoadNextResourcesSuccessArgs).GetHashCode();

        /// <summary>
        /// 将要展现的资源已加载完毕
        /// </summary>
        public bool LoadNextResourcesSuccess { get; set; }

        public LoadNextResourcesSuccessArgs Fill(bool loadSuccessflag)
        {
            LoadNextResourcesSuccess = loadSuccessflag;
            return this;
        }

        public override void Clear()
        {
            LoadNextResourcesSuccess = false;
        }

        public override int Id => EventId;
    }
}