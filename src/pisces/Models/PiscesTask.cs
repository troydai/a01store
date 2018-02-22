using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Pisces.Store.Models
{
    [Table("task", Schema = "public")]
    public class PiscesTask
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("annotation")]
        public string Annotation { get; set; }

        [Column("settings")]
        public string Settings { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("result_details")]
        public string ResultDetails { get; set; }

        [Column("result")]
        public string Result { get; set; }

        [ForeignKey("run_id")]
        public PiscesRun Run { get; set; }

        public PiscesTaskViewModel GetViewModel() => new PiscesTaskViewModel(this);
    }

    public class PiscesTaskViewModel
    {
        private static readonly string logTemplate = "https://azureclia01log.file.core.windows.net/k8slog/{0}?sv=2017-04-17&ss=f&srt=o&sp=r&se=2019-01-01T00:00:00Z&st=2018-01-04T10:21:21Z&spr=https&sig=I9Ajm2i8Knl3hm1rfN%2Ft2E934trzj%2FNnozLYhQ%2Bb7TE%3D";

        private readonly PiscesTask _task;
        private readonly Lazy<JObject> _details;
        private readonly Lazy<JObject> _settings;

        public PiscesTaskViewModel(PiscesTask task)
        {
            _task = task;
            _details = new Lazy<JObject>(() => JObject.Parse(_task.ResultDetails));
            _settings = new Lazy<JObject>(() => JObject.Parse(_task.Settings));
        }

        public PiscesTask Data => _task;

        public string Module
        {
            get
            {
                JToken pathToken;
                if (!_settings.Value.TryGetValue("path", out pathToken))
                {
                    pathToken = _settings.Value["classifier"]["identifier"];
                }

                var path = (string)pathToken;
                if (path.StartsWith("azure.cli.command_modules", StringComparison.InvariantCulture))
                {
                    return path.Split('.')[3];
                }
                else if (path.StartsWith("azure.cli.", StringComparison.InvariantCulture))
                {
                    return path.Split('.')[2];
                }
                else
                {
                    return "N/A";
                }
            }
        }

        public async Task<string> GetLog()
        {
            var filePath = string.Format(logTemplate, $"{Data.Run.Id}/task_{Data.Id}.log");
            var client = new HttpClient();
            var resp = await client.GetAsync(filePath);
            return await resp.Content.ReadAsStringAsync();
        }
    }
}
