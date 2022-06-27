﻿using System.Collections;
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
        playerController.OnCheckpointHit += OnCheckpointHit;

        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    public void OnAgentItemGet()
    {
        // BEST FOR PPO: 10f
        AddReward(5f);
    }

    public void OnAgentDeath()
    {
        //playerController.OnItemGetEvent -= OnAgentItemGet;
        // BEST FOR PPO: -5f
        AddReward(-5f);
        EndEpisode();
    }

    public void OnCheckpointHit()
    {
        //AddReward(1f);
    }

    public override void OnEpisodeBegin()
    {
        //playerController.Reset();
        gameController.Restart();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(playerController.currentVelocityX);
        //sensor.AddObservation(playerController.rb.position.x);
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

        AddReward(-0.01f);

        //AddReward(-0.1f);
    }

    public void OnItemMissed()
    {
        //SetReward(-1.5f);
    }

}
