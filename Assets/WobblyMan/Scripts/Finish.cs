using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Если ГГ нашел выход, то уровень пройден
        if(other.TryGetComponent<Hero>(out Hero hero))
        {
            Game.Instance.onWin?.Invoke();
        }
    }
}
