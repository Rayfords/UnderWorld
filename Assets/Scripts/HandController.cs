using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private KalmanFilter _filter;
    private RectTransform _rectTransform;
    private bool _isHover=false;
    // Start is called before the first frame update
    void Start()
    {
        _filter=GetComponent<KalmanFilter>();
        _rectTransform=GetComponent<RectTransform>();
    }
    public void Hover()
    {
        _isHover=true;
    }
    public void NoHover()
    {
        _isHover=false;
    }
    /*public void UpdateVectorFilterEvent()
    {
        if(_isHover)
        {
            _rectTransform.anchoredPosition=_filter.Vector2DFilterUpdate(_rectTransform.anchoredPosition);
        }
        else
        {
            _filter.Vector2DFilterUpdate(_rectTransform.anchoredPosition);
        }
    }*/
}
