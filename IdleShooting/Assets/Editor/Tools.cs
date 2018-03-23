using UnityEditor;
using UnityEngine;

public class Tools : MonoBehaviour {

    [MenuItem("Tools/ConfigAnalysis &%#c")]
    static void ToolsConfigAnalysis()
    {
        Debug.Log("---------------------------Begin ConfigAnalysis!");
        ConfigAnalysis.Instance.InitAllConfig();
        Debug.Log("---------------------------End ConfigAnalysis!");
    }

    [MenuItem("Tools/StructAnalysis &%#x")]
    static void ToolsStructAnalysis()
    {
        Debug.Log("---------------------------Begin ToolsStructAnalysis!");
        StructAnalysis.Instance.LoadStruct();
        Debug.Log("---------------------------End ToolsStructAnalysis!");
    }
}