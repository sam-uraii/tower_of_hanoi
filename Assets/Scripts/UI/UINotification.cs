using System.Collections;
using System.Collections.Generic;
using TOH.Core;
using UnityEngine;
using UnityEngine.UI;

public class UINotification : MonoBehaviour 
{
	[SerializeField]
	private Text text;

	private Queue<string> queuedMessages;

	void Awake()
	{
		queuedMessages = new Queue<string>();
	}

	void Start()
	{
		EventBroadcaster.AddObserver(EventNames.DisplayMessage, HandleOnDisplayNotificationEvent);
	}

	void OnDestroy()
	{
		StopAllCoroutines();
		EventBroadcaster.RemoveActionAtObserver(EventNames.DisplayMessage, HandleOnDisplayNotificationEvent);
	}

	public void Display(string message)
	{
		if (text.gameObject.activeInHierarchy)
		{
			if (queuedMessages != null && queuedMessages.Count <= 0 && text.text.Equals(message))
			{
				return;
			}
			else
			{
				queuedMessages.Enqueue(message);
			}
		}
		
		text.text = message;
		text.gameObject.SetActive(true);
		StartCoroutine("DelayHide", 3.0f);
	}

	#region Event handlers
	private void HandleOnDisplayNotificationEvent(Parameters p)
	{
		string message = p.GetStringExtra("message", string.Empty);
		if (!string.IsNullOrEmpty(message))
		{
			Display(message);
		}
	}
	#endregion //Event handlers

	#region Coroutines
	private IEnumerator DelayHide(float delay)
	{
		yield return new WaitForSeconds(delay);
		text.gameObject.SetActive(false);
		text.text = string.Empty;
	}
	#endregion // Coroutines


	#region Helpers
	private void CheckForQueuedMessages()
	{
		if (queuedMessages != null && queuedMessages.Count > 0)
		{
			string message = queuedMessages.Dequeue();
			text.text = message;
			StartCoroutine("DelayHide", 3.0f);
		}
	}
	#endregion // Helpers
}
