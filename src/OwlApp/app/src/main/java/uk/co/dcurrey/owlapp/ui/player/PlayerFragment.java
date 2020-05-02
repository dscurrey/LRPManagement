package uk.co.dcurrey.owlapp.ui.player;

import android.content.Intent;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProvider;
import androidx.lifecycle.ViewModelProviders;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.google.android.material.floatingactionbutton.FloatingActionButton;

import java.util.ArrayList;
import java.util.List;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;
import uk.co.dcurrey.owlapp.database.player.PlayerViewModel;
import uk.co.dcurrey.owlapp.sync.NetworkMonitor;
import uk.co.dcurrey.owlapp.sync.Synchroniser;

import static android.app.Activity.RESULT_OK;

public class PlayerFragment extends Fragment
{

    public static final int NEW_PLAYER_ACTIVITY_REQUEST_CODE = 1;
    private uk.co.dcurrey.owlapp.database.player.PlayerViewModel mPlayerViewModel;
    private PlayerListAdapter adapter;
    private List<PlayerEntity> players;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState)
    {
        PlayerUIViewModel slideshowViewModel = new ViewModelProvider(this).get(PlayerUIViewModel.class);
        View root = inflater.inflate(R.layout.fragment_player, container, false);

        EditText searchTerm = root.findViewById(R.id.playerSearch);

        // RecyclerView
        RecyclerView recyclerView = root.findViewById(R.id.recyclerview_player);
        adapter = new PlayerListAdapter(getContext());
        recyclerView.setAdapter(adapter);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));

        // ViewModel
        mPlayerViewModel = new ViewModelProvider(this).get(PlayerViewModel.class);
        mPlayerViewModel.getAllPlayers().observe(getViewLifecycleOwner(), playerEntities -> {
            players = playerEntities;
            adapter.setPlayers(players);
        });

        searchTerm.addTextChangedListener(new TextWatcher()
        {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after)
            {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count)
            {

            }

            @Override
            public void afterTextChanged(Editable s)
            {
                filter(s.toString());
            }
        });
        return root;
    }

    private void filter(String text)
    {
        List<PlayerEntity> filteredPlayers = new ArrayList<>();
        for (PlayerEntity p : players)
        {
            if ((p.FirstName.toLowerCase().contains(text.toLowerCase())) || (p.LastName.toLowerCase().contains(text.toLowerCase())) || String.valueOf(p.Id).contains(text))
            {
                filteredPlayers.add(p);
            }
        }
        adapter.setPlayers(filteredPlayers);
    }

    public void onActivityResult(int reqCode, int resCode, Intent data)
    {
        super.onActivityResult(reqCode, resCode, data);

        if (reqCode == NEW_PLAYER_ACTIVITY_REQUEST_CODE && resCode == RESULT_OK)
        {
            PlayerEntity playerEntity = new PlayerEntity();
            playerEntity.FirstName = data.getStringExtra(NewPlayerActivity.EXTRA_REPLY_FNAME);
            playerEntity.LastName = data.getStringExtra(NewPlayerActivity.EXTRA_REPLY_LNAME);
            savePlayer(playerEntity);
        }
        else
        {
            Toast.makeText(getContext(), "Player not saved", Toast.LENGTH_LONG).show();
        }
    }

    private void savePlayer(PlayerEntity player)
    {
        if (NetworkMonitor.checkNetConnectivity(getContext()))
        {
            saveApi(player);
        }
        else
        {
            saveLocal(player);
        }
    }

    private void saveLocal(PlayerEntity player)
    {
        mPlayerViewModel.insert(player);
    }

    private void saveApi(PlayerEntity player)
    {
        Synchroniser sync = new Synchroniser();
        sync.sendToAPI(getContext(), player);
    }
}
