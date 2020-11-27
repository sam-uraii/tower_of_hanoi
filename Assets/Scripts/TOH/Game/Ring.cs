using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOH.Core;

namespace TOH.Game
{
	public class Ring : MonoBehaviour 
	{
		[SerializeField]
		private int size;

		[SerializeField]
		private AudioClip sfxOnInputUp;

		[SerializeField]
		private AudioClip sfxOnInputDown;

		[SerializeField]
		private DragDropSpring dragDrop;

		public int Size { get { return size; } }

		void Start()
		{
			// We register this Ring object on Start instead of Awake to
			// ensure that the RingRegistry util has been properly instantiated.
			GameSystems.GetService<RingRegistry>().Register(this);
		}

		#region Ring functions
		public void DisableDragDropControls()
		{
			dragDrop.Disable();
		}
		#endregion // Ring functions

		#region Event handlers
		// public void HandleOnInputDown()
		// {
		// 	PlaySFX(sfxOnInputDown);
		// }

		// public void HandleOnInputUp()
		// {
		// 	PlaySFX(sfxOnInputUp);
		// }
		#endregion // Event handlers

		#region Helpers
		// private void PlaySFX(AudioClip clip)
		// {
		// 	GameSystems.GetService<IAudioHandler>().PlayOneShot(clip);
		// }
		#endregion // Helpers
	}
}
