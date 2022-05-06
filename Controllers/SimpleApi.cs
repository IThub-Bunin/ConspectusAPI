using Microsoft.AspNetCore.Mvc;

namespace ConspectusAPI.Controllers;

[ApiController]
[Route("api")]
public class SimpleApi : ControllerBase
{
    private readonly ILogger<SimpleApi> _logger;
    private readonly IStorageController _storageController;

    public SimpleApi(ILogger<SimpleApi> logger, IStorageController storageController)
    {
        _logger = logger;
        _storageController = storageController;
    }

    [HttpGet("UserExist/{name}")]
    public async ValueTask<bool> UserExist(string name)
    {
        bool result = await _storageController.UserExist(name);
        return result;
    }

    [HttpGet("GetConspectsOfUser/{name}")]
    public async ValueTask<IActionResult> GetConspectsOfUser(string name)
    {
        
        if (await _storageController.UserExist(name))
        {
            return Ok(string.Join('>', await _storageController.GetConspectsNameByUser(name)));
        }

        return NotFound();
    }

    [HttpGet("GetConspectData/{user}/{conspect}")]
    public async ValueTask<IActionResult> GetConspectData(string user, string conspect)
    {
        if (await _storageController.UserExist(user) && await _storageController.ConspectExist(user, conspect))
        {
            return Ok(await _storageController.GetConspectData(user, conspect));
        }

        Console.WriteLine($"User {user} - {await _storageController.UserExist(user)}");
        Console.WriteLine($"User {conspect} - {await _storageController.ConspectExist(user, conspect)}");
        Console.WriteLine($"Path: {_storageController.StoragePath + user + '/' + conspect + ".md"}");


        return NotFound();
    }

    [HttpPost("SetConspectData/{user}/{conspect}/{data}")]
    public async void SetConspectData(string user, string conspect, string data)
    {
        if (await _storageController.ConspectExist(user, conspect))
        {
            _storageController.SetConspectData(user, conspect, data);
        }
    }

    // TODO: CreateUser & CreateConspect
    [HttpPost("CreateUser/{user}")]
    public async void CreateUser(string user)
    {
        await _storageController.CreateUser(user);
    }

    [HttpPost("CreateConspect/{user}/{conspect}")]
    public async void CreateConspect(string user, string conspect)
    {
        await _storageController.CreateConspect(user, conspect);
    }
}