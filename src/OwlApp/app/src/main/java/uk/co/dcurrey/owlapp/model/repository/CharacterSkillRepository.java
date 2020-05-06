package uk.co.dcurrey.owlapp.model.repository;

import android.os.AsyncTask;
import android.util.Log;

import java.util.HashMap;
import java.util.concurrent.ExecutionException;

import uk.co.dcurrey.owlapp.database.OwlDatabase;
import uk.co.dcurrey.owlapp.database.characterSkill.CharacterSkillDao;
import uk.co.dcurrey.owlapp.database.characterSkill.CharacterSkillEntity;

public enum CharacterSkillRepository
{
    INSTANCE;

    private final CharacterSkillDao mCharSkillDao;
    private HashMap<Integer, CharacterSkillEntity> mCharSkills;

    CharacterSkillRepository()
    {
        mCharSkillDao = OwlDatabase.getDb().charSkillDao();
        mCharSkills = new HashMap<>();

        loadFromDatabase();
    }

    private void loadFromDatabase()
    {
        try
        {
            mCharSkills = new loadCharacterSkillEntitiesAsyncTask(mCharSkillDao, mCharSkills).execute().get();
        }
        catch (InterruptedException e)
        {
            e.printStackTrace();
        }
        catch (ExecutionException e)
        {
            e.printStackTrace();
        }
    }

    public CharacterSkillEntity get(int id)
    {
        loadFromDatabase();
        return mCharSkills.get(id);
    }

    public HashMap<Integer, CharacterSkillEntity> get()
    {
        mCharSkills.clear();
        loadFromDatabase();
        return mCharSkills;
    }

    private static class loadCharacterSkillEntitiesAsyncTask extends AsyncTask<Void, Void, HashMap<Integer, CharacterSkillEntity>>
    {
        private final CharacterSkillDao mCharSkillDao;
        private final HashMap<Integer, CharacterSkillEntity> mCharSkills;

        loadCharacterSkillEntitiesAsyncTask(CharacterSkillDao dao, HashMap<Integer, CharacterSkillEntity> characterSkills)
        {
            mCharSkillDao = dao;
            mCharSkills = characterSkills;
        }

        protected HashMap<Integer, CharacterSkillEntity> doInBackground(final Void... voids)
        {
            for (CharacterSkillEntity charSkill : mCharSkillDao.get())
            {
                mCharSkills.put(charSkill.Id, charSkill);
            }
            return mCharSkills;
        }
    }

    public void insert(CharacterSkillEntity charSkill)
    {
        try
        {
            new insertCharacterSkillEntitiesAsyncTask(mCharSkillDao, charSkill).execute().get();
        }
        catch (ExecutionException | InterruptedException e)
        {
            Log.e(this.name(), "An Error Occurred" ,e);
        }
    }

    private static class insertCharacterSkillEntitiesAsyncTask extends AsyncTask<Void, Void, Void>
    {
        private final CharacterSkillDao mDao;
        private final CharacterSkillEntity characterSkillEntity;

        insertCharacterSkillEntitiesAsyncTask(CharacterSkillDao characterSkillDao, CharacterSkillEntity character)
        {
            mDao = characterSkillDao;
            characterSkillEntity = character;
        }

        @Override
        protected Void doInBackground(Void... voids)
        {
            mDao.insert(characterSkillEntity);
            return null;
        }
    }

    public void update(CharacterSkillEntity charSkillEntity)
    {
        try
        {
            new updateCharacterSkillEntitiesAsyncTask(mCharSkillDao, charSkillEntity).execute().get();
        } catch (ExecutionException | InterruptedException e)
        {
            Log.e(this.name(), "DB Error Occurred");
        }
    }

    private static class updateCharacterSkillEntitiesAsyncTask extends AsyncTask<Void, Void, Void>
    {
        private final CharacterSkillDao mDao;
        private final CharacterSkillEntity charSkillEntity;

        updateCharacterSkillEntitiesAsyncTask(CharacterSkillDao charSkillDao, CharacterSkillEntity charSkill)
        {
            mDao = charSkillDao;
            charSkillEntity = charSkill;
        }

        protected Void doInBackground(final Void... voids)
        {
            mDao.update(charSkillEntity);
            return null;
        }
    }
}
