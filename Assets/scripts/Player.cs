using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;
            
    private float speedMultiplier;

    private float updateSpeedTime;
    private float currentValue;
    private float minSpeed = 0.5f;

    // Set this in the editor
    public string teamColour;

    void Start()
    {
        this._animator = GetComponent<Animator>();
        this.updateSpeedTime = Time.time;  
    }

    public void UpdateSpeed(float speed)
    {
        // Do not dampen the speed increase, give the feedback to the user that they are going faster
        this.speedMultiplier = Mathf.Clamp(this.minSpeed  + (speed / 10.0f), this.minSpeed, 1.0f);

        this._animator.SetFloat("speedMultiplier", this.speedMultiplier);
        this._animator.speed = this.speedMultiplier;

        Debug.Log("Player.UpdateSpeed: " + this.speedMultiplier);       
        this.updateSpeedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Dampening when the player has no input for n second
        float duration = 1.0f;
        float timeElapsed = Time.time - this.updateSpeedTime;
        float progress = timeElapsed / duration;

        this.speedMultiplier = Mathf.Lerp(speedMultiplier, this.minSpeed, progress);
        
        this._animator.SetFloat("speedMultiplier", this.speedMultiplier);
        this._animator.speed = this.speedMultiplier;        
    }
}
