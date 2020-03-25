package uk.co.dcurrey.owlapp.database.skill;

import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "skill")
public class SkillEntity
{
    public SkillEntity()
    {
    }

    @PrimaryKey(autoGenerate = true)
    public int Id;
    public String Name;
    public boolean IsSynced;
}
