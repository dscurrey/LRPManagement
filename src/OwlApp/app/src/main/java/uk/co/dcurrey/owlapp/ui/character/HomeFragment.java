package uk.co.dcurrey.owlapp.ui.character;

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
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.character.CharacterViewModel;
import uk.co.dcurrey.owlapp.sync.NetworkMonitor;
import uk.co.dcurrey.owlapp.sync.Synchroniser;

import static android.app.Activity.RESULT_OK;

public class HomeFragment extends Fragment
{

    public static final int NEW_CHAR_ACTIVITY_REQUEST_CODE = 1;
    private CharacterViewModel mCharacterViewModel;
    private CharacterListAdapter adapter;
    private List<CharacterEntity> characters;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState)
    {
        HomeViewModel homeViewModel = new ViewModelProvider(this).get(HomeViewModel.class);
        View root = inflater.inflate(R.layout.fragment_home, container, false);

        // Recycler
        RecyclerView recyclerView = root.findViewById(R.id.recyclerview);
        EditText searchTerm = root.findViewById(R.id.charSearch);
        adapter = new CharacterListAdapter(getContext());
        recyclerView.setAdapter(adapter);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));

        // ViewModel
        mCharacterViewModel = new ViewModelProvider(this).get(CharacterViewModel.class);
        mCharacterViewModel.getAllChars().observe(getViewLifecycleOwner(), characterEntities -> {
            // Update char cache in adapter
            characters = characterEntities;
            adapter.setChars(characters);
        });

        // FAB
        FloatingActionButton fab = root.findViewById(R.id.fab);
        fab.setVisibility(View.VISIBLE);
        fab.setOnClickListener(view -> {
            Intent intent = new Intent(getContext(), NewCharacterActivity.class);
            startActivityForResult(intent, NEW_CHAR_ACTIVITY_REQUEST_CODE);
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
        List<CharacterEntity> filteredChars = new ArrayList<>();
        for(CharacterEntity c : characters)
        {
            if (c.Name.toLowerCase().contains(text.toLowerCase()) || String.valueOf(c.Id).contains(text))
            {
                filteredChars.add(c);
            }
        }
        adapter.setChars(filteredChars);
    }

    public void onActivityResult(int reqCode, int resCode, Intent data)
    {
        super.onActivityResult(reqCode, reqCode, data);

        if (reqCode == NEW_CHAR_ACTIVITY_REQUEST_CODE && resCode == RESULT_OK)
        {
            CharacterEntity characterEntity = new CharacterEntity();
            characterEntity.Name = data.getStringExtra(NewCharacterActivity.EXTRA_REPLY_CHARNAME);
            characterEntity.IsRetired = data.getBooleanExtra(NewCharacterActivity.EXTRA_REPLY_CHARERETIRED, false);
            characterEntity.PlayerId = Integer.parseInt(data.getStringExtra(NewCharacterActivity.EXTRA_REPLY_CHARPLAYER));
            saveCharacter(characterEntity);
        }
        else
        {
            Toast.makeText(getContext(), R.string.empty_not_saved, Toast.LENGTH_LONG).show();
        }
    }

    private void saveCharacter(CharacterEntity characterEntity)
    {
        if (NetworkMonitor.checkNetConnectivity(getContext()))
        {
            saveAPI(characterEntity);
        }
        else
        {
            saveLocal(characterEntity);
        }
    }

    private void saveLocal(CharacterEntity characterEntity)
    {
        characterEntity.IsRetired = false;
        mCharacterViewModel.insert(characterEntity);
    }

    private void saveAPI(CharacterEntity characterEntity)
    {
        Synchroniser synchroniser = new Synchroniser();
        synchroniser.sendToAPI(getContext(), characterEntity);
        characterEntity.IsSynced = true;
        saveLocal(characterEntity);
    }
}
