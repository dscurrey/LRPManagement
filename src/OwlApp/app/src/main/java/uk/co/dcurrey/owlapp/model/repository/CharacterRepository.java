package uk.co.dcurrey.owlapp.model.repository;

import android.os.AsyncTask;
import android.util.Log;

import java.util.HashMap;
import java.util.concurrent.ExecutionException;

import uk.co.dcurrey.owlapp.database.OwlDatabase;
import uk.co.dcurrey.owlapp.database.character.CharacterDao;
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;

public enum CharacterRepository
{
    INSTANCE;

    private CharacterDao mCharacterDao;

    private HashMap<Integer, CharacterEntity> mCharacters;

    CharacterRepository()
    {
        mCharacterDao = OwlDatabase.getDb().characterDao();
        mCharacters = new HashMap<>();

        loadFromDatabase();
    }

    private void loadFromDatabase()
    {
        try
        {
            mCharacters = new loadCharacterEntitiesAsyncTask(mCharacterDao, mCharacters).execute().get();
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

    public CharacterEntity get(int id)
    {
        loadFromDatabase();
        return mCharacters.get(id);
    }

    public HashMap<Integer, CharacterEntity> get()
    {
        mCharacters.clear();
        loadFromDatabase();
        return mCharacters;
    }

    private static class loadCharacterEntitiesAsyncTask extends AsyncTask<Void, Void, HashMap<Integer, CharacterEntity>>
    {
        private CharacterDao mCharacterDao;
        private HashMap<Integer, CharacterEntity> mCharacterEntities;


        loadCharacterEntitiesAsyncTask(CharacterDao characterDao, HashMap<Integer, CharacterEntity> CharacterEntities) {
            mCharacterDao = characterDao;
            mCharacterEntities = CharacterEntities;
        }

        protected HashMap<Integer, CharacterEntity> doInBackground(final Void... voids) {
            for (CharacterEntity characterEntity : mCharacterDao.get())
            {
                mCharacterEntities.put(characterEntity.Id, characterEntity);
            }
            return mCharacterEntities;
        }

    }

    public void insert(CharacterEntity characterEntity)
    {
        try
        {
            new insertCharacterEntitiesAsyncTask(mCharacterDao, characterEntity).execute().get();
        } catch (ExecutionException | InterruptedException e)
        {
            Log.e(this.name(), "An error occurred inserting characters into the database", e);
        }
    }

    private static class insertCharacterEntitiesAsyncTask extends AsyncTask<Void, Void, Void>
    {
        private CharacterDao mDao;
        private CharacterEntity characterEntity;

        insertCharacterEntitiesAsyncTask(CharacterDao characterDao, CharacterEntity character)
        {
            mDao = characterDao;
            characterEntity = character;
        }

        protected Void doInBackground(final Void... voids)
        {
            mDao.insertAll(characterEntity);
            return null;
        }
    }

    public void update(CharacterEntity characterEntity)
    {
        try
        {
            new updateCharacterEntitiesAsyncTask(mCharacterDao, characterEntity).execute().get();
        } catch (ExecutionException | InterruptedException e)
        {
            Log.e(this.name(), "DB Error Occurred");
        }
    }

    private static class updateCharacterEntitiesAsyncTask extends AsyncTask<Void, Void, Void>
    {
        private CharacterDao mDao;
        private CharacterEntity characterEntity;

        updateCharacterEntitiesAsyncTask(CharacterDao characterDao, CharacterEntity character)
        {
            mDao = characterDao;
            characterEntity = character;
        }

        protected Void doInBackground(final Void... voids)
        {
            mDao.update(characterEntity);
            return null;
        }
    }
}
