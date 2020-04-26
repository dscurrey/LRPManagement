package uk.co.dcurrey.owlapp.database.characterSkill;

import android.app.Application;

import androidx.lifecycle.LiveData;

import java.util.List;

import uk.co.dcurrey.owlapp.database.OwlDatabase;

public class CharacterSkillRepository
{
    private CharacterSkillDao mDao;
    private LiveData<List<CharacterSkillEntity>> mCharSkills;

    CharacterSkillRepository(Application application)
    {
        OwlDatabase db = OwlDatabase.getDb(application);
        mDao = db.charSkillDao();
        mCharSkills = mDao.getAll();
    }

    LiveData<List<CharacterSkillEntity>> getAll()
    {
        return mCharSkills;
    }

    void insert(CharacterSkillEntity charSkill)
    {
        OwlDatabase.databaseWriteExecutor.execute(() -> {
            mDao.insert(charSkill);
        });
    }
}
