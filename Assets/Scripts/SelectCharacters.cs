using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacters : MonoBehaviour
{
    float time = 0.1f;


    public Text[] nameFish;
    public Text NameFish;


    [SerializeField] private Material[] _material;
    [SerializeField] private Material[] _materialCopy;
    [SerializeField] private Color[] _colorsArray;
    [SerializeField] private Image _colorImage;
    [SerializeField] private Image _colorTexture;


    private int numberCharacters;
    private int colorIndex;
    private int textureIndex;
    //private int CurrentCharacter;

    public GameObject[] allCharacters;

    //public GameObject buttonLeft;
    //public GameObject buttonRight;

    //public GameObject buttonSpawnCharacter;
    //public GameObject textSpawnCharacter;

    public GameObject Menu;
    public GameObject spawnMenu;
    public GameObject customMenu;

    public GameObject openMenu;
    public GameObject closedMenu;

    [SerializeField] private GameObject Burst;

    [SerializeField] private GameObject[] ObjToSpawn;

    //public List<Transform> spawnPoints = new List<Transform>();
    //public FishSpawn[] spawns;
    //[SerializeField] private GameObject[] SpawnZone;
    public Transform[] SpawnPosition;
    public Transform CustomSpawnPosition;
    private TextureGenerator _generator;
    private Texture2D[] _texturearray;

    Vector3 whereToSpawn;
    //float RandX;
    //float RandY;
    //float RandZ;

    //public Vector3 center;
    //public Vector3 size;

    [SerializeField] private GameObject FishSpawnSparks;


    // Start is called before the first frame update
    void Start()
    {
        canceledChange();

        //_material[numberCharacters].color = _materialCopy[numberCharacters].color;
        //_material[numberCharacters].mainTexture = _materialCopy[numberCharacters].mainTexture;

        //_material[numberCharacters].mainTexture = _materialCopy[numberCharacters].mainTexture;
        _generator = GetComponent<TextureGenerator>();

        //nameFish[numberCharacters].text = allCharacters[numberCharacters].name;
        NameFish.text = nameFish[numberCharacters].text;

        allCharacters = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            allCharacters[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in allCharacters)
        {
            go.SetActive(false);
        }

        if (allCharacters[0])
        {
            allCharacters[0].SetActive(true);
        }

        //if (PlayerPrefs.HasKey("CurrentCharacter"))
        //{
        //    numberCharacters = PlayerPrefs.GetInt("CurrentCharacter");
        //    CurrentCharacter = PlayerPrefs.GetInt("CurrentCharacter");
        //}
        //else
        //{
        //    PlayerPrefs.GetInt("CurrentCharacter", numberCharacters);
        //}

        //allCharacters[numberCharacters].SetActive(true);

        //buttonSpawnCharacter.SetActive(false);
        //textSpawnCharacter.SetActive(true);

        //if (numberCharacters > 0)
        //{
        //    buttonLeft.SetActive(true);
        //}

        //if (numberCharacters == allCharacters.Length)//����� ����������
        //{
        //    //numberCharacters = 0;
        //    buttonRight.SetActive(false);
        //}
    }

    public void RandomeChangeColor()
    {
        _material[numberCharacters].color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));


        //colorIndex = Random.Range(0, _colorsArray.Length - 1);
        //_material[numberCharacters].color = _colorsArray[colorIndex];
        //_colorImage.color = new Color(_colorsArray[colorIndex].r, _colorsArray[colorIndex].g, _colorsArray[colorIndex].b, 1);

    }
    public void RandomChangeTexture()
    {
        textureIndex = Random.Range(0, _generator.GetTextureArray().Length - 1);
        _material[numberCharacters].mainTexture = _generator.GetTexture(textureIndex);
        
    }
    public void TextureUp()
    {
        textureIndex++;
        if(textureIndex==_generator.GetTextureArray().Length)
        {
           textureIndex = 0;
        }
        _material[numberCharacters].mainTexture = _generator.GetTexture(textureIndex);
    }
    public void TextureDown()
    {
        textureIndex--;
        if(textureIndex<0)
        {
            textureIndex = _generator.GetTextureArray().Length-1;
        }
        _material[numberCharacters].mainTexture = _generator.GetTexture(textureIndex);
    }

    public void ColorUp()
    {
        colorIndex++;
        if (colorIndex == _colorsArray.Length)
        {
            colorIndex = 0;
        }
        _material[numberCharacters].color = _colorsArray[colorIndex];
        _colorImage.color = new Color(_colorsArray[colorIndex].r, _colorsArray[colorIndex].g, _colorsArray[colorIndex].b, 1);

    }
    public void ColorDown()
    {
        colorIndex--;
        if (colorIndex < 0)
        {
            colorIndex = _colorsArray.Length - 1;
        }
        _material[numberCharacters].color = _colorsArray[colorIndex];
        _colorImage.color = new Color(_colorsArray[colorIndex].r, _colorsArray[colorIndex].g, _colorsArray[colorIndex].b, 1);

    }

    public void ArrowRight()
    {
        allCharacters[numberCharacters].SetActive(false);
        numberCharacters++;

        _material[numberCharacters-1].color = _materialCopy[numberCharacters-1].color;
        _material[numberCharacters-1].mainTexture = _materialCopy[numberCharacters-1].mainTexture;

        //nameFish[numberCharacters].text = allCharacters[numberCharacters].name;


        if (numberCharacters == allCharacters.Length)
        {
            numberCharacters = 0;
        }
        allCharacters[numberCharacters].SetActive(true);

        NameFish.text = nameFish[numberCharacters].text;

        //if (numberCharacters < allCharacters.Length)
        //{
        //    if (numberCharacters == 0)
        //    {
        //        buttonLeft.SetActive(true);
        //    }

        //    allCharacters[numberCharacters].SetActive(false);//��������� ���������
        //    numberCharacters++;
        //    allCharacters[numberCharacters].SetActive(true);//�������� ���������� ���������

        //    if (CurrentCharacter == numberCharacters)
        //    {
        //        buttonSpawnCharacter.SetActive(false);
        //        textSpawnCharacter.SetActive(true);
        //    }
        //    else
        //    {
        //        buttonSpawnCharacter.SetActive(true);
        //        textSpawnCharacter.SetActive(false);
        //    }

        //    if (numberCharacters + 1 == allCharacters.Length/*numberCharacters == allCharacters.Length*/)
        //    {
        //        //numberCharacters = 0;
        //        buttonRight.SetActive(false);
        //    }
        //}
    }

    public void ArrowLeft()
    {
        allCharacters[numberCharacters].SetActive(false);
        numberCharacters--;

        _material[numberCharacters+1].color = _materialCopy[numberCharacters+1].color;
        _material[numberCharacters+1].mainTexture = _materialCopy[numberCharacters+1].mainTexture;

        //nameFish[numberCharacters].text = allCharacters[numberCharacters].name;


        if (numberCharacters < 0)
        {
            numberCharacters = allCharacters.Length - 1;
        }
        allCharacters[numberCharacters].SetActive(true);
        NameFish.text = nameFish[numberCharacters].text;

        //if (numberCharacters < allCharacters.Length)
        //{
        //    allCharacters[numberCharacters].SetActive(false);//��������� ���������
        //    numberCharacters--;
        //    allCharacters[numberCharacters].SetActive(true);//�������� ���������� ���������
        //    buttonRight.SetActive(true);

        //    if (CurrentCharacter == numberCharacters)
        //    {
        //        buttonSpawnCharacter.SetActive(false);
        //        textSpawnCharacter.SetActive(true);
        //    }
        //    else
        //    {
        //        buttonSpawnCharacter.SetActive(true);
        //        textSpawnCharacter.SetActive(false);
        //    }

        //    if (numberCharacters == 0)
        //    {
        //        buttonLeft.SetActive(false);
        //    }
        //}
    }

    //public void SelecCharacter()
    //{
    //    //PlayerPrefs.SetInt("CurrentCharacter", numberCharacters);
    //    //CurrentCharacter = numberCharacters;
    //    //buttonSpawnCharacter.SetActive(false);
    //    //textSpawnCharacter.SetActive(true);
    //}

    //------------------------------------------
    public void OpenMenu()
    {
        openMenu.SetActive(false);

        _material[numberCharacters].color = _materialCopy[numberCharacters].color;
        _material[numberCharacters].mainTexture = _materialCopy[numberCharacters].mainTexture;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1f;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        var burst = Instantiate(Burst, mousePosition, Quaternion.identity);
        Destroy(burst, 1f);

        //buttonLeft.SetActive(true);
        //buttonRight.SetActive(true);
        customMenu.SetActive(false);


        Menu.SetActive(true);
        spawnMenu.SetActive(true);
        closedMenu.SetActive(true);        
    }

    public void ClosedMenu()
    {
        openMenu.SetActive(true);
        Menu.SetActive(false);
        spawnMenu.SetActive(false);
        customMenu.SetActive(false);
        closedMenu.SetActive(false);

        _material[numberCharacters].color = _materialCopy[numberCharacters].color;
        _material[numberCharacters].mainTexture = _materialCopy[numberCharacters].mainTexture;

    }

    public void CustomMenu()
    {
        //Menu.SetActive(false);
        spawnMenu.SetActive(false);
        customMenu.SetActive(true);
    }

    public void buttonBack()
    {
        Menu.SetActive(true);
        spawnMenu.SetActive(true);
        customMenu.SetActive(false);

        _material[numberCharacters].color = _materialCopy[numberCharacters].color;
        _material[numberCharacters].mainTexture = _materialCopy[numberCharacters].mainTexture;
    }

    public void buttonSelect()
    {
        Menu.SetActive(false);
        customMenu.SetActive(false);
        closedMenu.SetActive(false);
        openMenu.SetActive(true);
        //------------------------------------------------------------
        //whereToSpawn = new Vector3(-1.279f, 0.726f, -10.5928f);

        ////Transform pos = SpawnPosition[numberCharacters];
        //Instantiate(ObjToSpawn[numberCharacters], whereToSpawn, transform.rotation);
        //-------------------------------------------------------------------------
        Transform pos = CustomSpawnPosition;//[Random.Range(0, SpawnPosition.Length)]
        var sparks = Instantiate(FishSpawnSparks, pos.position, pos.rotation);
        Destroy(sparks, 2f);
        Instantiate(ObjToSpawn[numberCharacters], pos.position, pos.rotation);

        Invoke("canceledChange", 0.5f);

        //for (int i = 0; i < _material.Length; i++)
        //{
        //    _material[i].color = _materialCopy[i].color;
        //    _material[i].mainTexture = _materialCopy[i].mainTexture;
        //}

        //_material[numberCharacters].color = _materialCopy[numberCharacters].color;
        //_material[numberCharacters].mainTexture = _materialCopy[numberCharacters].mainTexture;
    }

    void canceledChange()
    {
        for (int i = 0; i < _material.Length; i++)
        {
            _material[i].color = _materialCopy[i].color;
            _material[i].mainTexture = _materialCopy[i].mainTexture;
        }
    }
    //-------------------------------
    void FixedUpdate()
    {
        Quaternion rotationY = Quaternion.AngleAxis(1, Vector3.up/*(0, 1, 0)*/);
        transform.rotation *= rotationY;
    }
    //-------------------------------
    public void ObjSpawn()
    {
        //Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Transform pos = SpawnPosition[numberCharacters];//[Random.Range(0, SpawnPosition.Length)]
        Instantiate(ObjToSpawn[numberCharacters], pos.position, pos.rotation);
        //GameObject go = Instantiate(ObjToSpawn[numberCharacters], pos.position, pos.rotation);
        //go.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        
        
        //RandX = Random.Range(-0.949f, 0.472f);
        //RandY = Random.Range(0.244f, 0.905f);
        //RandZ = Random.Range(-11.547f, -10.608f);


        //whereToSpawn = new Vector3(RandX, RandY, RandZ);

        //Instantiate(ObjToSpawn[numberCharacters], whereToSpawn, transform.rotation);

        //Instantiate(ObjToSpawn[numberCharacters], SpawnPosition.position, SpawnPosition.rotation);

        //Instantiate(ObjToSpawn[numberCharacters], transform.position, Quaternion.identity);
    }
    //-------------------------------
    //public void RandomeChangeColor()
    //{
    //    _matrerial.color= new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    //    //gameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    //    //GetComponent<Renderer>().material.color = Random.ColorHSV();
    //    //gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
    //}
    //-------------------------------

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
