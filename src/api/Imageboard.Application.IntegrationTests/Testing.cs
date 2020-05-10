using Imageboard.Api;
using Imageboard.Domain.Entities;
using Imageboard.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.CompilerServices;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imageboard.Application.IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        public static IConfigurationRoot Configuration { get; private set; }
        private static IServiceScopeFactory _scopeFactory;

        private static ServiceProvider _serviceProvider;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var startup = new Startup(Configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "Imageboard.Api"));

            services.AddLogging();

            startup.ConfigureServices(services);

            services.AddScoped<IConfiguration>(provider => Configuration);

            _serviceProvider = services.BuildServiceProvider();
            _scopeFactory = _serviceProvider.GetService<IServiceScopeFactory>();

            RecreateDatabase();
        }

        private static void RecreateDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ImageboardDbContext>();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        public static TService GetService<TService>()
        {
            return _serviceProvider.GetRequiredService<TService>();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public static void ResetState()
        {
            RecreateDatabase();

            var filesDirectory = Configuration.GetValue<string>("FilesFolder");
            if (Directory.Exists(filesDirectory))
            {
                Directory.Delete(filesDirectory, true);
            }
        }

        public static async Task<TEntity> FindAsync<TEntity>(object id)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ImageboardDbContext>();

            return await context.FindAsync<TEntity>(id);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ImageboardDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task SeedTestData()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ImageboardDbContext>();

            var now = DateTime.UtcNow;

            context.Groups.Add(
                        new Group()
                        {
                            SortOrder = 1,
                            Title = "Test Group 1",
                            Description = "",
                            Boards =
                            {
                                new Board()
                                {
                                    SortOrder = 1,
                                    Title = "Test Board 1",
                                    ShortUrl = "tb1",
                                    Description = "Description for Test Board 1",
                                    Topics =
                                    {
                                        new Topic()
                                        {
                                            LastUpdated = now,
                                            Created = now,
                                            Title = "Test Post 1 Title",
                                            Signature = "Signature",
                                            Posts =
                                            {
                                                new Post()
                                                {
                                                    
                                                    Text = "Test Post 1 Text",
                                                    Signature = "Signature",
                                                    IsOp = true,
                                                    Created = now,
                                                },
                                                new Post()
                                                {
                                                    Text = "Test Post 2 Text",
                                                    IsOp = false,
                                                    Created = now
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    );

            await context.SaveChangesAsync();

            var reply = new Post()
            {
                ParentId = 1,
                Text = "Test Post 2 Text",
                Created = now,
                TopicId = 1
            };

            context.Posts.Add(reply);

            await context.SaveChangesAsync();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {

        }
    }
}
