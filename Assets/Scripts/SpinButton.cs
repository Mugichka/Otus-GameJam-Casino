using UnityEngine;
using System;
using TMPro;

public sealed class SpinButton : MonoBehaviour
{
    public event Action spinAllowed;

    [SerializeField] private Player _player;
    [SerializeField] private int _priceToSpin = 100;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private SpinButtonTrigger _spinButtonTrigger;

    private void Start()
    {
        _text.text = $"{_priceToSpin}";
    }

    private void OnEnable()
    {
        _spinButtonTrigger.ButtonTriggered += DoSpin;
    }

    private void OnDisable()
    {
        _spinButtonTrigger.ButtonTriggered -= DoSpin;
    }

    private void DoSpin()
    {
        if (_player.Money >= _priceToSpin+10)
        {
            spinAllowed?.Invoke();
            _player.Money -= _priceToSpin;
            _priceToSpin += 100;
            _text.text = $"{_priceToSpin}";
        }
    }
}
