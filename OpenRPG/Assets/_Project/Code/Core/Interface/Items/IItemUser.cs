using UnityEngine;

public interface IItemUser
{
    bool TryGet<T>(out T capability) where T : class;
}
