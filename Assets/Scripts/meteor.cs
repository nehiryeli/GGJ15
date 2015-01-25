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
	public string element;
	public Material[] mat_elem;
	//public Shader shader2 = Shader.Find("Transparent/Bumped Spectecular");

    [HideInInspector]
    public
        int partToRemove = -1;

    void Start()
    {
        generator();
        placeholder = GetComponentsInChildren<Renderer>();
    }

    void generator()
    {
        partToRemove = Random.Range(0, parts.Length);
        int elementIndex = Random.Range(0, 5);
        if (elementIndex != 4 && GameManager.inst.score > GameManager.inst.score_for_harder_game)
        {//son elemen elementsiz parçayı gösterir
			element = GameManager.inst.elementsList[elementIndex];
            //parts [partToRemove].gameObject.renderer.material.color = new Color(222,22,222);
        }
			
		if (elementIndex + 1 <= GameManager.inst.elementsList.Length && GameManager.inst.score>GameManager.inst.score_for_harder_game) {
			parts [elementIndex].gameObject.renderer.tag = "elemental";
			parts [elementIndex].gameObject.renderer.material = mat_elem [elementIndex];
		} else {
			parts [partToRemove].gameObject.SetActive(false);
		}
						

    }

    void Update()
    {
        position += Time.deltaTime / speed;

        transform.position = Vector3.Lerp(new Vector3(0, 0, 100), new Vector3(0, 0, -10), position);
        if (position > 1)
        {
            GameManager.inst.MeteorDestroyed(this);
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
