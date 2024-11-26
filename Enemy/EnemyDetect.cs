using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public Collider collider_;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBecameVisible()
    {
        //Debug.Log("OnBecameVisible");
        collider_.enabled = true;
    }

    private void OnBecameInvisible()
    {
        //Debug.Log("OnBecameInvisible");
        collider_.enabled = false;
    }

}
