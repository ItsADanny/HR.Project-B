namespace RSS.ILanguageInterface;
//interfaces

public interface ILanguageInterface
{
    void SetCulture(string Culture);
    string GetString(string key);
    void Display();
}


