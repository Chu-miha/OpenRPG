using System;
using UnityEngine;

public class FakeCameraTransition : ICameraTransition
{
    public bool IsRunning { get; private set; }

    public void ToFirstPerson(Action onComplete)
    {
        onComplete?.Invoke();
    }

    public void ToThirdPerson(Action onComplete)
    {
        onComplete?.Invoke();
    }
}
