using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyBomb : MonoBehaviour
{
    [SerializeField] private GameObject Explosion;
    private Camera mainCamera;
    private Collider coll;

    //public bool Whale = false;

    [SerializeField] private GameObject Whale;
    public Vector3 spawnPosition;

    // ѕеременна€ дл€ звука во врем€ попадани€ 
    //public AudioClip hitSound;


    void Start()
    {
        mainCamera = Camera.main;

        coll = GetComponent<Collider>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (coll.Raycast(ray, out hit, 100.0F))
            {
                Debug.Log("Destroy Bomb");
                // ¬оспроизвести звук попадани€ выстрела
                //GetComponent<AudioSource>().PlayOneShot(hitSound);
                Destroy(gameObject);//transform.position = ray.GetPoint(100.0F);
                

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 5.8f;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);//Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var explosion = Instantiate(Explosion, mousePosition, Quaternion.identity);
                Quake.Shake(1.3f, 0.2f);//StartCoroutine(shake.Shake(0.15f, 0.4f));
                Destroy(explosion, 1.2f);

                var whale = Instantiate(Whale, spawnPosition, Quaternion.identity);
                Destroy(whale, 36.1f);
                //Quake.Shake(3f, 0.2f);//StartCoroutine(shake.Shake(0.15f, 0.4f));

                //Whale = true;

            }
        }
    }
}













//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.EventSystems;
//public class DestroyBomb : MonoBehaviour/*, IPointerClickHandler*/
//{
//    //public void OnPointerClick(PointerEventData eventData)
//    //{
//    //    Destroy(this.gameObject);
//    //}

//    //public GameObject bombs;
//    public GameObject bombs;


//    void OnMouseOver()
//    {
//        var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit hit;
//        Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//        if (Physics.Raycast(Ray, out hit, 100.0f))
//        {
//            Debug.Log("asdasd");
//            Destroy(gameObject);
//        }
//        //if (Input.GetMouseButton(0))
//        //{
//        //    Debug.Log("asdasd");
//        //    Destroy(gameObject);
//        //}



//    }

//}
