using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class GameManager : Singleton<GameManager> {

    [Header("Screens")]
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject endScreen;
    [SerializeField] Text coinText;
    [Header("Game")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject lavaPrefab;

    Player _currentPlayer;
    Lava _currentLava;
    
    public void StartGame() {
        startScreen.SetActive(false);
        endScreen.SetActive(false);
        LevelManager.Instance.CreateLevel();
        _currentPlayer = Instantiate(playerPrefab).GetComponent<Player>();
        Camera.Instance.SetTarget(_currentPlayer.gameObject);
        _currentLava = Instantiate(lavaPrefab).GetComponent<Lava>();
        Invoke(nameof(StartLava), .5f);
    }

    public void NextLevel() {
        LevelManager.Instance.CreateLevel();
        _currentPlayer.ResetPlayer();
        _currentLava.ResetLava();
        Invoke(nameof(StartLava), .5f);
    }

    public void EndGame() {
        LevelManager.Instance.ResetLevel();
        coinText.text = "Moedas : " + _currentPlayer.GetTotalCoins().ToString();
        Destroy(_currentPlayer.gameObject);
        Destroy(_currentLava.gameObject);
        endScreen.SetActive(true);
    }

    void StartLava() {
        _currentLava.SetSpeed(.5f * LevelManager.Instance.GetLevel());
    }

}
