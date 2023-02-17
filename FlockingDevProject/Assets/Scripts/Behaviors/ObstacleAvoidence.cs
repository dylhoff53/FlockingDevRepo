using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : Seek
{
    // detector: CollisionDetector
    // the minimum distance to hit a wall (i.e. how far to avoid 
    // collision) should be greater than the radius of the character.
    public float avoidDistance = 3f;

    // The distance to look ahead for a collision
    // (i.e. the length of the collision ray)
    public float lookahead = 5f;

    protected override Vector3 getTargetPosition()
    {
        // Cast a ray and see if there's anything to avoid
        RaycastHit hit;
        //if (Physics.Raycast(character.transform.position, character.transform.TransformDirection(Vector3.forward), out hit, lookahead))
        if (Physics.Raycast(character.transform.position, character.linearVelocity, out hit, lookahead) || Physics.Raycast(character.transform.position, new Vector3(character.linearVelocity.x/3, character.linearVelocity.y, character.linearVelocity.z), out hit, lookahead/2) || Physics.Raycast(character.transform.position, new Vector3(character.linearVelocity.x, character.linearVelocity.y, character.linearVelocity.z/3), out hit, lookahead / 2))
        {
            Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * hit.distance, Color.red, 0.5f);
            Debug.Log("Hit " + hit.collider);
            return hit.point + (hit.normal * avoidDistance);
        }
        else
        {
            Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * lookahead, Color.green, 0.5f);
            Debug.Log("safe");
            // nothing to avoid
            return base.getTargetPosition();
            //return Vector3.positiveInfinity; // hack because I can't return null
        }
    }

}
