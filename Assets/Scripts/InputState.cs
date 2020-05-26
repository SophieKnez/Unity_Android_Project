using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputState : MonoBehaviour
{
    public enum InputType
    {
        swipeUp,
        swipeDown,
        swipeLeft,
        swipeRight,
        TapReleased,
        TapStarted,
        NoInput
    }

    public Vector2 SwipeDir;

    public InputType input { get; private set; }

    [SerializeField]
    private float inputCoolDownTime;

    private Vector2 touchOrigin = -Vector2.one;//is supposed to start offscreen so that we initially don't sense touch input

    private int horizontal;
    private int vertical;
    private int taps = 0;

    private bool tempHitCheck = false;
    private bool canDoTap = true;

    void Start()
    {
        input = InputType.NoInput;
    }

    void Update()
    {
        CheckForSwipes();
    }

    void CheckForSwipes()
    {
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)//if this is the start of our touch
            {
                touchOrigin = myTouch.position;
                input = InputType.TapStarted;
            }
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)//or if it's the end of a touch and we started with a touch that was on screen (if it changed from a neg)
            {
                Vector2 touchEnd = myTouch.position;//this is the end of our touch
                float x = touchEnd.x - touchOrigin.x;//difference between touches in x axis
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;//reset this to offscreen so that it has to be reset with a touch

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0)
                    {
                        input = InputType.swipeRight;
                        SwipeDir = new Vector2(1, 0);
                    }
                    else
                    {
                        input = InputType.swipeLeft;
                        SwipeDir = new Vector2(-1, 0);

                    }
                }
                else if (Mathf.Abs(x) < Mathf.Abs(y))
                {
                    if (y > 0)
                    {
                        input = InputType.swipeUp;
                        SwipeDir = new Vector2(0, 1);
                    }
                    else
                    {
                        input = InputType.swipeDown;
                        SwipeDir = new Vector2(0, -1);
                    }
                }
                else if (Mathf.Abs(x) == Mathf.Abs(y))//if our touch location hasn't changed
                {
                    input = InputType.TapReleased;
                }
                StartCoroutine(InputCooldown());//this lets the input stay for a little bit and then resets the input state back to no Input
            }

        }
    }

    private IEnumerator InputCooldown()
    {
        yield return new WaitForSeconds(inputCoolDownTime);
        input = InputType.NoInput;
    }

    private IEnumerator HitCooldown()//only tap on something once
    {
        canDoTap = false;
        yield return new WaitForSeconds(inputCoolDownTime);
        canDoTap = true;
    }

    public bool IsFingerDown()
    {
        if (input == InputType.TapStarted || input == InputType.TapReleased)
            return true;
        else return false;
    }
}
