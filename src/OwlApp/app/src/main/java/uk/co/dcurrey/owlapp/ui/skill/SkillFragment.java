package uk.co.dcurrey.owlapp.ui.skill;

import android.content.Intent;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
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

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.google.android.material.floatingactionbutton.FloatingActionButton;

import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.api.APIPaths;
import uk.co.dcurrey.owlapp.api.VolleySingleton;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;
import uk.co.dcurrey.owlapp.database.skill.SkillViewModel;
import uk.co.dcurrey.owlapp.sync.NetworkMonitor;
import uk.co.dcurrey.owlapp.sync.Synchroniser;

import static android.app.Activity.RESULT_OK;

public class SkillFragment extends Fragment
{
    private SkillUIViewModel skillUIViewModel;
    public static final int NEW_SKILL_ACTIVITY_REQUEST_CODE = 1;
    private SkillViewModel mSkillViewModel;
    private SkillListAdapter adapter;
    private EditText searchTerm;
    private List<SkillEntity> skills;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState)
    {
        skillUIViewModel =
                ViewModelProviders.of(this).get(SkillUIViewModel.class);
        View root = inflater.inflate(R.layout.fragment_skill, container, false);

        searchTerm = root.findViewById(R.id.skillSearch);

        // Recycler
        RecyclerView recyclerView = root.findViewById(R.id.recyclerview_skill);
        adapter = new SkillListAdapter(getContext());
        recyclerView.setAdapter(adapter);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));

        // Viewmodel
        mSkillViewModel = new ViewModelProvider(this).get(SkillViewModel.class);
        mSkillViewModel.getAllSkills().observe(getViewLifecycleOwner(), new Observer<List<SkillEntity>>()
        {
            @Override
            public void onChanged(List<SkillEntity> skillEntities)
            {
                skills = skillEntities;
                adapter.setSkills(skills);
            }
        });

        // FAB
        FloatingActionButton fab = root.findViewById(R.id.fab_skill);
        fab.setVisibility(View.INVISIBLE);

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
        List<SkillEntity> filteredSkills = new ArrayList<>();
        for (SkillEntity s : skills)
        {
            if ((s.Name.toLowerCase().contains(text.toLowerCase()) || (String.valueOf(s.Id).contains(text)) ))
            {
                filteredSkills.add(s);
            }
        }
        adapter.setSkills(filteredSkills);
    }

    public void onActivityResult(int reqCode, int resCode, Intent data)
    {
        super.onActivityResult(reqCode, resCode, data);

        if (reqCode == NEW_SKILL_ACTIVITY_REQUEST_CODE && resCode == RESULT_OK)
        {
            SkillEntity skillEntity = new SkillEntity();
            skillEntity.Name = data.getStringExtra(NewSkillActivity.EXTRA_REPLY);
            saveSkill(skillEntity);
        }
        else
        {
            Toast.makeText(getContext(), "Skill not saved", Toast.LENGTH_LONG).show();
        }
    }

    private void saveSkill(SkillEntity skill)
    {
        if (NetworkMonitor.checkNetConnectivity(getContext()))
        {
            saveAPI(skill);
        }
        else
        {
            saveLocal(skill);
        }
    }

    private void saveLocal(SkillEntity skill)
    {
        mSkillViewModel.insert(skill);
    }

    private void saveAPI(SkillEntity skill)
    {
        Synchroniser sync = new Synchroniser();
        sync.sendToAPI(getContext(), skill);
    }
}
