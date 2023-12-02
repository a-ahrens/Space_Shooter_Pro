﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //laser movement
        transform.Translate( new Vector3(0, 1, 0) * _laserSpeed * Time.deltaTime );

        //lasers dissappear when off screen
        if( transform.position.y >= 7.0f)
        {
            Destroy(this.gameObject);
        }
    }
}