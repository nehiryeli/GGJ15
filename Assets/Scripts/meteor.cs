using UnityEngine;
using System.Collections;

public class meteor : MonoBehaviour {
	public Transform T;
	float position = 0;
	public float speed = 5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		position += Time.deltaTime/speed;
		
		transform.position = Vector3.Lerp(new Vector3(0,1,100), new Vector3(0,1,-10), position );
		if (position>1) {
			Destroy(gameObject);		
		}
	}
}
