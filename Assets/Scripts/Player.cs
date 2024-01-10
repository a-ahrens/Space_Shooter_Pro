using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _score = 0;
                     private int _highScore = 0;
    [SerializeField] private int _playerLives = 3;

    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _fireRate = 0.5f;
                     private float _cooldownStart = 0f;
                     private float _cooldownEnd = -1f;

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldVisualizer;
    [SerializeField] private GameObject _rightEngineDamage;
    [SerializeField] private GameObject _leftEngineDamage;


    [SerializeField] private AudioClip _fireLaserAudio;
    [SerializeField] private AudioClip _explosionAudio;
                     private AudioSource _audioSource;


    [SerializeField] private bool _isTripleShotActive = false;
    [SerializeField] private bool _isShieldActive = false;

                     private SpawnManager _spawnManager;
                     private UIManager _uiManager;



    // Start is called before the first frame update
    void Start()
    {
        _rightEngineDamage.SetActive(false);
        _leftEngineDamage.SetActive(false);
        
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        
        LoadHighScore();

        if( _spawnManager == null )
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if( _uiManager == null )
        {
            Debug.LogError("The UI Manager is NULL");
        }

        if (_audioSource == null )
        {
            Debug.LogError("The Player Audio Source is Null");
        }
        else
        {
            _audioSource.clip = _fireLaserAudio;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
        if ( Input.GetKeyDown(KeyCode.Space) && Time.time > _cooldownEnd )
        {
            FireLaser();
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float leftConstraint = -11.3f;
        float rightConstraint = 11.3f;
        float upperConstraint = 0.0f;
        float lowerConstraint = -3.8f;

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, lowerConstraint, upperConstraint), 0);

        if (transform.position.x >= rightConstraint)
        {
            transform.position = new Vector3(leftConstraint, transform.position.y, 0);
        }
        else if (transform.position.x <= leftConstraint)
        {
            transform.position = new Vector3(rightConstraint, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        
        //set laser firing position
        Vector3 laserStart = new Vector3(transform.position.x, transform.position.y + 1.0f, 0);
        
        if ( _isTripleShotActive == true )
        {
            GameObject triple = Instantiate(_tripleShotPrefab, laserStart, Quaternion.identity );
        } 
        else
        {
            Instantiate(_laserPrefab, laserStart, Quaternion.identity);
        }

        //activate laser sound
        _audioSource.Play();

        //fire timer cooldown
        _cooldownStart = Time.time;
        _cooldownEnd = _cooldownStart + _fireRate;
    }

    public void Damage()
    {
        if ( _isShieldActive == true )
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive( false );
        }
        else
        {
            _playerLives--;
            EngineFireAnimation();
            _uiManager.UpdateLiveSprites(_playerLives);
        }

        if (_playerLives < 1) {
            _audioSource.clip = _explosionAudio;
            _audioSource.Play();

            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
            CheckHighScore();
        }
    }

    private void EngineFireAnimation()
    {
        if (_playerLives == 2)
        {
            _rightEngineDamage.SetActive( true );
        }

        if ( _playerLives == 1) 
        {
            _leftEngineDamage.SetActive( true );
        }
    }

    public void ActivateTripleShot()
    {
        //need to check if _isTriplShotActive is currently true.
        //if so, find way to add time to current coroutine or cancel it and start a new one
        
        _isTripleShotActive = true;
        StartCoroutine(DeactivateTripleShot());
    }

    IEnumerator DeactivateTripleShot()
    {
        yield return new WaitForSeconds(7.0f);
        _isTripleShotActive = false;
    }

    public void ActivateSpeedPowerup()
    {
        _speed *= 2.0f;
        StartCoroutine(DeactivateSpeedBoost());
    }

    IEnumerator DeactivateSpeedBoost()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= 2.0f;
    }

    public void ActivateShieldPowerup()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void IncreaseScore( int points )
    {
        _score += points;
        _uiManager.UpdateScoreText( _score );
    }
    private void CheckHighScore()
    {
        if (_score > _highScore)
        {
            _highScore = _score;
            _uiManager.UpdateHighScoreText( _highScore );
        }
    }
    private void LoadHighScore()
    {
        //_highScore = 20;
        Debug.Log("Loading High Score: " + _highScore);
        _uiManager.UpdateHighScoreText( _highScore );
    }
}


