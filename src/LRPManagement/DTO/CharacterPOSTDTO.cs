﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CharacterPOSTDTO
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public bool IsRetired { get; set; }
    }
}
