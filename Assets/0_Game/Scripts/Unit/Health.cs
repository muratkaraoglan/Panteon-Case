using UnityEngine;

public class Health : MonoBehaviour
{
    private int _maxHP;
    private int _currentHP;
    public void Init(int maxHP)
    {
        _maxHP = maxHP;
        _currentHP = _maxHP;
    }

    public void OnEnable()
    {
        _currentHP = _maxHP;
    }
}