using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemPlacementHelper : Singleton<MapItemPlacementHelper>
{
    public IPlacable Placable;

    private void Update()
    {
        if (Placable is null) return;

        Plane plane = new Plane(Vector3.forward, Vector3.zero);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float entry;
        if (plane.Raycast(ray, out entry))
        {
            var pos = ray.GetPoint(entry);
            pos.x = (int)pos.x;
            pos.y = (int)pos.y;
            pos.z = 0;
            Placable.ChangePosition(pos);
        }
        bool isValidPlacement = Placable.IsValidPlacement();
        Color color = isValidPlacement ? Color.white : Color.red;
        color.a = .5f;
        Placable.ChangeAreaBackgroundColor(color);

        if (Input.GetMouseButtonDown(0) && isValidPlacement)
        {
            Placable.Place();
            Placable = null;
            return;
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            Placable.GetPlacableObject().SetDisable();
            Placable = null;
        }
    }

}

public interface IPlacable
{
    public void ChangePosition(Vector3 position);
    public void ChangeAreaBackgroundColor(Color color);
    public bool IsValidPlacement();
    public void Place();
    public GameObject GetPlacableObject();
}
