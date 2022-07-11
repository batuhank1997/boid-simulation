using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    public Collider AgentCollider { get; set; }
    public Flock AgentFlock { get; set; }
    
    private void Start()
    {
        AgentCollider = GetComponent<Collider>();
    }

    public void SetFlock(Flock flock)
    {
        AgentFlock = flock;
    }
    
    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
        /*print(velocity);
        transform.Translate(velocity * Time.deltaTime);*/
    }
}
