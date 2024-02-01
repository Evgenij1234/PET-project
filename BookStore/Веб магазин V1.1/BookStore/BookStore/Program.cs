using BookStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

//�� �������
var builder = WebApplication.CreateBuilder(args);
//�� �������



/*������� ������*/
// ��������� ������� ������
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
/*������� ������*/







/*������� ��*/
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContextAutors>(options => options.UseSqlServer(connection));//������� ������
builder.Services.AddDbContext<ApplicationContextBooks>(options => options.UseSqlServer(connection));//������� �����
builder.Services.AddDbContext<ApplicationContextUsers>(options => options.UseSqlServer(connection));//������� ������������
builder.Services.AddDbContext<ApplicationContextFavorites>(options => options.UseSqlServer(connection));//������� �������� ����������
builder.Services.AddDbContext<ApplicationContextCart>(options => options.UseSqlServer(connection));//������� �������� �������
builder.Services.AddDbContext<ApplicationContextPlace>(options => options.UseSqlServer(connection));//������� �������� �����
/*������� ��*/

builder.Services.AddControllersWithViews();
var app = builder.Build();
//�� �������

//����������� �����
app.UseDefaultFiles();
app.UseStaticFiles();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseRouting();
//����������� � ����
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();   // ��������� middleware ��� ������ � ��������
//����������� � ����

//����������� �������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Main_page}/{id?}");
//�� �������
/*�� �������*/
app.Run();
/*�� �������*/