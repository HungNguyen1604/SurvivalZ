using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionViewListData
{
    public ConfigMissionRecord cf_mission;
    public MissionData mission_data;
}
public class MissonViewList : MonoBehaviour, IEnhancedScrollerDelegate
{
    /// <summary>
    /// Internal representation of our data. Note that the scroller will never see
    /// this, so it separates the data from the layout using MVC principles.
    /// </summary>
    private List<MissionViewListData> data_list;

    /// <summary>
    /// This is our scroller we will be a delegate for
    /// </summary>
    public EnhancedScroller scroller;

    /// <summary>
    /// This will be the prefab of each cell in our scroller. Note that you can use more
    /// than one kind of cell, but this example just has the one type.
    /// </summary>
    public EnhancedScrollerCellView cellViewPrefab;
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        // first, we get a cell from the scroller by passing a prefab.
        // if the scroller finds one it can recycle it will do so, otherwise
        // it will create a new cell.
        MissionViewIteam cellView = scroller.GetCellView(cellViewPrefab) as MissionViewIteam;

        // set the name of the game object to the cell's data index.
        // this is optional, but it helps up debug the objects in 
        // the scene hierarchy.
        cellView.name = "Cell " + dataIndex.ToString();

        // in this example, we just pass the data to our cell's view which will update its UI
        cellView.SetDataCell(data_list[dataIndex]);

        // return the cell to the scroller
        return cellView;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 600;
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return data_list.Count;
    }
    public void Setup()
    {
        data_list = new List<MissionViewListData>();
        Dictionary<string, MissionData> dic = DataAPIController.instance.GetMissionData();
        foreach (KeyValuePair<string, MissionData> kp in dic)
        {
            Debug.Log(kp.Key);
            Debug.Log(kp.Value.id);
        }
        foreach (ConfigMissionRecord cf in ConfigManager.instance.configMission.GetAllRecord())
        {
            MissionViewListData data = new MissionViewListData();
            data.cf_mission = cf;
            MissionData mission_data = null;
            dic.TryGetValue(cf.id.Tokey(), out mission_data);
            data.mission_data = mission_data;
            data_list.Add(data);
        }
        scroller.ReloadData();
    }
    void Start()
    {
        scroller.Delegate = this;
    }
}
