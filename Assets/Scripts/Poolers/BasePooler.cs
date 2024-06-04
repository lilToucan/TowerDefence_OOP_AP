using System.Collections.Generic;
using UnityEngine;

public class BasePooler<T> : MonoBehaviour where T : MonoBehaviour
{
	public List<T> ItemList = new List<T>();
	public float NumberOfItems;
	public T Prefab;
	protected virtual void Awake()
	{
		for (int i = 0; i < NumberOfItems; i++)
		{
			var item = InstanceateItem(transform);
			item.gameObject.SetActive(false);
			this.ItemList.Add(item);
		}
	}
	public virtual T GetItem(Transform endPosition, Transform target)
	{
		T item;
		if (this.ItemList.Count <= 0)
		{
			item = this.InstanceateItem(endPosition);
		}
		else
		{
			item = this.GetItemFromList();
		}

		return item;
	}

	public virtual T InstanceateItem(Transform endPosition)
	{
		T item = Instantiate(this.Prefab, endPosition.position, Quaternion.identity);
		item.transform.parent = transform;
		item.gameObject.SetActive(false);
		return item;
	}

	public virtual T GetItemFromList()
	{
		T item = this.ItemList[0];
		this.ItemList.RemoveAt(0);
		return item;
	}
}