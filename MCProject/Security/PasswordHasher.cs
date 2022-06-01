using System.Security.Cryptography;

namespace MCProject.Security;

public class PasswordHasher : IPasswordHasher
{
    public const string Splitter = ".";
    public readonly HashAlgorithmName AlgorithmName;
    public readonly int SaltSize;
    public readonly int KeySize;
    public readonly int Iterations;

    public PasswordHasher(HashAlgorithmName algorithmName, int saltSize = 16, int keySize = 32, int iterations = 256)
    {
        AlgorithmName = algorithmName;
        SaltSize = saltSize;
        KeySize = keySize;
        Iterations = iterations;
    }
    public PasswordHasher(int saltSize = 16, int keySize = 32, int iterations = 256) :
        this(HashAlgorithmName.SHA256, saltSize, keySize, iterations)
    {
        
    }

    public string Hash(string password)
    {
        using Rfc2898DeriveBytes algorithm = new(password, SaltSize, Iterations, AlgorithmName);
        var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{Iterations}{Splitter}{salt}{Splitter}{key}";
    }

    public bool Test(string password, string hash)
    {
        var splits = hash.Split(Splitter);
        if(splits.Length != 3)
            throw new FormatException($"{nameof(hash)} has wrong format(must have 2 seprators)");
        if(int.TryParse(splits[0], out int iterations) == false)
            throw new FormatException($"{nameof(hash)} has wrong format(iterations isn't an integer)");

        var salt = Convert.FromBase64String(splits[1]);
        var key = Convert.FromBase64String(splits[2]);

        using Rfc2898DeriveBytes algorithm = new(password, salt, iterations, AlgorithmName);

        var realKey = algorithm.GetBytes(KeySize);

        return realKey.SequenceEqual(key);
    }
}
