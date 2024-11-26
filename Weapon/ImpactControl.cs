using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactControl : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator OnEndLife()
    {
        yield return new WaitForSeconds(1);
        BYPoolManager.instance.dic_pool["Impact"].DeSpawned(transform);
    }

    public void Spawned()
    {
        StopCoroutine("OnEndLife");
        StartCoroutine("OnEndLife");
    }
    public void DeSpawned()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
}
