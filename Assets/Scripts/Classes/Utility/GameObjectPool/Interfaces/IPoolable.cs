using UnityEngine;
using System.Collections;

interface IPoolable {
    GameObject Create();
    GameObject Issue();
    void Decomission(GameObject gameObjectToDecomission);
    void Destroy(GameObject gameObjectToDestroy);
    void AddToAvailable(GameObject gameObjectToAdd);
    void AddToInUse(GameObject gameObjectToAdd);
    void PerformRecycleCheck();
}
