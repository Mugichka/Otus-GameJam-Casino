using UnityEngine;

public sealed class LoseScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private LoseScreen _loseScreenScript;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.PlayerDead += ShowLoseScreen;
    }

    private void OnDisable()
    {
        _player.PlayerDead -= ShowLoseScreen;
    }

    private void ShowLoseScreen()
    {
        _loseScreen.SetActive(true);
        _loseScreenScript.ShowLoseScreen();
    }
}
