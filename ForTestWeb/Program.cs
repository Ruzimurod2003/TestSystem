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
    // �������� ������������ �� id
    Person? user = db?.People?.FirstOrDefault(i => i.Id == id);
    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (user == null)
        return Results.NotFound(new { message = "������������ �� ������" });
    // ���� ������������ ������, ���������� ���
    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", (string id, PersonContext db) =>
{
    // �������� ������������ �� id
    Person? user = db?.People?.FirstOrDefault(i => i.Id == id);
    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (user == null)
    {
        return Task.FromResult(Results.NotFound("������������ �� ������"));
    }
    // ���� ������������ ������, ������� ���
    db?.People?.Remove(user);
    db?.SaveChanges();
    return Task.FromResult(Results.Json(user));
});

app.MapPost("/api/users", (Person user, PersonContext db) =>
{
    user.Id = Guid.NewGuid().ToString();
    // ��������� ������������ � ������
    db.Add(user);
    db.SaveChanges();
    return user;
});

app.MapPut("/api/users", (Person userData, PersonContext db) =>
{    // �������� ������������ �� id
    var user = db?.People?.FirstOrDefault(i => i.Id == userData.Id);
    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (user == null)
        return Results.NotFound(new { message = "������������ �� ������" });
    // ���� ������������ ������, �������� ��� ������ � ���������� ������� �������
    user.Name = userData.Name;
    user.Age = userData.Age;
    db?.SaveChanges();
    return Results.Json(user);
});

app.Run();