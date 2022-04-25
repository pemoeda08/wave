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
    private GameController gameController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerController.OnItemGetEvent += OnAgentItemGet;
        playerController.OnDeath += OnAgentDeath;

        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    public void OnAgentItemGet()
    {
        SetReward(1.0f);
    }

    public void OnAgentDeath()
    {
        //playerController.OnItemGetEvent -= OnAgentItemGet;
        AddReward(-10f);
        EndEpisode();
    }

    public override void OnEpisodeBegin()
    {
        //playerController.Reset();
        gameController.Restart();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(playerController.currentVelocityX);
        sensor.AddObservation(playerController.rb.velocity.y);
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

        AddReward(rb.velocity.y / 100);
    }

    public void OnItemMissed()
    {
        SetReward(-0.5f);
    }

}
