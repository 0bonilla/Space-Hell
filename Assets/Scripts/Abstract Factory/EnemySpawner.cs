using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFollowPlayer _enemyFollowPlayer;
    private EnemyFactory _enemyFactory;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LayerMask _hitteableLayer;
    private PlayerController player;

    private void Start()
    {
        _enemyFactory = new EnemyFactory(_enemyFollowPlayer);
        player = FindObjectOfType<PlayerController>();
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            EnemyFollowPlayer _newEnemy = (EnemyFollowPlayer)_enemyFactory.CreateProduct();
            _newEnemy.transform.position = _spawnPoint.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _hitteableLayer) != 0 && player.LightActive)
        {
            SpawnEnemies();
            Destroy(this.gameObject);
            Debug.Log("Spawn");
        }
    }

}
