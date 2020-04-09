package uk.co.dcurrey.owlapp.ui.player;

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
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;

public class PlayerListAdapter extends RecyclerView.Adapter<PlayerListAdapter.PlayerViewHolder>
{
    class PlayerViewHolder extends RecyclerView.ViewHolder
    {
        private final TextView playerFName;
        private final TextView playerLName;

        private PlayerViewHolder(View itemView)
        {
            super(itemView);
            playerFName = itemView.findViewById(R.id.playFName);
            playerLName = itemView.findViewById(R.id.playLName);
        }
    }

    private final LayoutInflater mInflater;
    private List<PlayerEntity> mPlayers; // player Cache
    private final Context mContext;
    SharedPreferences prefs;
    SharedPreferences.Editor prefEditor;

    public PlayerListAdapter(Context context)
    {
        mInflater = LayoutInflater.from(context);
        mContext = context;
        prefs = mContext.getSharedPreferences(context.getString(R.string.pref_playerId_key), Context.MODE_PRIVATE);
        prefEditor = prefs.edit();
    }

    @Override
    public PlayerListAdapter.PlayerViewHolder onCreateViewHolder(ViewGroup parent, int viewType)
    {
        View itemView = mInflater.inflate(R.layout.recycler_item_player, parent, false);
        return new PlayerListAdapter.PlayerViewHolder(itemView);
    }

    public void onBindViewHolder(PlayerListAdapter.PlayerViewHolder holder, int pos)
    {
        if (mPlayers != null)
        {
            PlayerEntity current = mPlayers.get(pos);
            holder.playerFName.setText(current.FirstName);
            holder.playerLName.setText(current.LastName);

            // OnClick
            holder.itemView.setOnClickListener((v) -> {
                openPlayer(current.Id);
            });
        }
        else
        {
            // Data not ready
            holder.playerFName.setText("No Players Loaded");
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

    private void openPlayer(int playerId)
    {
        Intent intent = new Intent(mContext, PlayerDetailsActivity.class);
        prefEditor.putInt(mContext.getString(R.string.pref_playerId_key), playerId);
        prefEditor.apply();
        mContext.startActivity(intent);
    }
}
