using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    public int _playerScore;
    public int _botScore;
    public bool _playing;
    public GameObject _playerScoreText;
    public GameObject _botScoreText;
    public TextMesh _playerScoreMeshComponent;
    public TextMesh _botScoreMeshComponent;
    public bool _botMovingToBall;
    public bool _ballToBotSide;
    public float _time = 0;
    public bool _startTimeCounter = false;

    public GameObject _ball;
    public Vector3 _ball_spawn_pos;
    public Rigidbody rb;

    void Start()
    {
        _botMovingToBall = false;
        _ballToBotSide = true;
        _playerScore = 0;
        _botScore = 0;
        _playing = false;
        _playerScoreText = GameObject.Find("PlayerScore");
        _playerScoreMeshComponent = _playerScoreText.GetComponent<TextMesh>();
        _botScoreText = GameObject.Find("BotScore");
        _botScoreMeshComponent = _botScoreText.GetComponent<TextMesh>();

        _ball = GameObject.Find("Ball");
        _ball_spawn_pos = _ball.transform.position;
        rb = GetComponent<Rigidbody>();

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.name == "ExampleAvatar")   //The game starts if player take up the ball
            _playing = true;


        else if (other.name == "Wall")  //A wall is over the net. If the ball passes this wall, bot will start follow to the ball.
        {
            if(_ballToBotSide == true)
                _botMovingToBall = true;
        }
            


    }

    private void newRound()
    {
        //Spawn ball         
        rb.isKinematic = false;  // not useing Kinematic so the ball can not rotate or move.
        _ball.transform.position = _ball_spawn_pos;
        _ball.transform.rotation = Quaternion.identity;
        rb.isKinematic = true;

        
        _botMovingToBall = false;
    }

    private void SpawnBallDelay()
    {
        if(_startTimeCounter == true)
        {
            _time += Time.deltaTime;
        }

        if(_time >= 2f) // Delay 2 secs before the ball spawn.
        {
            newRound();
            _startTimeCounter = false;
            _time = 0;
        }
    }

    private void ResetAfterScored()
    {
        _playing = false;
        _ballToBotSide = true;
        _startTimeCounter = true;
        _time = 0;
    }

    private void PlayerHitTheBall(Collision other)
    {
        if(other.gameObject.name == "PlayersScoredArea")
        {
            _playerScore += 1;
            ResetAfterScored();
        }

        else if (other.gameObject.name != "PlayersScoredArea" && other.gameObject.name != "StickyTable" && other.gameObject.name != "Racket")
        {
            _botScore += 1;
            ResetAfterScored();
        }
    }

    private void BotHitTheBall(Collision other)
    {
        if(other.gameObject.name =="BotScoredArea")
        {
            _botScore += 1;
            ResetAfterScored();
            Debug.Log(other.gameObject.name);
        }


        else if (other.gameObject.name != "BotScoredArea")
        {
            _playerScore += 1;
            Debug.Log(other.gameObject.name);
            ResetAfterScored();
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (_playing == true)
        {
            if (other.gameObject.name == "Racket")
            {
                _ballToBotSide = true;
            }

            if (_ballToBotSide == true)
            {
                PlayerHitTheBall(other);
            }
            else
            {
                BotHitTheBall(other);
            }

        }
  
       
    }



    public void UpdateScore()
    {
        if(_playerScore < 10)
            _playerScoreMeshComponent.text ="0" + _playerScore.ToString();
        else
            _playerScoreMeshComponent.text = _playerScore.ToString();

        if(_botScore < 10)
            _botScoreMeshComponent.text = "0" + _botScore.ToString();
        else
            _botScoreMeshComponent.text = _botScore.ToString();

    }

    void Update()
    {
        UpdateScore(); 
        SpawnBallDelay();


    }
}
