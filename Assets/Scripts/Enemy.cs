using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 4.0f;

    private Player _player;
    private Animator _animator;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if ( _player == null )
        {
            Debug.LogError("Player is null.");
        }

        _animator = gameObject.GetComponent<Animator>();

        if ( _animator == null )
        {
            Debug.LogError("Animator is null");
        }
    }

    void Update()
    {
        Vector3 movement = new Vector3(0, -1, 0);
        transform.Translate(movement * _movementSpeed * Time.deltaTime);

        //respawn at top with random x position
        if ( transform.position.y < -5.4f )
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
            _animator.SetTrigger("OnEnemyDeath");
            _movementSpeed = 0;
            Destroy(this.gameObject, 3.0f);
        }

        //laser collides with enemy
        if ( other.tag == "Laser" )
        {
            Destroy(other.gameObject);

            if (_player != null )       //if player isnt dead then
            {
                _player.IncreaseScore(10);
            }
            _animator.SetTrigger("OnEnemyDeath");
            _movementSpeed = 0;
            
            Destroy(this.gameObject, 3.0f);
        }

        
    }


}
