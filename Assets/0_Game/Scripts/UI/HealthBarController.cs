using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image _filledImage;
    [SerializeField] private GameObject _healthBarParentGameObject;

    public void ChangeHealthBarFillAmount(HealthChangeEventData? data)
    {
        if (data.HasValue)
        {
            _healthBarParentGameObject.SetEnable();
            HealthChangeEventData dataValue = data.GetValueOrDefault();
            _healthBarParentGameObject.SetEnable();
            float percent = (float)dataValue.CurrentHealth / dataValue.MaxHealth;
            _filledImage.fillAmount = percent;
            if (dataValue.CurrentHealth <= 0) _healthBarParentGameObject.SetDisable();
        }
        else _healthBarParentGameObject.SetDisable();
    }
}
