package uk.co.dcurrey.owlapp.database.player;

import androidx.lifecycle.LiveData;
import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;

import java.util.List;

import uk.co.dcurrey.owlapp.database.skill.SkillEntity;

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
    void deleteSkill(SkillEntity skill);

    @Query("DELETE FROM skill")
    void deleteAll();
}
