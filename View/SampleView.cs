using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SampleView : MonoBehaviour
{
    public Text wp_id;
    public Text wp_level;
    // Start is called before the first frame update
    void Start()
    {
        DataTrigger.RegisterValueChange(DataPath.DIC_WEAPON, (data) =>
        {
            Debug.Log("wp change");
        });
        DataTrigger.RegisterValueChange(DataPath.DIC_WEAPON + "/W_2", (data) =>
        {
            WeaponData weaponData = (WeaponData)data;
            wp_id.text = weaponData.id.ToString();
            wp_level.text = weaponData.level.ToString();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowInfoGun()
    {
        WeaponData weaponData = DataAPIController.instance.GetWeaponDataById(2);
        wp_id.text = weaponData.id.ToString();
        wp_level.text = weaponData.level.ToString();
    }
    public void UpgradeLevel()
    {
        DataAPIController.instance.UpgradeLevelWeaponDataById(2);
    }
}
