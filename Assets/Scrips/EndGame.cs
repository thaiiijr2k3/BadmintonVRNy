using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    //Score 10 first to win
    AudioSource _audioS;
    BallHandler _ballHandler;

    void Start()
    {
        _audioS = gameObject.GetComponent<AudioSource>();
        _ballHandler = GameObject.Find("Ball").GetComponent<BallHandler>();
    }

    private string CheckIfScore10()
    {
        if(_ballHandler._botScore == 1)
        {
            return "Bot"; // Bot win
        }
        else if(_ballHandler._playerScore == 10)
        {
            return "Player"; // Player win
        }

        return "";
    }

    
    void Update()
    {
        if (CheckIfScore10() != "")
        {
            _audioS.Play();
        }
    }
}
