using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : BySingleton<MenuControl>
{
    public RectTransform top;
    public RectTransform right;
    public RectTransform bottom;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == 1)
        {
            top.DOAnchorPosY(0, 0.5f);
            right.DOAnchorPosX(0, 0.5f);
            bottom.DOAnchorPosY(0, 0.25f);
        }
    }
}
