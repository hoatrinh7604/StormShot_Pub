using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class DataSaveController : MonoBehaviour
{
    string saveFile = "/";
    [SerializeField] string fileName;

    private void Awake()
    {
        saveFile += fileName;
    }

    public string ReadFile()
    {
        string data = "";
        if (File.Exists(Application.persistentDataPath + saveFile))
        {
            StreamReader reader = new StreamReader(Application.persistentDataPath + saveFile);
            data = reader.ReadToEnd();
            reader.Close();
        }

        var decrypt = Encrypt.Decrypts(data);
        return decrypt;
    }

    public void WriteFile(string jsonString)
    {
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + saveFile);
        //var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonUtility.ToJson(jsonString)));
        var encrypt = Encrypt.Encrypts(jsonString);
        writer.Write(encrypt); //Creates editable data in form of a string.
        //writer.Write(jsonString); //Creates editable data in form of a string.
        writer.Close();
    }

}

public class Encrypt : MonoBehaviour
{
    public static string Encrypts(string input)
    {
        byte[] data = UTF8Encoding.UTF8.GetBytes(input);
        using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(GameContracts.HASH_KEY));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() { Key=key,Mode=CipherMode.ECB,Padding = PaddingMode.PKCS7})
            {
                ICryptoTransform tr = trip.CreateEncryptor();
                byte[] result = tr.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(result, 0, result.Length);
            }
        }
    }

    public static string Decrypts(string input)
    {
        byte[] data = Convert.FromBase64String(input);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(GameContracts.HASH_KEY));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform tr = trip.CreateDecryptor();
                byte[] result = tr.TransformFinalBlock(data, 0, data.Length);
                return UTF8Encoding.UTF8.GetString(result);
            }
        }
    }
}
