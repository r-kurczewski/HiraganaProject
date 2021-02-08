using BayatGames.SaveGameFree;
using Hiragana.Other;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hiragana.World
{
	[SelectionBase]
	public class WorldPlayer : SaveObject
	{
		public static WorldPlayer player;
		public Rigidbody2D myRigidbody;
		public Animator animator;
		public float speed;
		public bool IsMoving { get; private set; }
		public bool save = true;

		private void Start()
		{
			player = this;
			animator.keepAnimatorControllerStateOnDisable = true;
			Load();
		}

		private void Update()
		{
			#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.LeftControl))
			{
				GetComponent<Collider2D>().enabled = false;
			}
			else if (Input.GetKeyUp(KeyCode.LeftControl))
			{
				GetComponent<Collider2D>().enabled = true;
			}
			#endif
		}

		public void OnDestroy()
		{
			if(save) Save();
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

		public override void Save()
		{
			SaveGame.Save<Vector2>("playerPos", transform.position);
			SaveGame.Save("currentLocation", SceneManager.GetActiveScene().buildIndex);
			SaveGame.Save("moveX", animator.GetFloat("moveX"));
			SaveGame.Save("moveY", animator.GetFloat("moveY"));
		}

		public override void Load()
		{
			if (SaveGame.Exists("playerPos")) transform.position = SaveGame.Load<Vector2>("playerPos");
			if (SaveGame.Exists("moveX")) animator.SetFloat("moveX", SaveGame.Load<float>("moveX"));
			if (SaveGame.Exists("moveY")) animator.SetFloat("moveY", SaveGame.Load<float>("moveY"));
		}

		public static void SetStartPosition(Vector2 pos, float moveX, float moveY)
		{
			SaveGame.Save("playerPos", pos);
			SaveGame.Save("moveX", moveX);
			SaveGame.Save("moveY", moveY);
		}
	}
}
