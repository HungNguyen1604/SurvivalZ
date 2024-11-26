using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MissionViewIteam : EnhancedScrollerCellView
{
    public Image iconMission;
    public TMP_Text mission_name;
    public GameObject lock_object;
    public GameObject star_parent_object;
    public GameObject[] stars;
    public GameObject cur_select;
    public void SetDataCell(MissionViewListData data)
    {
        iconMission.overrideSprite = SpriteLibraryControl.instance.GetSpriteByName("Mission_Image_" + data.cf_mission.id.ToString());
        mission_name.text = data.cf_mission.Name;


        if (data.mission_data == null)
        {
            lock_object.SetActive(true);
            star_parent_object.SetActive(false);
            cur_select.SetActive(false);
        }
        else
        {
            star_parent_object.SetActive(true);
            lock_object.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                stars[i].SetActive(data.mission_data.star > i);
            }
            cur_select.SetActive(data.mission_data.star == 0);
        }


    }
    public void OnSelected()
    {

    }
}
