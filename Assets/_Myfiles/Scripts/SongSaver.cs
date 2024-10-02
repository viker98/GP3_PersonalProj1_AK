using System.Collections.Generic;
using UnityEngine;

public class SongSaver : MonoBehaviour
{
    GameManager _gameManager;
    UIManager _uiManager;
    AudioSource _audioSource;

    Dictionary<string,GameObject> MusicDataBase = new Dictionary<string,GameObject>();

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
        _audioSource = _gameManager.gameObject.GetComponent<AudioSource>();

    }

    private void Update()
    {

    }
    public void PlaySongSoFar()
    {
        
        _gameManager.GetNoteParent().transform.position = new Vector3(0,((_gameManager.GetBPM() /60f) * 5) / 2,0);
        _audioSource.Stop();
        _gameManager.TurnOffSongCreator();
        StartCoroutine(_gameManager.PlaySoundAfterFiveSeconds());
    }
    public void RestartSongCreator()
    {
        _gameManager.GetNoteParent().transform.position = new Vector3(0,0,0);
        _audioSource.Stop();
        _audioSource.Play();
        _gameManager.TurnOnSongCreator();
    }

}
