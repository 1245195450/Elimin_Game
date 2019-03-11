//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年2月3日
//------------------------------------------------------------

using GameFramework.DataTable;
using GameFramework.Sound;
using GameMain.Scripts.DataTable;
using GameMain.Scripts.Utility;
using UnityGameFramework.Runtime;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.Sound
{
    public static class SoundExtension
    {
        public static int? PlaySound(this SoundComponent soundComponent, int soundId, Entity bindingEntity = null,
            object userData = null)
        {
            IDataTable<DRSound> dtSound = GameEntry.DataTable.GetDataTable<DRSound>();
            DRSound drSound = dtSound.GetDataRow(soundId);
            if (drSound == null)
            {
                Log.Warning("Can not load sound '{0}' from data table.", soundId.ToString());
                return null;
            }

            PlaySoundParams playSoundParams = new PlaySoundParams
            {
                VolumeInSoundGroup = 0.5f,
                Loop = false,
            };
            return soundComponent.PlaySound(AssetUtility.GetSoundAsset(drSound.AssetName), "Default");
        }
    }
}