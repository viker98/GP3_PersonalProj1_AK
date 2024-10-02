using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Notes = new List<GameObject>();
    [SerializeField] GameObject[] Activators;
    [SerializeField] float BPM = 5;
    [SerializeField] private bool bInSongCreator = false;
    [SerializeField] private GameObject NoteParent;

    private InputActionAsset _inputActions;
    private AudioSource _AudioSource;
    private UIManager _UIManager;
    private Camera _Camera;
    bool _bIsPauseMenuOpen;

    // Start is called before the first frame update

    public IEnumerator PlaySoundAfterFiveSeconds()
    {
        yield return new WaitForSeconds(5);
        _AudioSource.Play();
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;

        _AudioSource = GetComponent<AudioSource>();
        _UIManager = FindObjectOfType<UIManager>();
        _Camera = Camera.main;

        if (!bInSongCreator)
        {
            Notes.AddRange(GameObject.FindGameObjectsWithTag("Note"));
        }

        foreach (GameObject activator in Activators)
        {
            Vector2 transform = new Vector2(activator.transform.localPosition.x,0);
            _UIManager.AddLaneLocationsToList(transform);
        }
    }

    void Start()
    {
        if (!bInSongCreator)
        {
            StartCoroutine(PlaySoundAfterFiveSeconds());
        }
        else
        {
            _AudioSource.loop = true;
        }
    }

    private void Update()
    {
        if (_UIManager.CheckIfSongIsPaused())
        {

            _AudioSource.Pause();
            NoteParent.GetComponent<Note>().SetbPaused(_UIManager.CheckIfSongIsPaused() );
            NoteParent.transform.position = NoteParent.transform.position;

        }
        else
        {
            _AudioSource.UnPause();
            NoteParent.GetComponent<Note>().SetbPaused(_UIManager.CheckIfSongIsPaused());
        }

        if (_UIManager.CheckIfFastForwarding())
        {
            NoteParent.GetComponent<Note>().ChangeSpeed(BPM * 2);
        }
        else if (_UIManager.CheckIfRewinding()) 
        {
            NoteParent.GetComponent<Note>().ChangeSpeed(BPM * -2);
        }
        else
        {
            NoteParent.GetComponent<Note>().ChangeSpeed(BPM);
        }
    }

    public GameObject GetNoteParent()
    {
        return NoteParent;
    }

    public float GetBPM()
    {
        return BPM;
    }
    public bool AskIfInSongCreator()
    {
        return bInSongCreator;
    }
    public void AddToNoteList(GameObject note)
    {
        Notes.Add(note);
    }

    private void OnGreenActivator()
    {
        Activators[0].GetComponent<Activator>().ButtonIsPressed();
    }
    private void OnRedActivator()
    {
        Activators[1].GetComponent<Activator>().ButtonIsPressed();
    }
    private void OnYellowActivator()
    {
        Activators[2].GetComponent<Activator>().ButtonIsPressed();
    }
    private void OnBlueActivator()
    {
        Activators[3].GetComponent<Activator>().ButtonIsPressed();
    }
    private void OnClick()
    {
        var rayHit = Physics2D.GetRayIntersection(_Camera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider || !rayHit.collider.CompareTag("Note"))
        {
            return;
        }

        _UIManager.PauseSong();
        _AudioSource.Pause();
        NoteParent.GetComponent<Note>().SetbPaused(_UIManager.CheckIfSongIsPaused());
        NoteParent.transform.position = NoteParent.transform.position;
        rayHit.collider.gameObject.GetComponent<Note>().TurnOnSphere();
        _UIManager.TurnOnMoveButtons();
        _UIManager.UpdateCurrentSelectedNote(rayHit.collider.gameObject);
    }

    private void OnPause()
    {
        if (_bIsPauseMenuOpen)
        {
            _bIsPauseMenuOpen = false;
            _UIManager.PlaySong();
            _UIManager.GetPauseMenu().SetActive(false);
        }
        else
        {
            _bIsPauseMenuOpen = true;
            _UIManager.PauseSong();
            _UIManager.GetPauseMenu().SetActive(true);
        }

    }

    public void TurnOffSongCreator()
    {
        bInSongCreator = false;
        foreach(GameObject activator in Activators)
        {
            activator.GetComponent<Activator>().DoneWithSongCreator();
        }
    }
    public void TurnOnSongCreator()
    {
        bInSongCreator = true;
        foreach(GameObject activator in Activators)
        {
            activator.GetComponent<Activator>().TurnOnSongCreator();
        }
    }
}
