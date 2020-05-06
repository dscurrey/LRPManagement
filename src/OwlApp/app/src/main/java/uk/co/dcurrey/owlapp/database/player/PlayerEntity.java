package uk.co.dcurrey.owlapp.database.player;

import androidx.annotation.Nullable;
import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "player")
public class PlayerEntity
{
    public PlayerEntity()
    {
    }

    @PrimaryKey(autoGenerate = true)
    public int Id;
    public String FirstName;
    @Nullable
    public String LastName;
    public boolean IsSynced;
}
