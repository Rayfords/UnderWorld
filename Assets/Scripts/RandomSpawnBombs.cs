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
//    //���� ��������� ���� � ����������� ����, ���� ��� �����
//    RaycastHit hit;
//    //��� ���, ���������� �� ������� ����� ������� � ��������� � ������� ����
//    Ray ray = new Ray(transform.position, bombs.position - transform.position);
//    //������� ���
//    Physics.Raycast(ray, out hit);

//    //���� ��� � ���-�� ��������, ��..
//    if (hit.collider != null)
//    {
//        //���� ��� �� ����� � ����
//        if (hit.collider.gameObject != bombs.gameObject)
//        {
//            Debug.Log("���� � ����� ����������� ������: " + hit.collider.name);
//        }
//        //���� ��� ����� � ����
//        else
//        {
//            Debug.Log("������� �� �����!!!");
//        }
//        //������ ��� ����������� ������ ��� � ���� Scene
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