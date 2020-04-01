package uk.co.dcurrey.owlapp.model.repository;

import android.os.AsyncTask;

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
}
