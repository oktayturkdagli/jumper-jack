using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Sprite _pauseSprite, _playSprite;

    [SerializeField]
    private TextMeshProUGUI _diamondText, _levelText, _rewardItem1Text, _healthText;

    [SerializeField]
    private Image _Progressbar; //Progress Bar


    GameObject _gameCanvas, _finishCanvas, _winScreen, _loseScreen;

    private Transform _player;
    private GameObject _mainMenuCanvas, _playPauseButton;
    private float _distancePlayerbtwFinish; //The distance between the player and finish line
    PlayerPrefData _playerPrefData;

    private void Start()
    {
        AssignInitialValues();
        SetHealthText();
    }

    private void FixedUpdate()
    {
        ProgressbarFill();
        SetHealthText();
    }

    void AssignInitialValues()
    {
        _player = GameObject.FindWithTag("Player").transform; // Holds Player
        _mainMenuCanvas = GameObject.FindWithTag("Main Menu Canvas"); // Holds Main Menu Canvas
        _playPauseButton = GameObject.FindWithTag("Pause Button"); // Holds Play Button
        _diamondText.text = PlayerPrefs.GetInt("TotalDiamond").ToString(); // Total number of diamond
        _levelText.text = PlayerPrefs.GetInt("CurrentLevel").ToString(); // Current Level
        _distancePlayerbtwFinish = (GameObject.FindWithTag("Last Dancer").transform.position.z + 5) - _player.position.z; // The distance between the player and finish line
        _gameCanvas = GameObject.FindWithTag("Game Canvas"); // Holds Game Canvas
        _finishCanvas = GameObject.FindWithTag("Finish Canvas"); // Holds Finish Canvas
        _winScreen = _finishCanvas.transform.GetChild(0).gameObject; // Holds Win Screen
        _loseScreen = _finishCanvas.transform.GetChild(1).gameObject;  // Holds Lose Screen
        _playerPrefData = FindObjectOfType<PlayerPrefData>();
    }

    // Fill progress bar
    void ProgressbarFill()
    {
        _Progressbar.GetComponent<Image>().fillAmount = _player.position.z / _distancePlayerbtwFinish;
    }

    // Change Play / Pause button
    public void PausePlayButton()
    {
        if (Time.timeScale == 1f) // If the button was pressed while the game was running
        {
            _mainMenuCanvas.GetComponent<Canvas>().enabled = true; // Show Main Menu Canvas
            _playPauseButton.GetComponent<Image>().sprite = _playSprite;
            Time.timeScale = 0.0f; // Stop Game
        }
        else // if the button was pressed while the game was stopped
        {
            Time.timeScale = 1.0f; // Start Game
            _mainMenuCanvas.GetComponent<Canvas>().enabled = false; // Show Game Canvas
            _playPauseButton.GetComponent<Image>().sprite = _pauseSprite;
        }

    }

    public void Continue(bool isSuccessful)
    {
        if (!isSuccessful)
            FindObjectOfType<LevelManager>().ResetScene();
        else
            FindObjectOfType<LevelManager>().ChangeSceneNext();
    }

    public void Finish(bool isSuccessful)
    {
        _gameCanvas.SetActive(false); // Hide Game Canvas
        _finishCanvas.SetActive(true); // Show Finish Canvas
        //PlayerPrefs.SetInt("Heart", 0);
        Time.timeScale = 0.0f;

        if (isSuccessful)
        {
            _rewardItem1Text.text = _playerPrefData.GetCurrentDiamond().ToString(); // Show the number of earned diamonds on Finish Canvas
            _playerPrefData.AddCurrentDiamondOnTotalDiamond();
            _playerPrefData.SetCurrentDiamond(0);
            _winScreen.SetActive(true); // Show win screen
        }
        else
        {
            _playerPrefData.SetCurrentDiamond(0);
            _loseScreen.SetActive(true); // Show lose screen
        }
    }

    public void SetHealthText()
    {
        _healthText.text = FindObjectOfType<PlayerPrefData>().GetHeart().ToString();
    }

}
