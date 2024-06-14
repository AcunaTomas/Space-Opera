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
            Debug.Log(m_ImportedArgs.ImportedSuperMap.gameObject.transform.GetChild(0).GetChild(1).gameObject.name);
            
        
        }

}
