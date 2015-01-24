using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour {
	public Transform meteor;
	Transform meteorObj;
	float position = 0;
	public float spawnTime=3;
	// Use this for initialization
	IEnumerator Start () {
		while (true) {
			this.spawn ();
			yield return new WaitForSeconds(spawnTime);
			position=0;
		}


	}
	public void spawn(){
		meteorObj = (Transform)Instantiate(meteor, new Vector3(0, 0, 0), Quaternion.identity) as Transform;
	}
	// Update is called once per frame
	void Update () {
		position += Time.deltaTime;

		meteorObj.position = Vector3.Lerp(new Vector3(10,0,0), new Vector3(0,0,0), position );

	}


}
