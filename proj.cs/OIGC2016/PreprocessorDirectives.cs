using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OIGC2016
{
  public class PreprocessorDirectives
  {
#if UNITY_EDITOR
    public void EditorOnlyFunction()
    {

    }
#elif UNITY_ANDROID
    public void AndroidOnlyFunction()
    {

    }
#elif UNITY_IOS
    public void IOSOnlyFunction()
    {

    }
#else
    public void RuntimeFunction()
    {

    }
#endif 
  }
}
