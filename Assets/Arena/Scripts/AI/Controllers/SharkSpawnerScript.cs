using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
 * Author:     Thau Shien Hsu
 * Student ID: 1007996
 */


public class SharkSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject _entry;
    [SerializeField] private GameObject _exit;

    [SerializeField] private List<GameObject> _patrolNodes;


    [SerializeField] private GameObject _sharkPrefab;


    private GameObject _shark = null;


    private float _timer;

    // Use this for initialization
    void Start()
    {
        _timer = (float)Random.Range(5, 10);
    }
	
    // Update is called once per frame
    void Update()
    {
        if (_shark == null && _timer < 0.0f)
        {
            _shark = Instantiate(_sharkPrefab, _entry.transform.position, new Quaternion());
            _shark.GetComponent<SharkAIScript>().SetNewTarget(GetRandomNode());
            _shark.GetComponent<SharkAIScript>().SetSpawnerReference(gameObject);

        }
        else if(_timer > 0.0f)
        {
            _timer -= Time.deltaTime;
        }
    }

    public void DestroyShark()
    {
        Destroy(_shark);
        _shark = null;
        _timer = (float)Random.Range(10, 15);
    }

    public GameObject GetRandomNode()
    {
        return _patrolNodes[Random.Range(0, _patrolNodes.Count)];
    }

    public GameObject GetExit()
    {
        return _exit;
    }
}
