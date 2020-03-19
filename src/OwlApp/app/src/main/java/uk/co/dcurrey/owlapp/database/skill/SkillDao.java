package uk.co.dcurrey.owlapp.database.skill;

import androidx.lifecycle.LiveData;
import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;
import androidx.room.Update;

import java.util.List;

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
}
