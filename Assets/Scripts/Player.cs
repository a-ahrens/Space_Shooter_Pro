using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _cooldownStart = 0f;
    private float _cooldownEnd = -1f;

    [SerializeField]
    private int _playerLives = 3;
    private SpawnManager _spawnManager;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if( _spawnManager == null )
        {
            Debug.LogError("The Spawn Manager is NULL.");
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

        //fire timer cooldown
        _cooldownStart = Time.time;
        _cooldownEnd = _cooldownStart + _fireRate;
    }

    public void Damage()
    {
        _playerLives--;

        if (_playerLives < 1) {
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();

        }
    }

    public void ActivateTripleShot()
    {
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

}


