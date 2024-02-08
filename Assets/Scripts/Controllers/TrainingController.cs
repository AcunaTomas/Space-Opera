using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingController : MonoBehaviour
{
    public bool _training1Finish = false;
    public bool _training2Finish = false;


    [SerializeField]
    private GameObject _training1;
    [SerializeField]
    private GameObject _training1Coll;
    [SerializeField]
    private GameObject _training2;
    [SerializeField] 
    private GameObject _training2Coll;
    [SerializeField] 
    private GameObject _training3;
    [SerializeField] 
    private GameObject _training3Coll;

    public void ChangeTraining()
    {
        if (_training1Finish == false)
        {
            return;
        }
        else if(_training1Finish == true && _training2Finish == false)
        {
            _training1.gameObject.SetActive(false);
            _training1Coll.gameObject.SetActive(false);
            _training2.gameObject.SetActive(true);
            _training2Coll.gameObject.SetActive(true);
        }
        else if (_training1Finish == true && _training2Finish == true)
        {
            _training1.gameObject.SetActive(false);
            _training1Coll.gameObject.SetActive(false);
            _training2.gameObject.SetActive(false);
            _training2Coll.gameObject.SetActive(false);
            _training3.gameObject.SetActive(true);
            _training3Coll.gameObject.SetActive(true);
        }
    }

    public void FinishTraining1()
    {
        _training1Finish = true;
    }
    public void FinishTraining2()
    {
        _training2Finish = true;
    }


}
