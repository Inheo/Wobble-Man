using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    // скорость передвижения игрока
    public float Speed = 5;

    // на сцене есть не видимый джойстик
    [SerializeField] private Joystick joystick;

    // направление движения игрока
    private Vector3 moveVector;

    private Rigidbody rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // Так как ГГ спавнится вместе с уровнем джойстик у него равен null поэтому создал скрипт, который будет хранить все данные которе нужны
        joystick = DataInScene.Instance.Joystick;
        Subscribe();
    }

    void FixedUpdate()
    {
        // Если не мы проиграли или не выиграли можно двигаться
        if (!Game.Instance.Win && !Game.Instance.Lose)
        {
            Move();
        }
    }

    /// <summary>
    /// Передвижение персонажа
    /// </summary>
    private void Move()
    {
        // Двмгаемся по координатам X и Z
        moveVector = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveVector, Time.deltaTime * Speed);
        HeroRotate();
    }

    /// <summary>
    /// Поворот главного героя в ту сторону в которую движется
    /// </summary>
    private void HeroRotate()
    {
        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, Speed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    /// <summary>
    /// Подписываемся на события
    /// </summary>
    private void Subscribe()
    {
        Game.Instance.onWin += Jump;
        Game.Instance.onWin += Jump;
    }

    public void Jump()
    {
        rigidbody.velocity = Vector2.up * 2;
    }
}
