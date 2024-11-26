using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDialog : MonoBehaviour
{
    public DialogIndex dialogIndex;
    private BaseDialogAnimation baseDialogAnimation;
    private RectTransform rect_;
    // Start is called before the first frame update
    void Awake()
    {
        rect_ = gameObject.GetComponent<RectTransform>();
        baseDialogAnimation = gameObject.GetComponentInChildren<BaseDialogAnimation>();
    }

    public virtual void Setup(DialogParam data)
    {

    }
    private void ShowDialog(DialogCallBack dialogCallBack)
    {
        rect_.SetAsLastSibling();//dialog sau de len dialog truoc
        baseDialogAnimation.OnShowDialog(() =>
        {
            OnShowDialog();
            dialogCallBack.callback?.Invoke();
        });
    }
    public virtual void OnShowDialog()
    {

    }
    private void HideDialog(DialogCallBack dialogCallBack)
    {
        baseDialogAnimation.OnHideDialog(() =>
        {
            OnHideDialog();
            dialogCallBack.callback?.Invoke();
        });
    }
    public virtual void OnHideDialog()
    {

    }
}
