using UnityEngine;
using System.Collections;

public class elenemtalEff : MonoBehaviour
{
    public GameObject
        fire,
        water,
        earth,
        elect;

    public Material
        m_fire,
        m_water,
        m_earth,
        m_elect,
        m_def;


    public Renderer target_r;

	void Start ()
    {
        PlanetMovement.elementalCh += Effect;
	}

    public void Effect()
    {
        switch (GameManager.inst.pMove.status)
        {
            case Element.Ateş:
                fire.SetActive(true);
                water.SetActive(false);
                earth.SetActive(false);
                elect.SetActive(false);
                break;
            case Element.Su:
                fire.SetActive(false);
                water.SetActive(true);
                earth.SetActive(false);
                elect.SetActive(false);
                break;
            case Element.Toprak:
                fire.SetActive(false);
                water.SetActive(false);
                earth.SetActive(true);
                elect.SetActive(false);
                break;
            case Element.Elektrik:
                fire.SetActive(false);
                water.SetActive(false);
                earth.SetActive(false);
                elect.SetActive(true);
                break;
            case Element.Notr:
                fire.SetActive(false);
                water.SetActive(false);
                earth.SetActive(false);
                elect.SetActive(false);
                break;
            default:
                break;
        }
    }

    void OnDestroy()
    {
        PlanetMovement.elementalCh -= Effect;
    }
}
