using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Animator _animator;
    private Button _btn;
    // Start is called before the first frame update
    void Start()
    {
        _animator=GetComponent<Animator>();
        _btn=GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonClickEvent()
    {
        _animator.Play("ButtonNone");
        _btn.onClick.Invoke();
    }
}
