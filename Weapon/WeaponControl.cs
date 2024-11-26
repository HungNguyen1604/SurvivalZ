using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public delegate void ChangeWeaponHandle(WeaponBehaviour wp);
public class WeaponControl : BySingleton<WeaponControl>
{
    public CharacterControl characterControl;
    public List<WeaponBehaviour> weapons;
    public List<int> id_wps;
    public Transform anchor_wp;
    private int index_wp = -1;
    private WeaponBehaviour cur_wp;
    public UnityEvent<WeaponBehaviour> OnChangeWeapon;

    public List<PositionFire> positionFires;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        InputManager.instance.OnFire.AddListener(OnFire);
        InputManager.instance.OnChangeWP.AddListener(ChangeWeapon);
        //gan vu khi o folder Weapons vao anchor_wp
        foreach (int e in id_wps)
        {
            Debug.Log(e);
            ConfigWeaponRecord cf_wp = ConfigManager.instance.configWeapon.GetRecordByKeySearch(e, 1);
            Debug.LogError(cf_wp.Prefab);
            GameObject go = Instantiate(Resources.Load("Weapons/" + cf_wp.Prefab, typeof(GameObject))) as GameObject;

            WeaponBehaviour wp_behaviour = go.GetComponent<WeaponBehaviour>();
            WPDataIngame data = new WPDataIngame();
            data.cf = cf_wp;
            go.transform.SetParent(anchor_wp, false);
            go.SetActive(false);

            //use linq
            PositionFire positionFire = positionFires.Where(x => x.wpType == wp_behaviour.wpType).FirstOrDefault();
            positionFire.characterControl = characterControl;
            data.positionFire = positionFire;
            wp_behaviour.Setup(data);
            weapons.Add(wp_behaviour);



        }
        ChangeWeapon();
    }
    public void OnFire(bool isFire)
    {
        cur_wp.OnFire(isFire);
    }
    private void ChangeWeapon()
    {

        index_wp++;
        // kiểm tra nếu đổi tới súng cuối cùng thì quay lại súng đầu tiên
        if (index_wp >= weapons.Count)
        {
            index_wp = 0;
        }

        // Nếu có vũ khí hiện tại, hãy vô hiệu hóa nó
        if (cur_wp != null)
        {
            cur_wp.HideWP();
        }

        // Lấy vũ khí mới
        WeaponBehaviour wp = weapons[index_wp];

        // Gán vũ khí mới làm vũ khí hiện tại
        cur_wp = wp;

        if (OnChangeWeapon != null)
        {
            OnChangeWeapon.Invoke(cur_wp);
        }

        cur_wp.ReadyWP();
    }



    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        OnChangeWeapon.RemoveAllListeners();
    }
}



[Serializable]
public class PositionFire
{
    public WPType wpType;
    public Transform pos_fire;
    public Transform aim;
    public CharacterControl characterControl;
    // Tính toán hướng bắn và trả về vị trí bắn ra.
    public Vector3 GetPosFire(out Vector3 dir)
    {
        if (characterControl.cur_tran_enemy != null)
        {
            dir = characterControl.cur_tran_enemy.position - pos_fire.position; // Tính hướng từ điểm bắn đến mục tiêu.
            dir.Normalize();
        }
        else
        {
            dir = aim.position - pos_fire.position; // Tính hướng từ điểm bắn đến mục tiêu.
            dir.Normalize();
        }

        return pos_fire.position;
    }

}

