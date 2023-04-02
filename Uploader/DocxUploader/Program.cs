using DocxUploader.Configuration;
using DocxUploader.Options;
using DocxUploader.Services;
using DocxUploader.Services.Abstract;
using DocxUploader.Services.Concrete;


namespace DocxUploader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            var services = builder.Services;
            services.Configure<AzureOptions>(builder.Configuration.GetSection("Azure"));

            services.AddTransient<IFileService, FileService>();
            services.AddTransient<JsonFileMailService>();
            services.AddTransient<ISendEmailService, SendEmailService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}