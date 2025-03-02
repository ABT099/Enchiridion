using System.Security.Cryptography;

namespace Enchiridion.Api;

public static class KeyGen
{
    public static void Invoke()
    {
        var rsaKey = RSA.Create();
        var privateKey = rsaKey.ExportRSAPrivateKey();
        File.WriteAllBytes("key", privateKey);
    }
}