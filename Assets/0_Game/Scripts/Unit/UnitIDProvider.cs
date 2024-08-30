using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitIDProvider
{
    private static int _currentID = 0;

    public static int ProvideID => _currentID++;
}
