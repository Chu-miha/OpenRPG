using UnityEngine;
//рефакторить 
public class PlayerVisualReferences : MonoBehaviour
{
    [field: SerializeField]
    public Animator Animator { get; private set; }

    [field: SerializeField]
    public Transform WeaponSocket { get; private set; }
}
