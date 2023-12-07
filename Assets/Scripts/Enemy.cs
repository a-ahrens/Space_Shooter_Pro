using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 4.0f;

    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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

            Destroy(this.gameObject);
        }

        //laser collides with enemy
        if ( other.tag == "Laser" )
        {
            Destroy(other.gameObject);
            if (_player != null )
            {
                _player.IncreaseScore(10);
            }

            Destroy(this.gameObject);
        }
    }


}
