using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using Unity.Burst.CompilerServices;

public class MacheteWeapon : WeaponBehaviour
{
    public Transform attackPoint; // Điểm tấn công
    public float attackRange = 1.0f; // Phạm vi tấn công

    public override void Init()
    {
        iWeaponHandle = new Machete();
        iWeaponHandle.Init(this);
    }

    public override void Setup(WPDataIngame data)
    {
        base.Setup(data);
    }

    private void Update()
    {
        if (semi_auto)
        {
            if (isFire)
            {
                iWeaponHandle.FireHandle();
            }
        }
        else
        {
            if (isFire)
            {
                if (checkSemiAuto)
                {
                    checkSemiAuto = false;
                    iWeaponHandle.FireHandle();
                }
            }
        }
    }
}

public class Machete : IWeaponHandle
{
    MacheteWeapon wp;

    public void FireHandle()
    {
        wp.characterDataBinding.PlayFireWP(); // Chạy animation tấn công
        PerformAttack(); // Thực hiện tấn công
    }

    private void PerformAttack()
    {
        GameObject gameObject = new GameObject();
        MacheteControl macheteControl = gameObject.GetComponent<MacheteControl>();
        MacheteData macheteData = new MacheteData { damage = 5 };
        macheteControl.Setup(macheteData);
    }

    public void Init(WeaponBehaviour wp)
    {
        this.wp = (MacheteWeapon)wp;
    }

    public void ReloadHandle()
    {
        // Không cần thiết cho vũ khí cận chiến
    }
}

