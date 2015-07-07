using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZAProxy.Schema
{
    /// <summary>
    /// Describes the progress of an active scan.
    /// </summary>
    [JsonConverter(typeof(Converter))]
    public class ActiveScanProgress
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="ActiveScanProgress"/> class.
        /// </summary>
        public ActiveScanProgress()
        {
            HostProcesses = new List<HostProcess>();
        }

        /// <summary>
        /// Gets or sets set of processes (per host) that are performing a scan.
        /// </summary>
        public IList<HostProcess> HostProcesses { get; set; }

        /// <summary>
        /// Describes the scan process for a specific host.
        /// </summary>
        public class HostProcess
        {
            /// <summary>
            /// Initiates a new instance of the <see cref="HostProcess"/> class.
            /// </summary>
            public HostProcess()
            {
                Plugins = new List<Plugin>();
            }

            /// <summary>
            /// Gets or sets the host.
            /// </summary>
            public string Host { get; set; }

            /// <summary>
            /// Gets or sets set of plugins used in the scan.
            /// </summary>
            public IList<Plugin> Plugins { get; set; }

            /// <summary>
            /// Describes a plugin used in a scan.
            /// </summary>
            public class Plugin
            {
                /// <summary>
                /// Gets or sets the name.
                /// </summary>
                public string Name { get; set; }

                /// <summary>
                /// Gets or sets the ID.
                /// </summary>
                public int Id { get; set; }

                /// <summary>
                /// Gets or sets the current status.
                /// </summary>
                public string Status { get; set; }

                /// <summary>
                /// Gets or sets the duration of the plugin scan (in milliseconds).
                /// </summary>
                public int TimeInMs { get; set; }
            }
        }

        internal class Converter : JsonConverter
        {
            private const string HostProcessPropertyName = "HostProcess";
            private const string PluginPropertyName = "Plugin";

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(ActiveScanProgress);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var scanProgress = new ActiveScanProgress();

                var hostProcessesArray = JArray.Load(reader);
                for (int i = 0; i < hostProcessesArray.Count; i = i + 2)
                {
                    var hostProcess = new ActiveScanProgress.HostProcess
                    {
                        Host = hostProcessesArray[i].Value<string>()
                    };

                    var pluginsArray = hostProcessesArray[i + 1].Value<JArray>(HostProcessPropertyName);
                    foreach (var pluginValues in pluginsArray.Select(obj => obj.Value<JArray>(PluginPropertyName)))
                    {
                        hostProcess.Plugins.Add(new ActiveScanProgress.HostProcess.Plugin
                        {
                            Name = pluginValues.Value<string>(0),
                            Id = pluginValues.Value<int>(1),
                            Status = pluginValues.Value<string>(2),
                            TimeInMs = pluginValues.Value<int>(3)
                        });
                    }

                    scanProgress.HostProcesses.Add(hostProcess);
                }

                return scanProgress;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var scanProgress = (ActiveScanProgress)value;
                var hostProcessesArray = new JArray();

                foreach (var hostProcess in scanProgress.HostProcesses)
                {
                    var pluginsArray = new JArray();

                    hostProcessesArray.Add(hostProcess.Host);
                    hostProcessesArray.Add(new JObject(
                        new JProperty(HostProcessPropertyName, pluginsArray)));

                    foreach (var plugin in hostProcess.Plugins)
                    {
                        pluginsArray.Add(new JObject(
                            new JProperty(PluginPropertyName, new JArray(
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
}
