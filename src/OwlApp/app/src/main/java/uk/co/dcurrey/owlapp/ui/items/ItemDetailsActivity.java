package uk.co.dcurrey.owlapp.ui.items;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.item.ItemEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;

public class ItemDetailsActivity extends AppCompatActivity
{
    TextView itemIdView;
    EditText itemName;
    EditText itemForm;
    EditText itemReqs;
    EditText itemEffect;
    EditText itemMats;
    Button btnSave;
    ItemEntity item;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_item_details);

        popViews();

        int itemId = getSharedPreferences(getApplicationContext().getString(R.string.pref_itemId_key), Context.MODE_PRIVATE).getInt(getApplicationContext().getString(R.string.pref_itemId_key), 0);

        item = Repository.getInstance().getItemRepository().get(itemId);

        itemIdView.setText(""+item.Id);
        itemName.setText(item.Name);
        itemForm.setText(item.Form);
        itemReqs.setText(item.Reqs);
        itemEffect.setText(item.Effect);
        itemMats.setText(item.Materials);
    }

    private void popViews()
    {
        itemIdView = findViewById(R.id.detailsItemId);
        itemName = findViewById(R.id.detailsItemName);
        itemForm = findViewById(R.id.detailsItemForm);
        itemReqs = findViewById(R.id.detailsItemRequirements);
        itemEffect = findViewById(R.id.detailsItemEffect);
        itemMats = findViewById(R.id.detailsItemMaterials);
        btnSave = findViewById(R.id.btnEditItem);
    }
}
