using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemSelectionHelper : Singleton<MapItemSelectionHelper>
{
    [HideInInspector] public GameObject LastSelectedMapItemGameObject;//Stores the last selected item on the map
}
