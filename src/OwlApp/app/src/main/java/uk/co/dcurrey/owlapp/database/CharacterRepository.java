package uk.co.dcurrey.owlapp.database;

import android.app.Application;

import androidx.lifecycle.LiveData;

import java.util.List;

public class CharacterRepository
{
    private CharacterDao mCharacterDao;
    private LiveData<List<CharacterEntity>> mAllChars;

    CharacterRepository(Application application)
    {
        OwlDatabase db = OwlDatabase.getDb(application);
        mCharacterDao = db.characterDao();
        mAllChars = mCharacterDao.getAll();
    }

    LiveData<List<CharacterEntity>> getAllChars()
    {
        return mAllChars;
    }

    void insert(CharacterEntity character)
    {
        OwlDatabase.databaseWriteExecutor.execute(() -> {
            mCharacterDao.insertAll(character);
        });
    }
}
