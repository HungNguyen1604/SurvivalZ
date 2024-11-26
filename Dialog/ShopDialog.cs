using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopDialog : BaseDialog
{
    public TMP_Text cash_lb;
    public override void Setup(DialogParam data)
    {
        int cash = DataAPIController.instance.GetCash();
        cash_lb.text = cash.ToString();
    }
    public override void OnShowDialog()
    {
        DataTrigger.RegisterValueChange(DataPath.CASH, OnCashChange);
    }
    public override void OnHideDialog()
    {
        DataTrigger.UnRegisterValueChange(DataPath.CASH, OnCashChange);

    }
    private void OnCashChange(object dataChange)
    {
        cash_lb.text = ((int)dataChange).ToString();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnClose()
    {
        DialogManager.instance.HideDialog(this.dialogIndex);
    }
}
