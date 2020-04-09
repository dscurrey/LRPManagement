package uk.co.dcurrey.owlapp.ui.skill;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import java.util.List;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;

public class SkillListAdapter extends RecyclerView.Adapter<SkillListAdapter.SkillViewHolder>
{
    class SkillViewHolder extends RecyclerView.ViewHolder
    {
        private final TextView skillNameView;

        private SkillViewHolder(View itemView)
        {
            super(itemView);
            skillNameView = itemView.findViewById(R.id.skillName);
        }
    }

    private final LayoutInflater mInflater;
    private List<SkillEntity> mSkills;
    private final Context mContext;
    SharedPreferences prefs;
    SharedPreferences.Editor prefEditor;

    public SkillListAdapter(Context context)
    {
        mInflater = LayoutInflater.from(context);
        mContext = context;
        prefs = mContext.getSharedPreferences(context.getString(R.string.pref_skillId_key), Context.MODE_PRIVATE);
        prefEditor = prefs.edit();
    }

    @Override
    public SkillListAdapter.SkillViewHolder onCreateViewHolder(ViewGroup parent, int viewType)
    {
        View itemView = mInflater.inflate(R.layout.recycler_item_skill, parent, false);
        return new SkillListAdapter.SkillViewHolder(itemView);
    }

    public void onBindViewHolder(SkillListAdapter.SkillViewHolder holder, int pos)
    {
        if(mSkills != null)
        {
            SkillEntity current = mSkills.get(pos);
            holder.skillNameView.setText(current.Name);

            // OnClick
            holder.itemView.setOnClickListener((v) -> {
                openSkill(current.Id);
            });
        }
        else
        {
            holder.skillNameView.setText("No Skills Loaded");
        }
    }

    public void setSkills(List<SkillEntity> skills)
    {
        mSkills = skills;
        notifyDataSetChanged();
    }

    @Override
    public int getItemCount()
    {
        if (mSkills != null)
        {
            return mSkills.size();
        }
        else
        {
            return 0;
        }
    }

    private void openSkill(int skillId)
    {
        Intent intent = new Intent(mContext, SkillDetailsActivity.class);
        prefEditor.putInt(mContext.getString(R.string.pref_skillId_key), skillId);
        prefEditor.apply();
        mContext.startActivity(intent);
    }
}
