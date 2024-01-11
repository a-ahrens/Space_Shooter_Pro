using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _highScoreText;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Image _liveImage;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager not found");
        }
    }

    void Update()
    {

    }

    public void UpdateScoreText(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateHighScoreText(int highScore)
    {
        _highScoreText.text = "High Score: " + highScore.ToString();
        Debug.Log("High Score: " + highScore);
    }

    public void UpdateLiveSprites(int lives)
    {

        _liveImage.sprite = _liveSprites[lives];


        if( lives == 0 )
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        StartCoroutine(FlickerGameOver());
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
    }

    IEnumerator FlickerGameOver()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);

            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
        }

    }
}
