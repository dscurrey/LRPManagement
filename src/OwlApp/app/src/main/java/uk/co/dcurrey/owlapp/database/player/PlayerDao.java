package uk.co.dcurrey.owlapp.database.player;

import androidx.lifecycle.LiveData;
import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;
import androidx.room.Update;

import java.util.List;

import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
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

    @Update
    void update(PlayerEntity player);

    @Query("DELETE FROM player")
    void deleteAll();

    @Query("SELECT * FROM player")
    public abstract List<PlayerEntity> get();

    @Query("SELECT * FROM player WHERE Id = :id")
    public abstract PlayerEntity get(int id);

    @Query("SELECT * FROM player WHERE Id IN (:Ids)")
    public abstract List<PlayerEntity> get(int... Ids);
}
