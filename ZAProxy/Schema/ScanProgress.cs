using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAProxy.Schema
{
    [JsonConverter(typeof(ScanProgressConverter))]
    public class ScanProgress
    {
        public IEnumerable<HostProcess> HostProcesses { get; set; }

        public class HostProcess
        {
            public string Host { get; set; }
            public IEnumerable<Plugin> Plugins { get; set; }

            public class Plugin
            {
                public string Name { get; set; }
                public int Id { get; set; }
                public string Status { get; set; }
                public int TimeInMs { get; set; }
            }
        }
    }

    public class ScanProgressConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ScanProgress);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var scanProgress = new ScanProgress();
            var scanProgressHostProcesses = new List<ScanProgress.HostProcess>();

            scanProgress.HostProcesses = scanProgressHostProcesses;

            var hostProcessesArray = JArray.Load(reader);
            for (int i = 0; i < hostProcessesArray.Count; i = i + 2)
            {
                var hostProcess = new ScanProgress.HostProcess();
                var hostProcessPlugins = new List<ScanProgress.HostProcess.Plugin>();
                scanProgressHostProcesses.Add(hostProcess);

                hostProcess.Host = hostProcessesArray[i].Value<string>();
                hostProcess.Plugins = hostProcessPlugins;

                var pluginsArray = hostProcessesArray[i + 1].Value<JArray>("HostProcess");
                foreach (var pluginValues in pluginsArray.Select(obj => obj.Value<JArray>("Plugin")))
                {
                    hostProcessPlugins.Add(new ScanProgress.HostProcess.Plugin
                    {
                        Name = pluginValues[0].Value<string>(),
                        Id = pluginValues[1].Value<int>(),
                        Status = pluginValues[2].Value<string>(),
                        TimeInMs = pluginValues[3].Value<int>()
                    });
                }
            }

            return scanProgress;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var scanProgress = (ScanProgress)value;
            var hostProcessesArray = new JArray();

            foreach (var hostProcess in scanProgress.HostProcesses)
            {
                var pluginsArray = new JArray();

                hostProcessesArray.Add(hostProcess.Host);
                hostProcessesArray.Add(new JObject(
                    new JProperty("HostProcess", pluginsArray)));

                foreach (var plugin in hostProcess.Plugins)
                {
                    pluginsArray.Add(new JObject(
                        new JProperty("Plugin", new JArray(
                            plugin.Name,
                            plugin.Id,
                            plugin.Status,
                            plugin.TimeInMs))));
                }
            }

            hostProcessesArray.WriteTo(writer);
        }
    }
}
