using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    [SerializeField] Note[] Notes;
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

        Notes = FindObjectsByType<Note>(FindObjectsSortMode.InstanceID);

        for (int i = Notes.Count() - 1; i >= 0; i--) 
        {
            Notes[i].ChangeSpeed(BPM);
        }
        //PlayerControlls playerCtrl = new PlayerControlls();
        //playerCtrl.ActivateActivators.GreenActivator.performed += (X) => Activators[0].GetComponent<Activator>().ButtonIsPressed();
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
