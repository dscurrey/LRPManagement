package uk.co.dcurrey.owlapp.database.characterItem;

import androidx.lifecycle.LiveData;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;

import java.util.List;

public interface CharacterItemDao
{
    @Query("SELECT * FROM characteritem")
    LiveData<List<CharacterItemEntity>> get();

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    void insert(CharacterItemEntity characterItem);
}
