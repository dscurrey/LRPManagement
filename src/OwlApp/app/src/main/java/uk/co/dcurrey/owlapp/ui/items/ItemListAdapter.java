package uk.co.dcurrey.owlapp.ui.items;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import androidx.recyclerview.widget.RecyclerView;
import java.util.List;
import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.item.ItemEntity;


public class ItemListAdapter extends RecyclerView.Adapter<ItemListAdapter.ItemViewHolder>
{
    static class ItemViewHolder extends RecyclerView.ViewHolder
    {
        private final TextView itemName;
        private final ImageView itemSyncView;
        private final TextView itemForm;

        private ItemViewHolder(View itemView)
        {
            super(itemView);
            itemName = itemView.findViewById(R.id.itemName);
            itemForm = itemView.findViewById(R.id.itemForm);
            itemSyncView = itemView.findViewById(R.id.itemSync);
        }
    }

    private final LayoutInflater mInflater;
    private List<ItemEntity> mItems;
    private final Context mContext;
    SharedPreferences prefs;
    SharedPreferences.Editor prefEditor;

    public ItemListAdapter(Context context)
    {
        mInflater = LayoutInflater.from(context);
        mContext = context;
        prefs = mContext.getSharedPreferences(context.getString(R.string.pref_itemId_key), Context.MODE_PRIVATE);
        prefEditor = prefs.edit();
    }

    @Override
    public ItemViewHolder onCreateViewHolder(ViewGroup parent, int viewType)
    {
        View itemView = mInflater.inflate(R.layout.recycler_item_item, parent, false);
        return new ItemViewHolder(itemView);
    }

    @Override
    public void onBindViewHolder(ItemViewHolder holder, int pos)
    {
        if (mItems != null)
        {
            ItemEntity cur = mItems.get(pos);
            holder.itemName.setText(cur.Name);
            holder.itemForm.setText(cur.Form);

            boolean sync = cur.IsSynced;
            if (sync)
            {
                holder.itemSyncView.setImageResource(R.drawable.ic_tick);
            }
            else
            {
                holder.itemSyncView.setImageResource(R.drawable.ic_sync);
            }
            holder.itemSyncView.setVisibility(View.VISIBLE);

            holder.itemView.setOnClickListener((v) -> openItem(cur.Id));
        }
        else
        {
            holder.itemName.setText("Items not Loaded");
            holder.itemSyncView.setVisibility(View.INVISIBLE);
        }
    }

    private void openItem(int itemId)
    {
        Intent intent = new Intent(mContext, ItemDetailsActivity.class);
        prefEditor.putInt(mContext.getString(R.string.pref_itemId_key), itemId);
        prefEditor.apply();
        mContext.startActivity(intent);
    }

    public void setItems(List<ItemEntity> items)
    {
        mItems = items;
        notifyDataSetChanged();
    }

    @Override
    public int getItemCount()
    {
        if (mItems != null)
        {
            return mItems.size();
        }
        else
            return 0;
    }
}
