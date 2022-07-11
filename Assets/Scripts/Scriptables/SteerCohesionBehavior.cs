using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SteerCohesion")]
public class SteerCohesionBehavior : FilteredFlockBehavior
{
    private Vector3 velocity = Vector3.zero;

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

        cohesionMove /= context.Count;
        
        /*if (float.IsNaN(cohesionMove.x) || float.IsNaN(cohesionMove.y) || float.IsNaN(cohesionMove.z))
            currentVelocity = Vector3.zero;*/
        
        //for offset from agent pos
        cohesionMove -= agent.transform.position;
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref velocity, agentSmoothTime);

        

        return cohesionMove;
    }
}
