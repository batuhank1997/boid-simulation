using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filters/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> originalNeighbors)
    {
        List<Transform> filtered = new List<Transform>();

        foreach (Transform neighbor in originalNeighbors)
        {
            FlockAgent neighborAgent = neighbor.GetComponent<FlockAgent>();

            if (neighborAgent != null && neighborAgent.AgentFlock == agent.AgentFlock)
            {
                filtered.Add(neighbor);
            }
        }

        return filtered;
    }

}
