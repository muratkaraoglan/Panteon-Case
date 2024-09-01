using UnityEngine;

public interface IPlacable
{
    public void ChangePosition(Vector3 position);
    public void ChangeAreaBackgroundColor(Color color);
    public bool IsValidPlacement();
    public void Place();
    public GameObject GetPlacableObject();
}
