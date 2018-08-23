using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
 * Author:     Thau Shien Hsu
 * Student ID: 1007996
 */

public enum State
{
    PATROL,
    PURSUIT,
    SEARCH,
    EXIT
};


[RequireComponent(typeof(Rigidbody))]
public abstract class PredatorAI : MonoBehaviour
{
    [SerializeField] protected GameObject    _target;

    // predator movement speed
	protected float         _speed;

	// Speed Variables
	[SerializeField] protected float _pursuitSpeed = 10f;
	[SerializeField] protected float _normalSpeed = 5f;

    // pursuit and search timer
    protected float         _timer;



	[SerializeField] protected State _state;
    protected Rigidbody _rb;


	

    public virtual void SetNewTarget(GameObject inNewTarget)
    {
        if (_state != State.EXIT)
        {
            _target = inNewTarget;
        }
    }

    public State GetState()
    {
        return _state;
    }

}
