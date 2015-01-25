using UnityEngine;
using System.Collections;

public class elenemtalEff : MonoBehaviour
{
    public Transform
        fire,
        water,
        earth,
        elect;

	void Start ()
    {
        PlanetMovement.elementalCh += Effect;
	}

    public void Effect()
    {

    }

    void OnDestroy()
    {
        PlanetMovement.elementalCh -= Effect;
    }
}
