using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogIndex
{
    SettingDialog = 0,
    WeaponInfoDialog = 1,
    MessageDialog = 2,
    ShopDialog = 3,
    EquipWeaponDialog = 4,
    DialogTheSameAccount = 5
}
public class DialogParam
{

}
public class MessageDialogParam : ViewParam
{
    public string mess;
}
public class EquipWeaponDialogParam
{
    public int id_new_wp;
}
public class DialogConfig
{
    public static DialogIndex[] dialogIndices = {
        DialogIndex.SettingDialog,
        DialogIndex.WeaponInfoDialog,
        DialogIndex.MessageDialog,
        DialogIndex.ShopDialog,
        DialogIndex.EquipWeaponDialog,
        DialogIndex.DialogTheSameAccount
    };
}
