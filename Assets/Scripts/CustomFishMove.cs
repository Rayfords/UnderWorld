using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFishMove : MonoBehaviour
{
    //[SerializeField] private Vector3 EndPoint;
    [SerializeField] GameObject EndPoint;

    private float time = 0;


    public Rigidbody rb;

    public float moveXfloat = 0.4f;
    private bool bMoveX = true;
    private bool bMoveY = true;

    private bool bRotate = true;

    private float direction = 1f;

    private bool move = false;


    TriggerTest t;

    private void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (time < 2.4)
        {
            Quaternion rotationY = Quaternion.AngleAxis(1, Vector3.up/*(0, 1, 0)*/);
            transform.rotation *= rotationY;
        }
        else
        {
            Quaternion rotationY = Quaternion.AngleAxis(0, Vector3.up/*(0, 1, 0)*/);
            transform.rotation *= rotationY;
        }


    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= 2.4 && time < 5)
        {
            EndPoint = GameObject.Find("EndPoint");
            transform.LookAt(EndPoint.transform.position);
            transform.position = Vector3.Lerp(transform.position, EndPoint.transform.position, 0.02f);

            //transform.position = Vector3.MoveTowards(transform.position, EndPoint, Time.deltaTime);
        }
        if (time>5)
        {
            Debug.Log("Custom move=true");

            // Переміщює об'єкт по осі Х.
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
        }
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

    private void OnTriggerEnter(Collider fish)
    {
        
    }

    //public Rigidbody rb;

    //public float moveXfloat = 0.4f;
    //private bool bMoveX = true;
    //private bool bMoveY = true;

    //private bool bRotate = true;

    //private float direction = 1f;

    //void Start()
    //{

    //}

    //void FixedUpdate()
    //{
    //    Quaternion rotationY = Quaternion.AngleAxis(1, Vector3.up/*(0, 1, 0)*/);
    //    transform.rotation *= rotationY;
    //}

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

}
