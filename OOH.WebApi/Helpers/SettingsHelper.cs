using Microsoft.AspNetCore.Hosting;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Helpers
{
    public class SettingsHelper
    {
        public static void AddOrUpdateAppSetting<T>(T value, IWebHostEnvironment webHostEnvironment)
        {
            try
            {
                var settingFiles = new List<string> { "appsettings.json"};
                //var settingFiles = new List<string> { "appsettings.json", $"appsettings.{webHostEnvironment.EnvironmentName}.json" };
                foreach (var item in settingFiles)
                {


                    var filePath = Path.Combine(AppContext.BaseDirectory, item);
                    string json = File.ReadAllText(filePath);
                    AppSettings jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<AppSettings>(json);

                    Writeto newEmpresa = new Writeto()
                    {
                        Name = "Logger",
                        Args = new Args()
                        {
                            configureLogger = new Configurelogger()
                            {
                                Filter = new List<Filter>()
                                {
                                    new Filter()
                                    {
                                        Name = "ByIncludingOnly",
                                        Args = new Args1()
                                        {
                                            expression = "(@Properties['Empresa'] = 3)"
                                        }
                                    }
                                },
                                WriteTo = new List<Writeto1>()
                                {
                                    new Writeto1()
                                    {
                                        Name = "MongoDBBson",
                                        Args = new Args2()
                                        {
                                            databaseUrl = "mongodb://localhost/LogOOH",
                                            collectionName = "Logs3",
                                            cappedMaxSizeMb = "50000",
                                            cappedMaxDocuments ="1000000"
                                        }
                                    }
                                }
                            }
                        }
                    };

                    jsonObj.Serilog.WriteTo.Add(newEmpresa);

                    //SetValueRecursively(jsonObj, value);

                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(filePath, output);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error writing app settings | {ex.Message}", ex);
            }
        }



        private static void SetValueRecursively<T>(dynamic jsonObj, T value)
        {
            var properties = value.GetType().GetProperties();
            foreach (var property in properties)
            {
                var currentValue = property.GetValue(value);
                if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string) || property.PropertyType == typeof(decimal))
                {
                    if (currentValue == null) continue;
                    try
                    {
                        jsonObj[property.Name].Value = currentValue;

                    }
                    catch (RuntimeBinderException)
                    {
                        jsonObj[property.Name] = new JValue(currentValue);


                    }
                    continue;
                }
                try
                {
                    if (jsonObj[property.Name] == null)
                    {
                        jsonObj[property.Name] = new JObject();
                    }

                }
                catch (RuntimeBinderException)
                {
                    jsonObj[property.Name] = new JObject(new JProperty(property.Name));

                }
                SetValueRecursively(jsonObj[property.Name], currentValue);
            }


        }
    }
}
