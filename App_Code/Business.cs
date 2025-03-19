using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class TF_PageControls
{
    public TF_PageControls()
    {
    }
    public void enablefirstnav(Button Btn_Previous, Button Btn_First)
    {
        Btn_Previous.Enabled = true;
        Btn_Previous.CssClass = "pagingButton";

        Btn_First.Enabled = true;
        Btn_First.CssClass = "pagingButton";
    }
    public void disablefirstnav(Button Btn_Previous, Button Btn_First)
    {
        Btn_Previous.Enabled = false;
        Btn_Previous.CssClass = "pagingButtonDisabled";

        Btn_First.Enabled = false;
        Btn_First.CssClass = "pagingButtonDisabled";
    }
    public void enablelastnav(Button Btn_Next, Button Btn_Last)
    {
        Btn_Next.Enabled = true;
        Btn_Next.CssClass = "pagingButton";

        Btn_Last.Enabled = true;
        Btn_Last.CssClass = "pagingButton";
    }
    public void disablelastnav(Button Btn_Next, Button Btn_Last)
    {
        Btn_Next.Enabled = false;
        Btn_Next.CssClass = "pagingButtonDisabled";

        Btn_Last.Enabled = false;
        Btn_Last.CssClass = "pagingButtonDisabled";
    }
}
public class Encryption//This class is used to encrypt/decrypt the string provided to it. It uses SHA1 algorithm, however MD5 can also be used with the same code.
{
    string passPhrase = "ShrIY@Is";
    string saltValue = "sHriY@2009";
    string hashAlgorithm = "SHA1";
    int passwordIterations = 2;
    string initVector = "@1B2c3D4e5F6g7H8";
    int keySize = 256;
    public string encryptplaintext(string textvalue)//function used to encrypt plain text passed to it.
    {
        string plainText = textvalue;
        string cipherText = Encrypt(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        return cipherText;
    }
    public string decrypttext(string ciphervalue)//function used to decrypt cipher text passed to it.
    {
        string cipherText = ciphervalue;
        string plainText = Decrypt(cipherText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        return plainText;
    }
    public static string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);

        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        RijndaelManaged symmetricKey = new RijndaelManaged();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

        MemoryStream memoryStream = new MemoryStream();

        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

        cryptoStream.FlushFinalBlock();

        byte[] cipherTextBytes = memoryStream.ToArray();

        memoryStream.Close();

        cryptoStream.Close();

        string cipherText = Convert.ToBase64String(cipherTextBytes);

        return cipherText;
    }
    public static string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);

        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        RijndaelManaged symmetricKey = new RijndaelManaged();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

        memoryStream.Close();


        cryptoStream.Close();

        string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

        return plainText;
    }
   
    public string URLIDEncription(string urlId)
    {
        string[] dirtyCharacters = { ";", "/", "?", ":", "@", "&", "=", "+", "$", "," };
        string[] cleanCharacters = { "p2n3t4G5l6m", "s1l2a3s4h", "q1e2st3i4o5n", "T22p14nt2s", "TpM_stt", "a2n3nd", "e1q2ua88l", "p22l33u1ws", "d0l1ar5", "c0m8a1a" };
        urlId = encryptplaintext(urlId);
        foreach (string dirtyCharacter in dirtyCharacters)
        {
            urlId = urlId.Replace(dirtyCharacter, cleanCharacters[Array.IndexOf(dirtyCharacters, dirtyCharacter)]);
        }
        return urlId;
    }

    public string URLIDDecription(string urlId)
    {
        string[] dirtyCharacters = { ";", "/", "?", ":", "@", "&", "=", "+", "$", "," };
        string[] cleanCharacters = { "p2n3t4G5l6m", "s1l2a3s4h", "q1e2st3i4o5n", "T22p14nt2s", "TpM_stt", "a2n3nd", "e1q2ua88l", "p22l33u1ws", "d0l1ar5", "c0m8a1a" };
        foreach (string symbol in cleanCharacters)
        {
            urlId = urlId.Replace(symbol, dirtyCharacters[Array.IndexOf(cleanCharacters, symbol)]);
        }
        urlId = decrypttext(urlId);
        return urlId;
    }
}
