using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8.0f;

    void Update()
    {
        //laser movement
        transform.Translate( new Vector3(0, 1, 0) * _laserSpeed * Time.deltaTime );

        //lasers dissappear when off screen
        if( transform.position.y >= 7.0f )
        {
            if( transform.parent != null )
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
