using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PlayerAgent : Agent
{
    private Rigidbody rb;
    private PlayerController playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnAgentItemGet()
    {
        SetReward(1.0f);
    }

    public void OnAgentDeath()
    {
        playerController.OnItemGetEvent -= OnAgentItemGet;
        SetReward(-10f);
        EndEpisode();
    }

    public override void OnEpisodeBegin()
    {
        playerController = GetComponent<PlayerController>();
        playerController.OnItemGetEvent += OnAgentItemGet;
        playerController.OnDeath += OnAgentDeath;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(rb.transform.position.x);        
        sensor.AddObservation(rb.transform.position.y);

        sensor.AddObservation(playerController.currentSineX);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] == 0)
        {
            playerController.isBoosted = false;
        }
        else if (actions.DiscreteActions[0] == 1)
        {
            playerController.isBoosted = true;
        }

        SetReward(-0.1f);
    }

}
