using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour
{
    public Transform meteor;
    Transform meteorObj;

    public float spawnTime = 3;
    // Use this for initialization
    IEnumerator Start()
    {
        while (true)
        {
            this.spawn();
            yield return new WaitForSeconds(spawnTime);
        }


    }
    public void spawn()
    {
        meteorObj = (Transform)Instantiate(meteor) as Transform;
        meteorObj.position = new Vector3(500, 0, 0);

        var q = meteorObj.GetComponent<meteor>();
        q.speed = GameManager.inst.s_curr_speed;
    }
}
