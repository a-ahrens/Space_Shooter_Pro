using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    private float _fallRate = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3
        transform.Translate( new Vector3(0, -1, 0) * _fallRate * Time.deltaTime );

        //destroy object when leave screen
        if ( transform.position.y < -5.4f )
        {
            Destroy(this.gameObject);
        }

    }

    //make it collectable by the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.tag == "Player" )
        {
            Player player = other.GetComponent<Player>();
            if ( player != null )
            {
                player.ActivatePowerup(this.gameObject.name);
            }

            Destroy(this.gameObject);
        }
    }
}
