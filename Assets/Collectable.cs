using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Color[] Colors;
    public SpawnCollectables Spawner;

    [SerializeField] private float lifeTime = 1.5f, fadeTime = 1;

    private SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        SetRandomColor();

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(lifeTime);//wait until die

        for (float f = 1; f > 0; f -= Time.deltaTime / fadeTime)//fade out
        {
            Color temp = renderer.color;
            temp.a = f;
            renderer.color = temp;
            yield return new WaitForEndOfFrame();
        }

        Spawner.TakeOneOffScreen();
    }

    void SetRandomColor()
    {
        int random = UnityEngine.Random.Range(0, Colors.Length);
        renderer.color = Colors[random];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Spawner.TakeOneOffScreen();
        Destroy(this.gameObject, .5f);
    }
}
