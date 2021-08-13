using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KL;

public class HandManager : MonoBehaviour
{
    [SerializeField] private GameObject _wall;
    [SerializeField] private GameObject[] _hands;
    [SerializeField] private GameObject[] _filter = new GameObject[1];
    private GameObject[] _handobj = new GameObject[1];
    private RectTransform[] _handRect = new RectTransform[1];
    private KalmanFilter[] _klFilter = new KalmanFilter[1];
    private Vector2 invert = Vector2.zero;
    private RayCastUI[] Raycasters;
    private Image _handImg;

    // Start is called before the first frame update
    void Start()
    {

        Raycasters = new RayCastUI[1];
        for (int i = 0; i < 1; i++)
        {
            _handobj[i] = Instantiate(_hands[i], _wall.transform);
            _handRect[i] = _handobj[i].GetComponent<RectTransform>();
            Raycasters[i] = _handobj[i].GetComponent<RayCastUI>();
            _handImg = _handobj[i].GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Centers();
    }

    void Centers()
    {
        Vector2 Hand;
        List<Vector2> centersPositions = PipeManager.Instance.GetCenters();
        lock (centersPositions)
        {
            if (centersPositions.Count > 0)
            {
                Hand = centersPositions[0];

                _handRect[0].anchoredPosition = Hand;
                //_handobj[0].GetComponent<HandController>().UpdateVectorFilterEvent();
                Raycasters[0].CastRay(Hand);
            }
            else {
                _handImg.enabled = false;
            }
        }
    }
}