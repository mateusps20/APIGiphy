using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TelaDeLogin
{
    public class Startup
    {
        // Este método é chamado pelo tempo de execução.Use este método para adicionar serviços ao contêiner.
        // Para obter mais informações sobre como configurar seu aplicativo, visite https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddHttpClient("giphy", c => {
                c.BaseAddress = new Uri("https://api.giphy.com/v1/gifs/");
            });
        }

        // Este método é chamado pelo tempo de execução.Use este método para configurar o pipeline de solicitação HTTP.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default",
                    template: "{controller=Usuario}/{action=Index}/{id?}");
            });
        }
    }
}
