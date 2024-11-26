using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CrossBowWeapon : WeaponBehaviour
{
    //do chinh xac ,so cang lon chinh xac cang giam
    public float min_accuracy = 20;
    public float accuracy = 50; // Độ chính xác
    public float recovery_accuracy = 0.5f;
    public float max_accuracy = 70;
    public float drop_accuracy = 70;

    //ban ra mui ten
    public Transform projecties_pb;
    public string name_arrow_pool;
    public MuzzleFlash muzzleFlash;
    public override void Init()
    {
        iWeaponHandle = new ICrossBow();
        iWeaponHandle.Init(this);
    }

    public override void Setup(WPDataIngame data)
    {
        accuracy = min_accuracy;
        BYPool pool = new BYPool(name_arrow_pool, damage, projecties_pb);
        BYPoolManager.instance.AddNewPool(pool);
        base.Setup(data);
    }
    private void Update()
    {
        fire_time += Time.deltaTime;
        if (semi_auto)
        {
            if (isFire)
            {
                if (fire_time >= rof)
                {
                    fire_time = 0;
                    muzzleFlash.FireHandle();

                    iWeaponHandle.FireHandle();

                }
            }
        }
        else
        {
            if (isFire)
            {
                if (fire_time >= rof && checkSemiAuto)
                {
                    checkSemiAuto = false;
                    fire_time = 0;
                    muzzleFlash.FireHandle();

                    iWeaponHandle.FireHandle();

                }
            }
        }
        accuracy = Mathf.Lerp(accuracy, min_accuracy, Time.deltaTime * recovery_accuracy);
    }
}
public class ICrossBow : IWeaponHandle
{
    CrossBowWeapon wp;
    public void FireHandle()
    {
        wp.characterDataBinding.PlayFireWP(); // Chạy animation bắn
        //throw new System.NotImplementedException();
        CreateArrow();
    }
    // Tạo và bắn mũi tên.
    private void CreateArrow()
    {
        wp.accuracy += wp.drop_accuracy;
        wp.accuracy = Mathf.Clamp(wp.accuracy, wp.min_accuracy, wp.max_accuracy);

        wp.audioSource_.PlayOneShot(wp.sfx_fires.OrderBy(x => Guid.NewGuid()).FirstOrDefault());
        //AudioClip sfx = sfx_fires[UnityEngine.Random.Range(0, sfx_fires.Length)];
        //audioSource_.PlayOneShot(sfx);
        //StartCoroutine("SFXProgress");

        Transform arrow = BYPoolManager.instance.dic_pool[wp.name_arrow_pool].Spawned(); // Tạo mũi tên mới.

        arrow.position = wp.wpDataIngame.positionFire.GetPosFire(out Vector3 dir); // Đặt vị trí và hướng bắn.

        float accuracy_val = wp.accuracy * 0.08f;
        float x = UnityEngine.Random.Range(-accuracy_val, accuracy_val);
        float y = UnityEngine.Random.Range(-accuracy_val, accuracy_val);
        Quaternion q = Quaternion.Euler(x, y, 0);
        arrow.forward = q * dir;

        ArrowControl arrowControl = arrow.GetComponent<ArrowControl>();
        ArrowData arrowData = new ArrowData { damage = 5, name_pool = wp.name_arrow_pool };
        arrowData.force = wp.force * arrow.forward;
        arrowControl.Setup(arrowData);
    }
    public void Init(WeaponBehaviour wp)
    {
        this.wp = (CrossBowWeapon)wp;
        //throw new System.NotImplementedException();
    }

    public void ReloadHandle()
    {
        //throw new System.NotImplementedException();
    }
}
