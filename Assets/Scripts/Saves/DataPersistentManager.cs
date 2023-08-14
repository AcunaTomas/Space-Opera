using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistentManager : MonoBehaviour
{
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
            Debug.LogError("hay más de un DataPersistenManager en juego, la re putísima madre");
        }
        INSTANCE = this;
    }

    private void Start()
    {
        this._dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
        this._dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        //SaveGame();
    }

    public void NewGame()
    {
        this._gameData = new GameData();
    }

    public void LoadGame()
    {
        this._gameData = _dataHandler.Load();

        if (this._gameData == null)
        {
            Debug.Log("No se encontraron datos guardados. Esto puede suceder por varias razones: la primera es que soy de Boca, la segunda es porque seguramente no exista este archivo de guardado lo cual es lo más normal del mundo por lo que se debe revisar en primera instancia que se guarde una partida o que exista una partida guardada que se puede dar automáticamente por este sistema o porque el jugador hizo no sé qué cantidad de cosas en un nivel determinado que en cualquiera de los casos eso lo realiza la función SaveGame() y no esta función de acá así que andá a revisar el otro, la tercera y última por suerte es que probablemente este script no sirva para nada y me vi 30 minutos de video al pedo.");
            NewGame();
        }

        foreach (IDataPersistance dpo in _dataPersistanceObjects)
        {
            dpo.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistance dpo in _dataPersistanceObjects)
        {
            dpo.SaveData(ref _gameData);
        }

        _dataHandler.Save(_gameData);
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> _dpo = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance> (_dpo);
    }
}
