using Godot;

using System;

using System.IO;

public partial class SimBeginScene : Node3D
{

    MeshInstance3D anchor;

    MeshInstance3D ball;

    SpringModel spring;

    Label potentialEnergyLabel;

    Label kineticEnergyLabel;

    Label totalEnergyLabel;

    PendSim pend = new PendSim();

    double xA, yA, zA; // coords of anchor

    float length0; // natural length of pendulum

    float length; // length of pendulum

    double time;

    Vector3 endA; // end point of anchor

    Vector3 endB; // end point for pendulum bob

    // Called when the node enters the scene tree for the first time.

    public override void _Ready()
    {

        xA = 0.0; yA = 0.4; zA = 0.0;

        anchor = GetNode<MeshInstance3D>("Anchor");

        ball = GetNode<MeshInstance3D>("Ball1");

        spring = GetNode<SpringModel>("SpringModel");

        endA = new Vector3((float)xA, (float)yA, (float)zA);

        anchor.Position = endA;

        spring.GenMesh(0.05f, 0.015f, 0.9f, 6.0f, 62);

        endB.X = (float)pend.X;

        endB.Y = (float)pend.Y;

        endB.Z = (float)pend.Z;

        potentialEnergyLabel = GetNode<Label>("peLabel");

        kineticEnergyLabel = GetNode<Label>("keLabel");

        totalEnergyLabel = GetNode<Label>("ergLabel");

        PlacePendulum(endB);

        time = 0.0;

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.

    public override void _Process(double delta)
    {

        endB.X = (float)pend.X;

        endB.Y = (float)pend.Y;

        endB.Z = (float)pend.Z;

        PlacePendulum(endB);

        time += delta;

        double potentialEnergy = pend.CalculatePotentialEnergy();

        double kineticEnergy = pend.CalculateKineticEnergy();

        double totalEnergy = pend.CalculateTotalEnergy();

        // Update labels

        potentialEnergyLabel.Text = "Potential Energy: " + potentialEnergy.ToString("F2");

        kineticEnergyLabel.Text = "Kinetic Energy: " + kineticEnergy.ToString("F2");

        totalEnergyLabel.Text = "Total Energy: " + totalEnergy.ToString("F2");

    }

    public override void _PhysicsProcess(double delta)
    {

        base._PhysicsProcess(delta);

        pend.StepRK4(time, delta);

        endB.X = (float)pend.X;

        endB.Y = (float)pend.Y;

        endB.Z = (float)pend.Z;

    }

    private void PlacePendulum(Vector3 endBB)
    {

        spring.PlaceEndPoints(endA, endB);

        ball.Position = endBB;

    }

}
