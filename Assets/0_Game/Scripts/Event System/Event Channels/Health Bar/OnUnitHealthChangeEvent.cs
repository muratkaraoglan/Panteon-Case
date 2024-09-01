using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Health Change Event", menuName = "Events/Unit Health Change Event")]
public class OnUnitHealthChangeEvent : GameEventBaseSO<HealthChangeEventData?>
{


}

public struct HealthChangeEventData
{
    public int CurrentHealth;
    public int MaxHealth;
}