using System;
using System.Text;
using System.IO;
using UnityEngine;

public class FirstRunController : MonoBehaviour
{
    [SerializeField] TextAsset[] textData;
    [SerializeField] string[] pathData;
    [SerializeField] string base64;

    private void Awake()
    {
        WriteData();
    }

    public void WriteData()
    {
        //if(PlayerPrefs.GetInt("FirstRun") == 0)
        //{
        //    WriteAllData();
        //    PlayerPrefs.SetInt("FirstRun", 1);
        //}
        WriteAllData();
    }

    public void WriteAllData()
    {
        for(int i=0; i< textData.Length;i++)
        {
            WriteFileToPath(pathData[i], textData[i].ToString());
        }
    }

    public void WriteFileToPath(string path, string data)
    {
        if (!File.Exists(Application.persistentDataPath + path) || PlayerPrefs.GetInt("FirstInstall1", 0) < 1)
        {
            if (PlayerPrefs.GetInt("FirstInstall1", 0) < 1)
            {
                PlayerPrefs.SetInt("FirstInstall1", 1);
                PlayerPrefs.SetInt("CurrentProgress", 1);
            }
            StreamWriter writer = new StreamWriter(Application.persistentDataPath + path);
            var encrypt = Encrypt.Encrypts(data);
            writer.Write(encrypt);
            //writer.Write(base64); //Creates editable data in form of a string.
            writer.Close();
        }
    }

}
