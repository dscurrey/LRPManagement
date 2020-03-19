package uk.co.dcurrey.owlapp.database.character;

import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "character")
public class CharacterEntity
{
    public CharacterEntity()
    {
    }

    @PrimaryKey(autoGenerate = true)
    public int Id;
    public String Name;
}
