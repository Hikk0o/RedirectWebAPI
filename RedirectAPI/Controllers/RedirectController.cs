﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RedirectAPI.Data;

namespace RedirectAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class RedirectController : ControllerBase
{
    private readonly ApplicationContext _db;
    public RedirectController(ApplicationContext db)
    {
        _db = db;
    }
    
    // Get для получения длинной ссылки из короткой
    [HttpGet, Route("{shortUrl}")]
    public string GetUrl(string shortUrl)
    {
        return RedirectAPI.Redirect.GetUrl(_db, shortUrl);
    }
    
    // Get для получения файла из короткой ссылки
    [HttpGet, Route("img/{shortUrl}")]
    public string GetImg(string shortUrl)
    {
        return RedirectAPI.Redirect.GetImg(_db, shortUrl);
    }
    
    // Post для добавления новой длинной ссылки, вернет короткую
    [HttpPost]
    public string PostUrl([FromBody, Required] string r)
    {
        var remoteIp = Request.HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        var user = _db.Users.FirstOrDefault(userDb => userDb.Ip == remoteIp);
        if (user != null) return RedirectAPI.Redirect.AddUrl(_db, r, user);
        {
            user = new User { Ip = remoteIp };
            _db.Users.Add(user);
            _db.SaveChanges();
            user = _db.Users.FirstOrDefault(userDb => userDb.Ip == remoteIp)!;
            return RedirectAPI.Redirect.AddUrl(_db, r, user);
        }
    
        
    }
    
    [HttpPost, Route("img/")]
    public string PostImg([FromBody, Required] string data)
    {
        var remoteIp = Request.HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        Console.WriteLine(remoteIp);
        var user = _db.Users.FirstOrDefault(userDb => userDb.Ip == remoteIp);
        if (user != null) return RedirectAPI.Redirect.AddImg(_db, data, user);
        {
            Console.WriteLine("user null");
            user = new User { Ip = remoteIp };
            _db.Users.Add(user);
            _db.SaveChanges();
            user = _db.Users.FirstOrDefault(userDb => userDb.Ip == remoteIp)!;
            return RedirectAPI.Redirect.AddImg(_db, data, user);
        }
    }

}