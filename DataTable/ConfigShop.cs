using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class ConfigShopRecord
{
    //id	bundle_id    name   cost	  value	  bonus

    public int id;
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
    private string bundle_id;
    public string Bundle_id
    {
        get
        {
            return bundle_id;
        }
    }
    [SerializeField]
    private string cost;
    public string Cost
    {
        get
        {
            return cost;
        }
    }
    [SerializeField]
    private int value;
    public int Value
    {
        get
        {
            return value;
        }
    }
    [SerializeField]
    private int bonus;
    public int Bonus
    {
        get
        {

            return bonus;
        }
    }
}
public class ConfigShop : BYDataTable<ConfigShopRecord>
{
    public override ConfigCompare<ConfigShopRecord> DefineConfigCompare()
    {
        configCompare = new ConfigCompare<ConfigShopRecord>("id");
        return configCompare;
    }

    public List<string> GetAllStoreID()
    {
        return records.Select(x => x.Bundle_id).ToList();
    }
}
