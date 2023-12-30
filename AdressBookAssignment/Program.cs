using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AddressBookAssignment.Services;
using AddressBookAssignment.Interfaces;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddSingleton<IMenuService, MenuService>();
    services.AddSingleton<IFileService>(x =>
    new FileService(@"C:\EC\CSharp\AdressBookAssignment\AdressBookAssignment\contacts.json")
    );
    services.AddSingleton<IContactService, ContactService>();
}).Build();

builder.Start();

var menuService = builder.Services.GetRequiredService<IMenuService>();
menuService.ShowMainMenu();



