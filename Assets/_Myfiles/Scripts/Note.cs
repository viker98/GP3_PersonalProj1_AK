using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor.Rendering;
using UnityEngine;

public class Note : MonoBehaviour
{

    [SerializeField] private float BPM;

    private float _distancePerBeat = 1f;
    private float _MoveSpeed;
    Rigidbody2D _RigidBody;

    public bool bAreNoteHolder = false;
    [SerializeField]bool _bInSongCreator = false;
    [SerializeField]bool _Paused = false;
    [SerializeField] GameObject ClickedSphere; 

    private void Awake()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
    }
    public void ChangeSpeed(float BPM)
    {
        this.BPM = BPM;
    }

    private void Start()
    {
        float beatDuration = 60f / (BPM / 2);
        _MoveSpeed = _distancePerBeat / beatDuration;
    }

    public void TurnOnSphere()
    {
        if (ClickedSphere != null && _bInSongCreator == true)
        {
            ClickedSphere.SetActive(true);
        }
    }
    public void TurnOffSphere()
    {
        ClickedSphere.SetActive(false);
    }

    public void LeftZone()
    {
        Destroy(gameObject, 1);
    }

    public void SetbInSongCreator(bool value)
    {
        _bInSongCreator = value;
    }
    public void SetbPaused(bool value)
    {
        _Paused = value;
    }
    void Update()
    {
        if (bAreNoteHolder)
        {
            if (!_bInSongCreator)
            {
                if (_Paused)
                {
                    return;
                }
                transform.Translate(Vector2.down * _MoveSpeed * Time.deltaTime);
            }
            else
            {
                float beatDuration = 60f / (BPM/2);
                _MoveSpeed = _distancePerBeat / beatDuration;
                if (_Paused)
                {
                    return;
                }
                transform.Translate(Vector2.down * _MoveSpeed * Time.deltaTime);
            }
        }          
    }
}
