using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/* 
 * Author:     Thau Shien Hsu
 * Student ID: 1007996
 */

public class CrateSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cratePrefab;

    [SerializeField] private float _minSpawnTime = 2;
    [SerializeField] private float _maxSpawnTime = 5;


    [SerializeField] private float _minX = -3;
    [SerializeField] private float _maxX = 3;

    [SerializeField] private float _hoverSpeed = 10;
    private float _dir = -1;

    private float _timer;



    // Use this for initialization
    void Start()
    {
        _timer = Random.Range(_minSpawnTime, _maxSpawnTime + 1);

    }
	
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < _minX || transform.position.x > _maxX)
        {
            _dir *= -1;
        }

        transform.position += (new Vector3(_dir, 0, 0)) * _hoverSpeed * Time.deltaTime;

        if (_timer <= 0)
        {
            _timer = Random.Range(_minSpawnTime, _maxSpawnTime + 1);

            Instantiate(_cratePrefab,
                        transform.position,
                        new Quaternion((float)Random.Range(0,360),(float)Random.Range(0,360),(float)Random.Range(0,360), 0)
                        );
        }

        _timer -= Time.deltaTime;
    }
}
