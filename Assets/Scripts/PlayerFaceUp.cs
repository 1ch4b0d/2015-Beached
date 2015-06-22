using UnityEngine;
using System.Collections;
using Acrocatic;

public class PlayerFaceUp : MonoBehaviour {

	private GameObject playerReference;
	private Player playaRef;
	private Animator playaAnim;

	public AudioClip shootSound;

	// Use this for initialization
	void Start () {
		// Setting up references.
		playerReference = GameObject.Find("PlayerComplex");
		playaRef = playerReference.GetComponent<Player>();
		playaRef = playerReference.GetComponent("Player") as Player;
		playaAnim = playaRef.GetComponent<Animator>();
	}

	
	// Update is called once per frame
	void Update () {
//		if(playaRef.grounded && !playaRef.IsShooting && shoot ||  Input.GetKey(KeyCode.LeftControl)){

		if(Input.GetKeyDown(KeyCode.W)){
//		if(Input.GetButton(KeyCode.W)){
			playaRef.facingUp = true;
			playaAnim.SetBool("facingRight", true);

//			AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
//			Instantiate(bullet, firePoint.position, firePoint.rotation);
			//StartCoroutine(faceUpForSec());
			//StopCoroutine(faceUpForSec());
			//StopShooting();
			//StopFacingUp();
		}
		else if (Input.GetKeyUp(KeyCode.W)){
			playaRef.facingUp = false;
		}
	}


//	IEnumerator faceUpForSec() {
//		yield return new WaitForSeconds(100);
//	}

//	void StopFacingUp() {
//		playaRef.facingUp = false;
//	}
}
