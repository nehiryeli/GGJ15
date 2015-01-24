using UnityEngine;
using System.Collections;

public class meteor : MonoBehaviour
{
    float position = 0;
    public float speed = 5f;
    public Transform[] parts;
    Renderer[] placeholder;

    // Use this for initialization
    void Start()
    {
        generator();
        placeholder = GetComponentsInChildren<Renderer>();
    }

    void generator()
    {
        int partToRemove = Random.Range(0, parts.Length);
        parts[partToRemove].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        position += Time.deltaTime / speed;

        transform.position = Vector3.Lerp(new Vector3(0, 0, 100), new Vector3(0, 0, -10), position);
        if (position > 1)
        {
            Destroy(gameObject);
        }
        if (position > 0.9f)
        {
            foreach (var r in placeholder)
            {
                var col = r.material.color;
                var alpha = (1f - position) / .1f;
                r.material.color = new Color(col.r, col.g, col.b, alpha);
            }
        }

    }
}
