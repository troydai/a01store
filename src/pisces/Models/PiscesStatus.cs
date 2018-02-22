using System;
namespace Pisces.Store.Models
{
    public class PiscesStatus
    {
        private readonly DateTime _time;
        private readonly string _status;

        public PiscesStatus()
        {
            _time = DateTime.UtcNow;
            _status = "Healthy";
        }

        public string Time => _time.ToLongTimeString();

        public string Status => _status;
    }
}
