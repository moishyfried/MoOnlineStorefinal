using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.EntityClasses.OrderAggregate;

namespace MoOnlineStore.Infrastructure
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {

            
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (!context.ProductsBrands.Any())
                {
                    var brandsData =
                        File.ReadAllText(path + @"/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.ProductsBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductsTypes.Any())
                {
                    var typesData =
                        File.ReadAllText(path + @"/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductsTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData =
                        File.ReadAllText(path + @"/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Products>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.DeliveryMethods.Any())
                {
                    var productsData =
                        File.ReadAllText(path + @"Data/SeedData/delivery.json");

                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(productsData);

                    foreach (var item in methods)
                    {
                        context.DeliveryMethods.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
    }
