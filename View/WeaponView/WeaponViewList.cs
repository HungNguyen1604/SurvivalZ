using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class WeaponListData
{
    public ConfigWeaponRecord cf;
    public WeaponView weaponView;
}
public class WeaponViewList : MonoBehaviour, IEnhancedScrollerDelegate
{
    public WeaponView weaponView;
    /// <summary>
    /// Internal representation of our data. Note that the scroller will never see
    /// this, so it separates the data from the layout using MVC principles.
    /// </summary>
    private List<WeaponListData> data_list;

    /// <summary>
    /// This is our scroller we will be a delegate for
    /// </summary>
    public EnhancedScroller scroller;

    /// <summary>
    /// This will be the prefab of each cell in our scroller. Note that you can use more
    /// than one kind of cell, but this example just has the one type.
    /// </summary>
    public EnhancedScrollerCellView cellViewPrefab;

    /// <summary>
    /// Be sure to set up your references to the scroller after the Awake function. The 
    /// scroller does some internal configuration in its own Awake function. If you need to
    /// do this in the Awake function, you can set up the script order through the Unity editor.
    /// In this case, be sure to set the EnhancedScroller's script before your delegate.
    /// 
    /// In this example, we are calling our initializations in the delegate's Start function,
    /// but it could have been done later, perhaps in the Update function.
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        // first, we get a cell from the scroller by passing a prefab.
        // if the scroller finds one it can recycle it will do so, otherwise
        // it will create a new cell.
        WeaponViewListItem cellView = scroller.GetCellView(cellViewPrefab) as WeaponViewListItem;

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
        return 540;
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return data_list.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        scroller.Delegate = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void DelayJump()
    {
        scroller.RefreshActiveCellViews();
    }
    public void OnAll()
    {
        // set up some simple data
        data_list = new List<WeaponListData>();
        foreach (ConfigWeaponRecord cf in ConfigManager.instance.configWeapon.GetRecordByWeapon())
        {
            data_list.Add(new WeaponListData { cf = cf, weaponView = weaponView });
        }
        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
        scroller.JumpToDataIndex(0);
        Invoke("DelayJump", 0.1f);
    }
    public void OnRanger()
    {
        // set up some simple data
        data_list = new List<WeaponListData>();
        foreach (ConfigWeaponRecord cf in ConfigManager.instance.configWeapon.GetRecordByWeaponType(WeaponType.CrossBow))
        {
            data_list.Add(new WeaponListData { cf = cf, weaponView = weaponView });
        }
        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
        scroller.JumpToDataIndex(0);
        Invoke("DelayJump", 0.2f);
    }
    public void OnMelee()
    {
        // set up some simple data
        data_list = new List<WeaponListData>();
        foreach (ConfigWeaponRecord cf in ConfigManager.instance.configWeapon.GetRecordByWeaponType(WeaponType.Machete))
        {
            data_list.Add(new WeaponListData { cf = cf, weaponView = weaponView });
        }
        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
        scroller.JumpToDataIndex(0);
        Invoke("DelayJump", 0.2f);
    }
}
