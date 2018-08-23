using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Author:     Thau Shien Hsu
 * Student ID: 1007996
 */

public class SharkAIScript : PredatorAI
{
	[SerializeField] private List<GameObject> _nextTarget = new List<GameObject>();

    private SharkSpawnerScript _spawner;

	void Start()
	{
		_state = State.PATROL;
		_rb = GetComponent<Rigidbody>();
		_timer = Random.Range(10, 20);
        _speed = _normalSpeed;
	}
    // Update is called once per frame
    void Update()
    {

        if (_target.tag == "Player")
        {
            if (!_target.GetComponent<_Pawn>()._isAlive)
            {
                //SetNewTarget(_spawner.GetRandomNode());
                _target = _spawner.GetRandomNode();
            }
        }

        transform.forward = (_target.transform.position - transform.position).normalized;

		// if timer is 0
		if (_timer <= 0)
		{
			// head to exit if there are no player to be chased
			if (_nextTarget.Count > 0)
			{
				FindNextTarget();

			}
			else
			{
                if (_nextTarget.Count > 0)
                {
                    // Target is gone, next target spotted
                    FindNextTarget();
                }
                else if (_nextTarget.Count == 0)
                {
                    // Target is down, search for next target
                    _state = State.SEARCH;

                    _target = _spawner.GetRandomNode();

                    _speed = _normalSpeed;
                    _timer = 5;
                }

				_state = State.EXIT;
                _target = _spawner.GetExit();
			}
		}
		 
		_rb.AddForce(transform.forward * _speed, ForceMode.Acceleration);

		_timer -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            if (_state == State.PATROL || _state == State.SEARCH)
            {
                SetNewTarget(collide.gameObject);

                _timer = (float)Random.Range(5, 11);
                _state = State.PURSUIT;
				_speed = _pursuitSpeed;
            }
			else if (_state == State.PURSUIT && collide.gameObject != _target)
            {
				_nextTarget.Add(collide.gameObject);
            }
        }
    }

	void OnTriggerExit(Collider collide)
	{
		if (collide.gameObject.tag == "Player")
		{
			_nextTarget.Remove(collide.gameObject);
			_nextTarget.TrimExcess();

		}
	}


	void OnCollisionEnter(Collision collide)
	{
		if (collide.gameObject.tag == "Player")
		{
			GameObject collider = collide.collider.gameObject;

			if (_target == collider && _nextTarget.Count > 0)
			{
                // Target is down, next target spotted
				FindNextTarget();
			}
			else if (_target == collider && _nextTarget.Count == 0)
			{
                // Target is down, search for next target
				_state = State.SEARCH;

                _target = _spawner.GetRandomNode();

				_speed = _normalSpeed;
				_timer = 5;
			}

            // Destroy player
			collider.GetComponent<_Pawn>()._isAlive = false;

		}
	}

	void FindNextTarget()
	{
		SetNewTarget(_nextTarget[0]);

		_nextTarget.Remove(_nextTarget[0]);

		_nextTarget.TrimExcess();
	}


    public void SetSpawnerReference(GameObject inSpawner)
    {
        _spawner = inSpawner.GetComponent<SharkSpawnerScript>();
    }


}
