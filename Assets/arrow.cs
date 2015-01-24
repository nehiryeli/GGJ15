using UnityEngine;
using System.Collections;

public class arrow : MonoBehaviour
{
    void Awake()
    {
        GameManager.a_onInversTrigger += Rot_Arrow;
    }

    void Rot_Arrow()
    {
        transform.Rotate(new Vector3(0, 180, 0), Space.Self);
    }

    void OnDestroy()
    {
        GameManager.a_onInversTrigger -= Rot_Arrow;
    }
}
