using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _fallRate = 3;
    [SerializeField]
    private int _powerupID;

    void Update()
    {
        transform.Translate( new Vector3(0, -1, 0) * _fallRate * Time.deltaTime );

        if ( transform.position.y < -5.4f )
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.tag == "Player" )
        {
            Player player = other.GetComponent<Player>();
            if ( player != null && _powerupID == 0 )
            {
                player.ActivateTripleShot();
            }

            if ( player != null && _powerupID == 1 )
            {
                player.ActivateSpeedPowerup();
            }

            if ( player != null && _powerupID == 2 )
            {
                //player.ActivateShieldPowerup();
            }

            Destroy(this.gameObject);
        }
    }
}
