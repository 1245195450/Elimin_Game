//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年1月26日
//------------------------------------------------------------

using GameFramework.DataTable;
using GameMain.Scripts.DataTable;
using GameMain.Scripts.Utility;
using UnityGameFramework.Runtime;
using GameEntry = GameMain.Scripts.Base.GameEntry;

namespace GameMain.Scripts.UI
{
    public static class UIExtension
    {
        public static int? OpenUIForm(this UIComponent uiComponent, int uiFormId, object userData = null)
        {
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            string assetName = AssetUtility.GetUIFormAsset(drUIForm.AssetName);
            return uiComponent.OpenUIForm(assetName, drUIForm.GroupName,
                drUIForm.PauseCoveredUIForm, userData);
        }

        public static int? OpenUIForm(this UIComponent uiComponent, UIFormId uiFormId, object userData = null)
        {
            return uiComponent.OpenUIForm((int) uiFormId, userData);
        }


        public static void OpenDialog(this UIComponent uiComponent, DialogParams dialogParams)
        {
            uiComponent.OpenUIForm(UIFormId.DialogForm, dialogParams);
        }
    }
}