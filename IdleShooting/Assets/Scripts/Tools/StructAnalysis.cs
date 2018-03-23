using ExcelDataReader;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StructAnalysis : SingleInstance<StructAnalysis>
{
    private string url = "Assert/../../Data/struct.xlsx";
    private string structUrl = "Assets/Scripts/GlobalDefine/Struct.cs";

    private IExcelDataReader m_reader;
    private StreamWriter m_sw;
    private FileInfo m_file;
    private List<string> m_nameList;
    private List<string> m_typeList;

    private bool OpenStructFile()
    {
        m_file = new FileInfo(structUrl);
        if (m_file.Exists)
        {
            File.Delete(structUrl);
        }
        m_sw = m_file.CreateText();
        return true;
    }

    public bool LoadStruct()
    {
        m_nameList = new List<string>();
        m_typeList = new List<string>();
        OpenStructFile();
        WriteBaseStruct();

        FileStream stream = File.Open(url, FileMode.Open, FileAccess.Read);
        m_reader = ExcelReaderFactory.CreateOpenXmlReader(stream);        

        while (m_reader.Read())
        {
            AnalysisRow();
        }

        m_reader.Close();
        m_sw.Close();
        m_sw.Dispose();
        return true;
    }

    private bool AnalysisRow()
    {
        string _str = m_reader.GetString(0);       
        if (_str == "name")
        {
            WriteStructBegin();
        }
        else if (_str == "property")
        {
            m_nameList.Clear();
            m_typeList.Clear();
            WriteStructContext();
        }

        return true;
    }

    public interface IBaseStruct
    {
        void Init(string _str);
    }

    private void WriteBaseStruct()
    {
        m_sw.Write("#pragma warning disable 0414\npublic interface IBaseStruct {\n\tvoid Init(string _str, char _tag = ';');\n}\n\n");
    }

    private void WriteStructBegin()
    {
        Debug.Log(m_reader.GetString(1));
        m_sw.Write("public struct " + m_reader.GetString(1) + " : IBaseStruct {\n");
    }

    private void WriteStructContext()
    {
        m_sw.Write("\t" + ChangeValueType() + m_reader.GetString(2) + ";\n");
        m_nameList.Add(m_reader.GetString(2));
        m_typeList.Add(m_reader.GetString(1));
        if (m_reader.Read())
        {
            string _str = m_reader.GetString(1);
            if (_str == null)
            {
                WriteStructEnd();
            }
            else
            {
                WriteStructContext();
            }
        }
        else
        {
            WriteStructEnd();
        }
    }

    private void WriteStructEnd()
    {
        m_sw.Write("\n\tpublic void Init(string _str, char _tag = ';') {\n\t\tstring[] _strArray = _str.Split(_tag);\n");
        for (int i = 0; i < m_nameList.Count; ++i)
        {
            m_sw.Write("\t\t" + m_nameList[i] + ChangeStructInitValueType(i));
        }

        m_sw.Write("\t}\n}\n\n");
    }

    private string ChangeValueType()
    {
        string _str = m_reader.GetString(1);
        if (_str == "list")
        {
            return "list<" + m_reader.GetString(3) + "> ";
        }
        return _str + " ";
    }

    private string ChangeStructInitValueType(int i)
    {
        if (m_typeList[i] == "string")
        {
            return " = _strArray[" + i + "];\n";
        }
        else if (m_typeList[i] == "int")
        {
            return " = int.Parse(_strArray[" + i + "]);\n";
        }
        else if (m_typeList[i] == "uint")
        {
            return " = uint.Parse(_strArray[" + i + "]);\n";
        }
        else if (m_typeList[i] == "float")
        {
            return " = float.Parse(_strArray[" + i + "]);\n";
        }
        return "";
    }
}