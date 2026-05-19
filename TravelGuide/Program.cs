using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=travelguide.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cities}/{action=Index}/{id?}");

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();
    
    // Seed data if empty
    if (!context.Cities.Any())
    {
        var moscow = new City
        {
            Name = "Москва",
            Region = "Центральный федеральный округ",
            Population = 13104000,
            History = "Москва — столица Российской Федерации, город федерального значения, административный центр Центрального федерального округа и Московской области. Крупнейший по численности населения город России и её субъект.",
            CoatOfArms = "/images/moscow_coat.png",
            PhotoUrl = "/images/moscow.jpg"
        };

        var spb = new City
        {
            Name = "Санкт-Петербург",
            Region = "Северо-Западный федеральный округ",
            Population = 5601911,
            History = "Санкт-Петербург — город федерального значения Российской Федерации, административный центр Северо-Западного федерального округа. Основан в 1703 году Петром I.",
            CoatOfArms = "/images/spb_coat.png",
            PhotoUrl = "/images/spb.jpg"
        };

        var kazan = new City
        {
            Name = "Казань",
            Region = "Приволжский федеральный округ",
            Population = 1257391,
            History = "Казань — столица Республики Татарстан, крупный порт на левом берегу реки Волги. Один из крупнейших религиозных, экономических, политических, научных, образовательных, культурных и спортивных центров России.",
            CoatOfArms = "/images/kazan_coat.png",
            PhotoUrl = "/images/kazan.jpg"
        };

        context.Cities.AddRange(moscow, spb, kazan);

        context.Attractions.AddRange(
            new Attraction
            {
                Name = "Красная площадь",
                Description = "Главная площадь Москвы, расположенная в центре радиально-кольцевой планировки города между Китай-городом и Кремлём.",
                History = "Красная площадь возникла в конце XV века. На протяжении своей истории площадь была свидетелем многих важнейших событий в истории государства.",
                PhotoUrl = "/images/red_square.jpg",
                WorkingHours = "Круглосуточно",
                Price = 0,
                City = moscow
            },
            new Attraction
            {
                Name = "Московский Кремль",
                Description = "Крепость в центре Москвы и древнейшая часть города, главный общественно-политический и историко-художественный комплекс столицы.",
                History = "Первые укрепления на Боровицком холме появились в XII веке. Современный ансамбль Кремля сложился в конце XV — начале XVI века.",
                PhotoUrl = "/images/kremlin.jpg",
                WorkingHours = "10:00 - 17:00",
                Price = 700,
                City = moscow
            },
            new Attraction
            {
                Name = "Эрмитаж",
                Description = "Один из крупнейших и значительных художественных и культурно-исторических музеев России и мира.",
                History = "Основан в 1764 году императрицей Екатериной II как частное собрание живописи. Главный музейный комплекс расположен в Зимнем дворце.",
                PhotoUrl = "/images/hermitage.jpg",
                WorkingHours = "10:30 - 18:00",
                Price = 500,
                City = spb
            },
            new Attraction
            {
                Name = "Храм Спаса на Крови",
                Description = "Православный храм-памятник в Санкт-Петербурге, один из крупнейших музеев мозаики в России и мире.",
                History = "Построен в память о трагической гибели императора Александра II. Освящён в 1907 году.",
                PhotoUrl = "/images/savior_blood.jpg",
                WorkingHours = "10:30 - 18:00",
                Price = 450,
                City = spb
            },
            new Attraction
            {
                Name = "Казанский Кремль",
                Description = "Древнейшая часть Казани, главный архитектурный ансамбль города, объект Всемирного наследия ЮНЕСКО.",
                History = "История Казанского Кремля начинается с X века. Современный облик кремль приобрёл в XVI-XIX веках.",
                PhotoUrl = "/images/kazan_kremlin.jpg",
                WorkingHours = "08:00 - 22:00",
                Price = 0,
                City = kazan
            },
            new Attraction
            {
                Name = "Мечеть Кул-Шариф",
                Description = "Главная мечеть Республики Татарстан и Казани, расположена на территории Казанского Кремля.",
                History = "Названа в честь имама Кул Шарифа, погибшего при защите Казани в 1552 году. Восстановлена в 2005 году.",
                PhotoUrl = "/images/kul_sharif.jpg",
                WorkingHours = "09:00 - 20:00",
                Price = 0,
                City = kazan
            }
        );

        await context.SaveChangesAsync();
    }
}

app.Run();
