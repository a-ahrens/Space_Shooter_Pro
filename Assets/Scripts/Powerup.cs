using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _fallRate = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
            if ( player != null && transform.name == "Triple_Shot_Powerup")
            {
                player.ActivateTripleShot();
            }

            Destroy(this.gameObject);
        }
    }
}
