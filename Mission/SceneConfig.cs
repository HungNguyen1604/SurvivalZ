using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class SceneConfig : BySingleton<SceneConfig>
{
    public Transform player_pos;
    [SerializeField]
    private List<Transform> point_Spawns;

    public Transform GetPointSpawn(out int index)
    {
        index = UnityEngine.Random.Range(0, point_Spawns.Count);
        //Sap xep cac diem point va lay ra phan tu dau tien
        return point_Spawns[index];
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
