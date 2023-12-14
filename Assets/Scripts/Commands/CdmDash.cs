using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CdmDash : ICommand
{
    private DashController _dash;

    private Rigidbody2D _rb;
    private float _directionX;
    private float _directionY;
    private float _fixedSpeed;
    private float _speed;
    private float _dashSpeed;

    public CdmDash(Rigidbody2D rb, float directionX, float directionY, float speed, float DashSpeed, float FixedSpeed)
    {
        _rb = rb;
        _directionX = directionX;
        _directionY = directionY;
        _fixedSpeed = FixedSpeed;
        _dashSpeed = DashSpeed;
        _speed = speed;
    }
    public void Do()
    {
        _rb.velocity = new Vector2(_directionX * _speed *_dashSpeed * _fixedSpeed, _directionY * _speed * _dashSpeed * _fixedSpeed);
    }

    public void Undo()
    {
        _rb.velocity = new Vector2(_directionX * _speed * _dashSpeed * _fixedSpeed, _directionY * _speed * _dashSpeed * _fixedSpeed);
    }
}
