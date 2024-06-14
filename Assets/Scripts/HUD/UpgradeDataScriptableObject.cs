using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "UpgradeDataScriptableObject", menuName ="ScriptableObjects/UpgradeData")]
public class UpgradeDataScriptableObject : ScriptableObject
{
    /*
    [SerializeField]
    int level; //Para modificar en el editor
    public int Level{get => level; private set => level = value;}

    [SerializeField]
    GameObject nextLevelPrefab; //Lo que se convierte el objeto cuando levelea
                        //No tiene que ver con el prefab spawneado al incio del nivel
    public GameObject NextLevelPrefab{get => nextLevelPrefab; private set => nextLevelPrefab = value;}
    */
    [SerializeField]
    new string header;
    public string Header {get => header; set => header = value;}
    [SerializeField]
    new string name;
    public string Name {get => name; set => name = value;}
    [SerializeField]
    string description;
    public string Description {get => description; set => description = value;}
    [SerializeField]
    Sprite icon;    //Se modifica en el editor
    public Sprite Icon {get => icon; set => icon = value;}
}
