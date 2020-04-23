package uk.co.dcurrey.owlapp.database.item;

import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity(tableName = "item")
public class ItemEntity
{
    public ItemEntity()
    {
    }

    @PrimaryKey(autoGenerate = true)
    public int Id;
    public String Name;
    public String Form;
    public String Requirement;
    public String Effect;
    public boolean IsSynced;
}
