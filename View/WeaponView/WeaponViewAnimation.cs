using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class WeaponViewAnimation : BaseViewAnimation
{
    public RectTransform top;
    public RectTransform right;
    public RectTransform bottom;
    public override void OnHideView(Action callback)
    {
        top.DOAnchorPosY(250, 0.5f).OnComplete(() =>
        {
            callback();
        });
        right.DOAnchorPosX(600, 0.25f);
        bottom.DOAnchorPosY(-600, 0.25f);
    }
    public override void OnShowView(Action callback)
    {
        top.DOAnchorPosY(0, 0.5f).OnComplete(() =>
        {
            callback();
        });
        right.DOAnchorPosX(0, 0.25f);
        bottom.DOAnchorPosY(0, 0.25f);
    }
}
