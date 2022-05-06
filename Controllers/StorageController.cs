namespace ConspectusAPI.Controllers;

public class StorageController : IStorageController
{
    public string StoragePath { get; private set; }

    public StorageController()
    {
        string currentPath = Directory.GetCurrentDirectory();
        StoragePath = $"{currentPath}/Storage/";

        if (!Directory.Exists(StoragePath))
            Directory.CreateDirectory(StoragePath);
    }

    public async Task<List<string>> GetUsersName() =>
        await Task.Run(() => Directory.GetDirectories(StoragePath).ToList());

    public async Task<bool> UserExist(string userName) =>
        await Task.Run(() => Directory.Exists(StoragePath + userName));

    public async Task<List<string>> GetConspectsNameByUser(string userName)
    {
        List<string> result = await Task.Run(() =>
            Directory.GetFiles(StoragePath + userName, "*.md").ToList()
        );

        return (from r in result select r.Replace('\\', '/')[r.LastIndexOf('/')..]).ToList();
    }

    public async Task<bool> ConspectExist(string userName, string conspectName) =>
        await Task.Run(() => File.Exists(StoragePath + userName + '/' + conspectName + ".md"));

    public async Task<string> GetConspectData(string userName, string conspectName)
    {
        string filePath = StoragePath + userName + '/' + conspectName + ".md";

        return await Task.Run(() =>
        {
            using var sr = new StreamReader(new BufferedStream(new FileStream(filePath, FileMode.Open)));
            return sr.ReadToEnd();
        });
    }

    public async void SetConspectData(string userName, string conspectName, string data)
    {
        string filePath = StoragePath + userName + '/' + conspectName + ".md";

        using var sw = new StreamWriter(filePath);
        await sw.WriteAsync(data);
    }

    public async Task CreateUser(string name)
    {
        await Task.Run(() => Directory.CreateDirectory(StoragePath + name));
    }

    public async Task CreateConspect(string userName, string conspectName)
    {
        await Task.Run(() => File.Create(StoragePath + userName + "/" + conspectName));
    }

    class FileDoesNotExistException : SystemException
    {
        public override string Message => "File does not exist!\nUse 'ConspectExist' method for checking file.";
    }
}