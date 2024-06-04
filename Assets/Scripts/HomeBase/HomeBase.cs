using UnityEngine;

public class HomeBase : MonoBehaviour, IHp
{
	public Transform HpTransform => transform;
	[field: SerializeField] public float HP { get; set; }
	public float FullHp { get; set; }
	private void Start()
	{
		FullHp = HP;
	}

	public void Death()
	{
		// could not be asked to make the ui
		Application.Quit();
	}

	public void TakeDamage(float dmg)
	{
		HP -= dmg;
		if (HP <= 0)
		{
			Death();
		}
	}
}
