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
        AddReward(5f);

        gameController.currentDistance += 15;
        gameController.UpdateDistanceText();
    }

    public void OnAgentDeath()
    {
        AddReward(-5f);

        gameController.currentDistance += -3f;
        gameController.UpdateDistanceText();

        EndEpisode();
    }

    public override void OnEpisodeBegin()
    {
        gameController.Restart();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float speedX = playerController.currentVelocityX;
        sensor.AddObservation(speedX < 0 ? 0 : 1);
        sensor.AddObservation(Mathf.Abs(playerController.currentVelocityX));
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
    }

}
