package uk.co.dcurrey.owlapp.database.item;

import android.app.Application;

import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.LiveData;

import java.util.List;

public class ItemViewModel extends AndroidViewModel
{
    private final ItemRepository mRepo;
    private final LiveData<List<ItemEntity>> mItems;

    public ItemViewModel(Application application)
    {
        super(application);
        mRepo = new ItemRepository(application);
        mItems = mRepo.getAllItems();
    }

    public LiveData<List<ItemEntity>> getAllItems()
    {
        return mItems;
    }

    public void insert(ItemEntity item)
    {
        mRepo.insert(item);
    }
}
