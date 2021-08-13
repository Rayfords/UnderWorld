using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnBombs : MonoBehaviour
{
    public GameObject bombs;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;
    float RandX;
    Vector3 whereToSpawn;

    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject()
    {
        RandX = Random.Range(-1.3f, 0.912f);
        whereToSpawn = new Vector3(RandX, transform.position.y, transform.position.z);

        var Bombs = Instantiate(bombs, whereToSpawn, transform.rotation);
        Destroy(Bombs, 3.2f);

        //Instantiate(bombs, transform.position, transform.rotation);
        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}





















//void Update()
//{
//    //сюда запишется инфо о пересечении луча, если оно будет
//    RaycastHit hit;
//    //сам луч, начинается от позиции этого объекта и направлен в сторону цели
//    Ray ray = new Ray(transform.position, bombs.position - transform.position);
//    //пускаем луч
//    Physics.Raycast(ray, out hit);

//    //если луч с чем-то пересёкся, то..
//    if (hit.collider != null)
//    {
//        //если луч не попал в цель
//        if (hit.collider.gameObject != bombs.gameObject)
//        {
//            Debug.Log("Путь к врагу преграждает объект: " + hit.collider.name);
//        }
//        //если луч попал в цель
//        else
//        {
//            Debug.Log("Попадаю во врага!!!");
//        }
//        //просто для наглядности рисуем луч в окне Scene
//        Debug.DrawLine(ray.origin, hit.point, Color.red);
//    }
//}















//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RandomSpawn : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject bomb;
//    float RandX;
//    Vector2 whereToSpawn;
//    [SerializeField]
//    private float spawnRate = 2f;
//    float nextSpawn = 0.0f;

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(Time.time>nextSpawn)
//        {
//            nextSpawn = Time.time + spawnRate;
//            RandX = Random.Range(-1.3f, 0.912f);
//            whereToSpawn = new Vector2(RandX, transform.position.y);
//            Instantiate(bomb, whereToSpawn, Quaternion.identity);
//        }
//    }
//}