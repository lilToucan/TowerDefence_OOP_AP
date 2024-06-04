using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[HideInInspector] public Transform TargetEnemy;
	public Action<IHp, Bullet> onHit;
	public float Duration = 0.5f;
	[HideInInspector] public BulletPooler bulletPooler;
	Vector3 startPos;
	float lerpTimer;

	private void OnEnable()
	{
		startPos = transform.position;
		lerpTimer = 0;
	}

	private void FixedUpdate()
	{
		Vector3 pos = transform.position;
		if (lerpTimer < Duration)
		{
			pos = Vector3.Lerp(startPos, TargetEnemy.position, lerpTimer / Duration);
			lerpTimer += Time.fixedDeltaTime;
		}
		transform.position = pos;
	}

	private void OnDisable()
	{
		if (TargetEnemy == null)
			return;

		TargetEnemy = null;
		bulletPooler.ItemList.Add(this);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out IHp hp))
		{
			onHit?.Invoke(hp, this);
			gameObject.SetActive(false);
		}
	}
}