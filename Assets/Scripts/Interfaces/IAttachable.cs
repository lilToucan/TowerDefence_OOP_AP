using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttachable
{
    public Skyscraper Skyscraper { get; set; }
    public void Attached(Transform target, Skyscraper skyscraper);

    public void Detached(Vector3 targetPos);
}
