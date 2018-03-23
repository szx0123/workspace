using UnityEngine;
using System.IO;
using ExcelDataReader;

public class ExcelToCsv : SingleInstance<ExcelToCsv> {

    public string ExcelUrl = "Assets/../../Config/";

    private StreamWriter m_sw;
    private FileInfo m_file;
    private string m_name;
    private IExcelDataReader m_reader;


    public bool ConvertAllExcel()
    { 
        //获取指定路径下面的所有资源文件  
        if (Directory.Exists(ExcelUrl))
        {
            DirectoryInfo _direction = new DirectoryInfo(ExcelUrl);
            FileInfo[] _files = _direction.GetFiles("*.xlsx", SearchOption.AllDirectories);
            for (int i = 0; i < _files.Length; i++)
            {
                Debug.Log("Name:" + _files[i].Name);
                m_name = (_files[i].Name.Split('.'))[0];
                ConvertExcel();
            }
            return true;
        }
        else
        {
            Debug.LogError("Config url is error!Need config url:" + ExcelUrl);
            return false;
        }
    }

    public bool ConvertOneExcel(string _name)
    {
        m_name = _name;
        ConvertExcel();
        return true;
    }

    private void OpenEmptyCsvFile()
    {
        string _url = GlobalDefine.Instance.configUrl + m_name + ".csv";
        m_file = new FileInfo(_url);
        if (m_file.Exists)
        {
            File.Delete(_url);
        }
        m_sw = m_file.CreateText();
    }

    private bool ConvertExcel()
    {
        OpenEmptyCsvFile();
        string _url = ExcelUrl + m_name + ".xlsx";
        FileStream stream = File.Open(_url, FileMode.Open, FileAccess.Read);
        m_reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        
        while (m_reader.Read())
        {
            for (int i = 0; i < m_reader.FieldCount - 1; ++i)
            {
                if (m_reader.GetValue(i) == null)
                {
                    if (i == 0)
                    {
                        break;
                    }
                    else
                    {
                        m_sw.Write(",");
                    }                           
                }
                else
                {
                    m_sw.Write(m_reader.GetValue(i) + ",");
                }
            }
            m_sw.Write(m_reader.GetValue(m_reader.FieldCount - 1) + "\n");
        }

        m_reader.Close();
        m_sw.Close();
        m_sw.Dispose();
        return true;
    }
}
