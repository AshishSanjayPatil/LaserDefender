using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField]
    GameObject laserExplosionVFX;

    EnemySpawner enemySpawner;

    private float speed = 15f;

    private Rigidbody2D rigid;

    private int health = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(0, speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damaging = other.GetComponent<DamageDealer>();

        if (damaging)
        {
            health -= damaging.Damage();

            if (health <= 0)
            {
                GameObject newLExplosionVFX = Instantiate(laserExplosionVFX, transform.position, Quaternion.identity);
                newLExplosionVFX.transform.parent = enemySpawner.CleanUpContainer();
                Destroy(newLExplosionVFX, 1f);
                Destroy(this.gameObject);
            }
        }
    }
}
