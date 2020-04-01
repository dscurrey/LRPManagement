package uk.co.dcurrey.owlapp.ui.home;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import java.util.List;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;

public class CharacterListAdapter extends RecyclerView.Adapter<CharacterListAdapter.CharacterViewHolder>
{
    class CharacterViewHolder extends RecyclerView.ViewHolder
    {
        private final TextView charNameView;
        private final TextView charPlayerView;
        private final TextView charRetireView;
        private final ImageView charSyncView;

        private CharacterViewHolder(View itemView)
        {
            super(itemView);
            charNameView = itemView.findViewById(R.id.charName);
            charPlayerView = itemView.findViewById(R.id.charPlayer);
            charRetireView = itemView.findViewById(R.id.charRetired);
            charSyncView = itemView.findViewById(R.id.charSync);
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
        View itemView = mInflater.inflate(R.layout.recycler_item_character, parent, false);
        return new CharacterViewHolder(itemView);
    }

    public void onBindViewHolder(CharacterViewHolder holder, int pos)
    {
        if (mChars != null)
        {
            CharacterEntity current = mChars.get(pos);
            holder.charNameView.setText(current.Name);
            holder.charRetireView.setText(""+current.IsRetired);
            holder.charPlayerView.setText(""+current.PlayerId);

            boolean sync = current.IsSynced;
            if (sync)
            {
                //SYNC OK
                holder.charSyncView.setImageResource(R.drawable.ok);

            }
            else
            {
                holder.charSyncView.setImageResource(R.drawable.fail);
            }
            holder.charSyncView.setVisibility(View.VISIBLE);
        }
        else
        {
            // Data not ready
            holder.charNameView.setText("No Characters Loaded");
            holder.charSyncView.setVisibility(View.INVISIBLE);
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
