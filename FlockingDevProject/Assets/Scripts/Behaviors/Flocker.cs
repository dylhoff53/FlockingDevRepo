using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker : Kinematic
{
    public GameObject myCohereTarget;
    BlendedSteering mySteering;
    Kinematic[] kBirds;

    void Start()
    {
        // Separate from other birds
        Separation separate = new Separation();
        separate.character = this;
        GameObject[] goBirds = GameObject.FindGameObjectsWithTag("bird");
        kBirds = new Kinematic[goBirds.Length - 1];
        int j = 0;
        for (int i = 0; i < goBirds.Length - 1; i++)
        {
            if (goBirds[i] == this)
            {
                continue;
            }
            kBirds[j++] = goBirds[i].GetComponent<Kinematic>();
        }
        separate.targets = kBirds;

        // Cohere to center of mass - ez mode
        Arrive cohere = new Arrive();
        cohere.character = this;
        cohere.target = myCohereTarget;

        // look where center of mass is going - ez mode
        LookWhereGoing myRotateType = new LookWhereGoing();
        myRotateType.character = this;

        mySteering = new BlendedSteering();
        mySteering.behaviors = new BehaviorAndWeight[3];
        mySteering.behaviors[0] = new BehaviorAndWeight();
        mySteering.behaviors[0].behavior = separate;
        mySteering.behaviors[0].weight = 5f; //3
        mySteering.behaviors[1] = new BehaviorAndWeight();
        mySteering.behaviors[1].behavior = cohere;
        mySteering.behaviors[1].weight = 1f; //.5
        mySteering.behaviors[2] = new BehaviorAndWeight();
        mySteering.behaviors[2].behavior = myRotateType;
        mySteering.behaviors[2].weight = 1f;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate = mySteering.getSteering();
        steeringUpdate.linear.y = 0;
        base.Update();
    }
}

