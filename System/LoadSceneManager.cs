using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : BySingleton<LoadSceneManager>
{
    public GameObject UI_Loading;
    public TMP_Text progress_lb;
    public Image progress_image;

    // Tải cảnh bằng chỉ số
    public void LoadSceneByIndex(int index, Action callback)
    {
        LoadSceneData data = new LoadSceneData
        {
            index = index,
            callback = callback
        };
        StartCoroutine(ProgressLoadScene(data));
    }

    // Tải cảnh bằng tên
    public void LoadSceneByName(string scene_name, Action callback)
    {
        LoadSceneData data = new LoadSceneData
        {
            scene_name = scene_name,
            callback = callback
        };
        StartCoroutine(ProgressLoadScene(data));
    }

    IEnumerator ProgressLoadScene(LoadSceneData data)
    {
        yield return new WaitForEndOfFrame();
        UI_Loading.SetActive(true);
        AsyncOperation asyncOperation = null;

        if (data.index >= 0)
            asyncOperation = SceneManager.LoadSceneAsync(data.index, LoadSceneMode.Single);
        else
            asyncOperation = SceneManager.LoadSceneAsync(data.scene_name, LoadSceneMode.Single);

        while (!asyncOperation.isDone)
        {
            yield return new WaitForSeconds(0.1f);
            // Cập nhật giao diện tiến trình nếu cần

            //cai thanh loadgame chay den 100%
            progress_lb.text = (asyncOperation.progress * 100).ToString(".0") + "%";
            progress_image.fillAmount = asyncOperation.progress;
        }

        //yield return new WaitForSeconds(1);

        if (data.callback != null)
        {
            data.callback();
        }

        UI_Loading.SetActive(false);
    }

    private class LoadSceneData
    {
        public int index = -1;
        public string scene_name;
        public Action callback;
    }
}
