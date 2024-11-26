using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum WPType
{
    CrossBow = 1,
    Machete = 2
}
public class WPDataIngame
{
    public ConfigWeaponRecord cf;
    public PositionFire positionFire;
}
public abstract class WeaponBehaviour : MonoBehaviour
{
    public WPType wpType;
    public AnimatorOverrideController overrideController;
    public CharacterDataBinding characterDataBinding;

    public int damage = 10;


    public float rof = 0.2f; // Tốc độ tấn công
    public float range;// tam danh
    public float force = 100;// luc tan cong

    public bool isFire; // Có đang bắn hay không
    public float fire_time;

    //diem dich ban cung mot cho
    public WPDataIngame wpDataIngame;
    public AudioSource audioSource_;
    public AudioClip[] sfx_fires;

    public IWeaponHandle iWeaponHandle;

    //nhap thi moi tan cong (danh cho melee)
    public bool semi_auto = true;
    public bool checkSemiAuto;

    public virtual void Setup(WPDataIngame data)
    {
        range = data.cf.Range;
        this.wpDataIngame = data;
        characterDataBinding = gameObject.GetComponentInParent<CharacterDataBinding>();
        Init();
    }
    public abstract void Init();
    public void ReadyWP()
    {
        gameObject.SetActive(true);
        characterDataBinding.PlayShowWP();
    }

    public void HideWP()
    {
        gameObject.SetActive(false);
    }

    public void OnFire(bool isFire_)
    {
        this.isFire = isFire_;
        checkSemiAuto = true;
        fire_time = 0;
    }
}

