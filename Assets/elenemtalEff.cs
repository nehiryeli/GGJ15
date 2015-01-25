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
        a_ch,
		g_fire,
		g_water,
		g_lightning,
		g_earth;

    public AudioClip
        au_clash,
        au_point,
        au_succ;


    public Renderer target_r;

	void Start ()
    {
        PlanetMovement.elementalCh += Effect;
        PlanetMovement.onElemTrue += Play;
        GameManager.onLifeChanged += onClash;
        GameManager.onScoreUpdated += Score;
	}

    public void onClash(int i)
    {
        au_src.PlayOneShot(au_clash);
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

    public void Score(int i)
    {

    }

    public void Effect()
    {
        
		au_src.PlayOneShot(a_ch);
        switch (GameManager.inst.pMove.status)
        {
            case Element.Ateş:
                target_r.material = m_fire;
				//au_src.PlayOneShot(g_fire);
                fire.SetActive(true);
                water.SetActive(false);
                earth.SetActive(false);
                elect.SetActive(false);
                break;
            case Element.Su:
                target_r.material = m_water;
				//au_src.PlayOneShot(g_water);
                fire.SetActive(false);
                water.SetActive(true);
                earth.SetActive(false);
                elect.SetActive(false);
                break;
            case Element.Toprak:
                target_r.material = m_earth;
				//au_src.PlayOneShot(g_earth);
                fire.SetActive(false);
                water.SetActive(false);
                earth.SetActive(true);
                elect.SetActive(false);
                break;
            case Element.Elektrik:
                target_r.material = m_elect;
				//au_src.PlayOneShot(g_lightning);
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
        GameManager.onScoreUpdated -= Score;
        GameManager.onLifeChanged -= onClash;
        PlanetMovement.elementalCh -= Effect;
        PlanetMovement.onElemTrue -= Play;
    }
}
