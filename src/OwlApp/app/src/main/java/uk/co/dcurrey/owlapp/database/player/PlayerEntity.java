package uk.co.dcurrey.owlapp.database.player;

import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "player")
public class PlayerEntity
{
    public PlayerEntity()
    {
    }

    @PrimaryKey
    public int Id;
    public String FirstName;
    public String LastName;
}
