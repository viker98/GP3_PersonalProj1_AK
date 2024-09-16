using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] Notes;
    [SerializeField] GameObject[] Activators;
    [SerializeField] float BPM = 5;

    private InputActionAsset _inputActions;
    private AudioSource _AudioSorce;

    // Start is called before the first frame update

    IEnumerator PlaySoundAfterFiveSeconds()
    {
        yield return new WaitForSeconds(5);
        _AudioSorce.Play();
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;

        _AudioSorce = GetComponent<AudioSource>();

        Notes = GameObject.FindGameObjectsWithTag("Note");

        for (int i = Notes.Count() - 1; i >= 0; i--) 
        {
            Notes[i].GetComponent<Note>().ChangeSpeed(BPM);
        }
    }

    void Start()
    {
        StartCoroutine(PlaySoundAfterFiveSeconds());
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
}
