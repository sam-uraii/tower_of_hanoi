using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TOH.Core;
 using UnityEngine.SceneManagement;

public class UIToggleBtn : MonoBehaviour 
{
	[SerializeField]
	private bool toggleDisabled;

	[SerializeField]
	private string eventName;

	[SerializeField]
	private Button button;

	[System.Serializable]
	public class OnButtonToggleEvent : UnityEvent<bool> {}
	public OnButtonToggleEvent onButtonToggleEvent;

	private ColorBlock defaultNormalColor;
	private ColorBlock disabledNormalColor;

	public bool IsOn { get; private set; }

	void Awake()
	{
		defaultNormalColor = button.colors;
		disabledNormalColor = button.colors;
		disabledNormalColor.normalColor = button.colors.disabledColor;
		disabledNormalColor.highlightedColor = button.colors.disabledColor;
		IsOn = true;
	}

	#region Event handlers
	public void HandleOnButtonPressed()
	{
		SceneManager.LoadScene("main");
	}

	#endregion // Event handlers
}
