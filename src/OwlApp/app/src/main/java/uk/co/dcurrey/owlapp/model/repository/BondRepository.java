package uk.co.dcurrey.owlapp.model.repository;

import android.os.AsyncTask;
import android.util.Log;

import java.util.HashMap;
import java.util.concurrent.ExecutionException;

import uk.co.dcurrey.owlapp.database.OwlDatabase;
import uk.co.dcurrey.owlapp.database.characterItem.CharacterItemDao;
import uk.co.dcurrey.owlapp.database.characterItem.CharacterItemEntity;
import uk.co.dcurrey.owlapp.database.characterItem.CharacterItemRepository;

public enum BondRepository
{
    INSTANCE;

    private CharacterItemDao mCharItemDao;
    private HashMap<Integer, CharacterItemEntity> mBonds;

    BondRepository()
    {
        mCharItemDao = OwlDatabase.getDb().charItemDao();
        mBonds = new HashMap<>();

        loadFromDatabase();
    }

    private void loadFromDatabase()
    {
        try
        {
            mBonds = new loadCharacterItemEntitiesAsyncTask(mCharItemDao, mBonds).execute().get();
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

    public CharacterItemEntity get(int id)
    {
        loadFromDatabase();
        return mBonds.get(id);
    }

    private static class loadCharacterItemEntitiesAsyncTask extends AsyncTask<Void, Void, HashMap<Integer, CharacterItemEntity>>
    {
        private CharacterItemDao mBondDao;
        private HashMap<Integer, CharacterItemEntity> mBonds;

        loadCharacterItemEntitiesAsyncTask(CharacterItemDao dao, HashMap<Integer, CharacterItemEntity> characterItems)
        {
            mBondDao = dao;
            mBonds = characterItems;
        }

        protected HashMap<Integer, CharacterItemEntity> doInBackground(final Void... voids)
        {
            for (CharacterItemEntity bond : mBondDao.get())
            {
                mBonds.put(bond.Id, bond);
            }
            return mBonds;
        }
    }

    public void insert(CharacterItemEntity charItem)
    {
        try
        {
            new insertCharacterItemEntitiesAsyncTask(mCharItemDao, charItem).execute().get();
        }
        catch (ExecutionException | InterruptedException e)
        {
            Log.e(this.name(), "An Error Occurred" ,e);
        }
    }

    private static class insertCharacterItemEntitiesAsyncTask extends AsyncTask<Void, Void, Void>
    {
        private CharacterItemDao mDao;
        private CharacterItemEntity characterItemEntity;

        insertCharacterItemEntitiesAsyncTask(CharacterItemDao characterItemDao, CharacterItemEntity character)
        {
            mDao = characterItemDao;
            characterItemEntity = character;
        }

        @Override
        protected Void doInBackground(Void... voids)
        {
            mDao.insert(characterItemEntity);
            return null;
        }
    }
}
