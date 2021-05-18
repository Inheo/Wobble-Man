using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFieldOfView : MonoBehaviour
{
    // угол обзора
    [Range(0, 360)] public float ViewAngle = 90f;
    // дальность видимости
    public float ViewDistance = 15f;
    // Радиус видимости(например, если подойти слишком близко сзади к врагу, то он нас заметит)
    public float DetectionDistance = 0.3f;
    // transform с которого будет пускаться луч
    public Transform EnemyEye;

    // скорость поворота
    public float rotationSpeed;

    private void Update()
    {
        // Дистанция от врага до ГГ
        float distanceToPlayer = Vector3.Distance(Hero.Instance.transform.position, transform.position);
        // Если враг увидел героя...
        if (distanceToPlayer <= DetectionDistance || IsInView())
        {
            RotateToTarget();
            Game.Instance.onLose?.Invoke();
        }

        // DrawViewState();
    }

    private bool IsInView() // true если цель видна
    {
        // угол между ГГ и врагом
        float realAngle = Vector3.Angle(EnemyEye.forward, Hero.Instance.transform.position - EnemyEye.position);
        RaycastHit hit;
        if (Physics.Raycast(EnemyEye.position, Hero.Instance.transform.position - EnemyEye.position, out hit, ViewDistance))
        {
            // Проверка доходит ли луч до ГГ и нет ли преград.
            if (realAngle < ViewAngle / 2f && Vector3.Distance(EnemyEye.position, Hero.Instance.transform.position) <= ViewDistance && hit.transform == Hero.Instance.transform.transform)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Поворачиваемся к ГГ
    /// </summary>
    private void RotateToTarget() // поворачивает в сторону героя со скоростью rotationSpeed
    {
        // Вычисляем в какую сторону смотреть
        Vector3 lookVector = Hero.Instance.transform.position - transform.position;
        lookVector.y = 0;

        // Если он уже смотрит в сторону ГГ
        if (lookVector == Vector3.zero) return;

        // Поворачиваем Врага
        transform.rotation = Quaternion.RotateTowards
            (
                transform.rotation,
                // Направление к которому нужно повернуть
                Quaternion.LookRotation(lookVector, Vector3.up),
                rotationSpeed * Time.deltaTime
            );
    }

    /// <summary>
    /// Рисуем угол обзора
    /// </summary>
    private void DrawViewState()
    {
        Vector3 left = EnemyEye.position + Quaternion.Euler(new Vector3(0, ViewAngle / 2f, 0)) * (EnemyEye.forward * ViewDistance);
        Vector3 right = EnemyEye.position + Quaternion.Euler(-new Vector3(0, ViewAngle / 2f, 0)) * (EnemyEye.forward * ViewDistance);
        Debug.DrawLine(EnemyEye.position, left, Color.yellow);
        Debug.DrawLine(EnemyEye.position, right, Color.yellow);
    }


}