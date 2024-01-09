using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8.0f;
    private bool _isEnemyLaser = false;

    void Update()
    {
        //laser movement
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    private void MoveUp()
    {
        transform.Translate(new Vector3(0, 1, 0) * _laserSpeed * Time.deltaTime);

        //lasers disappear when off screen
        if (transform.position.y >= 7.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void MoveDown()
    {
        transform.Translate(new Vector3(0, -1, 0) * _laserSpeed * Time.deltaTime);

        //lasers disappear when off screen
        if (transform.position.y <= -7.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = collision.GetComponent<Player>();
            player.Damage();
            Destroy(this.gameObject);
        }

        if (collision.tag == "Enemy" && _isEnemyLaser == false)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Damage();
            Destroy(this.gameObject);
        }
    }
}
