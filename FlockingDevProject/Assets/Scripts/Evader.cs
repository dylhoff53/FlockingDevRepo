using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evader : Kinematic
{
    Evade myMoveType;
    LookWhereGoing myRotateType;

    private bool flee = true;

    // Start is called before the first frame update
    void Start()
    {
        myRotateType = new LookWhereGoing();
        myRotateType.character = this;
        myRotateType.target = myTarget;
        myMoveType.flee = flee;

        myMoveType = new Evade();
        myMoveType.character = this;
        myMoveType.target = myTarget;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.angular = myRotateType.getSteering().angular;
        steeringUpdate.linear = myMoveType.getSteering().linear;
        base.Update();
    }
}
