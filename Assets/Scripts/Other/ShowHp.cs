using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowHp : MonoBehaviour
{
	IHp myHp;
	[SerializeField] Image img;
	private void Start()
	{
		myHp = GetComponent<IHp>();
	}
	private void Update()
	{
		img.fillAmount = myHp.HP / myHp.FullHp;
	}
}
