using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;
    
    public string teamName;

    void Start()
    {
        this._animator = GetComponent<Animator>();
        // this._animator.suspended = true;
        // this._animator.speed = 0.0f;
    }

    public void setupTeam(string teamName)
    {
        Debug.Log("Player.setupTeam: " + teamName);
        this.teamName = teamName;
    }

    public void UpdateSpeed(float speed)
    {
        Debug.Log("Player.UpdateSpeed: " + speed);
        // this._animator.suspended = false;
        // this._animator.speed = Mathf.Clamp(speed, 0.0f, 25.0f);
        // this._animator.Play(animationName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
