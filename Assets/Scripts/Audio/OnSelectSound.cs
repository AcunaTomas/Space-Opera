using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnSelectSound : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.sfx_ui_select, this.transform.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.sfx_ui_navigate, this.transform.position);
    }


}
