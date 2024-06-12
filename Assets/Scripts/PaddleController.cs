using System;
using UnityEngine;

public class PaddleController : MonoBehaviour
{   

    private float speed = 0;
    private float weight = 1.65f;
    private float resistanceCoeff = 0.955f;
    private bool paused = true;

    void Start() { }

    public void setPaused(bool paused) {
        this.paused = paused;
    }

    public void resetToHomePosition() {
        transform.position = new Vector3(0, -4.325f, 0);
    }

    void Update()
    {   
        
        if (!paused) {

            var paddleX = transform.position.x;
            var mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            var force = mouseX - paddleX;     

            speed = (speed + force / weight) * resistanceCoeff;
            var translation = new Vector3(speed * Time.deltaTime, 0, 0);
            
            if (Math.Abs(paddleX + translation.x) > 5.18) { 
                translation.x = (paddleX > 0 ? 5.18f : -5.18f) - paddleX;
            }

            transform.Translate(translation);
        }
    }
}
