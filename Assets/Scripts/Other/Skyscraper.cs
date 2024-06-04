using System;
using UnityEngine;

public class Skyscraper : MonoBehaviour
{
	public delegate void TowerEvent();
	public TowerEvent TowersUpdate;

	///<summary>
	///	calls the onHitEffect of an effect
	///</summary>
	public Action<IHp> PowerUpEffect;

	///<summary>
	/// called when power up is attached or deteached
	///</summary>
	public Action onPowerUpInteraction;

	private void Update()
	{
		TowersUpdate?.Invoke();
	}

}
