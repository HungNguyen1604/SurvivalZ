using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowData
{
    public string name_pool;
    public int damage = 1;
    public Rigidbody rig_body;
    public Vector3 force;//vi tri ban
    public Vector3 point_impact;
    public BodyType bodyType;
}
public class ArrowControl : MonoBehaviour
{
    private Transform trans;
    private Transform impact_cf;

    private ArrowData data;

    public LayerMask mask;

    void Awake()
    {
        trans = transform;
    }


    private IEnumerator OnEndLife()
    {
        yield return new WaitForSeconds(3);
        BYPoolManager.instance.dic_pool[data.name_pool].DeSpawned(transform);
    }
    public void Spawned()
    {
        StopCoroutine("OnEndLife");
        StartCoroutine("OnEndLife");
    }
    public void DeSpawned()
    {
    }
    void Update()
    {
        trans.Translate(Vector3.forward * Time.deltaTime * 20);
        if (Physics.Raycast(trans.position, trans.forward, out RaycastHit hitInfo, 1, mask))
        {
            Transform impact = BYPoolManager.instance.dic_pool["Impact"].Spawned();
            impact.position = hitInfo.point;
            impact.forward = hitInfo.normal;
            BYPoolManager.instance.dic_pool[data.name_pool].DeSpawned(trans);
            data.point_impact = hitInfo.point;
            EnemyOnDamage enemyOnDamage = hitInfo.collider.GetComponent<EnemyOnDamage>();
            if (enemyOnDamage != null)
            {
                enemyOnDamage.OnDamage(data);
            }
        }
    }
    public void Setup(ArrowData data)
    {
        this.data = data;
    }

    private void OnBecameInvisible()
    {
        BYPoolManager.instance.dic_pool[data.name_pool].DeSpawned(trans);
    }
}


