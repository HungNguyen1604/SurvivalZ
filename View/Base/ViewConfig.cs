using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewIndex
{
    EmptyView = 0,
    HomeView = 1,
    WeaponView = 2,
    MissionView = 3
}
public class ViewParam
{

}
public class WeaponViewParam : ViewParam
{
    public int weaponID;
}
public class ViewConfig
{
    public static ViewIndex[] viewIndices = {
        ViewIndex.EmptyView,
        ViewIndex.HomeView,
        ViewIndex.WeaponView,
        ViewIndex.MissionView
    };
}
