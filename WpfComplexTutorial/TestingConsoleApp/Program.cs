
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;

IDataService<User> userService = new GenericDataService<User>(new SimpleTraderDbContextFactory());
Console.WriteLine(userService.GetAll().Result.Count());
//Console.WriteLine(userService.GetById(1).Result.Username);

//userService.Create(new User { Username = "Test" }).Wait();
