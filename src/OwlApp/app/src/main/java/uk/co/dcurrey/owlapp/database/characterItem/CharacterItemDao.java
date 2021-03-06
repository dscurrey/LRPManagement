package uk.co.dcurrey.owlapp.database.characterItem;

import androidx.lifecycle.LiveData;
import androidx.room.Dao;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;
import androidx.room.Update;

import java.util.List;

@Dao
public interface CharacterItemDao
{
    @Query("SELECT * FROM characteritem")
    LiveData<List<CharacterItemEntity>> getAll();

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    void insert(CharacterItemEntity characterItem);

    @Query("SELECT * FROM characteritem")
    List<CharacterItemEntity> get();

    @Update
    void update(CharacterItemEntity characterItemEntity);

    @Query("DELETE FROM characteritem")
    void deleteAll();
}
