using UnityEngine;
using System.Collections;

public class arrow : MonoBehaviour
{
    public int pID;

    Renderer rn;

    void Awake()
    {
        rn = renderer;
        GameManager.a_onInversTrigger += Rot_Arrow;
    }

    void Rot_Arrow()
    {
        transform.Rotate(new Vector3(0, 180, 0), Space.Self);
    }

    void Update()
    {
        var pl = GameManager.getPlayers();
        renderer.material.color = pl[pID].cekimTusuBasildiMi ? Color.green : Color.gray;
    }

    void OnDestroy()
    {
        GameManager.a_onInversTrigger -= Rot_Arrow;
    }
}
