using ForTestWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

builder.Services.AddDbContext<PersonContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("PersonConnection"));
});
var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", (PersonContext db) => db?.People?.ToList());

app.MapGet("/api/users/{id}", (string id, PersonContext db) =>
{
    // получаем пользовател€ по id
    Person? user = db?.People?.FirstOrDefault(i => i.Id == id);
    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (user == null)
        return Results.NotFound(new { message = "ѕользователь не найден" });
    // если пользователь найден, отправл€ем его
    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", (string id, PersonContext db) =>
{
    // получаем пользовател€ по id
    Person? user = db?.People?.FirstOrDefault(i => i.Id == id);
    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (user == null)
    {
        return Task.FromResult(Results.NotFound("ѕользователь не найден"));
    }
    // если пользователь найден, удал€ем его
    db?.People?.Remove(user);
    db?.SaveChanges();
    return Task.FromResult(Results.Json(user));
});

app.MapPost("/api/users", (Person user, PersonContext db) =>
{
    user.Id = Guid.NewGuid().ToString();
    // добавл€ем пользовател€ в список
    db.Add(user);
    db.SaveChanges();
    return user;
});

app.MapPut("/api/users", (Person userData, PersonContext db) =>
{    // получаем пользовател€ по id
    var user = db?.People?.FirstOrDefault(i => i.Id == userData.Id);
    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (user == null)
        return Results.NotFound(new { message = "ѕользователь не найден" });
    // если пользователь найден, измен€ем его данные и отправл€ем обратно клиенту
    user.Name = userData.Name;
    user.Age = userData.Age;
    db?.SaveChanges();
    return Results.Json(user);
});

app.Run();