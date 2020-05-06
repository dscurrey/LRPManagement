package uk.co.dcurrey.owlapp.database.character;

import android.app.Application;

import androidx.lifecycle.LiveData;

import java.util.List;

import uk.co.dcurrey.owlapp.database.OwlDatabase;

public class CharacterRepository
{
    private final CharacterDao mCharacterDao;
    private final LiveData<List<CharacterEntity>> mAllChars;
    private List<CharacterEntity> mUnSynced;

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

    List<CharacterEntity> getUnsynced()
    {
        return mUnSynced;
    }


    void insert(CharacterEntity character)
    {
        OwlDatabase.databaseWriteExecutor.execute(() -> mCharacterDao.insertAll(character));
    }

    void update(CharacterEntity character)
    {
        OwlDatabase.databaseWriteExecutor.execute(() ->
                mCharacterDao.update(character));
    }
}
