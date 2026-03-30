using System.IO.Enumeration;
using UnityEngine;
[CreateAssetMenu(fileName = "New Buff Augment", menuName = "Augment/Buff")]
public class BuffInfoSO : AugmentInfoSO
{
    public BuffType Type;
    public float amount;
}
