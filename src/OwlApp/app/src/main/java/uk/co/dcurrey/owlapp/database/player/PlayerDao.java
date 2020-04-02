package uk.co.dcurrey.owlapp.database.player;

import androidx.lifecycle.LiveData;
import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;
import androidx.room.Update;

import java.util.List;

@Dao
public interface PlayerDao
{
    @Query("SELECT * FROM player")
    LiveData<List<PlayerEntity>> getAll();

    @Query("SELECT * FROM player WHERE id IN (:playerIds)")
    List<PlayerEntity> loadAllById(int[] playerIds);

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    void insertAll(PlayerEntity... players);

    @Delete
    void deleteSkill(PlayerEntity skill);

    @Update
    void update(PlayerEntity player);

    @Query("DELETE FROM player")
    void deleteAll();

    @Query("SELECT * FROM player")
    List<PlayerEntity> get();

    @Query("SELECT * FROM player WHERE Id = :id")
    PlayerEntity get(int id);

    @Query("SELECT * FROM player WHERE Id IN (:Ids)")
    List<PlayerEntity> get(int... Ids);
}
