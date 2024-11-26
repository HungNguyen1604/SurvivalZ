using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : BySingleton<IngameUI>
{
    public Image icon_cash;
    public TMP_Text icon_text;
    public Button button;

    public Joystick joystick;
    public Transform trans_fire;
    public TMP_Text hp_lb;
    public Image hp_progress;
    public RectTransform parent_hub;
    public RectTransform parent_indicator;
    public TMP_Text wave_lb;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        WeaponControl.instance.OnChangeWeapon.AddListener(OnChangeWeapon);
        InputManager.instance.joystick = joystick;
        yield return new WaitForSeconds(0.2f);
        MissionManager.instance.OnWaveChange.AddListener((c_wave, total) =>
        {
            wave_lb.text = c_wave.ToString() + "/" + total.ToString();
        });
    }

    private void OnChangeWeapon(WeaponBehaviour wp)
    {
        Debug.LogError(" wp : " + wp.name);
    }
    public void OnHP_PlayerChange(int hp, int maxhp)
    {
        hp_lb.text = hp.ToString();

        float val = (float)hp / (float)maxhp;
        float x = val * 235f;
        hp_progress.rectTransform.sizeDelta = new Vector2(x, 30);
        // Đổi màu thanh máu theo lượng HP
        if (val < 0.3f)
        {
            hp_progress.color = Color.red; // Dưới 30% đổi sang màu đỏ
        }
        else if (val < 0.7f)
        {
            hp_progress.color = Color.yellow; // Từ 30% đến dưới 70% đổi sang màu vàng
        }
        else
        {
            hp_progress.color = Color.green; // Trên 70% giữ màu xanh lá cây
        }
    }
    private void Update()
    {


    }
    private void OnDisable()
    {
        //WeaponControl.instance.OnChangeWeapon -= OnChangeWeapon;
    }

    public void OnButton()
    {
        Debug.LogError("OnButton");
    }
    public void OnClickDemo()
    {
    }
    public void OnFire(bool isFire)
    {
        float scale = isFire ? 0.8f : 1f;
        trans_fire.localScale = Vector3.one * scale;
        InputManager.instance.SetFire(isFire);

    }
    public void OnChangeWP()
    {
        InputManager.instance.SetChangeWP();
    }

    public void OnBack()
    {
        LoadSceneManager.instance.LoadSceneByName("Buffer", null);
    }
}
