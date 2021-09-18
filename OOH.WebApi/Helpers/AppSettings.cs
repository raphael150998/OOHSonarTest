using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Helpers
{
    public class AppSettings
    {
        public Serilog Serilog { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class Serilog
    {
        public Minimumlevel MinimumLevel { get; set; }
        public List<Writeto> WriteTo { get; set; }
        public List<string> Enrich { get; set; }
        public Properties Properties { get; set; }
    }

    public class Minimumlevel
    {
        public string Default { get; set; }
        public Override Override { get; set; }
    }

    public class Override
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class Properties
    {
        public string Application { get; set; }
    }

    public class Writeto
    {
        public string Name { get; set; }
        public Args Args { get; set; }
    }

    public class Args
    {
        public Configurelogger configureLogger { get; set; }
    }

    public class Configurelogger
    {
        public List<Filter> Filter { get; set; }
        public List<Writeto1> WriteTo { get; set; }
    }

    public class Filter
    {
        public string Name { get; set; }
        public Args1 Args { get; set; }
    }

    public class Args1
    {
        public string expression { get; set; }
    }

    public class Writeto1
    {
        public string Name { get; set; }
        public Args2 Args { get; set; }
    }

    public class Args2
    {
        public string databaseUrl { get; set; }
        public string collectionName { get; set; }
        public string cappedMaxSizeMb { get; set; }
        public string cappedMaxDocuments { get; set; }
    }

}
