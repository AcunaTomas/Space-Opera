using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BossSceneBehaviour : MonoBehaviour
{
    [SerializeField] UnityEvent eventoBossFight;
    bool listo;
    void Awake()
    {
        if(PersistentGameInfo.skipTutorial) eventoBossFight.Invoke();
        listo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.INSTANCE.GetActivePlayer().GetHP() <= 0 && !listo)
        {
            listo = true;
            Invoke("WeWoadCene",GameManager.INSTANCE.Boludos.RespawnCooldown-.3f);
        }
    }

    void WeWoadCene()
    {
            ScenesManager.Instance.ReloadScene();
    }
}
