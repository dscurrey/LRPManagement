package uk.co.dcurrey.owlapp.ui.items;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import uk.co.dcurrey.owlapp.R;

public class NewItemActivity extends AppCompatActivity
{
    public static final String EXTRA_REPLY_ITEMNAME = "uk.co.dcurrey.owlapp.REPLY.ITEMNAME";
    public static final String EXTRA_REPLY_ITEMFORM = "uk.co.dcurrey.owlapp.REPLY.ITEMFORM";
    public static final String EXTRA_REPLY_ITEMREQ = "uk.co.dcurrey.owlapp.REPLY.ITEMREQ";
    public static final String EXTRA_REPLY_ITEMEFFECTS = "uk.co.dcurrey.owlapp.REPLY.ITEMEFFECTS";
    public static final String EXTRA_REPLY_ITEMMATS = "uk.co.dcurrey.owlapp.REPLY.ITEMMATS";
    private EditText mItemName;
    private EditText mItemForm;
    private EditText mItemReq;
    private EditText mItemEffects;
    private EditText mItemMats;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_item);

        mItemEffects = findViewById(R.id.newItemEffect);
        mItemForm = findViewById(R.id.newItemForm);
        mItemMats = findViewById(R.id.newItemMats);
        mItemName = findViewById(R.id.newItemName);
        mItemReq = findViewById(R.id.newItemReq);
        final Button btn = findViewById(R.id.item_save);

        btn.setOnClickListener(v -> {
            // Save
            Intent replyIntent = new Intent();
            if (TextUtils.isEmpty(mItemName.getText()) || TextUtils.isEmpty(mItemForm.getText()))
            {
                setResult(RESULT_CANCELED, replyIntent);
            }
            else
            {
                String itemName = mItemName.getText().toString();
                String itemForm = mItemForm.getText().toString();
                String itemReq = mItemReq.getText().toString();
                String itemEffects = mItemEffects.getText().toString();
                String itemMats = mItemMats.getText().toString();

                replyIntent.putExtra(EXTRA_REPLY_ITEMNAME, itemName);
                replyIntent.putExtra(EXTRA_REPLY_ITEMFORM, itemForm);
                replyIntent.putExtra(EXTRA_REPLY_ITEMREQ, itemReq);
                replyIntent.putExtra(EXTRA_REPLY_ITEMEFFECTS, itemEffects);
                replyIntent.putExtra(EXTRA_REPLY_ITEMMATS, itemMats);

                setResult(RESULT_OK, replyIntent);
            }
            finish();
        });
    }
}
