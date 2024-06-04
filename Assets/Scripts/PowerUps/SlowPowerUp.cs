using System.Collections;
using UnityEngine;

public class SlowPowerUp : BasePowerUp
{
    [SerializeField] float SlowSpeed;
    [SerializeField] float SlowDuration;
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
        base.OnHitEffect(hP);
        StartCoroutine(SpeedDown());
    }

    IEnumerator SpeedDown()
    {
        hitEnemy.NavAgent.speed -= SlowSpeed;
        yield return new WaitForSeconds(SlowDuration);
        hitEnemy.NavAgent.speed += SlowSpeed;
    }
}
