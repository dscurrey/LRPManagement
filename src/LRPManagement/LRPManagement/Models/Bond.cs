﻿using System;
using System.Collections.Generic;

namespace LRPManagement.Models
{
    public partial class Bond
    {
        public int CharacterId { get; set; }
        public int ItemId { get; set; }

        public virtual Character Character { get; set; }
        public virtual Craftable Item { get; set; }
    }
}