using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdMove : ICommand
{
    // Propiedades
    private Rigidbody2D _rb;
    private float _directionX;
    private float _directionY;
    private float _fixedSpeed;
    private float _speed;

    public CmdMove(Rigidbody2D rb, float directionX, float directionY, float speed,float FixedSpeed)
    {
        _rb = rb;
        _directionX = directionX;
        _directionY = directionY;
        _fixedSpeed = FixedSpeed;
        _speed = speed;
    }

    public void Do()
    {
        _rb.velocity = new Vector2(_directionX* _speed * _fixedSpeed, _directionY * _speed *  _fixedSpeed);
    }

    public void Undo()
    {
        _rb.velocity -= new Vector2(_directionX * _speed * _fixedSpeed, _directionY * _speed * _fixedSpeed);
    }
}
