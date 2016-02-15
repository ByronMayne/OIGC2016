using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

namespace OIGC2016
{
  public class SavingToExternalFile : EditorWindow
  {
    /// <summary>
    /// Our reference to our serialized object. 
    /// </summary>
    private SerializedObject m_SerializedSettings;

    [MenuItem("OIGC/Saving to external file")]
    private static void ShowWindow()
    {
      var window = GetWindow<SavingToExternalFile>(utility: false);
      window.name = "Saving and Loading";
    }

    /// <summary>
    /// Called by Unity to draw the editor window.
    /// </summary>
    private void OnGUI()
    {
      DrawSerializedSettings();

      GUILayout.FlexibleSpace();

      DrawButtons();
    }

    /// <summary>
    /// This draws our buttons that show up in our editor window.
    /// </summary>
    private void DrawButtons()
    {
      GUILayout.BeginHorizontal();
      {
        if (GUILayout.Button("Load", EditorStyles.miniButtonLeft))
        {
          LoadSettings();
        }
        if (GUILayout.Button("Save", EditorStyles.miniButtonMid))
        {
          SaveSettings();
        }

        if (GUILayout.Button("Show", EditorStyles.miniButtonRight, GUILayout.ExpandWidth(false)))
        {
          Application.OpenURL(savePath);
        }
      }
      EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// If the Serialized Settings is not equal to null we iterator
    /// over every property and draw them one by one. 
    /// </summary>
    private void DrawSerializedSettings()
    {
      if (m_SerializedSettings != null)
      {
        SerializedProperty iterator = m_SerializedSettings.GetIterator();

        while (iterator.Next(enterChildren: true))
        {
          EditorGUILayout.PropertyField(iterator);
        }
      }
    }

    /// <summary>
    /// The directory where we are going to save our settings. 
    /// </summary>
    private string savePath
    {
      get
      {
        return InternalEditorUtility.unityPreferencesFolder + "/LevelEditor/Settings/";
      }
    }
    
    /// <summary>
    /// The name of the file that we are going to save. 
    /// </summary>
    private string saveName
    {
      get
      {
        return "levelEditor.yaml";
      }
    }

    /// <summary>
    /// This saves the editor settings to disk. 
    /// </summary>
    private void SaveSettings()
    {
      if (m_SerializedSettings != null)
      {
        if(!Directory.Exists(savePath))
        {
          Directory.CreateDirectory(savePath);
        }

        //Apply changes to target
        m_SerializedSettings.ApplyModifiedProperties();

        Object[] savingFiles = m_SerializedSettings.targetObjects;
        InternalEditorUtility.SaveToSerializedFileAndForget(obj: savingFiles, path: savePath + saveName, allowTextSerialization: true);
      }
    }

    /// <summary>
    /// This loads the saved file from desk if it exists. Otherwise it creates
    /// a new instance. 
    /// </summary>
    private void LoadSettings()
    {
      if (File.Exists(savePath + saveName))
      {
        Object[] loadedObjects;

        loadedObjects = InternalEditorUtility.LoadSerializedFileAndForget(path: savePath + saveName);

        m_SerializedSettings = new SerializedObject(loadedObjects);
      }
      else
      {
        m_SerializedSettings = new SerializedObject(CreateInstance<LevelEditorSettings>());
      }
    }
  }
}
