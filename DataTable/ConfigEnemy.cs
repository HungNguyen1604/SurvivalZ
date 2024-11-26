using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigEnemyRecord
{
    //id	name	prefab	damage	hp	skill


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
    private string prefab;
    public string Prefab
    {
        get
        {
            return prefab;
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
    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
    }
    [SerializeField]
    private int skill;
    public int Skill
    {
        get
        {
            return skill;
        }
    }

}
public class ConfigEnemy : BYDataTable<ConfigEnemyRecord>
{
    public override ConfigCompare<ConfigEnemyRecord> DefineConfigCompare()
    {
        configCompare = new ConfigCompare<ConfigEnemyRecord>("id");
        return configCompare;
    }
}
