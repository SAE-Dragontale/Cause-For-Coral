using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Author:     Thau Shien Hsu
 * Student ID: 1007996
 */


public class OctopusAIScript : PredatorAI
{
    //[SerializeField] private GameObject _target;
    private GameObject _lastNode;

    // Used to fix unknown error
    [SerializeField] private GameObject _missingNode;
    // Use this for initialization
    void Start()
    {
        _state = State.PATROL;
        _speed = _normalSpeed;
    }
	
    // Update is called once per frame
    void Update()
    {
        Vector3 tempForward = _target.transform.position - transform.position;

        tempForward.y = 0;

        transform.forward = tempForward.normalized;
		
        transform.position += transform.forward * _speed * Time.deltaTime;

        if (_target.tag == "Player")
        {
            if (!_target.GetComponent<_Pawn>()._isAlive)
            {
                _target = _missingNode;
                _lastNode = null;
                _state = State.PATROL;
                _speed = _normalSpeed;
            }
        }
    }



    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && _target.tag != "Player")
        {
            _lastNode = _target;
            _target = collider.gameObject;
            _state = State.PURSUIT;
            _speed = _pursuitSpeed;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            _lastNode = _target;
            _state = State.PATROL;
            _speed = _normalSpeed;
        }
    }

    void OnCollisionEnter(Collision collide)
    {
        if (collide.collider.gameObject.tag == "Player")
        {
			collide.collider.gameObject.GetComponent<_Pawn>()._isAlive = false;
            _target = _lastNode;
            _lastNode = null;
            _state = State.PATROL;
            _speed = _normalSpeed;
        }
    }
}
