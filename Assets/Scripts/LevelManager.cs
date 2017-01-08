using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

public class LevelManager
{
    private static bool isEncrypted = false;
    public static bool IsEncrypted { get { return isEncrypted; } set { isEncrypted = value; } }

    private static readonly string encryptionKey = "FrOgGyStYle2016";//encryption password


    public static Level RestoreLevel(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("Could not find level file: " + path);

        string xml = File.ReadAllText(path);//get level data from file

        if (IsEncrypted)
            xml = Decrypt(xml);

        Level v;
        try
        {
            MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(xml));//push string into stream for deserialising
            XmlSerializer ser = new XmlSerializer(typeof(Level));
            v = (Level)ser.Deserialize(ms);
        }
        catch (Exception e)
        {
            throw new LevelReadException(xml, e);
        }

        return v;
    }

    public static void SaveLevel(Level v, string path)
    {
        string xml;
        try
        {
            XmlSerializer ser = new XmlSerializer(typeof(Level));
            MemoryStream ms = new MemoryStream();//xml serialise into memory stream
            ser.Serialize(ms, v);
            xml = Encoding.ASCII.GetString(ms.ToArray());
        }
        catch (Exception e)
        {
            throw new LevelWriteException(v, e);
        }

        if (IsEncrypted)
            xml = Encrypt(xml);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(Path.GetDirectoryName(path));

        File.WriteAllText(path, xml);
    }


    /// <summary>
    /// Adds AES string encryption on the raw data file to stop tampering
    /// </summary>
    /// <param name="clearText">raw data to encrypt</param>
    /// <returns>encyrpted string</returns>
    private static string Encrypt(string clearText)
    {
        string EncryptionKey = encryptionKey;
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    /// <summary>
    /// Removes AES encryption from a string to reveal data structure
    /// </summary>
    /// <param name="cipherText">encrypted text</param>
    /// <returns>decrypted data file</returns>
    private static string Decrypt(string cipherText)
    {
        string EncryptionKey = encryptionKey;
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }





    public class LevelReadException : Exception
    {
        public string JSON { get; private set; }
        public LevelReadException(string json, Exception x) : base("Failed to parse from JSON", x)
        {
            JSON = json;
        }
    }
    public class LevelWriteException : Exception
    {
        public Level Level { get; private set; }
        public LevelWriteException(Level v, Exception x) : base("Failed to convert to JSON", x)
        {
            Level = v;
        }
    }




    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}
