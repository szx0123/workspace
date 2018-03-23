using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CsvReader : SingleInstance <CsvReader>
{
    private List<string[]> m_ArrayData;

    public CsvReader()
    {
        m_ArrayData = new List<string[]>();
    }

    public bool LoadCsv(string _name)
    {
        m_ArrayData.Clear();
        StreamReader sr = null;
        try
        {
            string _url = GlobalDefine.Instance.configUrl + _name + ".csv";
            sr = File.OpenText(_url);
        }
        catch
        {
            return false;
        }

        string line;
        while ((line = sr.ReadLine()) != null)
        {
            m_ArrayData.Add(line.Split(','));
        }
        sr.Close();
        sr.Dispose();
        return true;
    }

    public List<string[]> ArrrayData
    {
        get { return m_ArrayData; }
    }
}
