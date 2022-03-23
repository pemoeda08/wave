using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PlayerAgent : Agent
{
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        Debug.Log("On Episode Begin");
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Debug.Log("Collect Obs");
        sensor.AddObservation(rb.transform.position.x);        
        sensor.AddObservation(rb.transform.position.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("On Action Received");
        Debug.Log($"OnActionReceived: {actions.ContinuousActions[0]}");
    }
}
