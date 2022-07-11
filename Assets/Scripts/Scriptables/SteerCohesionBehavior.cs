using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SteerCohesion")]
public class SteerCohesionBehavior : FilteredFlockBehavior
{
    private Vector3 currentVelocity;

    public float agentSmoothTime = 0.5f;
    
    //finds the average position between neigbors and tries to move there
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
            return Vector3.zero;
        
        
        //add all points together and average
        Vector3 cohesionMove = Vector3.zero;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach (Transform item in filteredContext)
        {
            cohesionMove += item.position;
        }

        cohesionMove /= filteredContext.Count;
        
        
        
        //for offset from agent pos
        cohesionMove -= agent.transform.position;
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);

        // cohesionMove = Vector3.Lerp(agent.transform.forward, cohesionMove, agentSmoothTime * Time.deltaTime);
        return cohesionMove;
    }
}
