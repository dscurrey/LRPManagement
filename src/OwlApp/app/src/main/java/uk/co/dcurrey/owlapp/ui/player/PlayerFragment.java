package uk.co.dcurrey.owlapp.ui.player;

import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProvider;
import androidx.lifecycle.ViewModelProviders;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.google.android.material.floatingactionbutton.FloatingActionButton;

import java.util.List;

import uk.co.dcurrey.owlapp.NewPlayerActivity;
import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;
import uk.co.dcurrey.owlapp.database.player.PlayerViewModel;

import static android.app.Activity.RESULT_OK;

public class PlayerFragment extends Fragment
{

    private PlayerUIViewModel slideshowViewModel;
    public static final int NEW_PLAYER_ACTIVITY_REQUEST_CODE = 1;
    private uk.co.dcurrey.owlapp.database.player.PlayerViewModel mPlayerViewModel;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState)
    {
        slideshowViewModel =
                ViewModelProviders.of(this).get(PlayerUIViewModel.class);
        View root = inflater.inflate(R.layout.fragment_player, container, false);
        final TextView textView = root.findViewById(R.id.text_slideshow);
        slideshowViewModel.getText().observe(getViewLifecycleOwner(), new Observer<String>()
        {
            @Override
            public void onChanged(@Nullable String s)
            {
                textView.setText(s);
            }
        });

        // RecyclerView
        RecyclerView recyclerView = root.findViewById(R.id.recyclerview_player);
        final PlayerListAdapter adapter = new PlayerListAdapter(getContext());
        recyclerView.setAdapter(adapter);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));

        // ViewModel
        mPlayerViewModel = new ViewModelProvider(this).get(PlayerViewModel.class);
        mPlayerViewModel.getAllPlayers().observe(getViewLifecycleOwner(), new Observer<List<PlayerEntity>>()
        {
            @Override
            public void onChanged(List<PlayerEntity> playerEntities)
            {
                adapter.setPlayers(playerEntities);
            }
        });

        // FAB
        FloatingActionButton fab = root.findViewById(R.id.fab_player);
        fab.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View view)
            {
                Intent intent = new Intent(getContext(), NewPlayerActivity.class);
                startActivityForResult(intent, NEW_PLAYER_ACTIVITY_REQUEST_CODE);
            }
        });

        return root;
    }

    public void onActivityResult(int reqCode, int resCode, Intent data)
    {
        super.onActivityResult(reqCode, resCode, data);

        if (reqCode == NEW_PLAYER_ACTIVITY_REQUEST_CODE && resCode == RESULT_OK)
        {
            PlayerEntity playerEntity = new PlayerEntity();
            playerEntity.FirstName = data.getStringExtra(NewPlayerActivity.EXTRA_REPLY);
            mPlayerViewModel.insert(playerEntity);
        }
        else
        {
            Toast.makeText(getContext(), "Player not saved", Toast.LENGTH_LONG).show();
        }
    }
}
