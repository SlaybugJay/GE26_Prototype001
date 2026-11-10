using UnityEngine;
using System.Collections;
using UnityEditor.Build.Reporting;
using TMPro;

namespace AstronautPlayer
{

	public class AstronautPlayer : MonoBehaviour 
	{

		private Animator anim;
		private CharacterController controller;

		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;
        private int count;

        void Start () 
		{
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
			count = 0;
			SetCountText();
			winTextObject.SetActive(false);
		}

		void Update (){
			if (Input.GetKey ("w")) {
				anim.SetInteger ("AnimationPar", 1);
			}  else {
				anim.SetInteger ("AnimationPar", 0);
			}

			if(controller.isGrounded){
				moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
			}

			float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
			controller.Move(moveDirection * Time.deltaTime);
			moveDirection.y -= gravity * Time.deltaTime;
		}
		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("PickUp"))
			{
				other.gameObject.SetActive(false);
				count = count + 1;
				SetCountText();
			}
		}
		public TextMeshProUGUI countText;

		void SetCountText()
		{
			countText.text = "Count: " + count.ToString();
			if (count >= 5)
			{
				winTextObject.SetActive(true);
			}
		}
		public GameObject winTextObject;
	}
}
