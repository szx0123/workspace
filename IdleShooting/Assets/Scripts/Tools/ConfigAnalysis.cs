using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConfigAnalysis : SingleInstance<ConfigAnalysis> {

    private string configDataManagerUrl = "Assets/Scripts/ConfigData/ConfigDataManager.cs";

    private StreamWriter m_sw;
    private FileInfo m_file;
    private string m_name;
    private string m_managerName;
    private List<string> m_nameList;

    public bool InitAllConfig()
    {
        string fullPath = "Assets/Configs/";
        m_nameList = new List<string>();

        //获取指定路径下面的所有资源文件  
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo _direction = new DirectoryInfo(fullPath);
            FileInfo[] _files = _direction.GetFiles("*.csv", SearchOption.AllDirectories);
            for (int i = 0; i < _files.Length; i++)
            {
                Debug.Log("Name:" + _files[i].Name);
                m_name = (_files[i].Name.Split('.'))[0];
                m_managerName = m_name + "Manager";
                m_nameList.Add(m_name);
                InitConfig();
            }
            WriteConfigDataManager(_files);
        }
        return true;
    }

    private bool InitConfig()
    {
        CsvReader.Instance.LoadCsv(m_name);
        string _url = GlobalDefine.Instance.configDataUrl + m_name + ".cs";
        m_file = new FileInfo(_url);
        if (m_file.Exists)
        {
            File.Delete(_url);
        }
        m_sw = m_file.CreateText();
        WriteFileBegin();
        WriteFileContent();
        WriteFileEnd();

        WriteFileManagerBegin();
        WriteFileManagerContent();
        WriteFileManagerEnd();

        m_sw.Close();
        m_sw.Dispose();
        return true;
    }

    private void WriteFileBegin()
    {
        m_sw.Write("using System.Collections.Generic;\n\n");
        m_sw.Write("public class " + m_name + " {\n");
    }

    private void WriteFileContent()
    {
        for (uint i = 0; i < CsvReader.Instance.ArrrayData[0].Length; ++i)
        {
            m_sw.Write("\tpublic readonly " + CsvReader.Instance.ArrrayData[0][i] + " m_" + CsvReader.Instance.ArrrayData[1][i] + ";\n");
        }

        m_sw.Write("\n\tpublic " + m_name + "(string[] _str)\n");
        m_sw.Write("\t{\n");
        m_sw.Write("\t\tuint i = 0;\n");
        for (uint i = 0; i < CsvReader.Instance.ArrrayData[0].Length; ++i)
        {
            m_sw.Write("\t\tm_" + CsvReader.Instance.ArrrayData[1][i] + ChangeValueType(CsvReader.Instance.ArrrayData[0][i]));
        }
        m_sw.Write("\t}\n");
    }

    private void WriteFileEnd()
    {
        m_sw.Write("}\n");
    }

    private void WriteFileManagerBegin()
    {
        m_sw.Write("\npublic class " + m_managerName + " : SingleInstance<" + m_managerName + ">{\n");
        m_sw.Write("\n\tpublic readonly Dictionary<uint, " + m_name + "> data;\n");
        m_sw.Write("\n\tpublic " + m_managerName + " () {\n");
        m_sw.Write("\t\tdata = new Dictionary<uint, " + m_name + ">();\n");
        m_sw.Write("\t}\n");
    }

    private void WriteFileManagerContent()
    {
        m_sw.Write("\n\tpublic void Init(string _name) {\n");
        m_sw.Write("\t\tCsvReader.Instance.LoadCsv(_name);\n");
        m_sw.Write("\t\tfor (int i = 3; i < CsvReader.Instance.ArrrayData.Count; ++i) {\n");
        m_sw.Write("\t\t\tif (CsvReader.Instance.ArrrayData[i][0] == \"\")\treturn;\n");
        m_sw.Write("\t\t\t" + m_name + " _config = new " + m_name + "(CsvReader.Instance.ArrrayData[i]);\n");
        m_sw.Write("\t\t\tdata.Add(uint.Parse(CsvReader.Instance.ArrrayData[i][0]), _config);\n");
        m_sw.Write("\t\t}\n\t}\n");
    }

    private void WriteFileManagerEnd()
    {
        m_sw.Write("}");
    }

    private string ChangeValueType(string _str)
    {
        if (_str == "int")
        {
            return " = GlobalFunction.Instance.ChangeStringToInt(_str[i++]);\n";
        }
        if (_str == "uint")
        {
            return " = GlobalFunction.Instance.ChangeStringToUint(_str[i++]);\n";
        }
        else if (_str == "float")
        {
            return " = GlobalFunction.Instance.ChangeStringToFloat(_str[i++]);\n";
        }
        else if (_str == "string")
        {
            return " = _str[i++];\n";
        }
        else if (_str == "string[]")
        {
            return " = _str[i++].Split(';');\n";
        }
        else if (_str == "int[]")
        {
            return " = GlobalFunction.Instance.ChangeStringToIntArray(_str[i++]);\n";
        }
        else if (_str == "float[]")
        {
            return " = GlobalFunction.Instance.ChangeStringToFloatArray(_str[i++]);\n";
        }
        else if (_str == "uint[]")
        {
            return " = GlobalFunction.Instance.ChangeStringToUintArray(_str[i++]);\n";
        }
        else
        {
            if (_str.Contains("[]"))
            {
                return " = GlobalFunction.Instance.ChangeStringToStructArray<" + _str +">(_str[i++]));\n";
            }
            else
            {
                return ".Init(_str[i++]);\n";
            }
        }        
    }

    private bool WriteConfigDataManager(FileInfo[] _files)
    {
        m_file = new FileInfo(configDataManagerUrl);
        if (m_file.Exists)
        {
            File.Delete(configDataManagerUrl);
        }
        m_sw = m_file.CreateText();

        m_sw.Write("public class ConfigDataManager : SingleInstance<ConfigDataManager> {\n");
        m_sw.Write("\tpublic void LoadConfig() {\n");
        for (int i = 0; i < m_nameList.Count; ++i)
        {
            m_sw.Write("\t\t" + m_nameList[i] + "Manager.Instance.Init(\"" + m_nameList[i] + "\");\n");
        }
        m_sw.Write("\t}\n}");

        m_sw.Close();
        m_sw.Dispose();

        return true;
    }
}
