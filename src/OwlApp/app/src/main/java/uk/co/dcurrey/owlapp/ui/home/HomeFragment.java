package uk.co.dcurrey.owlapp.ui.home;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
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

import com.android.volley.NetworkResponse;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.JsonRequest;
import com.google.android.material.floatingactionbutton.FloatingActionButton;

import org.json.JSONObject;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import uk.co.dcurrey.owlapp.NewCharacterActivity;
import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.api.APIPaths;
import uk.co.dcurrey.owlapp.api.VolleySingleton;
import uk.co.dcurrey.owlapp.api.requests.POSTCharacterRequest;
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.character.CharacterViewModel;
import uk.co.dcurrey.owlapp.sync.NetworkMonitor;

import static android.app.Activity.RESULT_OK;

public class HomeFragment extends Fragment
{

    private HomeViewModel homeViewModel;
    public static final int NEW_CHAR_ACTIVITY_REQUEST_CODE = 1;
    private CharacterViewModel mCharacterViewModel;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState)
    {
        homeViewModel =
                ViewModelProviders.of(this).get(HomeViewModel.class);
        View root = inflater.inflate(R.layout.fragment_home, container, false);
        final TextView textView = root.findViewById(R.id.text_home);
        homeViewModel.getText().observe(getViewLifecycleOwner(), new Observer<String>()
        {
            @Override
            public void onChanged(@Nullable String s)
            {
                textView.setText(s);
            }
        });

        // Recycler
        RecyclerView recyclerView = root.findViewById(R.id.recyclerview);
        final CharacterListAdapter adapter = new CharacterListAdapter(getContext());
        recyclerView.setAdapter(adapter);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));

        // ViewModel
        mCharacterViewModel = new ViewModelProvider(this).get(CharacterViewModel.class);
        mCharacterViewModel.getAllChars().observe(getViewLifecycleOwner(), new Observer<List<CharacterEntity>>()
        {
            @Override
            public void onChanged(List<CharacterEntity> characterEntities)
            {
                // Update char cache in adapter
                adapter.setChars(characterEntities);
            }
        });

        // FAB
        FloatingActionButton fab = root.findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View view)
            {
                Intent intent = new Intent(getContext(), NewCharacterActivity.class);
                startActivityForResult(intent, NEW_CHAR_ACTIVITY_REQUEST_CODE);
            }
        });


        return root;
    }

    public void onActivityResult(int reqCode, int resCode, Intent data)
    {
        super.onActivityResult(reqCode, reqCode, data);

        if (reqCode == NEW_CHAR_ACTIVITY_REQUEST_CODE && resCode == RESULT_OK)
        {
            CharacterEntity characterEntity = new CharacterEntity();
            characterEntity.Name = data.getStringExtra(NewCharacterActivity.EXTRA_REPLY);
            saveCharacter(characterEntity);
        }
        else
        {
            Toast.makeText(getContext(), R.string.empty_not_saved, Toast.LENGTH_LONG).show();
        }
    }

    public boolean checkNetConnectivity()
    {
        ConnectivityManager connectivityManager = (ConnectivityManager) getContext().getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo networkInfo = connectivityManager.getActiveNetworkInfo();
        return (networkInfo != null && networkInfo.isConnected());
    }

    private void saveCharacter(CharacterEntity characterEntity)
    {
        if (checkNetConnectivity())
        {
            // TODO - Send to API
            Toast.makeText(getContext(), "DEBUG: Char -> API", Toast.LENGTH_LONG).show();
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
        Map<String, String> params = new HashMap();
        params.put("Name", characterEntity.Name);

        // TODO - Refactor requests to separate classes

        JSONObject parameters = new JSONObject(params);
        JsonObjectRequest req = new JsonObjectRequest(Request.Method.POST, APIPaths.getURL(getContext())+"api/characters", parameters, new Response.Listener<JSONObject>()
        {
            @Override
            public void onResponse(JSONObject response)
            {
                // Success
                Toast.makeText(getContext(), "SUCCESS", Toast.LENGTH_SHORT).show();
                characterEntity.IsSynced = true;
                saveLocal(characterEntity);
            }
        },
                new Response.ErrorListener()
                {
                    @Override
                    public void onErrorResponse(VolleyError error)
                    {
                        //Failure
                        Toast.makeText(getContext(), error.getMessage(), Toast.LENGTH_LONG).show();
                        saveLocal(characterEntity);
                    }
                });
        VolleySingleton.getInstance(getContext()).getRequestQueue().add(req);
    }
}
