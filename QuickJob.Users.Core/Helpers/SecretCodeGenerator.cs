using System.Security.Cryptography;

namespace Users.Service.Helpers;

internal static class SecretCodeGenerator
{
    private static readonly RandomNumberGenerator Random = new RNGCryptoServiceProvider();
    
    public static string GenerateCode(int codeLength = 4)
    {
        if (codeLength is < 1 or > 10)
            throw new ArgumentException("The code length must be between 1 and 10.", nameof(codeLength));
        var b = new byte[4];
        Random.GetBytes(b);
        var minValue = (int)Math.Pow(10, codeLength - 1);
        var maxValue = minValue * 10 - 1;
        return Convert.ToString(BitConverter.ToUInt32(b, 0) % (maxValue - minValue + 1) + minValue);
    }
}