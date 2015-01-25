using UnityEngine;
using System.Collections;

public class bgRot : MonoBehaviour
{
    public float lim;
	public float duration = 3;
    Transform t;

    IEnumerator Start()
    {
        t = transform;

        var p = 0f;
		transform.position = getVec3();
	
        var f_pos = t.position;
        var t_pos = getVec3();

        while (true)
        {
            p += Time.deltaTime/duration;

            t.position = Vector3.Lerp(f_pos, t_pos,p);

            yield return new WaitForFixedUpdate();

            if(p > 1)
            {
                f_pos = t_pos;
                t_pos = getVec3();
                p = 0f;
            }
        }
    }

    Vector3 getVec3()
    {
        var r1 = Random.Range(-lim, lim);
        var r2 = Random.Range(-lim, lim);

        return new Vector3(r1, r2, t.position.z);
    }

}
