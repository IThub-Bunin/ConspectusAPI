namespace ConspectusAPI.Controllers;

public interface IStorageController
{
    public string _storagePath { get; set; }

    public Task<List<string>> GetUsersName();
    public Task<List<string>> GetConspectsNameByUser(string userName);
    public Task<bool> UserExist(string userName);
    public Task<bool> ConspectExist(string userName, string conspectName);
    public Task<string> GetConspectData(string userName, string conspectName);
}