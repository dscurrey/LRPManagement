package uk.co.dcurrey.owlapp.ui.skill;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import java.util.List;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;
import uk.co.dcurrey.owlapp.database.skill.SkillViewModel;

public class SkillListAdapter extends RecyclerView.Adapter<SkillListAdapter.SkillViewHolder>
{
    class SkillViewHolder extends RecyclerView.ViewHolder
    {
        private final TextView skillItemView;

        private SkillViewHolder(View itemView)
        {
            super(itemView);
            skillItemView = itemView.findViewById(R.id.textView);
        }
    }

    private final LayoutInflater mInflater;
    private List<SkillEntity> mSkills;

    public SkillListAdapter(Context context)
    {
        mInflater = LayoutInflater.from(context);
    }

    @Override
    public SkillListAdapter.SkillViewHolder onCreateViewHolder(ViewGroup parent, int viewType)
    {
        View itemView = mInflater.inflate(R.layout.recycler_item, parent, false);
        return new SkillListAdapter.SkillViewHolder(itemView);
    }

    public void onBindViewHolder(SkillListAdapter.SkillViewHolder holder, int pos)
    {
        if(mSkills != null)
        {
            SkillEntity current = mSkills.get(pos);
            holder.skillItemView.setText(current.Name);
        }
        else
        {
            holder.skillItemView.setText("No Skills Loaded");
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
}