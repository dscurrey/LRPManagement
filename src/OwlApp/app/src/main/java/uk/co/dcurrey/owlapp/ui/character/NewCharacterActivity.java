package uk.co.dcurrey.owlapp.ui.character;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;

import androidx.appcompat.app.AppCompatActivity;

import uk.co.dcurrey.owlapp.R;

public class NewCharacterActivity extends AppCompatActivity
{
    public static final String EXTRA_REPLY_CHARNAME = "uk.co.dcurrey.owlapp.REPLY.CHARNAME";
    public static final String EXTRA_REPLY_CHARPLAYER = "uk.co.dcurrey.owlapp.REPLY.CHARPLAYERID";
    public static final String EXTRA_REPLY_CHARERETIRED = "uk.co.dcurrey.owlapp.REPLY.CHARISRETIRED";
    private EditText mCharName;
    private EditText mCharPlayerId;
    private CheckBox mCharRetired;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_character);

        mCharName = findViewById(R.id.newCharName);
        mCharPlayerId = findViewById(R.id.newCharPlayId);
        mCharRetired = findViewById(R.id.newCharRetired);

        final Button btn = findViewById(R.id.char_save);
        btn.setOnClickListener(view -> {
            Intent replyIntent = new Intent();
            if (TextUtils.isEmpty(mCharName.getText()) || TextUtils.isEmpty(mCharPlayerId.getText()))
            {
                setResult(RESULT_CANCELED, replyIntent);
            }
            else
            {
                String charName = mCharName.getText().toString();
                String charPlayerId = mCharPlayerId.getText().toString();
                boolean isRetired = mCharRetired.isChecked();

                replyIntent.putExtra(EXTRA_REPLY_CHARNAME, charName);
                replyIntent.putExtra(EXTRA_REPLY_CHARPLAYER, charPlayerId);
                replyIntent.putExtra(EXTRA_REPLY_CHARERETIRED, isRetired);
                setResult(RESULT_OK, replyIntent);
            }
            finish();
        });
    }
}
