using UnityEngine;

public class BasePowerUp : MonoBehaviour, IAttachable
{
	public Skyscraper Skyscraper { get; set; }
	[SerializeField] LayerMask ignoreRayLayer;
	[SerializeField] protected float Hight;
	[HideInInspector] public Enemy hitEnemy;
	protected Vector3 startPos;

	public virtual void Attached(Transform target, Skyscraper skyscraper)
	{
		this.startPos = this.transform.position;
		this.Skyscraper = skyscraper;
		this.Skyscraper.PowerUpEffect += this.OnHitEffect;
		this.Skyscraper.onPowerUpInteraction?.Invoke();

		if (this.Skyscraper.TowersUpdate == null)
			Detached(startPos - Vector3.up);

		else
			this.transform.position = target.position + Vector3.up;

		if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
		{
			if (hit.transform.TryGetComponent<IAttachable>(out _))
			{
				hit.transform.gameObject.layer = ignoreRayLayer;
			}
		}
	}

	public virtual void Detached(Vector3 targetPos)
	{
		this.Skyscraper.PowerUpEffect -= this.OnHitEffect;
		this.Skyscraper.onPowerUpInteraction?.Invoke();
		if (Physics.Raycast(this.transform.position, Vector3.down, out RaycastHit hit))
		{
			if (hit.transform.TryGetComponent<IAttachable>(out _))
			{
				hit.transform.gameObject.layer = 0;
			}
		}
		this.transform.position = targetPos + Vector3.up * Hight;
		this.Skyscraper = null;
	}

	/// <summary>
	/// ci� che afflige ai nemici
	/// </summary>
	/// <param name="hP"></param>
	public virtual void OnHitEffect(IHp hP)
	{
		hP.HpTransform.TryGetComponent(out this.hitEnemy);
	}
}
