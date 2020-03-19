package uk.co.dcurrey.owlapp.database;

import android.app.Application;

import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.LiveData;

import java.util.List;

public class CharacterViewModel extends AndroidViewModel
{
    private CharacterRepository mRepo;
    private LiveData<List<CharacterEntity>> mChars;

    public CharacterViewModel(Application application)
    {
        super(application);
        mRepo = new CharacterRepository(application);
        mChars = mRepo.getAllChars();
    }

    public LiveData<List<CharacterEntity>> getAllChars()
    {
        return mChars;
    }

    public void insert(CharacterEntity character)
    {
        mRepo.insert(character);
    }
}
