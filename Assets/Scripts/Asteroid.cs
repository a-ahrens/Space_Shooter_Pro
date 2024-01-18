using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 3.0f;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;
    private Countdown _countdown;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _countdown = GameObject.Find("Canvas").GetComponent<Countdown>();
        if (_countdown == null)
        {
            Debug.LogError("Asteroid - Countdown not found");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //continuously rotate on the z axis
        transform.Rotate(new Vector3(0, 0, 1) * _rotationSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        _spawnManager.StartSpawning();
        _countdown.SetCountdown();
    }

    //check if Laser Collides with asteroid (trigger event)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "Laser" )
        {
            GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(this.gameObject, 0.25f);
        }
    }

}
