using System.Collections;
using UnityEngine;
public class Spawner : MonoBehaviour
{
	[SerializeField] float delayBetweenWaves;
	[SerializeField] float delayBetweenSpawns;
	[SerializeField] int numberOfEnemies = 1;
	[SerializeField] EnemyPooler enemyPooler;
	int enemyCounter = 0;

	private void Awake()
	{
		StartCoroutine(SpawnWave());
	}

	void EnemyDeath()
	{
		enemyCounter--;
	}

	IEnumerator SpawnWave()
	{
		while (enemyCounter < numberOfEnemies)
		{
			enemyCounter++;
			Enemy enemy = enemyPooler.GetItem(transform);
			enemy.OnDeath += EnemyDeath;
			yield return new WaitForSeconds(delayBetweenSpawns);
		}
		while (enemyCounter > 0)
		{
			yield return null;
		}
		numberOfEnemies++;
		yield return new WaitForSeconds(delayBetweenWaves);
		StartCoroutine(SpawnWave());
	}
}
