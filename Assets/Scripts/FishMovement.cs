using UnityEngine;
using UnityEngine.UI;

public class FishMovement : MonoBehaviour {

    //public Rigidbody rb;

    //public float moveXfloat = 0.4f;
    //private bool bMoveX = true;
    //private bool bMoveY = true;

    //private bool bRotate = true;

    //private float direction = 1f;

    //void Start()
    //{

    //}

    ////void FixedUpdate()
    ////{
    ////    Quaternion rotationY = Quaternion.AngleAxis(1, Vector3.up/*(0, 1, 0)*/);
    ////    transform.rotation *= rotationY;
    ////}

    ////Оновлення викликається один раз на кадр
    //void Update()
    //{
    //    //Переміщює об'єкт по осі Х.
    //    gameObject.transform.Translate(new Vector3(moveXfloat * direction, 0, 0) * Time.deltaTime);
    //    //Викликається метод для змінення швидкості переміщення по осі Х. Метод викликається раз у 1 - 3 секунди.
    //    float secX = Random.Range(1f, 3f);
    //    if (bMoveX)
    //    {
    //        Invoke("moveX", secX);
    //        bMoveX = false;
    //    }
    //    //Викликається метод для штовхання об'єкту по осі Y. Метод викликається раз у 400 - 500 мілісекунд.
    //    float secY = Random.Range(0.4f, 0.5f);
    //    if (bMoveY)
    //    {
    //        Invoke("moveY", secY);
    //        bMoveY = false;
    //    }
    //    //Змінення позиції тексту над рибою.
    //    //tempNameFish.transform.position = Camera.main.WorldToScreenPoint(transform.position + offsetText);
    //    //Викликається метод для повороту об'єкту по осі Х. Метод викликається раз у 5 секунд.
    //    if (bRotate)
    //    {
    //        Invoke("canRotate", 5f);
    //        bRotate = false;
    //    }
    //}

    //void moveX()
    //{
    //    moveXfloat = Random.Range(0.25f, 0.4f);
    //    bMoveX = true;
    //}

    //void moveY()
    //{
    //    float moveYfloat = Random.Range(-0.005f, 0.005f);
    //    rb.AddForce(0, moveYfloat, 0, ForceMode.VelocityChange);
    //    bMoveY = true;
    //}

    public Rigidbody rb;
    public float moveXfloat = 0.4f;
    public Text nameFish;
    protected Text tempNameFish;
    private bool bMoveX = true;
    private bool bMoveY = true;
    private bool bRotate = true;
    private Vector3 dirToRotate;
    public Vector3 offsetText;
    private bool bChangeTexture;
    private float direction = 1f;
    void Start()
    {
        //tempNameFish = Instantiate(nameFish, FindObjectOfType<Canvas>().transform).GetComponent<Text>();
        changeText();
        tempNameFish.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        //tempText = gameObject.GetComponent<Renderer>().material.mainTexture as Texture2D;
        bChangeTexture = false;
    }

    //Оновлення викликається один раз на кадр
    void Update()
    {
        //Змінює текстуру, якщо користувач у меню натиснув на кнопку "Set texture".
        if (gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>() != null &&
            gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material != null &&
            FindObjectOfType<ParticePaint>() != null &&
            FindObjectOfType<ParticePaint>().getChangedTexture() != null)
        {
            if (bChangeTexture)
            {
                Texture2D tempTexture = FindObjectOfType<ParticePaint>().getChangedTexture();
                gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = tempTexture;
                tempTexture.Apply();
                bChangeTexture = false;
            }
        }
        //Переміщює об'єкт по осі Х.
        gameObject.transform.Translate(new Vector3(moveXfloat * direction, 0, 0) * Time.deltaTime);
        //Викликається метод для змінення швидкості переміщення по осі Х. Метод викликається раз у 1 - 3 секунди.
        float secX = Random.Range(1f, 3f);
        if (bMoveX)
        {
            Invoke("moveX", secX);
            bMoveX = false;
        }
        //Викликається метод для штовхання об'єкту по осі Y. Метод викликається раз у 400 - 500 мілісекунд.
        float secY = Random.Range(0.4f, 0.5f);
        if (bMoveY)
        {
            Invoke("moveY", secY);
            bMoveY = false;
        }
        //Змінення позиції тексту над рибою.
        //tempNameFish.transform.position = Camera.main.WorldToScreenPoint(transform.position + offsetText);
        //Викликається метод для повороту об'єкту по осі Х. Метод викликається раз у 5 секунд.
        if (bRotate)
        {
            Invoke("canRotate", 5f);
            bRotate = false;
        }
        rotateTo();
    }

    public void changeTexture()
    {
        bChangeTexture = true;
    }

    void canRotate()
    {
        dirToRotate = new Vector3(Random.Range(gameObject.transform.position.x - 1f, gameObject.transform.position.x + 2f), gameObject.transform.position.y, gameObject.transform.position.z);
        bRotate = true;
    }

    void rotateTo()
    {
        Vector3 dir = dirToRotate - gameObject.transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rot, 0.1f * Time.deltaTime);

    }

    void changeText()
    {
        tempNameFish.text = "";
        int i = 0;
        while (i < gameObject.name.Length && gameObject.name[i] != '(')
            tempNameFish.text += gameObject.name[i++];
    }

    void moveX()
    {
        moveXfloat = Random.Range(0.25f, 0.4f);
        bMoveX = true;
    }

    void moveY()
    {
        float moveYfloat = Random.Range(-0.005f, 0.005f);
        rb.AddForce(0, moveYfloat, 0, ForceMode.VelocityChange);
        bMoveY = true;
    }

    public void destroyText()
    {
        Destroy(tempNameFish.gameObject);
    }
}