using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectables : MonoBehaviour
{
    public int NumOnScreen = 0;

    [SerializeField] private GameObject collectablePrefab;
    [SerializeField] private float minSpawnTime = 1, maxSpawnTime = 3;
    [SerializeField] private int maxNumOnScreen = 2;
    [SerializeField] private Color[] colors;

    GameObject collectableInstance;
    Collectable collScript;
    private Vector2 screenBounds;
    private SpawnCollectables spawner;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for (; ; )
        {
            //instantiate at random position on the screen if there aren't too many already on screen
            if (NumOnScreen < maxNumOnScreen)
            {
                collectableInstance = Instantiate(collectablePrefab, new Vector2(Random.Range(1, screenBounds.x - 1), Random.Range(1, screenBounds.y - 1)), Quaternion.identity);
                collScript = collectableInstance.GetComponent<Collectable>();
                collScript.Spawner = this;
                collScript.Colors = colors;
            }

            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));//wait for a random period of time between the min and max time in seconds
        }
    }

    public void TakeOneOffScreen()
    {
        NumOnScreen--;
    }

}
