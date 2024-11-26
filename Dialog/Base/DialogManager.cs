using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : BySingleton<DialogManager>
{
    public Transform anchorDialog;
    private Dictionary<DialogIndex, BaseDialog> dic_dialog = new Dictionary<DialogIndex, BaseDialog>();
    private List<BaseDialog> ls_dialog_show = new List<BaseDialog>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (DialogIndex dl in DialogConfig.dialogIndices)
        {
            string dialog_name = dl.ToString();
            GameObject dialog_object = Instantiate(Resources.Load("Dialog/" + dialog_name, typeof(GameObject))) as GameObject;
            dialog_object.transform.SetParent(anchorDialog, false);
            BaseDialog base_dialog = dialog_object.GetComponent<BaseDialog>();

            dic_dialog.Add(dl, base_dialog);
            dialog_object.SetActive(false);
        }
    }

    public void ShowDialog(DialogIndex dialogIndex, DialogParam param = null, Action callback = null)
    {
        BaseDialog base_dl = dic_dialog[dialogIndex];
        base_dl.gameObject.SetActive(true);
        base_dl.Setup(param);

        DialogCallBack dialogCallBack = new DialogCallBack();
        dialogCallBack.callback = callback;

        base_dl.SendMessage("ShowDialog", dialogCallBack);
        if (!ls_dialog_show.Contains(base_dl))
        {
            ls_dialog_show.Add(base_dl);
        }
    }
    public void HideDialog(DialogIndex dialogIndex)
    {
        BaseDialog base_dl = dic_dialog[dialogIndex];

        DialogCallBack dialogCallBack = new DialogCallBack();
        dialogCallBack.callback = () =>
        {
            base_dl.gameObject.SetActive(false);
        };


        base_dl.SendMessage("HideDialog", dialogCallBack);
        if (ls_dialog_show.Contains(base_dl))
        {
            ls_dialog_show.Remove(base_dl);
        }
    }
    public void HideAllDialog()
    {
        foreach (BaseDialog dl in ls_dialog_show)
        {
            DialogCallBack dialogCallBack = new DialogCallBack();
            dialogCallBack.callback = () =>
            {
                dl.gameObject.SetActive(false);
            };
            dl.SendMessage("HideDialog", dialogCallBack);
        }
        ls_dialog_show.Clear();
    }
    /*public void SwitchView(ViewIndex viewIndex, ViewParam param = null, Action callback = null)
    {
        if (current_view != null)
        {
            ViewCallBack viewCallBack = new ViewCallBack();
            viewCallBack.callback = () =>
            {
                current_view.gameObject.SetActive(false);
                OnShowNewView(viewIndex, param, callback);
            };
            current_view.SendMessage("HideView", viewCallBack);
        }
        else
        {
            OnShowNewView(viewIndex, param, callback);
        }
    }
    private void OnShowNewView(ViewIndex viewIndex, ViewParam param = null, Action callback = null)
    {
        current_view = dic_dialog[viewIndex];
        current_view.gameObject.SetActive(true);
        current_view.Setup(param);
        ViewCallBack viewCallBack = new ViewCallBack();
        viewCallBack.callback = callback;

        current_view.SendMessage("ShowView", viewCallBack);
    }*/
}

public class DialogCallBack
{
    public Action callback;
}

