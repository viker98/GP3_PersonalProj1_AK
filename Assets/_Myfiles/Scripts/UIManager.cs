using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI MultiplierText;
    [SerializeField] TextMeshProUGUI ComboText;
    [SerializeField] ParticleSystem HitParticle;
    [SerializeField] ParticleSystem MissParticle;
    [SerializeField] GameObject HitSprite;
    [SerializeField] GameObject MissSprite;
    [SerializeField] Slider TimeSlider;
    [SerializeField] GameObject ForwardButtonGO;
    [SerializeField] GameObject RewindButtonGO;
    [SerializeField] GameObject PauseButtonGO;
    [SerializeField] GameObject GreenLocationButton;
    [SerializeField] GameObject RedLocationButton;
    [SerializeField] GameObject YellowLocationButton;
    [SerializeField] GameObject BlueLocationButton;
    [SerializeField] GameObject TestSongButton;
    [SerializeField] GameObject ReEnterSongCreatorButton;
    [SerializeField] GameObject PauseMenu;


    GameObject CurrentSelectedNote;
    List<Vector2> LaneLocations = new List<Vector2>();
    bool bIsSongPaused;
    AudioSource _audioSource;
    bool finishedSong = false;
    bool bFastFoward = false;
    bool bRewinding = false;
    int _Score = 0;
    int _Multiplier = 1;
    int _Combo = 0;

    public void AddLaneLocationsToList(Vector2 location)
    {
        LaneLocations.Add(location);
    }

    public List<Vector2> GetLaneLocation()
    {
        return LaneLocations;
    }

    IEnumerator HitSpriteTimer()
    {
        yield return new WaitForSeconds(0.2f);
        HitSprite.SetActive(false);
    }
    IEnumerator MissSpriteTimer()
    {
        yield return new WaitForSeconds(0.2f);
        MissSprite.SetActive(false);
    }


    private void Awake()
    {
        _audioSource = FindObjectOfType<AudioSource>();


        ScoreText.text = ("Score: " + _Score);
        MultiplierText.text = ("Multiplier: " + _Multiplier);
        ComboText.text = ("Combo: " + _Combo);
    }


    public void HitNote()
    {
        HitSprite.SetActive(true);
        HitParticle.Play();
        Debug.Log("Hit hit");
        _Score += 10 * _Multiplier;
        Debug.Log(_Score);
        _Combo += 1; 

        if (_Combo >= 10)
        {
            _Multiplier = 2;
        }
        else if (_Combo > 20)
        {
            _Multiplier = 3;
        }
        else if (_Combo > 20)
        {
            _Multiplier = 4;
        }

        ScoreText.text = ("Score: " + _Score);
        MultiplierText.text = ("Multiplier: " + _Multiplier);
        ComboText.text = ("Combo: " + _Combo);
        
        StartCoroutine(HitSpriteTimer());
    } 
    public void MissedNote()
    {
        MissParticle.Play();
        MissSprite.SetActive(true);
        _Score -= 10;
        _Multiplier = 1;
        _Combo = 0;

        ScoreText.text = ("Score: " + _Score);
        MultiplierText.text = ("Multiplier: " + _Multiplier);
        ComboText.text = ("Combo: " + _Combo);

        StartCoroutine(MissSpriteTimer());
    }

    private void Update()
    {
        TimeSlider.value = _audioSource.time / _audioSource.clip.length;
    }
    

    public void PauseSong()
    {
        bIsSongPaused = true;
    }
    public void PlaySong()
    {
        bIsSongPaused = false;
    }
    public void PauseButton()
    {
        if (bIsSongPaused)
        {
            bIsSongPaused = false;
        }
        else
        {
            bIsSongPaused = true;
        }
    }

    private void FastForward()
    {
        _audioSource.pitch = _audioSource.pitch * 2;
        bFastFoward = true;
    }
    private void CancelFastForward()
    {
        _audioSource.pitch = _audioSource.pitch / 2;
        bFastFoward = false;
    }
    private void Rewind()
    {
        _audioSource.pitch = _audioSource.pitch * -2;
        bRewinding = true;
    }
    private void CancelRewind()
    {
        _audioSource.pitch = _audioSource.pitch / -2;
        bRewinding = false;
    }

    public bool CheckIfSongIsPaused()
    {
        return bIsSongPaused;
    }
    public bool CheckIfRewinding()
    {
        return bRewinding;
    }
    public bool CheckIfFastForwarding()
    {
        return bFastFoward;
    }

    public void TurnOnMoveButtons()
    {
        GreenLocationButton.SetActive(true);
        RedLocationButton.SetActive(true);
        YellowLocationButton.SetActive(true);
        BlueLocationButton.SetActive(true);
    }
    public void TurnOffMoveButtons()
    {
        GreenLocationButton.SetActive(false);
        RedLocationButton.SetActive(false);
        YellowLocationButton.SetActive(false);
        BlueLocationButton.SetActive(false);
    }
    public void UpdateCurrentSelectedNote(GameObject note)
    {
        CurrentSelectedNote = note;
    }

    public void GreenButtonPressed()
    {
        CurrentSelectedNote.transform.position = new Vector2(LaneLocations[0].x, CurrentSelectedNote.transform.position.y);
        TurnOffMoveButtons();
       // bIsSongPaused = false;
    }
    public void RedButtonPressed()
    {
        CurrentSelectedNote.transform.position = new Vector2(LaneLocations[1].x, CurrentSelectedNote.transform.position.y);
        TurnOffMoveButtons();
        //bIsSongPaused = false;
    }
    public void YellowButtonPressed()
    {
        CurrentSelectedNote.transform.position = new Vector2(LaneLocations[2].x, CurrentSelectedNote.transform.position.y);
        TurnOffMoveButtons();
        //bIsSongPaused = false;
    }
    public void BlueButtonPressed()
    {
        CurrentSelectedNote.transform.position = new Vector2(LaneLocations[3].x, CurrentSelectedNote.transform.position.y);
        TurnOffMoveButtons();
        //bIsSongPaused = false;
    }
    
    public void DoneButtonPushed()
    {
        finishedSong = true;
    }
    public bool GetIfFinishedSong()
    {
        return finishedSong;
    }
    public void SetFinishedSong(bool value)
    {
        finishedSong = value;
    }

    public void GitRidOfSongCreatorUI()
    {
        PauseButtonGO.SetActive(false);
        ForwardButtonGO.SetActive(false);
        RewindButtonGO.SetActive(false);
        GreenLocationButton.SetActive(false);
        BlueLocationButton.SetActive(false);
        YellowLocationButton.SetActive(false);
        RedLocationButton.SetActive(false);
        TestSongButton.SetActive(false);
        ReEnterSongCreatorButton.SetActive(true);
        _Score = 0;
        _Multiplier = 1;
        _Combo = 1;
        ScoreText.text = ("Score: " + _Score);
        MultiplierText.text = ("Multiplier: " + _Multiplier);
        ComboText.text = ("Combo: " + _Combo);

    }
    public void BringBackSongCreatorUI()
    {
        PauseButtonGO.SetActive(true);
        ForwardButtonGO.SetActive(true);
        RewindButtonGO.SetActive(true);
        TestSongButton.SetActive(true);
        ReEnterSongCreatorButton.SetActive(false);
    }

    public GameObject GetPauseMenu()
    {
        return PauseMenu;
    }
}
