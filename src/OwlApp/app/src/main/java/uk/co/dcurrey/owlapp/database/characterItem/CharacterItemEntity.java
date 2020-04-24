package uk.co.dcurrey.owlapp.database.characterItem;

import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "characteritem")
public class CharacterItemEntity
{
    @PrimaryKey(autoGenerate = true)
    public int Id;
    public int CharacterId;
    public int ItemId;
    public boolean IsSynced;
}
