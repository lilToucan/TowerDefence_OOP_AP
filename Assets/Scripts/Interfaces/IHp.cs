using UnityEngine;

public interface IHp
{
	///<summary>
	///	the transform of the script with IHp 
	///</summary>
	public Transform HpTransform { get; }
	public float HP { get; set; }
	public float FullHp { get; set; }
	public void TakeDamage(float dmg);

	public void Death();
}
