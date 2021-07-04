using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDotweenEvent : MonoBehaviour
{
    public void CreateEvent() { Debug.Log("Create"); }
    public void StartEvent() { Debug.Log("Start"); }
    public void PlayEvent() { Debug.Log("Play"); }
    public void UpdateEvent() { Debug.Log("Update"); }
    public void StepCompleteEvent() { Debug.Log("StepComplete"); }
    public void CompleteEvent() { Debug.Log("Complete"); }
    public void RewindEvent() { Debug.Log("Rewind"); }
}
