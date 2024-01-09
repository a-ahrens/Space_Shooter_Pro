using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 4.0f;
    private float _fireRate = 3.0f;
    private float _fireTime = -1;
    private bool _canFire = true;
    private AudioSource _audioSource;
    [SerializeField] private GameObject _enemyLaserPrefab;
    private Player _player;
    private Animator _animator;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if ( _player == null )
        {
            Debug.LogError("Player is null.");
        }

        if ( _animator == null )
        {
            Debug.LogError("Animator is null");
        }

        if ( _audioSource == null )
        {
            Debug.LogError("Enemy Audio Source is null");
        }
    }

    void Update()
    {
        CalculateMovement();

        if( Time.time > _fireTime && _canFire )
        {
            _fireRate = Random.Range(3f, 7f);
            _fireTime = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
            //Debug.Break();
        }
    }

    void CalculateMovement()
    {
        Vector3 movement = new Vector3(0, -1, 0);
        transform.Translate(movement * _movementSpeed * Time.deltaTime);

        //respawn at top with random x position
        if (transform.position.y < -5.4f)
        {
            float randomX = Random.Range(-11.3f, 11.3f);
            transform.position = new Vector3(randomX, 7.36f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        //player collides with enemy
        if ( other.tag == "Player" )
        {
            
            Player player = other.GetComponent<Player>();
            if ( player != null )
            {
                player.Damage();
            }

            _audioSource.Play();
            _animator.SetTrigger("OnEnemyDeath");
            _movementSpeed = 0;
            //_audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 3.0f);

        }
    }

    public void Damage()
    {
        if (_player != null)       //if player isnt dead then
        {
            _player.IncreaseScore(10);
        }

        _canFire = false;
        _audioSource.Play();
        _animator.SetTrigger("OnEnemyDeath");
        _movementSpeed = 0;
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 3.0f);
        
    }

}
