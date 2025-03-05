using System.Security.Cryptography;

namespace Enchiridion.Api;

public struct EnchiridionConstants
{
    public struct Roles
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);
    }

    public struct Claims
    {
        public const string AuthId = nameof(AuthId);
        public const string Role = nameof(Role);
    }

    public static class Keys
    {
        private static readonly RSA Rsa;

        static Keys()
        {
            Rsa = RSA.Create();
            var privateKeyBytes = File.ReadAllBytes("key");
            Rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
        }

        public static RSA RsaKey => Rsa;
    }

    public static readonly HashSet<string> BlackList = [];
}