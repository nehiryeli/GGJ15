using UnityEngine;
using System.Collections;

public class elenemtalEff : MonoBehaviour
{
    public AudioSource au_src;

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

    public AudioClip
        a_fire,
        a_water,
        a_earth,
        a_elect;


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
                au_src.PlayOneShot(a_fire);
                target_r.material = m_fire;
                fire.SetActive(true);
                water.SetActive(false);
                earth.SetActive(false);
                elect.SetActive(false);
                break;
            case Element.Su:
                au_src.PlayOneShot(a_water);
                target_r.material = m_water;
                fire.SetActive(false);
                water.SetActive(true);
                earth.SetActive(false);
                elect.SetActive(false);
                break;
            case Element.Toprak:
                au_src.PlayOneShot(a_earth);
                target_r.material = m_earth;
                fire.SetActive(false);
                water.SetActive(false);
                earth.SetActive(true);
                elect.SetActive(false);
                break;
            case Element.Elektrik:
                au_src.PlayOneShot(a_elect);
                target_r.material = m_elect;
                fire.SetActive(false);
                water.SetActive(false);
                earth.SetActive(false);
                elect.SetActive(true);
                break;
            case Element.Notr:
            default:
                target_r.material = m_def;
                fire.SetActive(false);
                water.SetActive(false);
                earth.SetActive(false);
                elect.SetActive(false);
                break;
        }
    }

    void OnDestroy()
    {
        PlanetMovement.elementalCh -= Effect;
    }
}
