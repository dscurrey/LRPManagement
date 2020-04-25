package uk.co.dcurrey.owlapp.database.characterItem;

import android.app.Application;

import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.LiveData;

import java.util.List;

public class CharacterItemViewModel extends AndroidViewModel
{
    private CharacterItemRepository mRepo;
    private LiveData<List<CharacterItemEntity>> mBonds;

    public CharacterItemViewModel(Application application)
    {
        super(application);
        mRepo = new CharacterItemRepository(application);
        mBonds = mRepo.getAllBonds();
    }

    public LiveData<List<CharacterItemEntity>> getAllBonds()
    {
        return mBonds;
    }

    public void insert(CharacterItemEntity charItem)
    {
        mRepo.insert(charItem);
    }
}
