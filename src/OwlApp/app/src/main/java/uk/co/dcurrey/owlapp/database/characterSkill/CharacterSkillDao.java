package uk.co.dcurrey.owlapp.database.characterSkill;

import androidx.lifecycle.LiveData;
import androidx.room.Dao;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;
import androidx.room.Update;

import java.util.List;

@Dao
public interface CharacterSkillDao
{
    @Query("SELECT * FROM characterskill")
    LiveData<List<CharacterSkillEntity>> getAll();

    @Query("SELECT * FROM characterskill")
    List<CharacterSkillEntity> get();

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    void insert(CharacterSkillEntity charSkill);

    @Update
    void update(CharacterSkillEntity charSkill);

    @Query("DELETE FROM characterskill")
    void deleteAll();
}
