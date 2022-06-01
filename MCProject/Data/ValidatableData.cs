namespace MCProject.Data;

public abstract class ValidatableData
{
    public string? Error { get; set; }

    public bool ValidateAndFix()
    {
        Error = ValidateFixAndGetError();
        return Error == null;
    }
    protected abstract string? ValidateFixAndGetError();
}
