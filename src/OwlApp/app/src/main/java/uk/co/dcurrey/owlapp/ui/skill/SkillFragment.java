package uk.co.dcurrey.owlapp.ui.skill;

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

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.google.android.material.floatingactionbutton.FloatingActionButton;

import org.json.JSONObject;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.api.APIPaths;
import uk.co.dcurrey.owlapp.api.VolleySingleton;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;
import uk.co.dcurrey.owlapp.database.skill.SkillViewModel;
import uk.co.dcurrey.owlapp.sync.NetworkMonitor;

import static android.app.Activity.RESULT_OK;

public class SkillFragment extends Fragment
{
    private SkillUIViewModel skillUIViewModel;
    public static final int NEW_SKILL_ACTIVITY_REQUEST_CODE = 1;
    private SkillViewModel mSkillViewModel;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState)
    {
        skillUIViewModel =
                ViewModelProviders.of(this).get(SkillUIViewModel.class);
        View root = inflater.inflate(R.layout.fragment_skill, container, false);
        final TextView textView = root.findViewById(R.id.text_skill);
        skillUIViewModel.getText().observe(getViewLifecycleOwner(), new Observer<String>()
        {
            @Override
            public void onChanged(@Nullable String s)
            {
                textView.setText(s);
            }
        });

        // Recycler
        RecyclerView recyclerView = root.findViewById(R.id.recyclerview_skill);
        final SkillListAdapter adapter = new SkillListAdapter(getContext());
        recyclerView.setAdapter(adapter);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));

        // Viewmodel
        mSkillViewModel = new ViewModelProvider(this).get(SkillViewModel.class);
        mSkillViewModel.getAllSkills().observe(getViewLifecycleOwner(), new Observer<List<SkillEntity>>()
        {
            @Override
            public void onChanged(List<SkillEntity> skillEntities)
            {
                adapter.setSkills(skillEntities);
            }
        });

        // FAB
        FloatingActionButton fab = root.findViewById(R.id.fab_skill);
        fab.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Intent intent = new Intent(getContext(), NewSkillActivity.class);
                startActivityForResult(intent, NEW_SKILL_ACTIVITY_REQUEST_CODE);
            }
        });

        return root;
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
        Map<String, String> params = new HashMap();
        params.put("Name", skill.Name);

        // TODO - Refactor API Requests

        JSONObject parameters = new JSONObject(params);
        JsonObjectRequest req = new JsonObjectRequest(Request.Method.POST, APIPaths.getURL(getContext()) + "api/skills", parameters, new Response.Listener<JSONObject>()
        {
            @Override
            public void onResponse(JSONObject response)
            {
                //Success
                skill.IsSynced = true;
                saveLocal(skill);
            }
        },
                new Response.ErrorListener()
                {
                    @Override
                    public void onErrorResponse(VolleyError error)
                    {
                        // Fail
                        saveLocal(skill);
                    }
                });
        VolleySingleton.getInstance(getContext()).getRequestQueue().add(req);
    }
}
