using UnityEngine;
using UnityEngine.EventSystems;

public class Health : MonoBehaviour, IPointerDownHandler
{

    [Header("Event")]
    [SerializeField] private OnUnitHealthChangeEvent _healthChangeEvent;

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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            _healthChangeEvent?.RaiseEvent(new HealthChangeEventData
            {
                CurrentHealth = _currentHP,
                MaxHealth = _maxHP
            });
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHP -= damage;

        if (MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject == gameObject)
        {
            _healthChangeEvent?.RaiseEvent(new HealthChangeEventData
            {
                CurrentHealth = _currentHP,
                MaxHealth = _maxHP
            });
        }
        if (_currentHP <= 0)
        {
            gameObject.SetDisable();
        }
    }


}