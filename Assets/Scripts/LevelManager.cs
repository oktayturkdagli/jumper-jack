using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    PlayerPrefData _playerPrefData;

    private void Start()
    {
        _playerPrefData = FindObjectOfType<PlayerPrefData>();
    }

    public void ResetScene()
    {
        StartCoroutine(ResetSceneCoroutine());
    }

    public void ChangeSceneNext()
    {
        StartCoroutine(ChangeSceneNextCoroutine());
    }
    
    private IEnumerator ResetSceneCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the same scene
    }
    
    private IEnumerator ChangeSceneNextCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (_playerPrefData.GetCurrentLevel() + 1 > _playerPrefData.GetHighLevel()) //Open random scene if your current level is higher than the highest level
        {
            _playerPrefData.CurrentLevelIncrease();
            SceneManager.LoadScene(Random.Range(0, _playerPrefData.GetHighLevel() - 1)); // Load random scene
        }
        else
        {
            _playerPrefData.CurrentLevelIncrease();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
