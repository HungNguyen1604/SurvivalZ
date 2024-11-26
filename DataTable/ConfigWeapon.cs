using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum WeaponType
{
    CrossBow = 1,
    Machete = 2
}

[Serializable]
public class ConfigWeaponRecord
{
    //id	 level     weaponType	name	prefab	rof	range	damage   price

    public int id;
    [SerializeField]
    private int level;
    public int Level
    {
        get
        {
            return level;
        }
    }
    [SerializeField]
    private WeaponType weaponType;
    public WeaponType WP_Type
    {
        get
        {
            return weaponType;
        }
    }
    [SerializeField]
    private string name;
    public string Name
    {
        get
        {
            return name;
        }
    }
    [SerializeField]
    private string prefab;
    public string Prefab
    {
        get
        {
            return prefab;
        }
    }
    [SerializeField]
    private float rof;
    public float ROF
    {
        get
        {
            return rof;
        }
    }
    [SerializeField]
    private int range;
    public int Range
    {
        get
        {
            return range;
        }
    }
    [SerializeField]
    private int damage;
    public int Damage
    {
        get
        {
            return damage;
        }
    }
    [SerializeField]
    private int price;
    public int Price
    {
        get
        {
            return price;
        }
    }
}
public class ConfigWeapon : BYDataTable<ConfigWeaponRecord>
{
    public override ConfigCompare<ConfigWeaponRecord> DefineConfigCompare()
    {
        configCompare = new ConfigCompare<ConfigWeaponRecord>("id", "level");
        return configCompare;
    }
    public List<ConfigWeaponRecord> GetRecordByWeapon(int level = 1)
    {
        return records.Where(x => x.Level == level).ToList();
    }
    public List<ConfigWeaponRecord> GetRecordByWeaponType(WeaponType wp_type, int level = 1)
    {
        return records.Where(x => x.WP_Type == wp_type && x.Level == level).ToList();
    }
    public int GetMaxLevel(int id)
    {
        return records.Where(x => x.id == id).Count();
    }
}

