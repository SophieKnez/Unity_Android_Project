using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
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
    }

    private void FixedUpdate()
    {
        MovePlayerWithSwipe();
        StopIfFingerDown();
        CheckForColorChange();     
    }
    void MovePlayerWithSwipe()   
    {
        rigidbody.velocity = input.SwipeDir * speed;
    }

    void StopIfFingerDown()
    {
        if (input.IsFingerDown())
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    void CheckForColorChange()
    {
        if (!input.IsFingerDown())
        {
            Vector2 pos = transform.position;

            if (prevPos == pos && !hasChangedColor)//if isn't moving (Hit a wall) change color
            {
                hasChangedColor = true;
                sprite.color = GetRandomColor();
            }
            else if (prevPos != pos)//if moving again, can change color
                hasChangedColor = false;
        }

        prevPos = transform.position;
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
