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
        a_elect,
        a_ch;


    public Renderer target_r;

	void Start ()
    {
        PlanetMovement.elementalCh += Effect;
        PlanetMovement.onElemTrue += Play;
	}

    public void Play(string s)
    {
        switch(s)
        {
            case "fire":
                au_src.PlayOneShot(a_fire);
                break;
            case "water":
                au_src.PlayOneShot(a_water);
                break;
            case "lightning":
                au_src.PlayOneShot(a_elect);
                break;
            case "earth":
                au_src.PlayOneShot(a_earth);
                break;
        }
    }

    public void Effect()
    {
        au_src.PlayOneShot(a_ch);
        switch (GameManager.inst.pMove.status)
        {
            case Element.Ateş:
                target_r.material = m_fire;
                fire.SetActive(true);
                water.SetActive(false);
                earth.SetActive(false);
                elect.SetActive(false);
                break;
            case Element.Su:
                target_r.material = m_water;
                fire.SetActive(false);
                water.SetActive(true);
                earth.SetActive(false);
                elect.SetActive(false);
                break;
            case Element.Toprak:
                target_r.material = m_earth;
                fire.SetActive(false);
                water.SetActive(false);
                earth.SetActive(true);
                elect.SetActive(false);
                break;
            case Element.Elektrik:
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
        PlanetMovement.onElemTrue -= Play;
    }
}
