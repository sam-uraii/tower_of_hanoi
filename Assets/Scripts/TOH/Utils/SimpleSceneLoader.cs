using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TOH.Utilities
{
	public class SimpleSceneLoader : MonoBehaviour 
	{
		[SerializeField]
		private string nextSceneToLoad;

		[SerializeField]
		private float delayBeforeLoad = 3;

		// Use this for initialization
		IEnumerator Start () 
		{
			yield return new WaitForSeconds(delayBeforeLoad);
			SceneManager.LoadSceneAsync(nextSceneToLoad);
		}
	}
}
