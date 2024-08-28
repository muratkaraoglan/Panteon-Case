using UnityEngine;

public class BarrackUnit : Unit, IPlacable
{
    private ArmyFactory _armyFactory;

    public void SetArmyFactory(ArmyFactory armyFactory) => _armyFactory = armyFactory;
    public void ChangeAreaBackgroundColor(Color color)
    {
        _backgroundSpriteRenderer.color = color;
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }

    public bool IsValidPlacement()
    {
        return GridManager.Instance.IsPointsValidToPlace(_tilePoints);
    }

    public GameObject GetPlacableObject()
    {
        return gameObject;
    }

    public void Place()
    {
        GridManager.Instance.FillEmptyPoints(_tilePoints);
    }
}
