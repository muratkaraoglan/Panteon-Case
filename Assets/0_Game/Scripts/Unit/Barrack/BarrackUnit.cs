using UnityEngine;

public class BarrackUnit : Unit, IPlacable
{
    private SoldierFactory _armyFactory;

    public void SetArmyFactory(SoldierFactory armyFactory) => _armyFactory = armyFactory;
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
        _backgroundSpriteRenderer.enabled = false;
        _isPlaced = true;
        GridManager.Instance.FillEmptyPoints(_tilePoints);
    }
    
}
