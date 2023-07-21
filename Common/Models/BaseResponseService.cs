﻿using System;
using System.Runtime.Serialization;

namespace Common.Models
{
    [DataContract]
    public class BaseResponseService
    {
        public bool ready_commit { get; set; } = true;
        public Exception exception { get; set; }
    }
}
