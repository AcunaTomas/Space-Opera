using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistentManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField]
    private bool _initializeDataIfNull = false;

    [Header("File Storage Config")]
    [SerializeField]
    private string _fileName;
    private GameData _gameData;
    private List<IDataPersistance> _dataPersistanceObjects;
    private FileDataHandler _dataHandler;
    public static DataPersistentManager INSTANCE { get; private set; }

    private void Awake()
    {
        if (INSTANCE != null)
        {
            Debug.Log("hay más de un DataPersistenManager en juego, la re putísima madre. Pero lo destruí :)");
            Destroy(gameObject);
            return;
        }
        INSTANCE = this;
        DontDestroyOnLoad(gameObject);
        _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void NewGame()
    {
        _gameData = new GameData();
    }

    public void LoadGame()
    {
        _gameData = _dataHandler.Load();

        if (_gameData == null && _initializeDataIfNull)
        {
            NewGame();
        }

        if (_gameData == null)
        {
            Debug.Log("No se encontraron datos guardados. Esto puede suceder por varias razones: la primera es que soy de Boca, la segunda es porque seguramente no exista este archivo de guardado lo cual es lo más normal del mundo por lo que se debe revisar en primera instancia que se guarde una partida o que exista una partida guardada que se puede dar automáticamente por este sistema o porque el jugador hizo no sé qué cantidad de cosas en un nivel determinado que en cualquiera de los casos eso lo realiza la función SaveGame() y no esta función de acá así que andá a revisar el otro, la tercera y última por suerte es que probablemente este script no sirva para nada y me vi 30 minutos de video al pedo.");
            return;
        }

        foreach (IDataPersistance dpo in _dataPersistanceObjects)
        {
            dpo.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        if (_gameData == null)
        {
            Debug.Log("No hay datos. Se tiene que empezar un juego nuevo para eso bro.");
            return;
        }

        foreach (IDataPersistance dpo in _dataPersistanceObjects)
        {
            dpo.SaveData(_gameData);
        }

        _dataHandler.Save(_gameData);
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> _dpo = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance> (_dpo);
    }

    public void DeleteSave()
    {
        Debug.LogWarning("Por las dudas, borraste la partida guardada porque apretaste el 5.");
        _dataHandler.Delete();
    }

    public bool HasGameData()
    {
        return _gameData != null;
    }
}
