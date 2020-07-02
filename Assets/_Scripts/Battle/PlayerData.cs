using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Hiragana.Battle
{
	public class PlayerData : MonoBehaviour, ITurn
	{
		[SerializeField] private int _speed;
		[SerializeField] private uint _health;

		public uint Health { get => _health; private set => _health = value; }
		public int Speed { get => _speed; private set => _speed = value; }

		public void Execute()
		{
			Wait();
		}

		public int GetSpeed()
		{
			return Speed;
		}

		public bool IsAlive()
		{
			return Health > 0;
		}

		void Wait()
		{
			Debug.Log("Twoja tura");
			for (int i = 0; i < 1000; i++)
			{
				Debug.Log(1);
			}
			Debug.Log("Koniec tury");
		}
	}
}