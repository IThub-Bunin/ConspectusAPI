using Microsoft.AspNetCore.Mvc;
using System.IO;

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

    [HttpGet("isitworks_qm")]
    public string IsItWork()
    {
        return "Hello! Conspectus api works!";
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
            return Ok(string.Join(',', await _storageController.GetConspectsNameByUser(name)));
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
        Console.WriteLine($"Path: {_storageController._storagePath + user + '/' + conspect + ".md"}");


        return NotFound();
    }
}
