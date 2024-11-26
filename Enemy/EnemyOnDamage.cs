using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyType
{
    HEAD = 1,
    NORMAL = 2,
    UP_BODY = 3,
    LOW_BODY = 4
}
public class EnemyOnDamage : MonoBehaviour
{
    public BodyType bodyType = BodyType.NORMAL;
    private EnemyControl parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.GetComponentInParent<EnemyControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnDamage(ArrowData arrowData)
    {
        arrowData.rig_body = gameObject.GetComponent<Rigidbody>();
        arrowData.bodyType = bodyType;
        parent.OnDamage(arrowData);
    }

    public void OnDamage(MacheteData macheteData)
    {
        macheteData.rig_body = gameObject.GetComponent<Rigidbody>();
        macheteData.bodyType = bodyType;
        parent.OnDamage(macheteData);
    }
}
