using UnityEngine;
using UnityEditor;

public class ToolsExcelToCsv : EditorWindow {

    public string excelName;

    [MenuItem("Tools/ExcelToCsv")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(ToolsExcelToCsv));
    }

    void OnGUI()
    {
        //在弹出窗口中控制变量  
        excelName = EditorGUILayout.TextField("ExcelName", excelName);

        //创建一个按钮  
        if (GUI.Button(new Rect(20, 50, 150, 30), "ConvertOneExcel"))
        {
            Debug.Log("---------------------------Begin ToolsConvertOneExcel!");
            ExcelToCsv.Instance.ConvertOneExcel(excelName);
            Debug.Log("---------------------------End ToolsConvertOneExcel!");
        }

        //创建一个按钮  
        if (GUI.Button(new Rect(200, 50, 150, 30), "ConvertAllExcel"))
        {
            Debug.Log("---------------------------Begin ToolsConvertAllExcel!");
            ExcelToCsv.Instance.ConvertAllExcel();
            Debug.Log("---------------------------End ToolsConvertAllExcel!");
        }
    }
}
