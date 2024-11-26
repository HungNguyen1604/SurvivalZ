using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TapWeaponButton : MonoBehaviour
{
    public List<Button> buttions;
    public List<UnityEvent> onEvent_Clicks;
    // Start is called before the first frame update
    public void Init()
    {
        OnClick(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick(int index)
    {
        for (int i = 0; i < buttions.Count; i++)
        {
            buttions[i].interactable = !(i == index);
        }
        if (onEvent_Clicks.Count >= index + 1)
            onEvent_Clicks[index].Invoke();
    }
}
