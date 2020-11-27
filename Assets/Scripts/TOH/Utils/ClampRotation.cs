using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOH.Utilities
{
	// Simple utility for clamping 2D object rotation
	// By: Lee
	public class ClampRotation : MonoBehaviour 
	{
		// Custom data object for specifying min and max limits
		// for activating rotation clamp
		[System.Serializable]
		public class InverseEulerAngleClamp
		{
			public float min;
			public float max;
		}

		[SerializeField]
		private InverseEulerAngleClamp inverseClamp;

		// Speed factor used in rotating 
		[SerializeField]
		private float speed = 1;

		[SerializeField]
		private Transform cachedTransform;

		[SerializeField]
		private Rigidbody2D rBody2D;

		private bool isClamping;
		private Quaternion targetRotation;

		// Use this for initialization
		void Awake () 
		{
			if (cachedTransform == null)
				cachedTransform = transform;

			if(rBody2D == null)
				rBody2D = GetComponent<Rigidbody2D>();

			// Default quaternion of object which we'd like 
			// the clamping animation to "rotate towards to"
			targetRotation = cachedTransform.rotation;
		}
		
		// Update is called once per frame
		void Update () 
		{
			if (!isClamping && ShouldClamp())
			{
				isClamping = true;
				rBody2D.freezeRotation = true;
			}
			else if (isClamping)
			{
				// We use Quaternion functionality to rotate towards a target angle, as opposed to 
				// simply altering the Euler angles. Quaternion rotation avoids issues that can exhibit
				// when using the latter. 
				// https://answers.unity.com/questions/717637/how-do-you-smoothly-transitionlerp-into-a-new-rota.html
				cachedTransform.rotation = Quaternion.RotateTowards(cachedTransform.rotation, targetRotation, speed * Time.deltaTime);
				if(ShouldStopClamp())
				{
					isClamping = false;
					rBody2D.freezeRotation = false;
				}
			}
		}

		#region Helpers
		// Returns true if the 2D object's euler angle along the z-axis is 
		// within the specified inverse min and max clamp angles
		private bool ShouldClamp()
		{
			float eulerZ = cachedTransform.eulerAngles.z;
			return eulerZ > inverseClamp.min && eulerZ < inverseClamp.max;
		}

		// Returns true if the 2D object's euler angle along the z-axis is 
		// outside that of the specifieid inverse min and max clamp angles
		private float clampDapening = 20.0f;
		private bool ShouldStopClamp()
		{
			float eulerZ = cachedTransform.eulerAngles.z;
			return eulerZ < (inverseClamp.min - clampDapening) || eulerZ > (inverseClamp.max + clampDapening);
		}
		#endregion // Helpers
	}
}
