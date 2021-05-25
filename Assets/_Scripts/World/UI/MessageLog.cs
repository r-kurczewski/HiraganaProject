using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hiragana.World.UI
{
	public class MessageLog : MonoBehaviour
	{
		public static MessageLog instance;

		[SerializeField] private TMP_Text message = default;
		[SerializeField] private TMP_Text source = default;
		Queue<Message> queue = new Queue<Message>();

		private void Awake()
		{
			instance = this;	
		}

		private void Start()
		{
			HideLog();
		}

		public void AddMessage(string source, string message)
		{
			var msg = new Message(source, message);
			queue.Enqueue(msg);
		}

		private IEnumerable IShowMessages()
		{

			while (queue.Count > 0)
			{
				var msg = queue.Dequeue();

				message.text = msg.message;
				source.text = msg.source;
				yield return new WaitUntil(() => Input.GetButtonUp("Confirm"));
				yield return new WaitUntil(() => Input.GetButtonDown("Confirm"));
			}
			HideLog();
		}

		public void ShowMessages()
		{
			ShowLog();
			StartCoroutine(IShowMessages().GetEnumerator());
		}

		public void ShowLog()
		{
			foreach(Transform item in transform)
			{
				item.gameObject.SetActive(true);
			}
			GetComponent<Image>().enabled = true;
		}

		public void HideLog()
		{
			foreach (Transform item in transform)
			{
				item.gameObject.SetActive(false);
			}
			GetComponent<Image>().enabled = false;
		}

		private class Message
		{
			public string source;
			public string message;

			public Message(string source, string message)
			{
				this.source = source;
				this.message = message;
			}
		}
	}
}