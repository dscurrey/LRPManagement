package uk.co.dcurrey.owlapp.database.character;

import androidx.lifecycle.LiveData;
import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;
import androidx.room.Update;

import java.util.List;

@Dao
public interface CharacterDao
{
    @Query("SELECT * FROM character WHERE isActive = 1")
    LiveData<List<CharacterEntity>> getAll();

    @Query("SELECT * FROM character WHERE id IN (:charIds)")
    List<CharacterEntity> loadAllById(int[] charIds);

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    void insertAll(CharacterEntity... characters);

    @Update
    void update(CharacterEntity character);

    @Delete
    void deleteCharacter(CharacterEntity character);

    @Query("DELETE FROM character")
    void deleteAll();

    @Query("SELECT * FROM character")
    List<CharacterEntity> get();

    @Query("SELECT * FROM character WHERE Id = :uuid")
    CharacterEntity get(int uuid);

    @Query("SELECT * FROM character WHERE Id IN (:Ids)")
    List<CharacterEntity> get(int... Ids);
}
