using System;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public Hp PlrHpScript;
    public float MoveSpeed;
    public bool CanMove;
    public Animator anim;

    private Rigidbody body;
    private float deadZone = 0f;
    private float WaitTime;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.Find("Body").gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody>();
        UpdatePosition();
        PlrHpScript = GetComponent<Hp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WaitTime < Time.time)
        {
            if (Math.Abs(Input.GetAxisRaw("Vertical")) > deadZone || Math.Abs(Input.GetAxisRaw("Horizontal")) > deadZone)
            {
                if (CanMove)
                {
                    Vector3 move = new Vector3(-Input.GetAxisRaw("Vertical"),0 ,Input.GetAxisRaw("Horizontal"));
                    move = move.normalized * MoveSpeed;
                    body.velocity = move;
                    UpdatePosition();
                }
            }
            else if( body.velocity != new Vector3(0,0,0))
            {
               body.velocity = new Vector3(0, 0, 0);
            }
        }
    }
    public void UpdatePosition()
    {
        PlayMoveAnimation();
    }
    public void WaitForTime(float t)
    {
        WaitTime = Time.time + t;
    }
    public void PlayMoveAnimation()
    {
        anim.SetTrigger("Move");
    }
}
