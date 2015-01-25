using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    Notr,
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
    public float maxPosition = 1.9f;
    Vector3 target;
    public float ivme = 50f;
    //private float Hiz = 0;
    private Vector3 hiz = new Vector3(0, 0, 0);
    Vector3 direction;
    float oldTime;
    private float friction = 5;
    float kuraTime;
    public bool IsGravityInverse
    {
        get;
        set;
    }
    Element[] status = new Element[2];
    // Use this for initialization
    void Start()
    {
        target = new Vector3(0, 0, transform.position.z);
        transform.position = new Vector3(0, 0, transform.position.z);
        direction = new Vector3(0, 0, transform.position.z);
        oldTime = Time.time;
        IsGravityInverse = false;
        kuraTime = 0;
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
        else
        {
            direction = new Vector3(0, 0, 0);
        }
        ElementBelirle();
        ///Debug.Log(System.Convert.ToString(status[0]) + "   " + System.Convert.ToString(status[1]));
        //Debug.Log ("P1 : "+System.Convert.ToString (p.ToArray()[0].elementTusuBasildiMi)+"P2 : "+System.Convert.ToString (p.ToArray()[1].elementTusuBasildiMi)+"P3 : "+System.Convert.ToString (p.ToArray()[2].elementTusuBasildiMi)+"P4 : "+System.Convert.ToString (p.ToArray()[3].elementTusuBasildiMi));
        //Debug.Log ("P1 : "+System.Convert.ToString (p.ToArray()[0].cekimTusuBasildiMi)+"P2 : "+System.Convert.ToString (p.ToArray()[1].cekimTusuBasildiMi)+"P3 : "+System.Convert.ToString (p.ToArray()[2].cekimTusuBasildiMi)+"P4 : "+System.Convert.ToString (p.ToArray()[3].cekimTusuBasildiMi));

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
            if (target.x == 0)
            {
                if (target.y > maxPosition * Mathf.Sqrt(2))
                {
                    target.y = maxPosition * Mathf.Sqrt(2);
                }
                if (target.y < (-1) * maxPosition * Mathf.Sqrt(2))
                {
                    target.y = (-1) * maxPosition * Mathf.Sqrt(2);
                }
            }
            if (target.y == 0)
            {
                if (target.x > maxPosition * Mathf.Sqrt(2))
                {
                    target.x = maxPosition * Mathf.Sqrt(2);
                }
                if (target.x < (-1) * maxPosition * Mathf.Sqrt(2))
                {
                    target.x = (-1) * maxPosition * Mathf.Sqrt(2);
                }
            }
            if (IsGravityInverse)
            {
                target.x *= (-1);
                target.y *= (-1);
            }
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
        float deltaT = Time.time - oldTime;

        hiz -= hiz * friction * deltaT;
        if ((target - transform.position).magnitude < ((deltaT * hiz)).magnitude)
        {
            transform.position = target;
            hiz = new Vector3(0, 0, 0);
        }
        else
        {
            hiz += dir * ivme * deltaT;
            transform.Translate((deltaT * hiz));
        }
    }

    public void OnTriggerEnter(Collider c)
    {
        var m = c.transform.parent.GetComponent<meteor>();
        GameManager.inst.Rem_Met(m);

        if (c.tag == "elemental")
        {

            if (!IsElementsEqual(m.element))
                GameManager.inst.LifeDec();
        }
        else
            GameManager.inst.LifeDec();
    }
    public void PivotPositionToLeft(int howManyTimes)
    {
        var p = GameManager.getPlayers();
        for (int j = 0; j < howManyTimes; j++)
        {
            Pozisyon buffer = p.ToArray()[0].kose;
            for (int i = 1; i < 4; i++)
            {
                p.ToArray()[i - 1].kose = p.ToArray()[i].kose;
            }
            p.ToArray()[3].kose = buffer;
        }
    }
    public void PivotPositionToRight(int howManyTimes)
    {
        var p = GameManager.getPlayers();
        for (int j = 0; j < howManyTimes; j++)
        {
            Pozisyon buffer = p.ToArray()[3].kose;
            for (int i = 3; i < 0; i++)
            {
                p.ToArray()[i].kose = p.ToArray()[i - 1].kose;
            }
            p.ToArray()[0].kose = buffer;
        }
    }
    public void PivotElementToLeft(int howManyTimes)
    {
        var p = GameManager.getPlayers();
        for (int j = 0; j < howManyTimes; j++)
        {
            Element buffer = p.ToArray()[0].element;
            for (int i = 1; i < 4; i++)
            {
                p.ToArray()[i - 1].element = p.ToArray()[i].element;
            }
            p.ToArray()[3].element = buffer;
        }
    }
    public void PivotElementToRight(int howManyTimes)
    {
        var p = GameManager.getPlayers();
        for (int j = 0; j < howManyTimes; j++)
        {
            Element buffer = p.ToArray()[3].element;
            for (int i = 3; i < 0; i++)
            {
                p.ToArray()[i].element = p.ToArray()[i - 1].element;
            }
            p.ToArray()[0].element = buffer;
        }
    }
    public List<int> PozisyondaHataYapanlarKimler(int emptyPartID)
    {
        var p = GameManager.getPlayers();
        bool[] pozisyonArray = new bool[4];
        List<Player> kimler = new List<Player>();
        List<int> idNumbers = new List<int>();
        switch (emptyPartID)
        {
            case 0:
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NW)] = true;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NE)] = true;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SE)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SW)] = false;
                break;
            case 1:
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NW)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NE)] = true;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SE)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SW)] = false;
                break;
            case 2:
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NW)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NE)] = true;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SE)] = true;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SW)] = false;
                break;
            case 3:
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NW)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NE)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SE)] = true;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SW)] = false;
                break;
            case 4:
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NW)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NE)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SE)] = true;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SW)] = true;
                break;
            case 5:
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NW)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NE)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SE)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SW)] = true;
                break;
            case 6:
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NW)] = true;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NE)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SE)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SW)] = true;
                break;
            case 7:
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NW)] = true;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NE)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SE)] = false;
                pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SW)] = false;
                break;
            default:
                break;
        }

        if (p.Find(x => x.kose == Pozisyon.NW).cekimTusuBasildiMi != pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NW)])
        {
            kimler.Add(p.Find(x => x.kose == Pozisyon.NW));
        }
        if (p.Find(x => x.kose == Pozisyon.NE).cekimTusuBasildiMi != pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.NE)])
        {
            kimler.Add(p.Find(x => x.kose == Pozisyon.NE));
        }
        if (p.Find(x => x.kose == Pozisyon.SE).cekimTusuBasildiMi != pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SE)])
        {
            kimler.Add(p.Find(x => x.kose == Pozisyon.SE));
        }
        if (p.Find(x => x.kose == Pozisyon.SW).cekimTusuBasildiMi != pozisyonArray[p.FindIndex(x => x.kose == Pozisyon.SW)])
        {
            kimler.Add(p.Find(x => x.kose == Pozisyon.SW));
        }
        foreach (var item in kimler)
        {
            idNumbers.Add(p.FindIndex(x => x.kose == item.kose));
        }
        return idNumbers;
    }

    public void ElementBelirle()
    {
        var p = GameManager.getPlayers();
        if (p.Find(x => x.element == Element.Elektrik).elementTusuBasildiMi == true && p.Find(x => x.element == Element.Toprak).elementTusuBasildiMi == false)
        {
            status[0] = Element.Elektrik;
        }
        else if (p.Find(x => x.element == Element.Elektrik).elementTusuBasildiMi == false && p.Find(x => x.element == Element.Toprak).elementTusuBasildiMi == true)
        {
            status[0] = Element.Toprak;
        }
        else
        {
            status[0] = Element.Notr;
        }
        if (p.Find(x => x.element == Element.Ateş).elementTusuBasildiMi == true && p.Find(x => x.element == Element.Su).elementTusuBasildiMi == false)
        {
            status[1] = Element.Ateş;
        }
        else if (p.Find(x => x.element == Element.Ateş).elementTusuBasildiMi == false && p.Find(x => x.element == Element.Su).elementTusuBasildiMi == true)
        {
            status[1] = Element.Su;
        }
        else
        {
            status[1] = Element.Notr;
        }
    }
    public bool IsElementsEqual(string meteorElementi)
    {
        switch (meteorElementi)
        {
            case "fire":
                for (int i = 0; i < 2; i++)
                {
                    if (status[i] == Element.Ateş)
                    {
                        return true;
                    }
                }
                return false;
            case "earth":
                for (int i = 0; i < 2; i++)
                {
                    if (status[i] == Element.Toprak)
                    {
                        return true;
                    }

                }
                return false;
            case "water":
                for (int i = 0; i < 2; i++)
                {
                    if (status[i] == Element.Su)
                    {
                        return true;
                    }

                }
                return false;
            case "lightning":
                for (int i = 0; i < 2; i++)
                {
                    if (status[i] == Element.Elektrik)
                    {
                        return true;
                    }

                }
                return false;
            default:
                return true;
        }

    }
}
