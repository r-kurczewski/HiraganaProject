using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Hiragana.World
{
	[SelectionBase]
	public class Player : MonoBehaviour
	{
		public Rigidbody2D myRigidbody;
		public Animator animator;
		public float speed;
		public bool IsMoving { get; private set; }

		void Start()
		{

		}

		private void Update()
		{

		}

		void FixedUpdate()
		{
				Vector2 move = new Vector2
				{
					x = Input.GetAxisRaw("Horizontal"),
					y = Input.GetAxisRaw("Vertical")
				};
				move = move.normalized;
				
				if (move != Vector2.zero)
				{
					animator.SetBool("isMoving", true);
					animator.SetFloat("moveX", move.x);
					animator.SetFloat("moveY", move.y);
					myRigidbody.MovePosition(myRigidbody.position + (move * Time.deltaTime * speed));
					IsMoving = true;
				}
				else
				{
					animator.SetBool("isMoving", false);
					IsMoving = false;
				}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			var a = collision.gameObject?.name;
			//Debug.Log(a);
		}
	}

}
