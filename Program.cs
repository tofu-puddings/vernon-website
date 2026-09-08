var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
// We change 'AddRazorPages' to 'AddControllersWithViews'
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 2. Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 3. MAP THE CONTROLLER (Crucial Step)
// This tells the app that "Home" means "HomeController"
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();