using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI MultiplierText;
    [SerializeField] TextMeshProUGUI ComboText;
    [SerializeField] ParticleSystem HitParticle;
    [SerializeField] ParticleSystem MissParticle;
    [SerializeField] GameObject HitSprite;
    [SerializeField] GameObject MissSprite;

 

    int _Score = 0;
    int _Multiplier = 1;
    int _Combo = 0;

    IEnumerator HitSpriteTimer()
    {
        yield return new WaitForSeconds(0.2f);
        HitSprite.SetActive(false);
    }
    IEnumerator MissSpriteTimer()
    {
        HitSprite.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        MissSprite.SetActive(false);
    }

    private void Awake()
    {
        ScoreText.text = ("Score: " + _Score);
        MultiplierText.text = ("Multiplier: " + _Multiplier);
        ComboText.text = ("Combo: " + _Combo);
    }

    public void HitNote()
    {
       
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
}
