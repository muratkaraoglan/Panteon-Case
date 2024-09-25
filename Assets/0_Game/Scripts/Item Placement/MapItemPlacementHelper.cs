using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MapItemPlacementHelper : Singleton<MapItemPlacementHelper>
{
    public IPlacable Placable;

    Plane plane = new Plane(Vector3.forward, Vector3.zero);
    private void Update()
    {
        if (Placable is null) return;

        Ray ray = CameraController.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);

        float entry;
        if (plane.Raycast(ray, out entry))
        {
            var pos = ray.GetPoint(entry).ToInt();

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
