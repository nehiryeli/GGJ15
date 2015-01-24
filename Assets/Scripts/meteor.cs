using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class meteor : MonoBehaviour
{
    float position = 0;
    public float speed = 5f;
    public Transform[] parts;
    Renderer[] placeholder;
    //List<string> elements = new List<string>();
    public string[] elements = new string[2];

    string[] elementsList = new string[] { "fire", "water", "lightning", "earth" };

    void Start()
    {
        generator();
        placeholder = GetComponentsInChildren<Renderer>();
    }

    void generator()
    {
        int partToRemove = Random.Range(0, parts.Length);
        int elementIndex = Random.Range(0, 5);
        if (elementIndex != 4)
        {//son elemen elementsiz parçayı gösterir
            this.elements[0] = elementsList[elementIndex];
            //parts [partToRemove].gameObject.renderer.material.color = new Color(222,22,222);
        }
        elementIndex = Random.Range(0, 5);
        if (elementIndex != 4)
        {//son elemen elementsiz parçayı gösterir
            this.elements[1] = elementsList[elementIndex];
            //parts [partToRemove].gameObject.renderer.material.color = new Color(222,22,222);
        }
        parts[partToRemove].gameObject.SetActive(false);
    }

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
