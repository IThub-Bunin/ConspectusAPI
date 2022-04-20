namespace ConspectusAPI.Controllers;

public class StorageController : IStorageController
{
    public string _storagePath { get; set; }

    public StorageController()
    {
        string currentPath = Directory.GetCurrentDirectory();
        _storagePath = $"{currentPath}/Storage/";

        if (!Directory.Exists(_storagePath))
            Directory.CreateDirectory(_storagePath);
    }

    public async Task<List<string>> GetUsersName() =>
        await Task.Run(() => Directory.GetDirectories(_storagePath).ToList());

    public async Task<bool> UserExist(string userName) =>
        await Task.Run(() => Directory.Exists(_storagePath + userName));

    public async Task<List<string>> GetConspectsNameByUser(string userName)
    {
        List<string> result = await Task.Run(() =>
            Directory.GetFiles(_storagePath + userName, "*.md").ToList()
        );

        return (from r in result select r.Replace('\\', '/').Substring(r.LastIndexOf('/'))).ToList();
    }

    public async Task<bool> ConspectExist(string userName, string conspectName) =>
        await Task.Run(() => File.Exists(_storagePath + userName + '/' + conspectName + ".md"));

    //private async Task<bool> ConspectExist(string filePath) =>
    //    await Task.Run(() => Directory.Exists(filePath));

    public async Task<string> GetConspectData(string userName, string conspectName)
    {
        string filePath = _storagePath + userName + '/' + conspectName + ".md";

        try
        {
            return await Task.Run(() =>
            {
                using (var sr = new StreamReader(new BufferedStream(new FileStream(filePath, FileMode.Open))))
                {
                    return sr.ReadToEnd();
                }
            });
        }
        catch
        {
            throw new FileDoesNotExistException();
        }
        
    }

    
    class FileDoesNotExistException : SystemException
    {
        public override string Message => "File does not exist!\nUse 'ConspectExist' method for checking file.";
    }
}