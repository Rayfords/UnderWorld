using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastUI : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private Vector3 _offset;
    private Animator _b;
    private Transform _obj;
    private RectTransform _rctransform;
    private bool _isHover;
    private float _t = 0;
    private bool _playAnimation;
    private HandController _handControll;
    private Image _img;
    private bool first = true;

    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        _obj = GetComponent<Transform>();
        _handControll = GetComponent<HandController>();
        _img = GetComponent<Image>();
        _rctransform = GetComponent<RectTransform>();
        _isHover = false;
        _playAnimation = false;
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    // Update is called once per frame
    public void CastRay(Vector2 position)
    {

        bool hasCast = false;
        var ray = Camera.main.ScreenPointToRay(position);

        // Ray ray = new Ray(_obj.position, _obj.forward);
        Debug.DrawRay(_obj.position, _obj.forward * 100f, Color.yellow);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null && hit.collider.tag == "UI")
        {
            _rctransform.position = position;
            _img.enabled = true;
            _handControll.Hover();
            hasCast = true;
            var b = hit.collider.name;
            Debug.Log("hit something" + b);
            //hit.collider.gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
            if (!_playAnimation)
            {
                StartTimer();
                Debug.Log("Animation play");
            }
            else if (_isHover)
            {
                if (time >= 1)
                {
                    _b = hit.collider.gameObject.GetComponent<Animator>();

                    _b.Play("ButtonClick");
                    _isHover = false;

                    time = 0;
                }
            }
        }

        if (!hasCast)
        {
            _img.enabled = false;
            _handControll.NoHover();
            Debug.Log("Animation Stop");
            _playAnimation = false;
        }


    }
    private void StartTimer()
    {
        _playAnimation = true;
    }
    void FixedUpdate()
    {
        if (_playAnimation)
        {
            _t++;
            if (_t == _timer)
            {
                _t = 0;
                Hover();
            }
        }
        else
        {
            _t = 0;
        }
    }

    private void Hover()
    {
        _isHover = true;
        _playAnimation = false;
    }
}
