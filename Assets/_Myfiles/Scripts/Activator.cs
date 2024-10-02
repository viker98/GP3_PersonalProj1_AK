using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Activator : MonoBehaviour
{

    [SerializeField] GameObject NoteHolder;
    [SerializeField] GameObject NotePrefab;
    [SerializeField] bool bPlaceingNotes = false;

    private GameManager _gameManager;
    private bool _buttonPressed = false;
    private UIManager _UIManager;
    private Transform NotePlacement;



    private void Awake()
    {
        NotePlacement = gameObject.transform;
        _UIManager = FindObjectOfType<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();

        NoteHolder.GetComponent<Note>().ChangeSpeed(_gameManager.GetBPM() * 2);
        NoteHolder.GetComponent<Note>().SetbInSongCreator(_gameManager.AskIfInSongCreator());
        NoteHolder.GetComponent<Note>().bAreNoteHolder = true;
    }
    public GameObject GetNoteHolder()
    {
        return NoteHolder;
    }
    public Transform GetNotePlacement()
    {
        return NotePlacement;
    }
    public UIManager GetUIManager()
    {
        return _UIManager;
    }
    public void ButtonIsPressed()
    {
        if (bPlaceingNotes)
        {
            GameObject createdNote = Instantiate(NotePrefab, NotePlacement);
            createdNote.transform.parent = NoteHolder.transform;
            createdNote.GetComponent<Note>().ChangeSpeed(_gameManager.GetBPM());
            createdNote.GetComponent<Note>().SetbInSongCreator(_gameManager.AskIfInSongCreator());
            Debug.Log(createdNote);
            _gameManager.AddToNoteList(createdNote);
        }
        else 
        {
            _buttonPressed = true;       
        }
    }

    public void TurnOnSongCreator()
    {
        bPlaceingNotes = true;
    }

    public void DoneWithSongCreator()
    {
        bPlaceingNotes = false;
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D note)
    {
        if (_buttonPressed && !bPlaceingNotes)
        {
            Destroy(note.gameObject);
            _UIManager.HitNote();
        }
    }

    private void OnTriggerExit2D(Collider2D note)
    {
        if (!bPlaceingNotes)
        {
            note.GetComponent<Note>().LeftZone();
            if (_buttonPressed == false)
            {
                _UIManager.MissedNote();
            }
            _buttonPressed = false;
        }
    }

    
}
