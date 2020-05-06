package uk.co.dcurrey.owlapp.ui.player;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Build;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import java.util.Comparator;
import java.util.List;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;

public class PlayerListAdapter extends RecyclerView.Adapter<PlayerListAdapter.PlayerViewHolder>
{
    static class PlayerViewHolder extends RecyclerView.ViewHolder
    {
        private final TextView playerName;
        private final TextView playerId;
        private final ImageView playerSyncView;

        private PlayerViewHolder(View itemView)
        {
            super(itemView);
            playerName = itemView.findViewById(R.id.playerName);
            playerId = itemView.findViewById(R.id.playerId);
            playerSyncView = itemView.findViewById(R.id.playerSync);
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
        return new PlayerViewHolder(itemView);
    }

    public void onBindViewHolder(PlayerListAdapter.PlayerViewHolder holder, int pos)
    {
        if (mPlayers != null)
        {
            PlayerEntity current = mPlayers.get(pos);
            holder.playerName.setText(current.FirstName+" " +current.LastName);
            holder.playerId.setText("ID: "+current.Id);

            boolean sync = current.IsSynced;
            if (sync)
            {
                //SYNC OK
                holder.playerSyncView.setImageResource(R.drawable.ic_tick);
            }
            else
            {
                holder.playerSyncView.setImageResource(R.drawable.ic_sync);
            }
            holder.playerSyncView.setVisibility(View.VISIBLE);

            // OnClick
            holder.itemView.setOnClickListener((v) -> openPlayer(current.Id));
        }
        else
        {
            // Data not ready
            holder.playerName.setText("No Players Loaded");
            holder.playerSyncView.setVisibility(View.INVISIBLE);
        }
    }

    public void setPlayers(List<PlayerEntity> players)
    {
        mPlayers = players;
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N)
        {
            mPlayers.sort(new PlayerComparator());
        }
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

    class PlayerComparator implements Comparator<PlayerEntity>
    {
        @Override
        public int compare(PlayerEntity a, PlayerEntity b)
        {
            return a.LastName.compareToIgnoreCase(b.LastName);
        }
    }
}
