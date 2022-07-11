using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public FlockBehaviour behaviour;
    
    private List<FlockAgent> agents = new List<FlockAgent>();

    [Range(1, 500)] public int startingCount;
    [Range(1f, 100f)] public float driveFactor;
    [Range(1f, 100f)] public float maxSpeed;
    [Range(1f, 10f)] public float neigbourRadius;
    [Range(0f, 1f)] public float avoidanceRadiusMultiplier;
    
    const float agentDensity = 0.08f;

    private float squareMaxSpeed;
    private float squareNeigbourRadius;
    public float SquareAvoidanceRadius { get; set; }

    

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeigbourRadius = neigbourRadius * neigbourRadius;
        SquareAvoidanceRadius = squareNeigbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(agentPrefab, Random.insideUnitSphere * startingCount * agentDensity,
                Quaternion.Euler(Vector3.up * Random.Range(0, 360f)));
            newAgent.name = "Agent " + i;
            newAgent.SetFlock(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            
            Vector3 move = behaviour.CalculateMove(agent, context, this);

            move *= driveFactor;
            
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();

        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neigbourRadius);

        foreach (Collider col in contextColliders)
        {
            if (col != agent.AgentCollider)
            {
                context.Add(col.transform);
            }
        }

        return context;
    }
}
