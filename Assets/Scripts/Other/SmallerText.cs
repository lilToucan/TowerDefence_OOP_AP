
using TMPro;
using UnityEngine;

public class SmallerText : MonoBehaviour
{
	[SerializeField] string biggerText;
	[SerializeField] string smallerText;

	private void Start()
	{
		string finishedString = $"{biggerText} <size=30>{smallerText}</size>";

		var x = GetComponent<TextMeshPro>();

		x.text = finishedString;
	}
}
