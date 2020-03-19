package uk.co.dcurrey.owlapp;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class NewCharacterActivity extends AppCompatActivity
{
    public static final String EXTRA_REPLY = "uk.co.dcurrey.owlapp.REPLY";
    private EditText mEditCharView;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_character);

        mEditCharView = findViewById(R.id.edit_char);

        final Button btn = findViewById(R.id.button_save);
        btn.setOnClickListener(new View.OnClickListener()
        {
            public void onClick(View view)
            {
                Intent replyIntent = new Intent();
                if (TextUtils.isEmpty(mEditCharView.getText()))
                {
                    setResult(RESULT_CANCELED, replyIntent);;
                }
                else
                {
                    String character = mEditCharView.getText().toString();
                    replyIntent.putExtra(EXTRA_REPLY, character);
                    setResult(RESULT_OK, replyIntent);
                }
                finish();
            }
        });
    }
}
