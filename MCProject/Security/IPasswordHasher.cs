namespace MCProject.Security;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Test(string password, string hash);

}
