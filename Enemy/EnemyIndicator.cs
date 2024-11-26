using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    private RectTransform parent_rect;
    private Transform anchor;
    //public RectTransform rect;
    private void Awake()
    {
        //rect = gameObject.GetComponent<RectTransform>();
    }
    public void Init(RectTransform parent, Transform anchor)
    {
        this.parent_rect = parent;
        transform.SetParent(parent, true);
        this.anchor = anchor;
        //rect.anchoredPosition = Vector2.zero;
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
