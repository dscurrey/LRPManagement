﻿namespace DTO
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int Xp { get; set; }
        public bool IsActive { get; set; }
        public bool IsRetired { get; set; }
    }
}
