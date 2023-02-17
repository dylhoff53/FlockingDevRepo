using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sep : Kinematic
{
    Separation myMoveType;

    public Kinematic[] List;

    void Start()
    {
        myMoveType = new Separation();
        myMoveType.character = this;
        myMoveType.targets = List;

    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        base.Update();
    }
}
