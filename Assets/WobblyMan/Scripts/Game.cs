using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    [SerializeField] private LevelSpawner levelSpawner;

    // Флаг с помощью которого можно узнать пройден ли уровень
    public bool Win { get; private set; }

    // Флаг с помощью которого можно узнать проигран ли уровень
    public bool Lose { get; private set; }

    // Вызывается, когда герой прикосается с Finish дверью 
    public UnityAction onWin;
    // Вызывается, если ГГ попал в зрение стражей 
    public UnityAction onLose;

    // Текст с информацией на каком мы уровне
    [SerializeField] private Text LevelCountText;

    [SerializeField] private Animator WinPanel;
    [SerializeField] private Animator LosePanel;

    public static Game Instance;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // В текст выводим на каком уровне находимся
        LevelCountText.text = "Level: " + (LevelSpawner.CurrentLevel + 1).ToString();
        // Скрываем Все панельки
        HidePanels();
        // Подписываемся при выигрыше и проигрыше
        onWin += ShowWinPanel;
        onLose += ShowLosePanel;
    }

    /// <summary>
    /// Скрывает панели
    /// </summary>
    private void HidePanels()
    {
        WinPanel.gameObject.SetActive(false);
        LosePanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Показывает панель при успешном прохождении уровня
    /// </summary>
    private void ShowWinPanel()
    {
        Win = true;
        // Показываем панель через 1 секундны
        StartCoroutine(InvokeDelegateCor(() => {  
            WinPanel.gameObject.SetActive(true);
            WinPanel.SetBool("Show", true);
        }, 1f));
    }

    /// <summary>
    /// Показывает панель при провальном прохождении уровня
    /// </summary>
    private void ShowLosePanel()
    {
        Lose = true;
        // Показываем панель через 1 секундны
        StartCoroutine(InvokeDelegateCor(() =>
        {
            LosePanel.gameObject.SetActive(true);
            LosePanel.SetBool("Show", true);
        }, 1f));
    }

    /// <summary>
    /// КОрутин, чтобы сделать задержку
    /// </summary>
    /// <param name="func">Какой-нибудь метод</param>
    /// <param name="time">Насколько нужно сделать задержку</param>
    /// <returns></returns>
    private static IEnumerator InvokeDelegateCor(Action func, float time)
    {
        yield return new WaitForSeconds(time);
        func();
    }

    /// <summary>
    /// Перезагружает сцену
    /// </summary>
    public void LoadSceneGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /// <summary>
    /// Загрузка следующего уровня
    /// </summary>
    public void LoadNextLevel()
    {
        LevelSpawner.CurrentLevel++;
        // Если вдруг CurrentLevel больше чем количество уровней
        LevelSpawner.CurrentLevel = Mathf.Clamp(LevelSpawner.CurrentLevel, 0, levelSpawner.Levels.Length - 1);
        // Сохраняем индекс текущего уровня
        PlayerPrefs.SetInt("CurrentLevel", LevelSpawner.CurrentLevel);
        LoadSceneGame();
    }
}
