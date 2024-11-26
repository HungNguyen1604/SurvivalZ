using SWS;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControl : FSM_System
{
    public int hp;
    public int max_hp;
    public int damage = 5;
    public float range_Attack;
    public int range_Detect;
    public float attack_speed;//toc do danh
    public float time_count_attack;//cooldown danh tay


    public Transform trans;
    public CharacterControl characterControl;

    public NavMeshAgent agent_;//day la cai map
    public bool isMoveAgent;//di vong qua chuong ngai vat tren navmesh
    public float time_delay_agent = 0;

    protected ArrowData arrowdata;
    protected MacheteData machetedata;

    public Transform anchor_hub;
    private EnemyHub enemyhub;
    private EnemyIndicator enemyIndicator;

    public bool isAlive = true;
    public GameObject enemy_detect;

    private void Awake()
    {
        trans = transform;

        //gan tag cho player
        GameObject char_object = GameObject.FindGameObjectWithTag("Player");
        characterControl = char_object.GetComponent<CharacterControl>();

    }
    public virtual void Setup(ConfigEnemyRecord configEnemy)
    {
        hp = configEnemy.HP;
        max_hp = hp;
        damage = configEnemy.Damage;
        /*//Invoke("OnDead", 3);
        int index = 0;
        transform.position = SceneConfig.instance.GetPointSpawn(out index).position;
        int min = index + 1;
        int max = min + 1;
        string path = "Path_" + UnityEngine.Random.Range(min, max);
        splineMove_.pathContainer = WaypointManager.Paths[path];*/


        //create Hub
        enemyhub = BYPoolManager.instance.dic_pool["Enemyhub"].Spawned().GetComponent<EnemyHub>();
        enemyhub.Init(IngameUI.instance.parent_hub, anchor_hub);

        //create Indicator
        enemyIndicator = BYPoolManager.instance.dic_pool["EnemyIndicator"].Spawned().GetComponent<EnemyIndicator>();
        enemyIndicator.Init(IngameUI.instance.parent_indicator, anchor_hub);

    }

    private void Start()
    {

    }
    public void OnDead()
    {
        isAlive = false;
        Destroy(enemy_detect);
        BYPoolManager.instance.dic_pool["Enemyhub"].DeSpawned(enemyhub.transform);
        BYPoolManager.instance.dic_pool["EnemyIndicator"].DeSpawned(enemyIndicator.transform);
        MissionManager.instance.EnemyOnDead(this);
        Invoke("DelayDestroy", 3);
    }
    private void DelayDestroy()
    {
        Destroy(gameObject);
    }
    public override void Update()
    {
        base.Update();
        time_count_attack += Time.deltaTime;
    }


    public virtual void OnDamage(ArrowData arrowData)
    {
        enemyhub.UpDateDamage(hp, max_hp);
    }
    public virtual void OnDamage(MacheteData macheteData)
    {
        enemyhub.UpDateDamage(hp, max_hp);
        Debug.Log(gameObject.name + "HP : " + hp);
    }

    //tao cai vong tron co mau xanh duong quanh nguoi con zombie
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range_Attack);
    }

}
