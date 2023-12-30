

using System.Diagnostics;

namespace AddressBookAssignment.Services;

public interface IFileService
{
    bool SaveContentToFile(string content);
    string GetContentFromFile();
}

public class FileService(string filePath) : IFileService
{
    private readonly string _filePath = filePath;

    /// <summary>
    /// Hämtar innehåll från filen
    /// </summary>
    public string GetContentFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using (var sr = new StreamReader(_filePath))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
    /// <summary>
    /// Sparar saker till filen
    /// </summary>
    public bool SaveContentToFile(string content)
    {
        try
        {
            if (content != null)
            {
                using (var sw = new StreamWriter(_filePath))
                {
                    sw.WriteLine(content);
                }
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

}
