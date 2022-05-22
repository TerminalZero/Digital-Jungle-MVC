internal class Digital_Jungle_Startup
{
    readonly IWebHostEnvironment _environment;

    public Digital_Jungle_Startup(IWebHostEnvironment environment){
        _environment = environment;
    }

    public void Configure(IApplicationBuilder app)
    {
        if (!_environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        
        app.UseEndpoints(configure => {
            configure.MapControllerRoute("default", "/{action=Index}", new {controller = "Home"});
        });
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
    }
}