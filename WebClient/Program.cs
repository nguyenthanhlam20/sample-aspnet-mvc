using WebClient.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
// Services
builder.Services.AddScoped<ClientService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configure role-based routing
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
       name: "Admin",
       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
   );

    endpoints.MapControllerRoute(
       name: "User",
       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
   );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );
});

app.Run();
