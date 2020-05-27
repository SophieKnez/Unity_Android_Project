using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This goes on the player to keep them on screen

public class KeepPlayerOnScreen : MonoBehaviour
{
    private Vector3 onScreenPos;
    private Vector3 bottomLeftWorldCoordinates;
    private Vector3 topRightWorldCoordinates;
    private Vector3 movementRangeMin;
    private Vector3 movementRangeMax;

    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();

        bottomLeftWorldCoordinates = Camera.main.ViewportToWorldPoint(Vector3.zero);//this is bottom left
        topRightWorldCoordinates = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));//this is top right

        movementRangeMin = bottomLeftWorldCoordinates + renderer.bounds.extents;//this is our minimum 
        movementRangeMax = topRightWorldCoordinates - renderer.bounds.extents;//this is our maximum
    }
    void LateUpdate ()
    {
        onScreenPos = transform.position;
        onScreenPos.x = Mathf.Clamp(transform.position.x, movementRangeMin.x, movementRangeMax.x);
        onScreenPos.y = Mathf.Clamp(transform.position.y, movementRangeMin.y, movementRangeMax.y);
        transform.Translate(onScreenPos - transform.position);
	}
}
