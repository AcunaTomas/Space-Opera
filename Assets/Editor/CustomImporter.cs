using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using SuperTiled2Unity;
using SuperTiled2Unity.Editor;


public class CustomImporter : CustomTmxImporter
{
        private TmxAssetImportedArgs m_ImportedArgs;

        public override void TmxAssetImported(TmxAssetImportedArgs args)
        {
            m_ImportedArgs = args;
            var rootGameOBJ = m_ImportedArgs.ImportedSuperMap.gameObject.transform.GetChild(0);

            Transform confinerLayer = null;

            for (int i = 0; i < rootGameOBJ.transform.childCount; i++) //find the layer with confiners
            {
                if (rootGameOBJ.transform.GetChild(i).GetComponent<SuperCustomProperties>().TryGetCustomProperty("CC", out CustomProperty prop) == true)
                {
                    confinerLayer = rootGameOBJ.transform.GetChild(i);
                    processLayers(confinerLayer);
                }
            } 
        }

        public void processLayers(Transform layer) //Where the confiners are set up
        {
            for (int i = 0; i < layer.childCount; i++)
            {
                layer.GetChild(i).gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
                layer.GetChild(i).gameObject.AddComponent<ConfinerController>();
            }
        }

}
