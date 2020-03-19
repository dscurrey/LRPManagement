package uk.co.dcurrey.owlapp.ui.home;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import java.util.List;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.CharacterEntity;

public class CharacterListAdapter extends RecyclerView.Adapter<CharacterListAdapter.CharacterViewHolder>
{
    class CharacterViewHolder extends RecyclerView.ViewHolder
    {
        private final TextView charItemView;

        private CharacterViewHolder(View itemView)
        {
            super(itemView);
            charItemView = itemView.findViewById(R.id.textView);
        }
    }

    private final LayoutInflater mInflater;
    private List<CharacterEntity> mChars; // Character Cache

    public CharacterListAdapter(Context context)
    {
        mInflater = LayoutInflater.from(context);
    }

    @Override
    public CharacterViewHolder onCreateViewHolder(ViewGroup parent, int viewType)
    {
        View itemView = mInflater.inflate(R.layout.recycler_item, parent, false);
        return new CharacterViewHolder(itemView);
    }

    public void onBindViewHolder(CharacterViewHolder holder, int pos)
    {
        if (mChars != null)
        {
            CharacterEntity current = mChars.get(pos);
            holder.charItemView.setText(current.Name);
        }
        else
        {
            // Data not ready
            holder.charItemView.setText("No Characters Loaded");
        }
    }

    public void setChars(List<CharacterEntity> characters)
    {
        mChars = characters;
        notifyDataSetChanged();
    }

    @Override
    public int getItemCount()
    {
        if (mChars != null)
        {
            return mChars.size();
        }
        else
            return 0;
    }
}
