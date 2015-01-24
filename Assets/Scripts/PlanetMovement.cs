using UnityEngine;
using System.Collections;
public enum Pozisyon
{
    NW,
    NE,
    SW,
    SE,
}
public enum Element
{
    Ateş,
    Su,
    Toprak,
    Elektrik,
}

public class Player
{
    public Player(KeyCode gelenCekimTusu, KeyCode gelenElementTusu, Pozisyon gelenKose, Element gelenElement)
    {
        cekimTusu = gelenCekimTusu;
        elementTusu = gelenElementTusu;
        kose = gelenKose;
        element = gelenElement;
        cekimTusuBasildiMi = false;
    }
    public KeyCode cekimTusu
    {
        get;
        set;
    }
    public KeyCode elementTusu
    {
        get;
        set;
    }
    public Pozisyon kose
    {
        get;
        set;
    }
    public Element element
    {
        get;
        set;
    }
    public bool cekimTusuBasildiMi
    {
        get;
        set;
    }
    public bool elementTusuBasildiMi
    {
        get;
        set;
    }

}
public class PlanetMovement : MonoBehaviour
{
    public float maxPosition = 20;
    Vector3 target;
    public float Hiz = 100;
    Vector3 direction;
    float oldTime;
    // Use this for initialization
    void Start()
    {
        target = new Vector3(0, 0, transform.position.z);
        transform.position = new Vector3(0, 0, transform.position.z);
        direction = new Vector3(0, 0, transform.position.z);
        oldTime = Time.time;
    }

    // Update is called once per frame

    void Update()
    {
        var p = GameManager.getPlayers();
        foreach (var item in p)
        {
            if (Input.GetKeyDown(item.cekimTusu))
            {
                item.cekimTusuBasildiMi = true;
            }
            if (Input.GetKeyUp(item.cekimTusu))
            {
                item.cekimTusuBasildiMi = false;
            }
            if (Input.GetKeyDown(item.elementTusu))
            {
                item.elementTusuBasildiMi = true;
            }
            if (Input.GetKeyUp(item.elementTusu))
            {
                item.elementTusuBasildiMi = false;
            }
        }
        target = new Vector3(0, 0, transform.position.z);
        TargetHesapla();

        if ((target - transform.position).magnitude != 0)
        {
            direction = (target - transform.position) / (target - transform.position).magnitude;
        }

        HareketEttir(direction);
        oldTime = Time.time;
    }

    void TargetHesapla()
    {
        var p = GameManager.getPlayers();
        foreach (var item in p)
        {
            if (item.cekimTusuBasildiMi)
            {
                Vector3 targetFaktor = PlayerPozisyonuIleTargetBelirle(item.kose);
                target.x += targetFaktor.x;
                target.y += targetFaktor.y;
            }
        }
        if (p[0].cekimTusuBasildiMi == false && p[1].cekimTusuBasildiMi == false && p[2].cekimTusuBasildiMi == false && p[3].cekimTusuBasildiMi == false)
        {
            target = new Vector3(0, 0, transform.position.z);
        }
        else
        {
            if (target.x > maxPosition)
                target.x = maxPosition;
            if (target.y > maxPosition)
                target.y = maxPosition;
            if (target.x < (-1) * maxPosition)
                target.x = (-1) * maxPosition;
            if (target.y < (-1) * maxPosition)
                target.y = (-1) * maxPosition;
        }
    }

    Vector3 PlayerPozisyonuIleTargetBelirle(Pozisyon pozisyon)
    {
        switch (pozisyon)
        {
            case Pozisyon.NE:
                return new Vector3(maxPosition, maxPosition, transform.position.z);
            case Pozisyon.NW:
                return new Vector3((-1) * maxPosition, maxPosition, transform.position.z);
            case Pozisyon.SE:
                return new Vector3(maxPosition, (-1) * maxPosition, transform.position.z);
            case Pozisyon.SW:
                return new Vector3((-1) * maxPosition, (-1) * maxPosition, transform.position.z);
            default:
                return new Vector3(0, 0, transform.position.z);
        }
    }

    void HareketEttir(Vector3 dir)
    {
        if ((target - transform.position).magnitude < (dir * ((Time.time - oldTime) * Hiz)).magnitude)
            transform.position = target;
        else
        {
            transform.Translate(dir * ((Time.time - oldTime) * Hiz));
        }

    }
}
