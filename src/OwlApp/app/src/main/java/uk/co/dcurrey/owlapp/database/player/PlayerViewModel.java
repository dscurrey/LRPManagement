package uk.co.dcurrey.owlapp.database.player;

import android.app.Application;

import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.LiveData;

import java.util.List;

public class PlayerViewModel extends AndroidViewModel
{
    private PlayerRepository mRepo;
    private LiveData<List<PlayerEntity>> mPlayers;

    public PlayerViewModel(Application application)
    {
        super(application);
        mRepo = new PlayerRepository(application);
        mPlayers = mRepo.getAllPlayers();
    }

    public LiveData<List<PlayerEntity>> getAllPlayers()
    {
        return mPlayers;
    }

    public void insert(PlayerEntity player)
    {
        mRepo.insert(player);
    }
}
