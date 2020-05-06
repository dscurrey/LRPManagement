package uk.co.dcurrey.owlapp.database.skill;

import android.app.Application;

import androidx.lifecycle.LiveData;

import java.util.List;

import uk.co.dcurrey.owlapp.database.OwlDatabase;

public class SkillRepository
{
    private final SkillDao mSkillDao;
    private final LiveData<List<SkillEntity>> mAllSkills;

    SkillRepository(Application application)
    {
        OwlDatabase db = OwlDatabase.getDb(application);
        mSkillDao = db.skillDao();
        mAllSkills = mSkillDao.getAll();
    }

    LiveData<List<SkillEntity>> getAllSkills()
    {
        return mAllSkills;
    }

    // Unlikely to be used by users.
    void insert(SkillEntity skill)
    {
        OwlDatabase.databaseWriteExecutor.execute(() ->
                mSkillDao.insertAll(skill));
    }

    // Unlikely to be used by users, or at all
    void update (SkillEntity skill)
    {
        OwlDatabase.databaseWriteExecutor.execute(() ->
                mSkillDao.update(skill));
    }
}
