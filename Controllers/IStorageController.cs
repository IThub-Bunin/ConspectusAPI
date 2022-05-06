namespace ConspectusAPI.Controllers;

public interface IStorageController
{
    public string StoragePath { get; }

    public Task<List<string>> GetUsersName();
    public Task<List<string>> GetConspectsNameByUser(string userName);
    public Task<bool> UserExist(string userName);
    public Task<bool> ConspectExist(string userName, string conspectName);
    public Task<string> GetConspectData(string userName, string conspectName);
    public void SetConspectData(string userName, string conspectName, string data);
    public Task CreateUser(string name);
    public Task CreateConspect(string userName, string name);
}