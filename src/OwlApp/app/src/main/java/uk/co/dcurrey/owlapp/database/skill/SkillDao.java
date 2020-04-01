package uk.co.dcurrey.owlapp.database.skill;

import androidx.lifecycle.LiveData;
import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;
import androidx.room.Update;

import java.util.List;

import uk.co.dcurrey.owlapp.database.player.PlayerEntity;

@Dao
public interface SkillDao
{
    @Query("SELECT * FROM skill")
    LiveData<List<SkillEntity>> getAll();

    @Query("SELECT * FROM skill WHERE id IN (:skillIds)")
    List<SkillEntity> loadAllById(int[] skillIds);

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    void insertAll(SkillEntity... skill);

    @Update
    void update(SkillEntity skill);

    @Delete
    void deleteSkill(SkillEntity skill);

    @Query("DELETE FROM skill")
    void deleteAll();

    @Query("SELECT * FROM skill")
    public abstract List<SkillEntity> get();

    @Query("SELECT * FROM skill WHERE Id = :id")
    public abstract SkillEntity get(int id);

    @Query("SELECT * FROM skill WHERE Id IN (:Ids)")
    public abstract List<SkillDao> get(int... Ids);
}
