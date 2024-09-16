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
        float beatDuration = 60f / BPM;
        _MoveSpeed = _distancePerBeat / beatDuration;
    }

    public void LeftZone()
    {
        Destroy(gameObject, 4);
    }
    void Update()
    {
        transform.Translate(Vector2.down * _MoveSpeed * Time.deltaTime);
    }
}
