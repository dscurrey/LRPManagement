package uk.co.dcurrey.owlapp.model.repository;

import android.os.AsyncTask;
import android.util.Log;

import java.util.HashMap;
import java.util.concurrent.ExecutionException;

import uk.co.dcurrey.owlapp.database.OwlDatabase;
import uk.co.dcurrey.owlapp.database.character.CharacterDao;
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.skill.SkillDao;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;

public enum SkillRepository
{
    INSTANCE;

    private SkillDao mSkillDao;

    private HashMap<Integer, SkillEntity> mSkills;

    SkillRepository()
    {
        mSkillDao = OwlDatabase.getDb().skillDao();
        mSkills = new HashMap<>();

        loadFromDatabase();
    }

    private void loadFromDatabase()
    {
        try
        {
            mSkills = new loadSkillEntitiesAsyncTask(mSkillDao, mSkills).execute().get();
        }
        catch (InterruptedException e)
        {
            Log.e(this.name(), "Error Occurred reading from DB");
            e.printStackTrace();
        }
        catch (ExecutionException e)
        {
            Log.e(this.name(), "Error Occurred reading from DB");
            e.printStackTrace();
        }
    }

    public SkillEntity get(int id)
    {
        return mSkills.get(id);
    }

    public HashMap<Integer, SkillEntity> get()
    {
        mSkills.clear();
        loadFromDatabase();
        return mSkills;
    }

    private static class loadSkillEntitiesAsyncTask extends AsyncTask<Void, Void, HashMap<Integer, SkillEntity>>
    {
        private SkillDao mSkillDao;
        private HashMap<Integer, SkillEntity> mSkillEntities;


        loadSkillEntitiesAsyncTask(SkillDao skillDao, HashMap<Integer, SkillEntity> skillEntities) {
            mSkillDao = skillDao;
            mSkillEntities = skillEntities;
        }

        protected HashMap<Integer, SkillEntity> doInBackground(final Void... voids) {
            for (SkillEntity skillEntity : mSkillDao.get())
            {
                mSkillEntities.put(skillEntity.Id, skillEntity);
            }
            return mSkillEntities;
        }

    }

}
