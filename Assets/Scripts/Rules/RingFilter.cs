using System.Collections;
using System.Collections.Generic;
using TOH.Core;
using TOH.Game;
using UnityEngine;

// Component that implements simple filtering logic
// on rings being passed down on  pins. 		
// By: Lee							                
public class RingFilter : MonoBehaviour 
{
	// Amount of force that will be applied to rings not allowed on a pin
	[SerializeField]
	private float pushForce; 

	// Direction of the force (x-axis) that will be applied
	[SerializeField]
	[Range(-1, 1)]
	private float xDirection;

	[SerializeField]
	private LayerMask checkLayer;

	[SerializeField]
	private Transform raycastOrigin;

	[SerializeField]
	private AudioClip sfxClip;

	// Cache the push direction for efficient use in succeeding collisions
	private Vector2 pushDirection;

	// In case the ring does not get off the pin during the first push
	// this will be the interval between force pushes as long as the ring
	// "stays" within the ring filter's collider area.
	private const float PushInterval = 0.25f;

	// Countdown timer to push interval
	private float pushTrigger = 0.0f;

	void Awake()
	{
		pushDirection = new Vector2(xDirection, 1);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (!Allow(other))
		{
			pushTrigger = 0;
			DisableDragDropOnRing(other.gameObject.GetInstanceID());
			DisplayRingDropRule();
			//TriggerSFX();
			other.attachedRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
		}
	}	

	void OnTriggerStay2D(Collider2D other)
	{
		pushTrigger += Time.deltaTime;
		if (pushTrigger > PushInterval)
		{
			pushTrigger = 0;
			if (!Allow(other))
			{
				DisplayRingDropRule();
				DisableDragDropOnRing(other.gameObject.GetInstanceID());
				//TriggerSFX();
				other.attachedRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
			}
		}
	}

	#region Helpers
	private void DisplayRingDropRule()
	{
		// Display message about ring sizes and order
		Parameters p = new Parameters();
		p.PutExtra("message", "Whoops! You can only drop a ring onto a larger one!");
		EventBroadcaster.PostEvent(EventNames.DisplayMessage, p);
	}

	// private void TriggerSFX()
	// {
	// 	// Play the sfx for not allowing larger rings to drop
	// 	GameSystems.GetService<IAudioHandler>().PlayOneShot(sfxClip);
	// }

	private void DisableDragDropOnRing(int instanceID)
	{
		Ring ring = GameSystems.GetService<RingRegistry>().Get(instanceID);
		if (ring != null)
		{
			ring.DisableDragDropControls();
		}
	}

	private bool Allow(Collider2D incomingCollider)
	{
		//Debug.Log("<color=yellow>RingFilter | Check if " + incomingCollider.gameObject.name + " is allowed on a pin</color>");
		if (incomingCollider == null)
		{
			// We only want to run checks for valid colliders
			return false;
		}

		// Determine if the ring falling down is of a smaller size compared to the topmost
		// ring placed on this pin.
		RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, Vector2.down, Mathf.Infinity, checkLayer);
        if (hit.collider != null) 
		{
			//Debug.Log("<color=yellow>RingFilter | Hit a collider " + hit.collider.gameObject.name + "</color>");
            Ring incomingRing = GameSystems.GetService<RingRegistry>().Get(incomingCollider.gameObject.GetInstanceID());
			Ring hitRing = GameSystems.GetService<RingRegistry>().Get(hit.collider.gameObject.GetInstanceID());

			if (incomingRing == null || hitRing == null) return false;
			if (incomingRing.Size == hitRing.Size) return true;
			//Debug.Log("<color=yellow>RingFilter | Hit a ring with size: " + hitRing.Size.ToString() + " (compared to incoming collider with size: " + incomingRing.Size.ToString() + ")</color>");
			return incomingRing.Size < hitRing.Size;
        }

		// No rings hit, pin is empty, so we allow 
		// the incoming ring to fall down this pin
		return true;
	}
	#endregion // Helpers
}
