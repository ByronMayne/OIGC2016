
using UnityEngine;

namespace OIGC2016
{
  public class LevelEditorSettings : ScriptableObject
  {
    [SerializeField]
    private int m_LevelHeight;
    [SerializeField]
    private string m_LevelName;
    [SerializeField]
    private Color m_SkyColor;

    public int levelHeight
    {
      get { return m_LevelHeight; }
      set { m_LevelHeight = value; }
    }

    public string levelName
    {
      get { return m_LevelName; }
      set { m_LevelName = value; }
    }

    public Color MyProperty
    {
      get { return m_SkyColor; }
      set { m_SkyColor = value; }
    }
  }
}
