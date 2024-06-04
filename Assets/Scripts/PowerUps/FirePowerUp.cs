using System.Collections;
using UnityEngine;

public class FirePowerUp : BasePowerUp
{
	[SerializeField] float dmg;
	[SerializeField] float OnFireDuration;
	Transform lastHp;
	private void Start()
	{
		lastHp = transform;
	}
	public override void Attached(Transform target, Skyscraper skyscraper)
	{
		base.Attached(target, skyscraper);
	}

	public override void Detached(Vector3 targetPos)
	{
		base.Detached(targetPos);
	}
	public override void OnHitEffect(IHp hP)
	{
		if (hP.HpTransform != lastHp)
			return;

		lastHp = hP.HpTransform;
		StartCoroutine(Burn(hP));
	}

	IEnumerator Burn(IHp hP)
	{
		float timer = 0;
		while (timer < OnFireDuration || hP.HpTransform.gameObject.activeSelf == false)
		{
			hP.TakeDamage(dmg * Time.deltaTime);
			yield return null;
		}
	}
}
