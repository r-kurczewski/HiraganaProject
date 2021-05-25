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
		public static WorldPlayer instance;

		public Rigidbody2D myRigidbody;
		public Animator animator;
		public float speed;

		public bool IsMoving { get; private set; }

		protected override string ObjectID => "playerPos";

		public bool blockMove;

		private void Start()
		{
			instance = this;
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
			else if (Input.GetKeyDown(KeyCode.Z))
			{
				SaveManager.GameSave(1);
			}
			else if (Input.GetKeyDown(KeyCode.X))
			{
				SaveManager.GameSave(2);
			}
			else if (Input.GetKeyDown(KeyCode.C))
			{
				SaveManager.GameLoad(1);
			}
			else if (Input.GetKeyDown(KeyCode.V))
			{
				SaveManager.GameLoad(2);
			}

#endif
		}

		void FixedUpdate()
		{
			if (blockMove)
			{
				IsMoving = false;
				animator.SetBool("isMoving", false);
				return;
			}
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
			var data = new SaveWrapper()
			{
				position = transform.localPosition,
				moveX = animator.GetFloat("moveX"),
				moveY = animator.GetFloat("moveY"),
			};
			SaveGame.Save(SavePath, data);
		
		}

		public void SaveStartPosition(Vector2 pos, float moveX, float moveY)
		{
			var data = new SaveWrapper()
			{
				position = pos,
				moveX = moveX,
				moveY = moveY,
			};
			SaveGame.Save(SavePath, data);
		}

		public override void Load()
		{
			if (SaveGame.Exists(SavePath))
			{
				var data = SaveGame.Load<SaveWrapper>(SavePath);
				transform.localPosition = data.position;
				animator.SetFloat("moveX", data.moveX);
				animator.SetFloat("moveY", data.moveY);
			}
		}

		[Serializable]
		public class SaveWrapper
		{
			public Vector2 position;
			public float moveX;
			public float moveY;
		}
	}
}
