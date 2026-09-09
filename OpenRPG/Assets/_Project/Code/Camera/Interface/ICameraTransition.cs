using System;
using UnityEngine;

public interface  ICameraTransition
{
    bool IsRunning { get; }

    void ToFirstPerson(Action onComplete);

    void ToThirdPerson(Action onComplete);
}
