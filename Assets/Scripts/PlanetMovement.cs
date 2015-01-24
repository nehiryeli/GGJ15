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
	private Vector3 hiz = new Vector3 (0, 0, 0);
    Vector3 direction;
    float oldTime;
    private float friction = 5;
	float kuraTime ;
	public bool IsGravityInverse {
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

		if ((target - transform.position).magnitude != 0) {
			direction = (target - transform.position) / (target - transform.position).magnitude;
		} else {
			direction = new Vector3(0,0,0);
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
			if(target.x == 0)
			{
				if(target.y >maxPosition*Mathf.Sqrt(2)){
					target.y =maxPosition*Mathf.Sqrt(2);
				}
				if(target.y < (-1)*maxPosition*Mathf.Sqrt(2)){
					target.y = (-1)*maxPosition*Mathf.Sqrt(2);
				}
			}
			if(target.y == 0)
			{
				if(target.x >maxPosition*Mathf.Sqrt(2)){
					target.x =maxPosition*Mathf.Sqrt(2);
				}
				if(target.x < (-1)*maxPosition*Mathf.Sqrt(2)){
					target.x= (-1)*maxPosition*Mathf.Sqrt(2);
				}
			}
			if(IsGravityInverse){
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
	{float deltaT = Time.time - oldTime;
		
		hiz -= hiz * friction *deltaT;
		if ((target - transform.position).magnitude < (  (deltaT * hiz)).magnitude) {
			transform.position = target;
			hiz = new Vector3(0,0,0);
		}						
		else
		{
			hiz += dir*ivme*deltaT;
			transform.Translate((deltaT * hiz));
		}
    }

    public void OnTriggerEnter(Collider c)
    {
		var m = c.transform.parent.GetComponent<meteor> ();

        switch(m.element)
        {
            case "fire": break;
            case "earth": break;
            case "water": break;
            case "lightning": break;
            default:
                GameManager.inst.LifeDec();
                break;
        }
    }
	public void PivotToLeft(int howManyTimes){
		var p = GameManager.getPlayers();
		for (int j = 0; j < howManyTimes; j++) {
			Pozisyon buffer = p.ToArray () [0].kose;
			for (int i = 1; i <4; i++) {
				p.ToArray () [i-1].kose = 	p.ToArray () [i].kose; 
			}
			p.ToArray () [3].kose = buffer;
				}
	}
	public void PivotToRight(int howManyTimes){
		var p = GameManager.getPlayers();
		for (int j = 0; j < howManyTimes; j++) {
						Pozisyon buffer = p.ToArray () [3].kose;
						for (int i = 3; i <0; i++) {
								p.ToArray () [i].kose = p.ToArray () [i - 1].kose; 
						}
						p.ToArray () [0].kose = buffer;
				}
	}

	public Element[] ElementBelirle(){
		var p = GameManager.getPlayers();
		int ates =0;
		int su =0;
		int toprak =0;
		int elektrik =0;
		foreach (var item in p) {
			if(item.element == Element.Ateş){
				break;
			}
			ates++;
				}
		foreach (var item in p) {
			if(item.element == Element.Su){
				break;
			}
			su++;
		}
		foreach (var item in p) {
			if(item.element == Element.Toprak){
				break;
			}
			toprak++;
		}
		foreach (var item in p) {
			if(item.element == Element.Elektrik){
				break;
			}
			elektrik++;
		}
		if (p.ToArray () [elektrik].elementTusuBasildiMi ^ p.ToArray () [toprak].elementTusuBasildiMi) {
			status[0] = Element.Notr;
				} else if (p.ToArray () [elektrik].elementTusuBasildiMi) {
			status[0] = Element.Elektrik;
				} else {
			status[0] = Element.Toprak;
				}
		if (p.ToArray () [ates].elementTusuBasildiMi ^ p.ToArray () [su].elementTusuBasildiMi) {
			status[1] = Element.Notr;
		} else if (p.ToArray () [ates].elementTusuBasildiMi) {
			status[1] = Element.Ateş;
		} else {
			status[1] = Element.Su;
		}

		}
	public bool IsElementsEqual(string meteorElementi){
		switch (meteorElementi) {
		case "fire":
			for (int i = 0; i < 2; i++) {
				if(status[i]== Element.Ateş){
					return true;
				}
				return false;
			}
		case "earth": 
			for (int i = 0; i < 2; i++) {
				if(status[i]== Element.Toprak){
					return true;
				}
				return false;
			}
		case "water":
			for (int i = 0; i < 2; i++) {
				if(status[i]== Element.Su){
					return true;
				}
				return false;
			}
		case "lightning":
			for (int i = 0; i < 2; i++) {
				if(status[i]== Element.Elektrik){
					return true;
				}
				return false;
			}
				default:
			return true;
		}

	}

}
