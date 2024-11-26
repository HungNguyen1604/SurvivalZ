using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacheteData
{
    public string name_pool;
    public int damage = 1;
    public Rigidbody rig_body;
    public BodyType bodyType;
}
public class MacheteControl : MonoBehaviour
{
    private Transform trans;
    private MacheteData data;
    public LayerMask mask;

    void Awake()
    {
        trans = transform;
    }
    // Update is called once per frame
    void Update()
    {
        trans.Translate(Vector3.forward * Time.deltaTime * 20);
        if (Physics.Raycast(trans.position, trans.forward, out RaycastHit hitInfo, 1, mask))
        {
            EnemyOnDamage enemyOnDamage = hitInfo.collider.GetComponent<EnemyOnDamage>();
            if (enemyOnDamage != null)
            {
                enemyOnDamage.OnDamage(data);
            }
        }
    }
    public void Setup(MacheteData data)
    {
        this.data = data;
    }
}
