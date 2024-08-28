using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //public bool IsSelected;
    //public List<Transform> tilePoints;
    //public SpriteRenderer backgroundSprite;



    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)) IsSelected = !IsSelected;

    //    if (!IsSelected) return;

    //    Plane plane = new Plane(Vector3.forward, Vector3.zero);

    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    float entry;
    //    if (plane.Raycast(ray, out entry))
    //    {
    //        var pos = ray.GetPoint(entry);
    //        pos.x = (int)pos.x;
    //        pos.y = (int)pos.y;
    //        transform.position = pos;
    //    }

    //    bool isValidPlace = GridManager.Instance.IsPointsValidToPlace(tilePoints);
    //    Color color = isValidPlace ? Color.white : Color.red;
    //    color.a = .5f;
    //    backgroundSprite.color = color;
    //}
    public int index;
    public FactoryBase Factory;
    public void Create()
    {
        Factory.ProvideUnit(Factory.GetUnitBase(index));
    }
}
