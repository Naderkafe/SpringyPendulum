using Godot;
using System;

public partial class SimBeginScene : Node3D
{

	MeshInstance3D anchor;
	MeshInstance3D ball;
	double xA, yA, zA; // coords of anchor
	float length; // length of pendulum
	double angle; // pendulum angle
	double angleInit; // initial pendulum angle
	double time;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello MEE 381 in Godot!");
		xA = 0.0; yA = 1.2; zA = 0.0;
		anchor = GetNode<MeshInstance3D>("Anchor");
		ball = GetNode<MeshInstance3D>("Ball1");
		anchor.Position = new Vector3((float)xA, (float)yA, (float)zA);
		
		length = 0.9f;
		angleInit = Mathf.DegToRad (60.0);
		angle = angleInit;
		PlacePendulum((float)angle);

		time = 0.0;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		angle = Math.Sin(3.0 * time);
		PlacePendulum((float)angle);
		time += delta;
	}

	private void PlacePendulum(float angleF)
	{
		float xB = (float)xA + length*Mathf.Sin(angleF);
		float yB = (float)yA - length*Mathf.Cos(angleF);
		float zB = 0.0f;

		ball.Position = new Vector3(xB, yB, zB);
	}
}
