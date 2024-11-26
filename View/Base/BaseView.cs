using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    public ViewIndex viewIndex;
    private BaseViewAnimation baseViewAnimation;
    // Start is called before the first frame update
    void Awake()
    {
        baseViewAnimation = gameObject.GetComponentInChildren<BaseViewAnimation>();
    }

    public virtual void Setup(ViewParam data)
    {

    }
    private void ShowView(ViewCallBack viewCallBack)
    {
        baseViewAnimation.OnShowView(() =>
        {
            OnShowView();
            viewCallBack.callback?.Invoke();
        });
    }
    public virtual void OnShowView()
    {

    }
    private void HideView(ViewCallBack viewCallBack)
    {
        baseViewAnimation.OnHideView(() =>
        {
            OnHideView();
            viewCallBack.callback?.Invoke();
        });
    }
    public virtual void OnHideView()
    {

    }
}
