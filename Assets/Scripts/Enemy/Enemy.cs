using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHp
{
	[field: SerializeField] public float HP { get; set; }
	public float Speed;
	public float Dmg;
	[HideInInspector] public NavMeshAgent NavAgent;
	[HideInInspector] public Transform HomeBaseTran;
	[HideInInspector] public EnemyPooler EnemyPooler;
	[HideInInspector] public Vector3 StartPos;
	public Transform HpTransform { get => transform; }

	public Action OnDeath;
	public float FullHp { get; set; }
	void Awake()
	{
		NavAgent = GetComponent<NavMeshAgent>();
		FullHp = HP;
	}
	private void OnEnable()
	{
		if (HomeBaseTran == null)
		{
			return;
		}

		NavAgent.speed = Speed;
		StartPos = transform.position;
		SetDestination(HomeBaseTran.position);
		HP = FullHp;
	}

	private void OnDisable()
	{
		if (HomeBaseTran == null)
		{
			return;
		}

		EnemyPooler.ItemList.Add(this);
	}
	public void SetDestination(Vector3 newDestination)
	{
		if (NavAgent == null || gameObject.activeSelf == false)
		{
			return;
		}

		NavAgent.destination = newDestination;
	}

	public void Death()
	{
		transform.position = Vector3.up * -30;
		OnDeath?.Invoke();
		gameObject.SetActive(false);
	}

	public void TakeDamage(float dmg)
	{
		HP -= dmg;

		if (HP <= 0)
		{
			Death();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out IHp hP))
		{
			hP.TakeDamage(Dmg);
			Death();
		}
	}
}
