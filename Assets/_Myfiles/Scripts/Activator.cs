using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Activator : MonoBehaviour
{
    bool _buttonPressed = false;
    private UIManager _UIManager;

    private void Awake()
    {
        _UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    public void ButtonIsPressed()
    {
        _buttonPressed = true;
    }

    private void OnTriggerStay2D(Collider2D note)
    {
        if (_buttonPressed)
        {
            Destroy(note.gameObject);
            _UIManager.HitNote();
        }
    }

    private void OnTriggerExit2D(Collider2D note)
    {
        note.GetComponent<Note>().LeftZone();
        if (_buttonPressed == false)
        {
            _UIManager.MissedNote();
        }
        _buttonPressed = false;
    }
}
