package uk.co.dcurrey.owlapp.ui.player;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import java.util.List;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;
import uk.co.dcurrey.owlapp.ui.home.CharacterListAdapter;

public class PlayerListAdapter extends RecyclerView.Adapter<PlayerListAdapter.PlayerViewHolder>
{
    class PlayerViewHolder extends RecyclerView.ViewHolder
    {
        private final TextView playerItemView;

        private PlayerViewHolder(View itemView)
        {
            super(itemView);
            playerItemView = itemView.findViewById(R.id.textView);
        }
    }

    private final LayoutInflater mInflater;
    private List<PlayerEntity> mPlayers; // player Cache

    public PlayerListAdapter(Context context)
    {
        mInflater = LayoutInflater.from(context);
    }

    @Override
    public PlayerListAdapter.PlayerViewHolder onCreateViewHolder(ViewGroup parent, int viewType)
    {
        View itemView = mInflater.inflate(R.layout.recycler_item, parent, false);
        return new PlayerListAdapter.PlayerViewHolder(itemView);
    }

    public void onBindViewHolder(PlayerListAdapter.PlayerViewHolder holder, int pos)
    {
        if (mPlayers != null)
        {
            PlayerEntity current = mPlayers.get(pos);
            holder.playerItemView.setText(current.FirstName+" "+current.LastName);
        }
        else
        {
            // Data not ready
            holder.playerItemView.setText("No Players Loaded");
        }
    }

    public void setPlayers(List<PlayerEntity> players)
    {
        mPlayers = players;
        notifyDataSetChanged();
    }

    @Override
    public int getItemCount()
    {
        if (mPlayers != null)
        {
            return mPlayers.size();
        }
        else
            return 0;
    }
}
