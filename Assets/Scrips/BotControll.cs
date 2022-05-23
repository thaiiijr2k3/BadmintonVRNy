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
    public Transform _aimTarget;
    float force = 4f;

    void Start()
    {
        _movingSpeed = 3f;
        _bot = GameObject.Find("Bot");
        _ball = GameObject.Find("Ball");
        _ballPos = _ball.transform.position;
        _botPos = _bot.transform.position;
        _ballHandler = GameObject.Find("Ball").GetComponent<BallHandler>();
        _animator = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ball"))
        {

            Debug.Log("Hit");
            _animator.Play("Hit The Ball");
            Vector3 dir = _aimTarget.position - transform.position;
            _ball.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 7, 0);
            //_ballHandler._ballToBotSide = false;
            _ballHandler._botMovingToBall = false;
            

        }
    }


    void Update()
    {
       

        if (_ballHandler._botMovingToBall == true)
        {
            _ballPos = new Vector3(_ball.transform.position.x, 0, _ball.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, _ballPos, _movingSpeed * Time.deltaTime);
            _animator.SetBool("_movingRight", true);
            //_animator.SetBool("_movingForward", false);
        }

        else
        {      
            transform.position = Vector3.MoveTowards(transform.position, _botPos, _movingSpeed * Time.deltaTime);
        }

        if (transform.position == _botPos)
            _animator.SetBool("_movingRight", false);
        
    }

}
