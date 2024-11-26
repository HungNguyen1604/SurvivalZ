using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponViewListItem : EnhancedScrollerCellView
{
    public Image icon_wp;
    public TMP_Text name_lb;
    public TMP_Text level_lb;
    public TMP_Text equip_lb;

    public WeaponData wp_data;
    public WeaponView weaponView;

    public GameObject lock_object;
    public GameObject select_object;
    public WeaponListData data;
    public void SetDataCell(WeaponListData data)
    {
        this.data = data;
        this.weaponView = data.weaponView;
        select_object.SetActive(false);
        icon_wp.overrideSprite = SpriteLibraryControl.instance.GetSpriteByName(data.cf.Prefab);
        name_lb.text = data.cf.Name;
        equip_lb.text = string.Empty;
        level_lb.text = string.Empty;

        wp_data = DataAPIController.instance.GetWeaponDataById(data.cf.id);
        if (wp_data != null)
        {
            level_lb.text = "Level " + wp_data.level.ToString();
            int index = -1;
            if (DataAPIController.instance.CheckWeaponEquip(data.cf.id, out index))
            {
                equip_lb.text = "Equip " + index.ToString();
            }
        }
        lock_object.SetActive(wp_data == null);

    }
    public void OnClick()
    {
        if (weaponView.Cur_wp_item_list == this)
        {
            return;
        }
        select_object.SetActive(true);
        weaponView.OnWeaponSelected(this);
    }
    public void OnDeSelect()
    {
        select_object.SetActive(false);
    }
    public override void RefreshCellView()
    {
        if (cellIndex == 0)
        {
            OnClick();
        }
    }
}
