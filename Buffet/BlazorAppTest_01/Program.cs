using BlazorAppTest_01.Areas.Identity;
using BlazorAppTest_01.Areas.Identity.Models;
using BlazorAppTest_01.Areas.Identity.Services;
using BlazorAppTest_01.Data;
using BlazorAppTest_01.Models.Candidates;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


//���ο� DbContext �߰�
//builder.Services.AddDbContext<CandidateAppDbContext>(options =>
//    options.UseSqlServer(connectionString));
builder.Services.AddDbContextFactory<CandidateAppDbContext>(options =>
    options.UseSqlServer(connectionString)); 

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
builder.Services.AddSingleton<WeatherForecastService>();

// ���Ӽ� ���� �߰�
builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

// ���� ȯ�濡�� Update-Database, Seed ������ �߰�
if (app.Environment.IsDevelopment())
{
    await CheckCandidateDbMigrated(app.Services);
    CandidateSeedData(app);
    CandidateDbInitializer.Initialize(app);
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

#region CandidateSeedData: Candidates ���̺��� �⺻ ������ �Է�
// Candidates ���̺��� �⺻ ������ �Է�
static void CandidateSeedData(WebApplication app)
{
    // https://docs.microsoft.com/ko-kr/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0#resolve-a-service-at-app-start-up
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;

        var candidateDbContext = services.GetRequiredService<CandidateAppDbContext>();

        // Candidates ���̺��� �����Ͱ� ���� ������ ������ �Է�
        if (!candidateDbContext.Candidates.Any())
        {
            candidateDbContext.Candidates.Add(
                new Candidate { FirstName = "�浿", LastName = "ȫ", IsEnrollment = false });
            candidateDbContext.Candidates.Add(
                new Candidate { FirstName = "�λ�", LastName = "��", IsEnrollment = false });

            candidateDbContext.SaveChanges();
        }
    }
}
#endregion

#region CheckCandidateDbMigrated: �����ͺ��̽� ���̱׷��̼� ����
// �����ͺ��̽� ���̱׷��̼� ����
async Task CheckCandidateDbMigrated(IServiceProvider services)
{
    using var scope = services.CreateScope();

    using var candidateContext = scope.ServiceProvider.GetService<CandidateAppDbContext>();
    if (candidateContext is not null)
    {
        await candidateContext.Database.MigrateAsync();
    }
} 
#endregion