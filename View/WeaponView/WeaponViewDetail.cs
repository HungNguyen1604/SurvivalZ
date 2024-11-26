using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponViewDetail : MonoBehaviour
{
    public Image icon_wp;
    public TMP_Text name_lb;
    public GameObject price_object;
    public TMP_Text price_lb;
    public TMP_Text level_lb;
    public Vector2 original_size_dl;
    public TMP_Text damage_val_;
    public Image damage_progress;
    public Image damage_progress_next;
    public TMP_Text range_val_;
    public Image range_progress;
    public Image range_progress_next;
    public TMP_Text rof_val_;

    public Button btn_buy;
    public Button btn_equip;
    public Button btn_upgrade;
    public GameObject max_lv;

    private ConfigWeaponRecord cf_weapon;
    private ConfigWeaponRecord cf_weapon_level_next;
    private WeaponData wp_data;
    private int cash = 0;
    private bool isRegisterCash = false;
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
    }

    public void Setup(WeaponViewListItem wp_list_item)
    {
        cash = DataAPIController.instance.GetCash();
        if (!isRegisterCash)
        {
            DataTrigger.RegisterValueChange(DataPath.CASH, (data_change) =>
            {
                cash = DataAPIController.instance.GetCash();
                SetInfo();
            });
            isRegisterCash = true;
        }
        cf_weapon = wp_list_item.data.cf;
        wp_data = wp_list_item.wp_data;
        SetInfo();
    }
    private void SetInfo()
    {
        icon_wp.overrideSprite = SpriteLibraryControl.instance.GetSpriteByName(cf_weapon.Prefab);
        name_lb.text = cf_weapon.Name;
        level_lb.text = string.Empty;
        btn_buy.gameObject.SetActive(false);
        btn_equip.gameObject.SetActive(false);
        btn_upgrade.gameObject.SetActive(false);
        price_object.SetActive(false);
        max_lv.SetActive(false);
        damage_progress_next.gameObject.SetActive(false);
        range_progress_next.gameObject.SetActive(false);
        string s_rof_next = string.Empty;
        int num_max_level = ConfigManager.instance.configWeapon.GetMaxLevel(cf_weapon.id);
        if (wp_data != null)
        {
            cf_weapon = ConfigManager.instance.configWeapon.GetRecordByKeySearch(cf_weapon.id, wp_data.level);
            if (wp_data.level >= num_max_level)
            {
                max_lv.SetActive(true);
            }
            else
            {
                price_object.SetActive(true);
                level_lb.text = "Level " + wp_data.level.ToString();
                cf_weapon_level_next = ConfigManager.instance.configWeapon.GetRecordByKeySearch(cf_weapon.id, wp_data.level + 1);
                price_lb.text = cf_weapon.Price.ToString();
                btn_upgrade.gameObject.SetActive(true);
                btn_upgrade.interactable = cash >= cf_weapon.Price;

                damage_progress_next.gameObject.SetActive(true);
                range_progress_next.gameObject.SetActive(true);

                float time_anim_next = 0.5f;
                float val_damage_n = (float)cf_weapon_level_next.Damage / (float)ConfigManager.instance.configDefault.max_damage;
                damage_progress_next.rectTransform.DOSizeDelta(new Vector2(val_damage_n * original_size_dl.x, original_size_dl.y), time_anim_next);
                float val_range_n = (float)cf_weapon_level_next.Range / (float)ConfigManager.instance.configDefault.max_range;
                range_progress_next.rectTransform.DOSizeDelta(new Vector2(val_range_n * original_size_dl.x, original_size_dl.y), time_anim_next);


                s_rof_next = " -> <color=#00ffffff>" + cf_weapon_level_next.ROF.ToString();

            }
            int index = -1;
            if (DataAPIController.instance.CheckWeaponEquip(cf_weapon.id, out index))
            {

            }
            else
            {
                btn_equip.gameObject.SetActive(true);
            }
        }
        else
        {
            cf_weapon = ConfigManager.instance.configWeapon.GetRecordByKeySearch(cf_weapon.id, 1);
            price_lb.text = cf_weapon.Price.ToString();
            price_object.SetActive(true);
            btn_buy.gameObject.SetActive(true);
            btn_buy.interactable = cash >= cf_weapon.Price;
        }
        float time_anim = 0.5f;
        damage_val_.text = cf_weapon.Damage.ToString();
        float val_damage = (float)cf_weapon.Damage / (float)ConfigManager.instance.configDefault.max_damage;
        damage_progress.rectTransform.DOSizeDelta(new Vector2(val_damage * original_size_dl.x, original_size_dl.y), time_anim);
        range_val_.text = cf_weapon.Range.ToString();
        float val_range = (float)cf_weapon.Range / (float)ConfigManager.instance.configDefault.max_range;
        range_progress.rectTransform.DOSizeDelta(new Vector2(val_range * original_size_dl.x, original_size_dl.y), time_anim);


        rof_val_.text = cf_weapon.ROF.ToString() + s_rof_next;
    }


    public void OnEquip()
    {

    }
    public void OnUpgrade()
    {
        DataAPIController.instance.OnUpgrade(wp_data, (res) =>
        {
            if (res != null)
            {
                wp_data = res;
                SetInfo();

            }
            else
            {

            }
        });
    }
    public void OnBuy()
    {
        DataAPIController.instance.BuyWP(cf_weapon.id, (res) =>
        {
            if (res != null)
            {
                wp_data = res;
                SetInfo();

            }
            else
            {

            }
        });
    }
}
