﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Grenade : MonoBehaviour {

	[SerializeField]
	AudioClip impact;
	[SerializeField]
	float radius;
	GameObject explosion;
	[SerializeField]
	public float life;
	[SerializeField]
	public float force;
	private AudioSource audio;
	private int counter;
	// Use this for initialization
	void Start () {
		audio = this.GetComponent<AudioSource> ();
		counter = 0;
		explosion = Resources.Load ("Explosion") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		counter++;
		if (counter > life) {
			Collider[] colliders = Physics.OverlapSphere (this.transform.position,radius);
			foreach (Collider hit in colliders) {
				Rigidbody rb = hit.GetComponent<Rigidbody> ();
				if (rb != null) {
					rb.AddExplosionForce (force, this.transform.position, radius);
					if (hit.tag == "Player") {
						Debug.Log ("player");
						hit.GetComponent<CharacterController> ().SimpleMove (Vector3.forward * 100);
						//hit.GetComponent<CharacterController>().SimpleMove(Vector3.MoveTowards(this.transform.position, hit.transform.position, force));
					}
				}
			}
			GameObject newExplosion = Instantiate (explosion);
			newExplosion.transform.position = this.transform.position;
			Object.Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter (Collision col){
		audio.PlayOneShot (impact);
	}
}
