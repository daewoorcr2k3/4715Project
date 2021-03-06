﻿using UnityEngine;
using System.Collections;

public class FlyingControls : MonoBehaviour {

	private GameObject OVRCamera;
	private GameObject FPSCamera;
	private GameManager gameManager;
	private bool oculusEnabled;
	private bool allowJetpack;

	public float moveSpeed = 1.0f;
	public float maxSpeed = 15.0f;
	public float minSpeed = 7.0f;


	private float currentSpeed;

	// Use this for initialization
	void Start () {

		//Find and store the camera objects and manager
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		OVRCamera = gameManager.getOVRCamera();
		FPSCamera = gameManager.getFPSCamera();

		oculusEnabled = gameManager.isOculusEnabled();

		rigidbody.AddForce(new Vector3(0, 0, 1) * minSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		//Detect the player's current speed
		currentSpeed = rigidbody.velocity.magnitude;

		//Before any Input is processed, check to see if Oculus is enabled
		oculusEnabled = gameManager.isOculusEnabled();

		//Input grabbing - moves relative to current camera
		if(Input.GetKey(KeyCode.W) && !checkMaxSpeed()){
			rigidbody.AddForce(new Vector3(0, 0, 1.0f) * moveSpeed);
		}
		if(Input.GetKey(KeyCode.S) && !checkMinSpeed()){
			rigidbody.AddForce(new Vector3(0, 0, -1.0f) * moveSpeed);
		}
		if(Input.GetKey(KeyCode.A)){
			rigidbody.AddForce(new Vector3(-1.0f, 0, 0) * moveSpeed);
		}
		if(Input.GetKey(KeyCode.D)){
			rigidbody.AddForce(new Vector3(1.0f, 0, 0) * moveSpeed);
		}
		if(Input.GetKey(KeyCode.E)){
			rigidbody.AddForce(new Vector3(0, 1.0f, 0) * moveSpeed);
		}
		if(Input.GetKey(KeyCode.Q)){
			rigidbody.AddForce(new Vector3(0, -1.0f, 0) * moveSpeed);
		}
	}
	void FixedUpdate() {
		Vector3 tempVelocity = rigidbody.velocity;
		//If we exceed the maximum speed, determine velocity direction and lock it
		if(checkMaxSpeed()){
			Debug.Log("Speed too high: " + tempVelocity.z);
			rigidbody.AddForce(new Vector3(0, 0, 1.0f) * maxSpeed);
		}  
		if(checkMinSpeed()){
			Debug.Log("Speed not sufficient: " + tempVelocity.z);
			rigidbody.AddForce(new Vector3(0, 0, 1.0f) * minSpeed);
		}
	}
	private bool checkMaxSpeed(){
		Vector3 tempVelocity = rigidbody.velocity;
		if(tempVelocity.z > maxSpeed){
			return true;
		}
		else{
			return false;
		}
	}
	private bool checkMinSpeed(){
		Vector3 tempVelocity = rigidbody.velocity;
		if(tempVelocity.z < minSpeed){
			return true;
		}
		else{
			return false;
		}
	}
	public void resetVelocity(){
		rigidbody.velocity = new Vector3(0,0,0);
	}
	public float getCurrentSpeed(){
		return currentSpeed;
	}
}
