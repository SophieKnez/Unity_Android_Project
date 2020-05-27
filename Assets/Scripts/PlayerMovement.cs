using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float acceleration, minSpeed, maxSpeed;
    private float speed;

    [SerializeField] public InputState input;
    [SerializeField] private Color[] colors;

    private Rigidbody2D rigidbody;
    private Vector2 prevPos;
    private SpriteRenderer sprite;

    private bool canBlast = true;
    private bool hasChangedColor = false;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        speed = minSpeed;
    }


    private void FixedUpdate()
    {
        MovePlayerWithSwipe();
        StopIfFingerDown();
        ChangeColorOnWallHit();
        ResetVelocityIfHitWall();

        prevPos = transform.position;
    }

    void MovePlayerWithSwipe()   
    {
        //set pos via velocity
        rigidbody.velocity = input.SwipeDir * speed;

        //increase speed;
        if (speed < maxSpeed)
            speed += acceleration;
    }

    void StopIfFingerDown()
    {
        if (input.IsFingerDown())
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// returns whether cube is moving or not
    /// </summary>
    bool hasStoppedMoving()
    {
        Vector2 pos = transform.position;

        if (prevPos == pos)//
            return true;
        else
            return false;        
    }

    /// <summary>
    /// if has hit a wall, change color of sprite
    /// </summary>
    private void ChangeColorOnWallHit()
    {                                                           
        if (hasStoppedMoving() && !input.IsFingerDown())//if it's not because of tappin           
        {
            if (!hasChangedColor)
            {
                sprite.color = GetRandomColor();
                hasChangedColor = true;
            }
        }
        else hasChangedColor = false;//set has changed to false once moving again
    }

    private void ResetVelocityIfHitWall()
    {
        if (hasStoppedMoving())
            speed = minSpeed;
    }
    
    /// <summary>
    /// returns a random color from a predetermined array of colors
    /// </summary>
    /// <returns></returns>
    private Color GetRandomColor()
    {
        int numOfColors = colors.Length;
        int index = UnityEngine.Random.Range(0, numOfColors);
        return colors[index];
    }
}
