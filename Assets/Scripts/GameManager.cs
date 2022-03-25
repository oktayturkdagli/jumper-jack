using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private PlayerPrefData _playerPrefData;

    private void Awake()
    {
        _playerPrefData = FindObjectOfType<PlayerPrefData>();
        //ResetPPData();
        _playerPrefData.LoadPPData();
        Time.timeScale = 0.0f;
    }


    public void ResetPPData()
    {
        _playerPrefData.ResetPPData();
    }



}
