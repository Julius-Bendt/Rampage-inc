using UnityEngine;
using System.Collections;

namespace Devdog.LosPro.Demo
{
    public class MyObserverCallbacks : MonoBehaviour, IObserverCallbacks
    {
        public void OnTargetCameIntoRange(SightTargetInfo info)
        {
            Debug.Log("OnTargetCameIntoRange");
        }

        public void OnTargetWentOutOfRange(SightTargetInfo info)
        {
            Debug.Log("OnTargetWentOutOfRange");
        }

        public void OnTargetDestroyed(SightTargetInfo info)
        {
            Debug.Log("OnTargetDestroyed");
        }

        public void OnTryingToDetectTarget(SightTargetInfo info)
        {
            Debug.Log("OnTryingToDetectTarget");
        }

        public void OnDetectingTarget(SightTargetInfo info)
        {
            Debug.Log("OnDetectingTarget");
        }

        public void OnDetectedTarget(SightTargetInfo info)
        {
            Debug.Log("OnDetectedTarget");
        }

        public void OnStopDetectingTarget(SightTargetInfo info)
        {
            Debug.Log("OnStopDetectingTarget");
        }

        public void OnUnDetectedTarget(SightTargetInfo info)
        {
            Debug.Log("OnUnDetectedTarget");
        }
    }
}