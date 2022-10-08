using System.Security.Cryptography;
using System.Text;

namespace UsersManagement.Extentions;

public static class UserManagmentExtention
{
    private const string initVector = "pemgail9uzpgzl88";
    private const int keysize = 256;
    public static string EncryptString(this string text, string key = "@#$_Jabbekhaneh7091")
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);
        PasswordDeriveBytes password = new PasswordDeriveBytes(key, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
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
        return Convert.ToBase64String(cipherTextBytes);
    }
    public static string DecryptString(this string textHash, string key = "@#$_Jabbekhaneh7091")
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] cipherTextBytes = Convert.FromBase64String(textHash);
        PasswordDeriveBytes password = new PasswordDeriveBytes(key, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }
}
