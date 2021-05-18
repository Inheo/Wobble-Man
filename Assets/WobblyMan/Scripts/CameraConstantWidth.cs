using UnityEngine;

public class CameraConstantWidth : MonoBehaviour
{

    public Vector2 DefaultResolution = new Vector2(720, 1280);
    [Range(0f, 1f)] public float WidthOrHeight = 0;

    private Camera componentCamera;

    //private float initialSize;
    private float targetAspect;

    private float initialFov;
    private float horizontalFov = 120f;
    private void Awake()
    {
        // Кэшируем камеру
        componentCamera = GetComponent<Camera>();

        // Соотношение ширины и высоты
        targetAspect = DefaultResolution.x / DefaultResolution.y;

        // Сохраняем угол между верхней и нижней плоскости камеры
        initialFov = componentCamera.fieldOfView;
        // Переводим вертикальный изначальный угол в горизонтальный, если передать 1 деленное на соотношение сторон, то возвращает по вертикальному углу горизонтальный
        horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);

        SetSize();
    }


    private void Update()
    {
        SetSize();
    }

    private void SetSize()
    {
        float constantWidthFov = CalcVerticalFov(horizontalFov, componentCamera.aspect);
        componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, WidthOrHeight);
    }

    /// <summary>
    /// Принимает горизонтальный угол и соотношение сторон и по не му возвращает горизонтальный угол
    /// </summary>
    /// <param name="hFovInDeg">Горизонтальный угол</param>
    /// <param name="aspectRatio">Соотношение сторон</param>
    /// <returns></returns>
    private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    {
        float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

        float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

        return vFovInRads * Mathf.Rad2Deg;
    }
}