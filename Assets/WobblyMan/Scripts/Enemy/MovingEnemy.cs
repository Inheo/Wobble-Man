using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
    // Скорость передвижения врага
    [SerializeField] private float speed = 1;
    // скорость поворота врага
    [SerializeField] private float rotationSpeed = 3;
    // точки к которым движется враг
    [SerializeField] private Transform[] points;

    // Индекс текущей точки к которому движется враг
    private int currentPoint = 0;

    private void Update()
    {
        // Если мы не проиграли враг продолжает движение
        if (!Game.Instance.Lose)
        {
            Move();
        }
    }

    private void Move()
    {
        // Координаты текущей точки к котормоу должен двигаться враг
        Vector3 pointPos = new Vector3(points[currentPoint].position.x, transform.position.y, points[currentPoint].position.z);
        // Само движение
        transform.position = Vector3.MoveTowards(transform.position, pointPos, speed * Time.deltaTime);

        // Если враг дошел до точки, то выбираем следующую точку
        if (transform.position == pointPos)
        {
            currentPoint++;
            if (currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
        RotateToPoint();
    }

    /// <summary>
    /// Поворот к точке, к которой движется враг
    /// </summary>
    private void RotateToPoint() // поворачивает в сторону точки со скоростью rotationSpeed
    {
        // Вычисляем в какую сторону смотреть
        Vector3 lookVector = points[currentPoint].position - transform.position;
        lookVector.y = 0;

        // Если он уже смотрит в сторону точки
        if (lookVector == Vector3.zero) return;

        // Поворачиваем врага
        transform.rotation = Quaternion.RotateTowards
            (
                transform.rotation,
                // Направление к которому нужно повернуть
                Quaternion.LookRotation(lookVector, Vector3.up),
                rotationSpeed * Time.deltaTime
            );

    }
}
