using UnityEngine;
using UnityEngine.InputSystem;

public class AttachableHolder : MonoBehaviour
{
	ActionMap inputs;
	IAttachable attachable;
	bool toRemove;
	public LayerMask IgnoreRay;
	Transform lastHitTran;

	private void Awake()
	{
		//if inputs == null
		inputs ??= new ActionMap();

		inputs.Enable();
		lastHitTran = transform;
	}
	private void OnEnable()
	{
		inputs.Mouse.Left.performed += Hold;
	}
	private void Hold(InputAction.CallbackContext context)
	{
		Vector3 mousePos = Mouse.current.position.ReadValue();
		Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

		Debug.DrawRay(mouseRay.origin, mouseRay.direction * 50, Color.yellow, 3);

		if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, ~IgnoreRay))
		{
			if (toRemove && hit.transform != lastHitTran)
			{

				attachable.Detached(hit.point);
				attachable = null;
				toRemove = false;
			}
			else if (hit.transform.TryGetComponent(out Skyscraper skyscraper))
			{
				if (attachable != null)
				{

					attachable.Attached(hit.transform, skyscraper);
					attachable = null;
				}
			}
			else if (hit.transform.TryGetComponent<IAttachable>(out var x))
			{
				if (x.Skyscraper != null && attachable != null && lastHitTran != hit.transform)
				{

					attachable.Attached(hit.transform, x.Skyscraper);
					attachable = null;
				}
				else if (x.Skyscraper != null)
				{

					attachable = x;
					toRemove = true;
				}
				else
				{

					attachable = x;
				}
			}
			lastHitTran = hit.transform;
		}
	}

	private void OnDisable()
	{
		inputs.Mouse.Left.performed -= Hold;
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 1f);
	}
#endif
}
