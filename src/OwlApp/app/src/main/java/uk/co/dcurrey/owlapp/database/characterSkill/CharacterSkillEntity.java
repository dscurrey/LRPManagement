package uk.co.dcurrey.owlapp.database.characterSkill;

import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "characterskill")
public class CharacterSkillEntity
{
    @PrimaryKey(autoGenerate = true)
    public int Id;
    public int CharacterId;
    public int SkillId;
    public boolean IsSynced;
}
