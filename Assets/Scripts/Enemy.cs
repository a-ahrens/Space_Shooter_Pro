using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        //spawn in random locations within the screen
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0, -1, 0);
        //enemy movement
        transform.Translate(movement * _movementSpeed * Time.deltaTime);

        //respawn at top with random x position
        if ( transform.position.y < -5.4f )
        {
            float randomX = Random.Range(-11.3f, 11.3f);
            transform.position = new Vector3(randomX, 7.36f, 0);
        }

    }

    private void OnTriggerEnter(Collider other)
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
            Destroy(this.gameObject);
        }
    }


}
