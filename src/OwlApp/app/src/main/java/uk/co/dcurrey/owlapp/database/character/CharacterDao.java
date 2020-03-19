package uk.co.dcurrey.owlapp.database.character;

import androidx.lifecycle.LiveData;
import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;

import java.util.List;

import uk.co.dcurrey.owlapp.database.character.CharacterEntity;

@Dao
public interface CharacterDao
{
    @Query("SELECT * FROM character")
    LiveData<List<CharacterEntity>> getAll();

    @Query("SELECT * FROM character WHERE id IN (:charIds)")
    List<CharacterEntity> loadAllById(int[] charIds);

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    void insertAll(CharacterEntity... characters);

    @Delete
    void deleteCharacter(CharacterEntity character);

    @Query("DELETE FROM character")
    void deleteAll();
}
