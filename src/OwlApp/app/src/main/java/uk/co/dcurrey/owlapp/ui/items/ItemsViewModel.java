package uk.co.dcurrey.owlapp.ui.items;

import android.app.Application;
import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.LiveData;
import java.util.List;
import uk.co.dcurrey.owlapp.database.item.ItemEntity;
import uk.co.dcurrey.owlapp.database.item.ItemRepository;

public class ItemsViewModel extends AndroidViewModel
{
    private final ItemRepository mRepo;
    private final LiveData<List<ItemEntity>> mItems;

    public ItemsViewModel(Application application)
    {
        super(application);
        mRepo = new ItemRepository(application);
        mItems = mRepo.getAllItems();
    }

    public LiveData<List<ItemEntity>> getAllItems()
    {
        return mItems;
    }

    public void insert (ItemEntity item)
    {
        mRepo.insert(item);
    }
}
