using KutuphaneYonetim.Data;
using Microsoft.EntityFrameworkCore;
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// DbContext - PostgreSQL yapýlandýrmasý
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

// --- OTOMATÝK MÝGRASYON EKLEMESÝ ---
// Uygulama her baþladýðýnda eksik tablolarý veritabanýna ekler
// Veritabanýný Hazýrla (Migrations ve Tablo Oluþturma)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<KutuphaneYonetim.Data.LibraryContext>();

        // 1. Önce tablolarýn varlýðýný kontrol et ve yoksa oluþtur
        context.Database.EnsureCreated();

        // 2. Ardýndan varsa migrasyonlarý uygula
        context.Database.Migrate();

        Console.WriteLine("Veritabaný ve tablolar baþarýyla hazýrlandý.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Veritabaný hazýrlanýrken hata: {ex.Message}");
    }
}
// ----------------------------------

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();