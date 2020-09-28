using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace Hiragana.World
{
	[SelectionBase]
	public class WorldPlayer : MonoBehaviour
	{
		public static WorldPlayer player;
		public static int locationId;

		public Rigidbody2D myRigidbody;
		public Animator animator;
		public float speed;
		public bool IsMoving { get; private set; }
		bool blocked;

		void Awake()
		{
			if (player)
			{
				Destroy(gameObject);
			}
			else
			{
				player = this;
				DontDestroyOnLoad(gameObject);
			}
		}

		void FixedUpdate()
		{
			if (!blocked)
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
		}

		public void Deactivate()
		{
			FindObjectOfType<AudioListener>().enabled = false;
			animator.SetBool("isMoving", false);
			blocked = true;
		}

		public void Activate()
		{
			FindObjectOfType<AudioListener>().enabled = true;
			blocked = false;
		}
	}

}
