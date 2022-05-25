using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotControll : MonoBehaviour
{
    GameObject _bot;
    GameObject _ball;
    public float _movingSpeed;
    Vector3 _ballPos;
    Vector3 _botPos;
    BallHandler _ballHandler;
    Animator _animator;
    float force = 4f;
    public GameObject[] _aimTargets;

    void Start()
    {
        _movingSpeed = 4f;
        _bot = GameObject.Find("Bot");
        _ball = GameObject.Find("Ball");
        _ballPos = _ball.transform.position;
        _botPos = _bot.transform.position;
        _ballHandler = GameObject.Find("Ball").GetComponent<BallHandler>();
        _animator = GetComponent<Animator>();
        _aimTargets = GameObject.FindGameObjectsWithTag("AimTarget");
        
    }

    private Vector3 PickTarget()
    {
        int _rand = Random.Range(0, _aimTargets.Length);
        return _aimTargets[_rand].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ball"))
        {

            Debug.Log("Hit");
            _animator.Play("Hit The Ball");
            _ballHandler._ballToBotSide = false;
            _ballHandler._botMovingToBall = false;
            Vector3 dir = PickTarget() - transform.position;
            _ball.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 7, 0);
            
            

        }
    }

    private string CheckIfScore10()
    {
        if (_ballHandler._botScore == 10)
        {
            _animator.Play("Victory Idle");
        }
        else if (_ballHandler._playerScore == 10)
        {
            _animator.Play("Defeat");
        }

        return "";
    }


    void Update()
    {
       

        if (_ballHandler._botMovingToBall == true)
        {
            _ballPos = new Vector3(_ball.transform.position.x, 0, _ball.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, _ballPos, _movingSpeed * Time.deltaTime);
            _animator.SetBool("_moving", true);
          
        }

        else
        {      
            transform.position = Vector3.MoveTowards(transform.position, _botPos, _movingSpeed * Time.deltaTime);
        }

        if (transform.position == _botPos)
            _animator.SetBool("_moving", false);
        CheckIfScore10();
    }

}
