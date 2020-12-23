using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [Serializable]
    public class DTO
    {
        public long Id { get; set; }
        public string TimeStamp { get; set; }
        public string  Column { get; set; }
        public string Request { get; set; }
        public string Status { get; set; }
        public string Port { get; set; }
        public string Method { get; set; }
        public string EndPoint { get; set; }
        public string ColumnNone { get; set; }
        public string Format { get; set; }

    }
}
